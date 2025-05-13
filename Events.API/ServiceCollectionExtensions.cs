using Events.Application;
using Events.Application.Interfaces;
using Events.Application.Models;
using Events.Application.Services;
using Events.Application.Services.AccountService;
using Events.Application.Validators;
using Events.DataAccess;
using Events.DataAccess.Repositories;
using Events.Domain.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text.Json.Serialization;

namespace Events.API
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDb(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<EventsDbContext>(options => options.UseNpgsql(connectionString));
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IEventsRepository, EventsRepository>();
            services.AddTransient<IParticipantsRepository, ParticipantsRepository>();
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapperProfile));
        }

        public static void AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<EventRequest>, EventRequestValidator>();
            services.AddTransient<IValidator<ParticipantLoginRequest>, ParticipantLoginRequestValidator>();
            services.AddTransient<IValidator<ParticipantRegisterRequest>, ParticipantRegisterRequestValidator>();
        }

        public static void AddAuth(this IServiceCollection services)
        {
            services.AddAuthorization();
            services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };
                });
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IEventService, EventService>();
            services.AddTransient<IParticipantService, ParticipantService>();
            services.AddTransient<IAccountService,AccountService>();
        }

        public static void AddJsonConverting(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        }

        public static void CreateLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }
    }
}

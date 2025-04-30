using Events.API;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDb(builder.Configuration.GetConnectionString("DefaultConnection")!);
builder.Services.AddRepositories();
builder.Services.AddAutoMapper();
builder.Services.AddAuth();
builder.Services.AddValidators();
builder.Services.AddServices();
builder.Services.AddJsonConverting();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

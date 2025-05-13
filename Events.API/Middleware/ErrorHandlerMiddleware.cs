using Events.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Events.API.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(ClientException ex)
            {
                Log.Error("Client error: {ex}", ex);

                await HandleExceptionAsync(context,ex.StatusCode,ex.Message);

            }
            catch (Exception ex)
            {
                Log.Fatal("Server error: {ex}",ex);

                await HandleExceptionAsync(context, 500, ex.Message);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, int statusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var response = new
            {
                statusCode,
                message,
            };

            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}

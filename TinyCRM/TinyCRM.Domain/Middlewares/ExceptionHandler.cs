﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Serilog;
using TinyCRM.Domain.HttpExceptions;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

namespace TinyCRM.Domain.Middlewares
{
    public static class HttpExceptionHandlerMiddlewareExtension
    {
        public static IApplicationBuilder UseHttpExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseExceptionHandler(exceptionHandlerApp =>
            {
                exceptionHandlerApp.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                    if (exceptionHandlerPathFeature?.Error is HttpException ex)
                    {
                        Log.Error(ex.Message);

                        context.Response.StatusCode = (int)ex.StatusCode;
                        await context.Response.WriteAsync(ex.Response);
                    }
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                        var message = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development" ? exceptionHandlerPathFeature?.Error?.Message : "Something went wrong!";

                        var response = $@"{{
                            ""statusCode"": {StatusCodes.Status500InternalServerError},
                            ""code"": ""{"INTERNAL"}"",
                            ""message"": ""{message}""
                        }}";

                        Log.Error(message);

                        await context.Response.WriteAsync(response);
                    }
                });
            });
        }
    }
}
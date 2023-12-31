﻿using AspNetCoreRateLimit;
using SalesSystem.Core.Exceptions;
using SalesSystem.Api.Middlewares;

namespace SalesSystem.Api.Infrastructure.Extensions
{
    public static class AppBuilderExtensions
    {
        public static void ConfigureMiddlewarePipeline(this WebApplication app)
        {

            app.UseCustomExceptionHandler();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseIpRateLimiting();

            app.UseAuthentication();
            app.UseAuthorization();



            app.MapGet("/not-found-example", () =>
            {
                throw new NotFoundException("Information is not found in DB");
            });

            app.MapGet("/bad-request-example", () =>
            {
                throw new BadRequestException("Requester URL is invalid");
            });

            app.MapGet("/forbidden-request-example", () =>
            {
                throw new ForbiddenException("Requester URL is invalid");
            });


            app.MapControllers();

        }
    }
}

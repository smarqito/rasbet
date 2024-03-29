﻿using BetGamesAggregator.Services;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Extensions.Http;
using System.Net;
using System.Reflection.PortableExecutable;

namespace BetGamesAggregator
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddTransient<LoggingDelegatingHandler>();

            services.AddHttpClient<IBetService, BetService>(c =>
                c.BaseAddress = new Uri(Configuration["ApiSettings:BetUrl"]));
            //.AddHttpMessageHandler<LoggingDelegatingHandler>()
            //.AddPolicyHandler(GetRetryPolicy())
            //.AddPolicyHandler(GetCircuitBreakerPolicy());

            services.AddHttpClient<IGameOddService, GameOddService>(c =>
                c.BaseAddress = new Uri(Configuration["ApiSettings:GameOddUrl"]));
            //.AddHttpMessageHandler<LoggingDelegatingHandler>()
            //.AddPolicyHandler(GetRetryPolicy())
            //.AddPolicyHandler(GetCircuitBreakerPolicy());


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "BetGamesAggregator",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JTW authorization w/ Bearer scheme\n\r
                                    Enter 'Bearer' [space] and then the token\n\n
                                    \r\r Example: 'Bearer 12345abdefg'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                        },
                        Array.Empty<string>()
                    }
                });
            });
            //
            //services.AddHealthChecks()
            //    .AddUrlGroup(new Uri($"{Configuration["ApiSettings:CatalogUrl"]}/swagger/index.html"), "Catalog.API", HealthStatus.Degraded)
            //    .AddUrlGroup(new Uri($"{Configuration["ApiSettings:BasketUrl"]}/swagger/index.html"), "Basket.API", HealthStatus.Degraded)
            //    .AddUrlGroup(new Uri($"{Configuration["ApiSettings:OrderingUrl"]}/swagger/index.html"), "Ordering.API", HealthStatus.Degraded);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                //{
                //    Predicate = _ => true,
                //    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                //});
            });
        }

        //private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        //{
        //    // In this case will wait for
        //    //  2 ^ 1 = 2 seconds then
        //    //  2 ^ 2 = 4 seconds then
        //    //  2 ^ 3 = 8 seconds then
        //    //  2 ^ 4 = 16 seconds then
        //    //  2 ^ 5 = 32 seconds
        //
        //    return HttpPolicyExtensions
        //        .HandleTransientHttpError()
        //        .WaitAndRetryAsync(
        //            retryCount: 5,
        //            sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
        //            onRetry: (exception, retryCount, context) =>
        //            {
        //                Log.Error($"Retry {retryCount} of {context.PolicyKey} at {context.OperationKey}, due to: {exception}.");
        //            });
        //}

        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: 5,
                    durationOfBreak: TimeSpan.FromSeconds(30)
                );
        }
    }
}

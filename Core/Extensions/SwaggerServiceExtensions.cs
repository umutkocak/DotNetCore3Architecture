using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using Swashbuckle.Swagger;

namespace Core.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("CoreSwagger", new OpenApiInfo
                {
                    Title = "SeyirNet By SeyirMobil-FK",
                    Version = "2.0.0",
                    Description = "SeyirNet Service 2019 by SeyirMobil-FK",
                    Contact = new OpenApiContact()
                    {
                        Name = "Swagger Implementation Faruk Kaya",
                        Url = new Uri("http://ngnet.seyirmobil.com"),
                        Email = "faruk.kaya@seyirmobil.com"
                    },
                    TermsOfService = new Uri("http://swagger.io/terms/")
                });

                var securityScheme = new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Lütfen alana 'Bearer' kelimesini ve ardından bir boşluk ve JWT değerini girin.\r\n Örnek: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    Scheme = "bearer"

                };
                options.AddSecurityDefinition("Bearer", securityScheme);
                var securityRequirement = new OpenApiSecurityRequirement {{securityScheme, new string[] { }}};
                options.AddSecurityRequirement(securityRequirement);
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/CoreSwagger/swagger.json", "SeyirNet By SeyirMobil-FK");
                c.DocumentTitle = "SeyirNet By SeyirMobil-FK";
                c.DocExpansion(DocExpansion.None);
            });

            return app;
        }
    }
}

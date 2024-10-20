using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            OpenApiInfo openApi = new()
            {
                Title = "LOGIN API",
                Version = "v1",
                Description = "API para generar un login, haciendo uso de una Base de Datos en Memoria.",
                TermsOfService = new Uri("https://opensource.org/licenses/NIT"),
                Contact = new OpenApiContact
                {
                    Name = "Hamilton Renteria",
                    Email = "hamiltonrenmordev@gmail.com",
                    Url = new Uri("https://hamiltondevtech.com/contacto/")
                },
                License = new OpenApiLicense
                {
                    Name = "License",
                    Url = new Uri("https://opensource.org/licenses/")
                }
            };

            _ = services.AddSwaggerGen(x =>
            {
                openApi.Version = "v1";
                x.SwaggerDoc("v1", openApi);

                OpenApiSecurityScheme securityScheme = new()
                {
                    Name = "JWT Authentication",
                    Description = "JWT Bearer Token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                x.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securityScheme, new string[]{ } }
                });
            });

            return services;
        }
    }
}

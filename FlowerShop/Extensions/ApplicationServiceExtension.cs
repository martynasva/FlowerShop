using FlowerShop.Interfaces;
using FlowerShop.Repositories;
using FlowerShop.Services;
using Microsoft.OpenApi.Models;

namespace FlowerShop.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration config)
        {
            services.AddCors();
            services.AddScoped<ITokenService, TokenService>();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "dotnetClaimAuthorization", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference= new OpenApiReference
                                {
                                    Type=ReferenceType.SecurityScheme,
                                    Id="Bearer"
                                }
                            },
                            new string[]{}
                        }
                    });
            });


            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IItemsRepository, ItemsRepository>();
            services.AddScoped<IMerchandiseRepository, MerchandiseRepository>();
            services.AddScoped<IMerchandiseCategoryRepository, MerchandiseCategoryRepository>();
            services.AddScoped<IOrdersRepository, OrdersRepository>();

            return services;
        }
    }
}
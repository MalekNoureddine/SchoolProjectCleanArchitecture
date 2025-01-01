using CleanArchProject.Data.Entities.Identies;
using CleanArchProject.Data.Entities.Identities;
using CleanArchProject.Data.Healper;
using CleanArchProject.Infrastracture.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Infrastracture
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            //Connection SQL
            string dbConnectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            if (string.IsNullOrEmpty(dbConnectionString))
            {
                throw new InvalidOperationException("Database connection string is not configured.");
            }

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(dbConnectionString);
            });

            services.AddIdentity<User, Role>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

                // SighnIn settings.

                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedAccount = false;

            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();



            //JWT Authentication

            var jwtSettings = new JwtSettings
            {
                Secret = Environment.GetEnvironmentVariable("JWT_SECRET"),
                Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
                Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
                ValidateAudience = bool.Parse(Environment.GetEnvironmentVariable("JWT_VALIDATE_AUDIENCE") ?? "true"),
                ValidateIssuer = bool.Parse(Environment.GetEnvironmentVariable("JWT_VALIDATE_ISSUER") ?? "true"),
                ValidateLifeTime = bool.Parse(Environment.GetEnvironmentVariable("JWT_VALIDATE_LIFETIME") ?? "true"),
                ValidateIssuerSigningKey = bool.Parse(Environment.GetEnvironmentVariable("JWT_VALIDATE_ISSUER_SIGNING_KEY") ?? "true"),
                AccessTokenExpireDate = int.Parse(Environment.GetEnvironmentVariable("JWT_ACCESS_TOKEN_EXPIRE_DATE") ?? "1"),
                RefreshTokenExpireDate = int.Parse(Environment.GetEnvironmentVariable("JWT_REFRESH_TOKEN_EXPIRE_DATE") ?? "20")
            };
            services.AddSingleton(jwtSettings);

            var emailSettings = new EmailSettings
            {
                Port = int.Parse(Environment.GetEnvironmentVariable("EMAIL_PORT")),
                Host = Environment.GetEnvironmentVariable("EMAIL_HOST"),
                FromEmail = Environment.GetEnvironmentVariable("EMAIL_FROMEMAIL"),
                Password = Environment.GetEnvironmentVariable("EMAIL_PASSWORD")
            };
            services.AddSingleton(emailSettings);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(x =>
           {
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = jwtSettings.ValidateIssuer,
                   ValidIssuers = new[] { jwtSettings.Issuer },
                   ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                   ValidAudience = jwtSettings.Audience,
                   ValidateAudience = jwtSettings.ValidateAudience,
                   ValidateLifetime = jwtSettings.ValidateLifeTime,
               };
           });

            //Swagger Gn
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "School Project", Version = "v1" });
                c.EnableAnnotations();

                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
            Array.Empty<string>()
                    }
                });
            });
            services.AddAuthorization(option =>
            {
                option.AddPolicy("AddStudent", policy => policy.RequireClaim("CreateStudent", "True"));
                option.AddPolicy("DeleteStudent", policy => policy.RequireClaim("DeleteStudent", "True"));
                option.AddPolicy("RetriveStudentLists", policy => policy.RequireClaim("RetriveStudentLists", "True"));
                option.AddPolicy("EditStudent", policy => policy.RequireClaim("EditStudent", "True"));
                option.AddPolicy("SendingEmails", policy => policy.RequireClaim("SendingEmails", "True"));
            });


            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddTransient<IUrlHelper>(x =>
            {
                var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = x.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext);
            });
            return services;

        }
    }
}

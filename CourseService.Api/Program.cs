


using CourseService.Application;
using CourseService.Application.Common;
using CourseService.Infrastructure;
using DotNetEnv;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CourseService.Api.Middleware;

namespace CourseService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly));
            Env.Load();

            var appSettings = new AppSettings
            {
                DbConnectionString = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION"),
                JwtSecretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY"),
                CloudinaryCloudName = Environment.GetEnvironmentVariable("CLOUDINARY_CLOUD_NAME"),
                CloudinaryApiSecrut = Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET"),
                CloudinaryApiKey = Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY"),
                Host = Environment.GetEnvironmentVariable("RABBITMQ_HOST"),
                UserName = Environment.GetEnvironmentVariable("RABBITMQ_USER"),
                Password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD"),
            };

            builder.Services.AddSingleton(appSettings);
            builder.Services.AddInfrastructureServices(appSettings);

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "nLearn_API", });

                // Add JWT Authentication in Swagger
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your token"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            // Add services to the container.
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = Environment.GetEnvironmentVariable("JWT_ISSUER"); 
                    options.Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"); 
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {

                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JwtSecretKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                    };
                });


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<UserIdMiddleware>();

            app.MapControllers();
            app.Run();
        }
    }
}

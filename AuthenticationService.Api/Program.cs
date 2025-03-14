
using AuthenticationService.Application.Common;
using AuthenticationService.Infrastructure;
using DotNetEnv;

namespace AuthenticationService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            Env.Load();
            var appSettings = new Appsettings
            {
                DbConnectionString = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION"),
                JwtSecretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY"),

            };
            builder.Services.AddSingleton(appSettings);

            builder.Services.AddInfrastructureServices(appSettings);


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
           
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

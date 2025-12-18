
using HytaleList_Backend_API.Data;
using HytaleList_Backend_API.Services;
using Microsoft.EntityFrameworkCore;

namespace HytaleList_Backend_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // Server related services
            builder.Services.AddScoped<ServerRepository>();
            builder.Services.AddScoped<ServerService>();

            // User related services
            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<IPasswordService, PasswordService>();

            builder.Services.AddDbContext<MyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // CORS policy to allow requests from the frontend application in development stage
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            // Use the CORS policy
            app.UseCors("AllowFrontend");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

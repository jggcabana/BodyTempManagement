using BodyTemp.Repositories;
using BodyTemp.Repositories.Interfaces;
using BodyTemp.Repositories.Repositories;
using BodyTemp.Services.Interfaces;
using BodyTemp.Services.Services;
using Qless.WebAPI.Middleware;
using Serilog;

namespace BodyTemp.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add serilog
            builder.Host.UseSerilog((context, loggerConfig) => loggerConfig
                .ReadFrom.Configuration(context.Configuration));

            // Add services to the container.
            builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();
            builder.Services.AddPersistence();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

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

            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
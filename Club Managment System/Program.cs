
using CMS.DataAccess.Repositories;
using Core.Interfaces;
using Serilog;

namespace Club_Managment_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Serilog
              Log.Logger = new LoggerConfiguration()
                .WriteTo.File("Logs/api_logs.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            builder.Host.UseSerilog();
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // Connection string 
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            //Repo Providers
            builder.Services.AddScoped<ICategoryRepository>(sp => new CategoryRepository(connectionString, sp.GetRequiredService<ILogger<CategoryRepository>>()));
            builder.Services.AddScoped<IPlayerRepository>(provider =>
            {
                // ???? ??? ??? Logger ?????? ???? ??? Repository
                var logger = provider.GetRequiredService<ILogger<PlayerRepository>>();

                // ???? ???? ????? ????? ??? ??? ??????? ???????
                return new PlayerRepository(connectionString!, logger);
            });

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


using CMS.Core.Interfaces;
using CMS.DataAccess.Repositories;
using Core.Interfaces;
using Serilog;
/*
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
            builder.Services.AddScoped<ICategoryRepository>(sp => new CategoryRepository(connectionString!, sp.GetRequiredService<ILogger<CategoryRepository>>()));
            builder.Services.AddScoped<IPlayerRepository>(provider =>
            {
                
                var logger = provider.GetRequiredService<ILogger<PlayerRepository>>();

                
                return new PlayerRepository(connectionString!, logger);
            });
            builder.Services.AddScoped<IEventsRepository>(provider =>
            {
                var logger = provider.GetRequiredService<ILogger<EventsRepository>>();

                
                return new EventsRepository(connectionString!, logger);
            });

            builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
            builder.Services.AddScoped<ICoachRepository, CoachRepository>();
            builder.Services.AddScoped<IEventsRepository, EventsRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IExpencesRepository, ExpensesRepository>();
            builder.Services.AddScoped<IOtherIncomesRepository, OtherIncomesRepository>();
            builder.Services.AddScoped<IRoleRepository, RolesRepository>();
            builder.Services.AddScoped<ISessionsRepository, SessionsRepository>();
            builder.Services.AddScoped<IUsersRepository, UserRepository>();

            // Note: Matching your specific file naming from the directory list
            builder.Services.AddScoped<ISubscriptionsRepository, SubscriptionsRepository>();
            builder.Services.AddScoped<IPlayerAttendanceRepository, PlayerAttendanceRepository>();
            builder.Services.AddScoped<IExtraInfoRepository, ExtraInfoRepository>();

            // The Generic fallback
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


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

*/

namespace Club_Managment_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Serilog 
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("Logs/api_logs.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            builder.Host.UseSerilog();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // 2. Connection string (Added a check to ensure not null)
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            // 3. Repo Providers - Using Factory Pattern for all to pass ConnectionString
            // This ensures the DI container knows exactly how to build your repositories.

            builder.Services.AddScoped<ICategoryRepository>(sp =>
                new CategoryRepository(connectionString, sp.GetRequiredService<ILogger<CategoryRepository>>()));

            builder.Services.AddScoped<IPlayerRepository>(sp =>
                new PlayerRepository(connectionString, sp.GetRequiredService<ILogger<PlayerRepository>>()));

            builder.Services.AddScoped<ICoachRepository>(sp =>
                new CoachRepository(connectionString, sp.GetRequiredService<ILogger<CoachRepository>>()));

            builder.Services.AddScoped<IEventsRepository>(sp =>
                new EventsRepository(connectionString, sp.GetRequiredService<ILogger<EventsRepository>>()));

            builder.Services.AddScoped<IExpencesRepository>(sp =>
                new ExpensesRepository(connectionString, sp.GetRequiredService<ILogger<ExpensesRepository>>()));

            builder.Services.AddScoped<IOtherIncomesRepository>(sp =>
                new OtherIncomesRepository(connectionString, sp.GetRequiredService<ILogger<OtherIncomesRepository>>()));

            builder.Services.AddScoped<IRoleRepository>(sp =>
                new RolesRepository(connectionString, sp.GetRequiredService<ILogger<RolesRepository>>()));

            builder.Services.AddScoped<ISessionsRepository>(sp =>
                new SessionsRepository(connectionString, sp.GetRequiredService<ILogger<SessionsRepository>>()));

            builder.Services.AddScoped<IUsersRepository>(sp =>
                new UserRepository(connectionString, sp.GetRequiredService<ILogger<UserRepository>>()));

            builder.Services.AddScoped<ISubscriptionsRepository>(sp =>
                new SubscriptionsRepository(connectionString, sp.GetRequiredService<ILogger<SubscriptionsRepository>>()));

            builder.Services.AddScoped<IPlayerAttendanceRepository>(sp =>
                new PlayerAttendanceRepository(connectionString, sp.GetRequiredService<ILogger<PlayerAttendanceRepository>>()));

            builder.Services.AddScoped<IExtraInfoRepository>(sp =>
                new ExtraInfoRepository(connectionString, sp.GetRequiredService<ILogger<ExtraInfoRepository>>()));

            // 4. Generic Fallback
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            var app = builder.Build();

            // Pipeline Configuration
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
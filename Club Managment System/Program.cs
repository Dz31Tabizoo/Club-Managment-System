
using CMS.Core.Interfaces;
using CMS.DataAccess.Repositories;
using Core.Interfaces;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Club_Managment_System.Services;

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

            //Member Repo
            builder.Services.AddScoped<IMemberRepository>(sp =>
            new MemberRepository(connectionString, sp.GetRequiredService<ILogger<MemberRepository>>())
            );

            builder.Services.AddScoped<IMemberServices, MemberServices>();
             


            // 4. Generic Fallback
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));



            //Jwt service
            // 1. Add Authentication Services
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
                };
            });

            // 2. Register your TokenService
            builder.Services.AddScoped<TokenService>();
            var app = builder.Build();

            // Pipeline Configuration
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //test hashed passsword check 
            // Temporairement, juste pour générer ton hash de test
            //var hash = BCrypt.Net.BCrypt.HashPassword("admin123");
            //Console.WriteLine($"MON_HASH_TEST: {hash}");
            //System.Diagnostics.Debug.WriteLine($"MON_HASH_TEST: {hash}");

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
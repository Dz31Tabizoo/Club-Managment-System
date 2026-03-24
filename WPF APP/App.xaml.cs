using Club_Management_System.WPF.ViewModels;
using Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Net.Http;
using System.ServiceProcess;
using System.Windows;
using ClubManagementSystem.Core;
using ClubManagementSystem.Services;
using ClubManagementSystem.ViewModels;
using ClubManagementSystem.Views;
using Serilog;

namespace ClubManagementSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public new static App Current => (App)Application.Current;

        public App()
        {
            var services = new ServiceCollection();

            // 1. On enregistre d'abord la classe AuthService comme le SEUL Singleton
            // On ne laisse pas AddHttpClient décider du cycle de vie.
            services.AddSingleton<AuthService>();

            // 2. On lie l'interface à cette instance précise
            services.AddSingleton<IAuthenticationClientService>(s => s.GetRequiredService<AuthService>());

            // 3. On configure le HttpClient pour la classe AuthService
            // Cette syntaxe permet de garder le Singleton tout en profitant de l'injection du HttpClient
            services.AddHttpClient<AuthService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7135/");
            });

            services.AddTransient<AuthenticationHandler>();

            // ViewModels
            services.AddTransient<LoginViewModel>();
            services.AddSingleton<MainViewModel>();

            // Fenêtres
            services.AddTransient<LoginWindow>();
            services.AddTransient<MainWindow>();

            ServiceProvider = services.BuildServiceProvider();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            var LoginWindow = ServiceProvider.GetRequiredService<LoginWindow>();            
            LoginWindow.Show();
            base.OnStartup(e);
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day,retainedFileCountLimit:10)
                .CreateLogger();
            Log.Information("Application started");
        }

    }

}

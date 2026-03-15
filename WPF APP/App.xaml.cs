using Club_Management_System.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Net.Http;
using System.ServiceProcess;
using System.Windows;
using WPF_APP.Services;
using WPF_APP.ViewModels;
using WPF_APP.Views;

namespace WPF_APP
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            var services = new ServiceCollection();
            // 1. Enregistrer le HttpClient et le Service d'Auth
            services.AddHttpClient<IAuthenticationClientService, AuthService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7135/");
            });
            //2.Enregistrer les ViewModels
            services.AddTransient<LoginViewModel>();
            services.AddTransient<MainViewModel>();

            // 3. Enregistrer les Fenêtres
            services.AddTransient<LoginWindow>();
            services.AddTransient<MainWindow>();

            ServiceProvider = services.BuildServiceProvider();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            var LoginWindow = ServiceProvider.GetRequiredService<LoginWindow>();            
            LoginWindow.Show();
            base.OnStartup(e);
        }

    }

}

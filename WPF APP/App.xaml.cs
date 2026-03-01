using System.Configuration;
using System.Data;
using System.Windows;
using WPF_APP.Views;

namespace WPF_APP
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainView = new LoginWindow();
            MainWindow.Show();
        }

    }

}

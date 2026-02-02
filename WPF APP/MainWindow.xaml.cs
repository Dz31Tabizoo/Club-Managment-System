using CMS.DTOs;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http.Json;


namespace WPF_APP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void LoadPlayers(List<PlayerDTO> players)
        {
            dgPlayers.ItemsSource = players;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadPlayersAsync();
        }

        private async Task LoadPlayersAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // (laptop :: Port)
                    string url = "https://localhost:7135/api/Players/playersWithDetails";

                    var players = await client.GetFromJsonAsync<List<PlayerDTO>>(url);

                    if (players != null)
                    {
                        dgPlayers.ItemsSource = players;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"خطأ في جلب البيانات: {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        
    }
}
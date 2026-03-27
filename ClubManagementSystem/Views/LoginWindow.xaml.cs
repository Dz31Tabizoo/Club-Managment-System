using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ClubManagementSystem.Helpers;
using ClubManagementSystem.ViewModels;

namespace ClubManagementSystem.Views
{
    /// <summary>
    /// Logique d'interaction pour LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow(LoginViewModel viewModel)
        {
            InitializeComponent();

            this.DataContext = viewModel;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowHelper.EnableDrag(this, e);
        }       
        

        
        private void txtPassWord_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Si l'utilisateur a tapé quelque chose, on cache le texte "Mot de passe"
            if (txtPassWord.Password.Length > 0)
            {
                textPasswordPlaceholder.Visibility = Visibility.Collapsed;
            }
            else
            {
                textPasswordPlaceholder.Visibility = Visibility.Visible;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.CloseApp();
        }
    }
}

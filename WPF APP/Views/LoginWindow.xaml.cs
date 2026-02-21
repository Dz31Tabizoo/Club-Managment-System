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
using WPF_APP.Helpers;

namespace WPF_APP.Views
{
    /// <summary>
    /// Logique d'interaction pour LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowHelper.EnableDrag(this, e);
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.CloseApp();
        }

        private void txtUserName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtUserName.Text == "Nom d'utilisateur")
            {
                txtUserName.Text = "";
                txtUserName.Opacity = 1; 
            }
        }

        private void txtUserName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                txtUserName.Text = "Nom d'utilisateur";
                txtUserName.Opacity = 0.5; 
            }
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
    }
}

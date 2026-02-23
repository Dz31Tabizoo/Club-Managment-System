using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WPF_APP.ViewModels
{
    public  partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private bool _rememberMe;

        [ObservableProperty]
        private string _errorMessage;

        [RelayCommand]
        private async Task Login(Object parameter)
        {
            var passwordBox = parameter as System.Windows.Controls.PasswordBox;
            string password = passwordBox.Password;

            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(password))
            {
                ErrorMessage = "Entrez le nom d'utilisateur et le mot de pass S.V.P";
                return;
            }

            // login dev time
            if (Username == "admin" && password == "admin")
            {
                if(RememberMe)
                {
                    // Save credentials logic here (e.g., using secure storage)

                }
                ErrorMessage = string.Empty; // Clear error message on successful login

                // Navigate to the next view or perform other actions on successful login

                NavigateToMainShell();
            }
            else
            {
                ErrorMessage = "Nom d'utilisateur ou mot de passe incorrect.";
            }
        }



        private void NavigateToMainShell()
        {
            //var mainShell = new MainWindow();
            //mainShell.Show();

            Application.Current.MainWindow.Close(); 
        }
    }
}

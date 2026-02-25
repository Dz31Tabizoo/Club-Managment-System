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
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        private string? _username;      

        [ObservableProperty]
        private string? _errorMessage;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        private bool _isLoggingIn;


        [RelayCommand(CanExecute = nameof(CanLogin))]
        private async Task LoginAsync(Object? parameter)
        {
            var passwordBox = parameter as System.Windows.Controls.PasswordBox;
            if (passwordBox == null) return;


            string password = passwordBox.Password;

            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(password))
            {
                ErrorMessage = "Entrez le nom d'utilisateur et le mot de pass S.V.P";
                return;
            }

            IsLoggingIn = true;
            ErrorMessage = string.Empty; // Clear previous error message

            try
            {
                


                // login dev time
                if ()
                {

                    
                }
                else
                {
                    ErrorMessage = "Nom d'utilisateur ou mot de passe incorrect.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred during login: {ex.Message}";
            }
            finally
            {
                IsLoggingIn = false;
            }
        }

        private bool CanLogin(Object? parameter) => !string.IsNullOrEmpty(Username) && !IsLoggingIn;
        

        private void NavigateToMainShell()
        {
            //var mainShell = new MainWindow();
            //mainShell.Show();

            Application.Current.Windows[0]?.Close(); 
        }
    }
}

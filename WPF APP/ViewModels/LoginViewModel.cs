using Club_Management_System.WPF.ViewModels;
using ClubManagementSystem.Core;
using ClubManagementSystem.Services;
using ClubManagementSystem.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Serilog;


namespace ClubManagementSystem.ViewModels
{
    public  partial class LoginViewModel : BaseViewModel
    {
        private readonly IAuthenticationClientService _authService;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        private string? _username;      

        [ObservableProperty]
        private string? _errorMessage;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        private bool _isLoggingIn;


        public LoginViewModel(IAuthenticationClientService authService)
        {
            _authService = authService;
        }


        [RelayCommand(CanExecute = nameof(CanLogin))]
        private async Task LoginAsync(Object? parameter)
        {
            var passwordBox = parameter as System.Windows.Controls.PasswordBox;
            if (passwordBox == null) return;


            string password = passwordBox.Password;

            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(password))
            {
                ErrorMessage = "Veuillez entrez vos identifiqnts.";
                Log.Warning("Tentative de connecxion avec un champ utilisateur vide.");
                return;
            }

            IsLoggingIn = true;
            ErrorMessage = string.Empty; // Clear previous error message

            try
            {

                var response = await _authService.LoginAsync(Username.Trim(), password);


                // login dev time
                if (response != null && response.Success)
                {
                    UserSession.UserId = response.Id;
                    UserSession.DisplayName = response.DisplayName;
                    UserSession.Token = response.Token;
                    UserSession.Role = response.Role;


                    _authService.Login(new Models.UserModel
                    {
                        UserID = response.Id,
                        RoleID = response.Role,
                        LastLogin = DateTime.Now,
                        Token = response.Token,
                        UserName = response.DisplayName

                    });


                    NavigateToMainShell(passwordBox);
                }
                else
                {
                    ErrorMessage = response.Message ?? "Identification incorrect. Veuillez réessayer.";
                    Log.Information("Échec de la connexion pour l'utilisateur {Username}: {Message}", Username, response.Message);
                }

                
               
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Une Erreur technique est survenue: {ex.Message}";
                Log.Error(ex, "Erreur technique lors de la tentative de connexion pour l'utilisateur {Username}", Username);
            }
            finally
            {
                IsLoggingIn = false;
            }
        }

        private bool CanLogin(Object? parameter) => !IsLoggingIn;
        

        private void NavigateToMainShell(PasswordBox passwordBox)
        {
            var mainWindow = App.Current.ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            var currentWindow = Window.GetWindow(passwordBox);
            currentWindow?.Close();
        }
    }
}

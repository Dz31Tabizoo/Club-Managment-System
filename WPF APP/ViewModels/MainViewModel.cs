using ClubManagementSystem.Core;
using System;
using ClubManagementSystem.ViewModels;
using System.Net;
using Core.Interfaces;
using CMS.DTOs;
using ClubManagementSystem.Services;
using ClubManagementSystem.Models;
using ClubManagementSystem;
using Microsoft.Extensions.DependencyInjection;
using ClubManagementSystem.Views;
using System.Windows;
using CommunityToolkit.Mvvm.Input;

namespace Club_Management_System.WPF.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        private readonly IAuthenticationClientService _authenticationService;
        public UserModel? CurrentUser => _authenticationService.CurrentUser;

        public bool IsLoggedIn => _authenticationService.IsLoggedIn;

        public MainViewModel(IAuthenticationClientService authenticationService)
        {
            _authenticationService = authenticationService;
            //sub
            _authenticationService.OnAuthenticationStateChanged += UpdateUI;

            UpdateUI();
        }

        public void UpdateUI()
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                OnPropertyChanged(nameof(CurrentUser));
                OnPropertyChanged(nameof(IsLoggedIn));
                CurrentUserRole = _authenticationService.CurrentUser?.UserRole?.RoleName;
            });
        }
        [RelayCommand]
        public void Logout()
        {
            var result = MessageBox.Show("Voulez-vous vraiment changer l'utilisateur ou Déconnecter ?", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (result == MessageBoxResult.OK)
            {
                _authenticationService?.Logout();

                var loginWindow = App.Current.ServiceProvider.GetRequiredService<LoginWindow>();
                loginWindow.Show();
                Application.Current.MainWindow = loginWindow;

                foreach (Window w in Application.Current.Windows)
                {
                    if (w != loginWindow)
                    {
                        w.Close();
                    }
                }
            }
        }
    }    
}

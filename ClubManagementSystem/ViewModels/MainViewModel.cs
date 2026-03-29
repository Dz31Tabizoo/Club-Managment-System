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
using CommunityToolkit.Mvvm.ComponentModel;

namespace Club_Management_System.WPF.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        private readonly IAuthenticationClientService _authenticationService;
        private readonly IMemberService _memberService;
        public UserModel? CurrentUser => _authenticationService.CurrentUser;

        public bool IsLoggedIn => _authenticationService.IsLoggedIn;

        [ObservableProperty]
        private string? _caption;

        [ObservableProperty]
        private object? _currentChildView;

        [RelayCommand]
        public void ExecuteShowView(string viewName)
        {
            switch (viewName)
            {
                case "Inscriptions":
                    Caption = "Gestion des inscriptions";
                    // CurrentChildView = new DashboardViewModel();
                    break;

                case "Effectif": // Doit correspondre exactement au CommandParameter du XAML
                    Caption = "Gestion de l'Effectif";
                     CurrentChildView = new MembersViewModel(_memberService);
                    break;

                case "Entrainements":
                    Caption = "Séances d'Entraînement";
                    break;

                case "Evenements":
                    Caption = "Gestion Des évenements";
                    break;

                case "Transactions":
                    Caption = "Flux Financiers";
                    break;

                case "Stock":
                    Caption = "Stock";
                    break;

                case "Finannces":
                    Caption = "Finannces";
                    break;

                case "Paramettres":
                    Caption = "Configuration";
                    break;

                default:
                    Caption = "Club Management Ecosystem";
                    break;
            }
        }


        public MainViewModel(IAuthenticationClientService authenticationService)
        {
            _authenticationService = authenticationService;
            //sub
            _authenticationService.OnAuthenticationStateChanged += UpdateUI;

            UpdateUI();

            ExecuteShowView("Tableau de bord");
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

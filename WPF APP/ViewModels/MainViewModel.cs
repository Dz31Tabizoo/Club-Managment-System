using ClubManagementSystem.Core;
using System;
using ClubManagementSystem.ViewModels;
using System.Net;
using Core.Interfaces;
using CMS.DTOs;
using ClubManagementSystem.Services;
using ClubManagementSystem.Models;
using ClubManagementSystem;

namespace Club_Management_System.WPF.ViewModels
{
    public class MainViewModel : BaseViewModel
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
    }    
}

using WPF_APP.Core;
using System;
using WPF_APP.ViewModels;
using System.Net;
using Core.Interfaces;
using CMS.DTOs;

namespace Club_Management_System.WPF.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IAuthenticationService _authenticationService;
        public UserDTO? CurrentUser => _authenticationService.CurrentUser;

        public bool IsLoggedIn => _authenticationService.IsLoggedIn;

        public MainViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            //sub
            _authenticationService.OnAuthenticationStateChanged += () =>
            {
                OnPropertyChanged(nameof(CurrentUser));
                OnPropertyChanged(nameof(IsLoggedIn));
                CurrentUserRole = _authenticationService.CurrentUser?.RoleName;
            };
        }
    }    
}

using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;


namespace ClubManagementSystem.Models
{
    public partial class UserModel : ObservableObject
    {
        [ObservableProperty]   private int _UserID;

        [ObservableProperty] private string _userName = string.Empty;

        [ObservableProperty] private DateTime? _lastLogin;

        [ObservableProperty] private bool _isActive;

        [ObservableProperty] private RoleModel? _userRole;
    
        [ObservableProperty] private string? _password;

        [ObservableProperty] private int _roleID;
        //pour UI
        [ObservableProperty] private bool? _rememberMe;
    }
}

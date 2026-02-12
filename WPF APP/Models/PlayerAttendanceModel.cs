using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace WPF_APP.Models
{
    public partial class PlayerAttendanceModel : ObservableObject
    {
        [ObservableProperty]
        private int _AttendanceID;
        [ObservableProperty]
        private int _sessionID;
        [ObservableProperty]
        private int _playerID;
        [ObservableProperty]
        private bool? _isPresent;
        [ObservableProperty]
        private string? _note;
        
        [ObservableProperty]
        private string _playerName;
    }
}

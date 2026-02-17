using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace WPF_APP.Models
{
    public partial class SessionModel : ObservableObject
    {
        [ObservableProperty]
        private int _sessionID;

        [ObservableProperty]
        private int _trainingDayID;

        [ObservableProperty]
        private int _categoryID;

        [ObservableProperty]
        private int _CoachID;


        [ObservableProperty]
        private CategoryModel _category;

        [ObservableProperty]
        private CoachModel _sessionCoach;

        [ObservableProperty]
        private ObservableCollection<PlayerAttendanceModel> _playerAttendance = new();

    }
}

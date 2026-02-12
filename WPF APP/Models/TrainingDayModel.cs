using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace WPF_APP.Models
{
    public partial class TrainingDayModel : ObservableObject
    {
        [ObservableProperty]
        private int _trainingDayID;
        [ObservableProperty]
        private DateTime? _trainingDate;
        [ObservableProperty]
        private string? _notes;
        [ObservableProperty]
        private bool _isClosed;

        [ObservableProperty]
        private ObservableCollection<SessionModel> _session = new();
    }
}

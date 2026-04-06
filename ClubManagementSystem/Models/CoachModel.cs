using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClubManagementSystem.Models
{
    public partial class CoachModel : PersonModel
    {
        public CoachModel() : base() { }

        [ObservableProperty]
        private int _coachID;

        [ObservableProperty]
        private string? _specialization;

        [ObservableProperty]
        private decimal? _salary;

        [ObservableProperty]
        private bool _isActive;

        
    }
}

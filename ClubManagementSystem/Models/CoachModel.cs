using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClubManagementSystem.Models
{
    public partial class CoachModel : PersonModel
    {
        [ObservableProperty]
        private int _coachID;

        [ObservableProperty]
        private string? _specialization;

        [ObservableProperty]
        private decimal? _salary;

        [ObservableProperty]
        private bool? isActive;

        [ObservableProperty]
        private ExtraInfoModel? _extraInfo;
    }
}

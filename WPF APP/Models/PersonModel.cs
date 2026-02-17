using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Xaml.Behaviors.Media;
using System;
using System.Collections.Generic;
using System.Net.Cache;
using System.Text;

namespace WPF_APP.Models
{
    public partial class PersonModel : ObservableObject
    {
        private int _personID;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FullName))]
        private string _firstName;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FullName))]
        private string _lastName;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Age))]
        private DateTime _dateOfBirth;
        [ObservableProperty]
        private string _email;
        [ObservableProperty]
        private string _phone;
        [ObservableProperty]
        private string _address;
        [ObservableProperty]
        private string _gender;

        public string FullName => $"{_firstName} {_lastName}";

        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - _dateOfBirth.Year;
                if (_dateOfBirth.Date > today.AddYears(-age)) age--;
                return age;
            }
        }
    }
}

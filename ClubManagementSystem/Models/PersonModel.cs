using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Xaml.Behaviors.Media;
using System;
using System.Collections.Generic;
using System.Net.Cache;
using System.Text;
using System.Text.Json.Serialization;

namespace ClubManagementSystem.Models
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(PlayerModel), "player")]
    [JsonDerivedType(typeof(CoachModel), "coach")]
    public partial class PersonModel : ObservableObject
    {
        [ObservableProperty]
        private int _personID; // Now public 'PersonID' is generated and visible to JSON

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FullName))]
        private string _firstName = string.Empty;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FullName))]
        private string _lastName = string.Empty;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Age))]
        private DateTime _dateOfBirth;

        [ObservableProperty]
        private string? _email;

        [ObservableProperty]
        private string? _phone;

        [ObservableProperty]
        private string? _address;

        [ObservableProperty]
        private string? _gender;

        [ObservableProperty]
        private byte[]? _photo;

        public string FullName => string.IsNullOrWhiteSpace(FirstName) && string.IsNullOrWhiteSpace(LastName)
            ? "Nom non défini"
            : $"{FirstName} {LastName}";
        public int Age
        {
            get
            {
                if (DateOfBirth == default) return 0;
                var today = DateTime.Today;
                var age = today.Year - DateOfBirth.Year;
                if (DateOfBirth.Date > today.AddYears(-age)) age--;
                return age;
            }
        }

        public DateTime? LastUpdate { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}

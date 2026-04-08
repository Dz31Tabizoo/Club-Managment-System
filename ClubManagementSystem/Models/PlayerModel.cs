using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ClubManagementSystem.Models
{
    


    public partial class PlayerModel : PersonModel
    {
        public PlayerModel() : base() { }

        [ObservableProperty]
        private int _playerID;

        [ObservableProperty]
        private int _categoryID;

        // This matches the "categoryName" in your JSON
        [ObservableProperty]
        private string? _categoryName;

        [ObservableProperty]
        private bool _isActive;

        [ObservableProperty]
        private bool _hasDebts;

        // Computed property for display

        public ObservableCollection<SubscriptionModel>? subscriptions { get; set; }
    }
}

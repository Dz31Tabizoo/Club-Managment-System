using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace WPF_APP.Models
{
    public partial class PlayerModel : PersonModel
    {
        [ObservableProperty]
        private int _playerID;

        [ObservableProperty]
        private int _categoryID;

        [ObservableProperty]
        private bool _isActive;

        [ObservableProperty]
        private CategoryModel? _playerCategory;

        [ObservableProperty]
        private ExtraInfoModel? _extraDetails;
        //pour UI logic
        [ObservableProperty]
        private bool _hasDebts;

        public string categoryNameDisplay => _playerCategory?.CategoryName ?? "No Category";

        public ObservableCollection<SubscriptionModel>? subscriptions { get; set; } 
    }
}

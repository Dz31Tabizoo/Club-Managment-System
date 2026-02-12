using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace WPF_APP.Models
{
    public partial class CategoryModel : ObservableObject
    {
        [ObservableProperty]        
        private int _categoryid;

        [ObservableProperty]       
        private string _categoryName;

        [ObservableProperty]
        private int? _minAge;

        [ObservableProperty]
        private int? _maxAge;

        [ObservableProperty]
        private decimal? _MonthlyFee;

        public string DisplayInfo => (_minAge.HasValue && _maxAge.HasValue)
            ? $"{_categoryName} ({_minAge}-{_maxAge} yrs)"
            : _categoryName;

    }
}

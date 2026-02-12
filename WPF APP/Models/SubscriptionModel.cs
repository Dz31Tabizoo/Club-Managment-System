using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace WPF_APP.Models
{
    public partial class SubscriptionModel : ObservableObject
    {
        [ObservableProperty]
        private int _subscriptionID;

        [ObservableProperty]
        private int _playerID;

        [ObservableProperty]
        private byte _month;

        [ObservableProperty]
        private int _year;

        [ObservableProperty]
        private decimal _amount;

        [ObservableProperty]
        private DateTime? _paymentDate;


        [ObservableProperty]
        private bool? isPaid;

        public string MonthName => System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(_month);

    }
}

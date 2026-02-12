using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace WPF_APP.Models
{
    public partial class ExtraInfoModel : ObservableObject
    {
        [ObservableProperty]
        private int _personID;

        [ObservableProperty]
        private DateTime? _lastUpdate;

        [ObservableProperty]
        private byte[]? _photo;
    }
}

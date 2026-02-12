using CommunityToolkit.Mvvm.ComponentModel;

namespace WPF_APP.Models
{
    public partial class RoleModel : ObservableObject
    {
        [ObservableProperty]
        private int _roleID;
        [ObservableProperty]
        private string _roleName;
    }
}

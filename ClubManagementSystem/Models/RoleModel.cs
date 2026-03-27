using CommunityToolkit.Mvvm.ComponentModel;

namespace ClubManagementSystem.Models
{
    public partial class RoleModel : ObservableObject
    {
        [ObservableProperty]
        private int _roleID;
        [ObservableProperty]
        private string _roleName;
    }
}

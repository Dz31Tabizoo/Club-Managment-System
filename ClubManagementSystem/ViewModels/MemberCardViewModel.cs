using ClubManagementSystem.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ClubManagementSystem.ViewModels
{
    // Indispensable pour la génération de code
    public partial class MemberCardViewModel : BaseViewModel
    {
        [ObservableProperty]
        // Crucial : On notifie TOUTES les propriétés dépendantes quand le membre change
        [NotifyPropertyChangedFor(nameof(IsPlayer), nameof(IsStaff), nameof(Position), nameof(SubscriptionStatus))]
        [NotifyPropertyChangedFor(nameof(FirstName), nameof(LastName), nameof(Phone), nameof(Email), nameof(Address), nameof(Age), nameof(Gender), nameof(Photo), nameof(PersonID))]
        private PersonModel? _member;

        [ObservableProperty]
        private bool _isReadOnly = true;

        #region Propriétés mappées (Accessoires)
        // On ajoute les propriétés manquantes utilisées dans ton XAML
        public string PersonID => Member?.PersonID.ToString() ?? "0";
        public byte[] Photo => Member?.Photo ?? Array.Empty<byte>();
        public string Gender => Member?.Gender ?? "N/A";
        public string Age => Member?.DateOfBirth.ToShortDateString() ?? "N/A";

        public string FirstName
        {
            get => Member?.FirstName ?? "";
            set { if (Member != null) { Member.FirstName = value; OnPropertyChanged(); } }
        }

        public string LastName
        {
            get => Member?.LastName ?? "";
            set { if (Member != null) { Member.LastName = value; OnPropertyChanged(); } }
        }

        public string Phone
        {
            get => Member?.Phone ?? "";
            set { if (Member != null) { Member.Phone = value; OnPropertyChanged(); } }
        }

        public string Email
        {
            get => Member?.Email ?? "";
            set { if (Member != null) { Member.Email = value; OnPropertyChanged(); } }
        }

        public string Address
        {
            get => Member?.Address ?? "";
            set { if (Member != null) { Member.Address = value; OnPropertyChanged(); } }
        }
        #endregion

        #region Helpers UI (Couleurs & Logique)
        public bool IsPlayer => Member is PlayerModel;
        public bool IsStaff => Member is CoachModel;

        public string Position => Member switch
        {
            PlayerModel p => $"Joueur - {p.CategoryName}",
            CoachModel c => $"Coach - {c.Specialization}",
            _ => "Membre"
        };

        public string SubscriptionStatus => (Member is PlayerModel p) ? (p.HasDebts ? "IMPAYÉ" : "À JOUR") : "N/A";
        public string SubscriptionColor => (Member is PlayerModel p) ? (p.HasDebts ? "#D32F2F" : "#2E7D32") : "#757575";

        // Simulé pour le design, à lier à tes données réelles plus tard
        public int AttendanceRate => 85;
        public string AttendanceColor => "#4CAF50";
        #endregion

        [RelayCommand]
        private void Edit() => IsReadOnly = false;

        [RelayCommand]
        private void Cancel() => IsReadOnly = true;

        [RelayCommand]
        private void Save()
        {
            // Ton code Dapper ici
            IsReadOnly = true;
        }
    }
}

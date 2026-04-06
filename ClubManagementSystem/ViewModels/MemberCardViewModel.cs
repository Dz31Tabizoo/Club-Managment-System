using ClubManagementSystem.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace ClubManagementSystem.ViewModels
{
    // Indispensable pour la génération de code
    public partial class MemberCardViewModel : BaseViewModel
    {
        public event Action? RequestClose;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsPlayer), nameof(IsStaff), nameof(Position), nameof(SubscriptionStatus), nameof(SubscriptionColor))]
        [NotifyPropertyChangedFor(nameof(FirstName), nameof(LastName), nameof(Phone), nameof(Email), nameof(Address), nameof(Age), nameof(Gender), nameof(Photo), nameof(PersonID))]
        private PersonModel? _member;

        [ObservableProperty]
        private bool _isReadOnly = true;

        #region Propriétés mappées
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

        #region Helpers UI
        public bool IsPlayer => Member is PlayerModel;
        public bool IsStaff => Member is CoachModel;

        public string Specialization
        {
            get => (Member is CoachModel c) ? (c.Specialization ?? "N/A") : "N/A";
            set { if (Member is CoachModel c) { c.Specialization = value; OnPropertyChanged(); } }
        }

        public string Position => Member switch
        {
            PlayerModel p => $"Joueur - {p.CategoryName}",
            CoachModel c => "Coach",
            _ => "Membre"
        };

        public string SubscriptionStatus => (Member is PlayerModel p) ? (p.HasDebts ? "IMPAYÉ" : "À JOUR") : "N/A";
        public string SubscriptionColor => (Member is PlayerModel p) ? (p.HasDebts ? "#D32F2F" : "#2E7D32") : "#757575";

        public int AttendanceRate => 85;
        public string AttendanceColor => "#4CAF50";
        #endregion

        #region Commandes

        [RelayCommand]
        private void Edit() => IsReadOnly = false;

        [RelayCommand]
        private void Cancel()
        {
            IsReadOnly = true;
            // On ferme le dialogue lors de l'annulation
            RequestClose?.Invoke();
        }

        [RelayCommand]
        private void Close()
        {
            // Simple fermeture (Mode Lecture)
            RequestClose?.Invoke();
        }

        [RelayCommand]
        private void Save()
        {
            // Ton code Dapper ici
            // Ex: _repository.Update(Member);
            IsReadOnly = true;
            RequestClose?.Invoke(); // On ferme après enregistrement
        }
        #endregion
    }
}

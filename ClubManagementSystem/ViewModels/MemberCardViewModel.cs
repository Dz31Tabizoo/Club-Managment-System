using ClubManagementSystem.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;


namespace ClubManagementSystem.ViewModels
{
    // Indispensable pour la génération de code
    public partial class MemberCardViewModel : BaseViewModel
    {
        public event Action? RequestClose;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsPlayer), nameof(IsStaff), nameof(Position), nameof(SubscriptionStatus),nameof(CategoryDisplay), nameof(SubscriptionColor))]
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

        [ObservableProperty]
        private ObservableCollection<CategoryModel> _allcategories = new (){
        new CategoryModel { CategoryID = 1, CategoryName = "U9", MinAge = 7, MaxAge = 9, MonthlyFee = 2000 },
        new CategoryModel { CategoryID = 2, CategoryName = "U11", MinAge = 9, MaxAge = 11, MonthlyFee = 2200 },
        new CategoryModel { CategoryID = 3, CategoryName = "U13", MinAge = 11, MaxAge = 13, MonthlyFee = 2500 },
        new CategoryModel { CategoryID = 4, CategoryName = "U15", MinAge = 13, MaxAge = 15, MonthlyFee = 2800 }
    };
        [ObservableProperty]
        private CategoryModel? _selectedCategory;

        partial void OnMemberChanged(PersonModel? value)
        {
            if(value is PersonModel p)
            {
                SelectedCategory = Allcategories.FirstOrDefault(c => c.CategoryID == (p as PlayerModel)?.CategoryID);
            }
        }
        partial void OnSelectedCategoryChanged(CategoryModel? value)
        {
            if (Member is PlayerModel p && value != null)
            {
                p.CategoryID = value.CategoryID;
                p.CategoryName = value.CategoryName;
                
                OnPropertyChanged(nameof(Position));
                OnPropertyChanged(nameof(CategoryDisplay));
            }
        }


        // Modifie ou ajoute ceci dans ton ViewModel
        public DateTime DateOfBirth
        {
            get => Member?.DateOfBirth ?? DateTime.Now;
            set
            {
                if (Member != null && Member.DateOfBirth != value)
                {
                    Member.DateOfBirth = value;
                    OnPropertyChanged();
                    // Crucial : On prévient l'UI que la chaîne "Age" a aussi changé
                    OnPropertyChanged(nameof(Agetaken));
                }
            }
        }

        // Ta propriété Age reste pour l'affichage en mode lecture
        public string Agetaken => Member?.DateOfBirth.ToShortDateString() ?? "N/A";
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

        public string CategoryDisplay => Member switch
        {
            PlayerModel p => p.CategoryName ?? "Sans catégorie",
            _ => "N/A"
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

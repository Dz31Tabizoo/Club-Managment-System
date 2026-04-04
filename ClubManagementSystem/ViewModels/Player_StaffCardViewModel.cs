using ClubManagementSystem.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClubManagementSystem.ViewModels
{
    public class Player_StaffCardViewModel : BaseViewModel
    {

        // Le modèle de données actuel (peut être un Player ou un Coach)
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsPlayer))]
        [NotifyPropertyChangedFor(nameof(Position))]
        [NotifyPropertyChangedFor(nameof(SubscriptionStatus))]
        private PersonModel? _member;

        // État de l'interface : True = Lecture seule, False = Mode édition
        [ObservableProperty]
        private bool _isReadOnly = true;

        public Player_StaffCardViewModel()
        {
        }

        // Logic Helpers pour le XAML
        public bool IsPlayer => Member is PlayerModel;

        public string Position => Member switch
        {
            PlayerModel p => $"Joueur - {p.CategoryName}",
            CoachModel c => $"Coach - {c.Specialization}",
            _ => "Membre"
        };

        public string SubscriptionStatus
        {
            get
            {
                if (Member is PlayerModel player)
                    return player.HasDebts ? "Non Payé" : "À jour";
                return "N/A";
            }
        }

        // Couleurs dynamiques pour le Sidebar
        public string SubscriptionColor => SubscriptionStatus == "À jour" ? "#2E7D32" : "#D32F2F";

        // Exemple simple de calcul de présence (à lier à tes données réelles plus tard)
        public int AttendanceRate => 85;
        public string AttendanceColor => AttendanceRate > 75 ? "#4CAF50" : "#FFC107";

        #region Commandes

        [RelayCommand]
        private void Edit()
        {
            IsReadOnly = false;
        }

        [RelayCommand]
        private void Save()
        {
            // Ici, tu appelles ton Repository Dapper pour sauvegarder
            // Ex: _memberRepository.Update(Member);
            IsReadOnly = true;
        }

        [RelayCommand]
        private void Cancel()
        {
            // Optionnel : Recharger les données depuis la DB pour annuler les modifs
            IsReadOnly = true;
        }

        [RelayCommand]
        private void Close()
        {
            // Logique pour fermer le UserControl ou le Dialog
        }

        [RelayCommand]
        private void ChangePhoto()
        {
            // Logique d'ouverture de fichier (OpenFileDialog)
        }

        #endregion
    }
}

    


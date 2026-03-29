using ClubManagementSystem.Models;
using ClubManagementSystem.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ClubManagementSystem.ViewModels
{
    public partial class MembersViewModel : BaseViewModel
    {
        //choisir entre les joueurs et les coachs
        [ObservableProperty]
        private bool _isShowingPlayers = true;
        

        public readonly IMemberService _memberService;

        private List<PersonModel> _allMembers = new();

        [ObservableProperty]
        private ObservableCollection<PersonModel> _filterdmembers = new();

        [ObservableProperty]
        private string _searchText = string.Empty;

        [ObservableProperty]
        private PersonModel _selectedMember;

        [ObservableProperty]
        private ObservableCollection<CategoryModel> _categories = new();

        [ObservableProperty]
        private CategoryModel? _selectedCategory;


        public MembersViewModel(IMemberService memberService)
        {
            _memberService = memberService;

            LoadMembersAsync();
        }

        private async void LoadMembersAsync()
        {
            //var members = await _memberService.GetAllMembersAsync();
            //_allMembers = members;
            //FilteredMembers = new ObservableCollection<PersonModel>(_allMembers);
        }

        partial void OnSearchTextChanged(string value) => ApplyFilter();

        partial void OnSelectedCategoryChanged(CategoryModel? value) => ApplyFilter();
        partial void OnIsShowingPlayersChanged(bool value) => ApplyFilter();
        private void ApplyFilter()
        {
            var filtered = _allMembers.Where(m =>
            {
                // 1. FILTRE PRIORITAIRE : Type (Joueurs vs Staff)
                if (IsShowingPlayers)
                {
                    // Si on veut les joueurs, on ignore tout ce qui n'est pas PlayerModel
                    if (m is not PlayerModel) return false;
                }
                else
                {
                    // Si on veut le staff, on ignore tout ce qui n'est pas CoachModel
                    if (m is not CoachModel) return false;
                }

                // 2. FILTRE NOM (Recherche textuelle)
                bool matchesText = string.IsNullOrWhiteSpace(SearchText) ||
                                  (m.FullName?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false);

                // 3. FILTRE CATÉGORIE (Seulement pour les joueurs)
                bool matchesCategory = true;
                if (IsShowingPlayers && SelectedCategory != null && m is PlayerModel player)
                {
                    matchesCategory = player.CategoryID == SelectedCategory.Categoryid;
                }

                return matchesText && matchesCategory;

            }).ToList();

            Filterdmembers = new ObservableCollection<PersonModel>(filtered);
        }
    }
}


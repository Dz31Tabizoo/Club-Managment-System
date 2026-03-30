using ClubManagementSystem.Models;
using ClubManagementSystem.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Data;
using Serilog;
namespace ClubManagementSystem.ViewModels
{
    public partial class MembersViewModel : BaseViewModel
    {

        public readonly IMemberService _memberService;

        private ObservableCollection<PersonModel> _allMembers = new();        
      
        
        private ICollectionView MembersView {  get; }
        // joueurs ou coachs
        [ObservableProperty]
        private bool _isShowingPlayers = true;

        [ObservableProperty]
        private bool _isBusy;

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

            MembersView = CollectionViewSource.GetDefaultView(_allMembers);

            MembersView.Filter = FilterLogic;

            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            //await LoadMembersAsync();
        }

        private async Task LoadMembersAsync()
        {
            IsBusy = true;
            try
            {
                var members = await _memberService.GetAllMembersasync();

                App.Current.Dispatcher.Invoke(() =>
                {
                    _allMembers.Clear();
                    foreach (var m in members)                    
                        _allMembers.Add(m);
                    
                    MembersView?.Refresh();
                });

            }
            catch(Exception ex)
            {
                Log.Error("ErrorMessage while Loading Members: " + ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private bool FilterLogic(object obj)
        {
            if (obj is not PersonModel m) return false;

            // joueurs ou coachs
            if (IsShowingPlayers && m is not PlayerModel) return false;
            if (!IsShowingPlayers && m is not CoachModel) return false;

            //  Search Text Filter 
            bool matchesText = string.IsNullOrWhiteSpace(SearchText) ||
                              (m.FullName?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false);

            // Category Filter
            bool matchesCategory = true;
            if (IsShowingPlayers && SelectedCategory != null && m is PlayerModel player)
            {
                matchesCategory = player.categoryNameDisplay == SelectedCategory.CategoryName;
            }

            return matchesText && matchesCategory;
        }

        private void ApplyFilter()
        {
            MembersView.Refresh();
        }

        partial void OnSearchTextChanged(string value) => ApplyFilter();

        partial void OnSelectedCategoryChanged(CategoryModel? value) => ApplyFilter();
        partial void OnIsShowingPlayersChanged(bool value)
        {
            if (!value) SelectedCategory = null;
            ApplyFilter();
        }
        
       
    }
}


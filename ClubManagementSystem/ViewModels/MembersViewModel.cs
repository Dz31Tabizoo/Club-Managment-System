using ClubManagementSystem.Models;
using ClubManagementSystem.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
namespace ClubManagementSystem.ViewModels
{
    public partial class MembersViewModel : BaseViewModel
    {

        public readonly IMemberService _memberService;

        public ObservableCollection<PersonModel> AllMembers { get; } = new ObservableCollection<PersonModel>();

        public ICollectionView MembersView { get; }

        //empty MemberView
        [ObservableProperty]
        private bool _hasNoResults;

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
        private ObservableCollection<CategoryModel> _categories = new()
{
    new CategoryModel { CategoryID = 1, CategoryName = "U9", MinAge = 7, MaxAge = 9, MonthlyFee = 2000 },
    new CategoryModel { CategoryID = 2, CategoryName = "U11", MinAge = 9, MaxAge = 11, MonthlyFee = 2200 },
    new CategoryModel { CategoryID = 3, CategoryName = "U13", MinAge = 11, MaxAge = 13, MonthlyFee = 2500 },
    new CategoryModel { CategoryID = 4, CategoryName = "U15", MinAge = 13, MaxAge = 15, MonthlyFee = 2800 }
};

        [ObservableProperty]
        private CategoryModel? _selectedCategory;

        public int FilterdCont => MembersView.Cast<object>().Count();
        public int TotalCount => AllMembers.Count;
        public string ResultSummary => $"[{FilterdCont} members sur {TotalCount}]";




        public MembersViewModel(IMemberService memberService)
        {
            _memberService = memberService;


            MembersView = CollectionViewSource.GetDefaultView(AllMembers);

            MembersView.Filter = FilterLogic;

            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            await LoadMembersAsync();
        }

        private async Task LoadMembersAsync()
        {
            IsBusy = true;
            try
            {
                if (_memberService == null) return;
                var members = await _memberService.GetAllMembersasync();

                App.Current.Dispatcher.Invoke(() =>
                {
                    AllMembers.Clear();
                    foreach (var m in members)
                        AllMembers.Add(m);
                    OnPropertyChanged(nameof(MembersView));
                    ApplyFilter();
                });

            }
            catch (Exception ex)
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
                matchesCategory = player.CategoryName == SelectedCategory.CategoryName;
            }

            return matchesText && matchesCategory;
        }

        private void ApplyFilter()
        {
            MembersView.Refresh();

            OnPropertyChanged(nameof(TotalCount));
            OnPropertyChanged(nameof(FilterdCont));
            OnPropertyChanged(nameof(ResultSummary));

            HasNoResults = !MembersView.Cast<object>().Any();
        }

        partial void OnSearchTextChanged(string value) => ApplyFilter();

        partial void OnSelectedCategoryChanged(CategoryModel? value) => ApplyFilter();
        partial void OnIsShowingPlayersChanged(bool value)
        {
            if (!value) SelectedCategory = null;
            ApplyFilter();

        }

        [RelayCommand]
        private void ResetFilters()
        {
            SearchText = string.Empty;
            SelectedCategory = null;
        }

        [RelayCommand]
        private void AddMember()
        {
            if (IsShowingPlayers)
            {
                //(e.g., open a dialog or navigate to a new page)
            }
            else
            {
                //(e.g., open a dialog or navigate to a new page)
            }
            
        }
    }
}


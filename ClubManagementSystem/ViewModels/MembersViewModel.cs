using ClubManagementSystem.Models;
using ClubManagementSystem.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
namespace ClubManagementSystem.ViewModels
{
    public partial class MembersViewModel : BaseViewModel
    {
        private readonly IMemberService _memberService;

        [ObservableProperty]
        private bool _isMemberCardOpen;

        [ObservableProperty]
        private MemberCardViewModel _selectedMemberCardVM = new();

        [ObservableProperty]
        private PersonModel? _selectedMember;

        public ObservableCollection<PersonModel> AllMembers { get; } = new();
        public ICollectionView MembersView { get; }

        [ObservableProperty] private bool _hasNoResults;
        [ObservableProperty] private bool _isShowingPlayers = true;
        [ObservableProperty] private bool _isBusy;
        [ObservableProperty] private string _searchText = string.Empty;

        [ObservableProperty]
        private ObservableCollection<CategoryModel> _categories = new()
    {
        new CategoryModel { CategoryID = 1, CategoryName = "U9", MinAge = 7, MaxAge = 9, MonthlyFee = 2000 },
        new CategoryModel { CategoryID = 2, CategoryName = "U11", MinAge = 9, MaxAge = 11, MonthlyFee = 2200 },
        new CategoryModel { CategoryID = 3, CategoryName = "U13", MinAge = 11, MaxAge = 13, MonthlyFee = 2500 },
        new CategoryModel { CategoryID = 4, CategoryName = "U15", MinAge = 13, MaxAge = 15, MonthlyFee = 2800 }
    };

        [ObservableProperty] private CategoryModel? _selectedCategory;

        public int FilteredCount => MembersView.Cast<object>().Count();
        public int TotalCount => AllMembers.Count;
        public string ResultSummary => $"[{FilteredCount} membres sur {TotalCount}]";

        public MembersViewModel(IMemberService memberService)
        {
            _memberService = memberService;
            MembersView = CollectionViewSource.GetDefaultView(AllMembers);
            MembersView.Filter = FilterLogic;

            // --- LIAISON CRUCIALE ---
            // On écoute quand la carte demande à se fermer
            SelectedMemberCardVM.RequestClose += () => IsMemberCardOpen = false;

            _ = InitializeAsync();
        }

        private async Task InitializeAsync() => await LoadMembersAsync();

        private async Task LoadMembersAsync()
        {
            IsBusy = true;
            try
            {
                var members = await _memberService.GetAllMembersasync();
                App.Current.Dispatcher.Invoke(() =>
                {
                    AllMembers.Clear();
                    foreach (var m in members) AllMembers.Add(m);
                    ApplyFilter();
                });
            }
            catch (Exception ex)
            {
                // Log.Error...
            }
            finally { IsBusy = false; }
        }

        private bool FilterLogic(object obj)
        {
            if (obj is not PersonModel m) return false;

            if (IsShowingPlayers && m is not PlayerModel) return false;
            if (!IsShowingPlayers && m is not CoachModel) return false;

            bool matchesText = string.IsNullOrWhiteSpace(SearchText) ||
                              (m.FirstName?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                              (m.LastName?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false);

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
            OnPropertyChanged(nameof(FilteredCount));
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
        private void ShowMemberDetails()
        {
            if (SelectedMember == null) return;
            SelectedMemberCardVM.Member = SelectedMember;
            SelectedMemberCardVM.IsReadOnly = true;
            IsMemberCardOpen = true;
        }
    }
}


using WPF_APP.Core;
using System;

namespace Club_Management_System.WPF.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        
        public RelayCommand NavigateToPlayerCommand { get; set; }
        public RelayCommand NavigateToDashboardCommand { get; set; }

        // 3. المشيد (Constructor)
        public MainViewModel()
        {
            NavigateToPlayerCommand = new RelayCommand(
                execute: (o) =>
                {
                    // CurrentView = new PlayerView(); 
                    Console.WriteLine("Navigating to PlayerView via Delegate");
                },
                canExecute: (o) => true
            );

            NavigateToDashboardCommand = new RelayCommand(
                execute: (o) =>
                {
                    // CurrentView = new DashboardView();
                    Console.WriteLine("Navigating to DashboardView via Delegate");
                },
                canExecute: (o) => true
            );
        }
    }
}

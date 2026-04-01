using ClubManagementSystem.Services;
using ClubManagementSystem.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClubManagementSystem.Views
{
    /// <summary>
    /// Logique d'interaction pour MembersView.xaml
    /// </summary>
    public partial class MembersView : UserControl
    {

        public MembersView() // Add this back!
        {
            InitializeComponent();
            this.DataContext = App.Current.ServiceProvider.GetRequiredService<MembersViewModel>();
        }




    }
}

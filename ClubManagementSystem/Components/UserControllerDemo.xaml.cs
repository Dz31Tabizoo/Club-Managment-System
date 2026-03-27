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

namespace ClubManagementSystem.Components
{
    /// <summary>
    /// Logique d'interaction pour UserControllerDemo.xaml
    /// </summary>
    public partial class UserControllerDemo : UserControl
    {
        public UserControllerDemo()
        {
            InitializeComponent();
        }

        private void SunbmitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Submited");
        }
    }
}

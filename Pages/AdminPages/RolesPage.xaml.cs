using Final_Practice.database;
using Final_Practice.ViewModels.AdminPagesViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Final_Practice.Pages.AdminPages
{
    /// <summary>
    /// Interaction logic for RolesPage.xaml
    /// </summary>
    public partial class RolesPage : Page
    {
        public RolesViewModel rolesVM { get; set; }
        public RolesPage()
        {
            rolesVM = new RolesViewModel();
            InitializeComponent();
        }

        private void OnClearInfo_Click(object sender, RoutedEventArgs e)
        {
            rolesVM.ClearInfo();
        }

        private void OnSaveData_Click(object sender, RoutedEventArgs e)
        {
            rolesVM.LoadRoles();
        }
    }
}

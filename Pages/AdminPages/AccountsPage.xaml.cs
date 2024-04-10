using Final_Practice.database;
using Final_Practice.ViewModels.AdminPagesViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
    /// Interaction logic for AccountsPage.xaml
    /// </summary>
    public partial class AccountsPage : Page
    {
       public AccountsViewModel accountsVM { get; set; }
        public AccountsPage()
        {
            accountsVM = new AccountsViewModel();
            InitializeComponent();
        }

        private void OnClearInfo_Click(object sender, RoutedEventArgs e)
        {
            accountsVM.ClearInfo();
        }

        private void OnLoadData_Click(object sender, RoutedEventArgs e)
        {
            accountsVM.LoadAccounts();
        }
    }
}

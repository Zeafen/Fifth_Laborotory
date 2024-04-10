using Final_Practice.Pages.AdminPages;
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
using System.Windows.Shapes;

namespace Final_Practice
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            PageSelection.ItemsSource = new object[]
            {
                AdminTables.Roles,
                AdminTables.Employees,
                AdminTables.Accounts,
             };
        }

        private void onSelectedPageChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PageSelection.SelectedItem != null && PageSelection.SelectedItem is AdminTables)
            {
                switch (PageSelection.SelectedItem)
                {
                    case AdminTables.Roles:
                        AdminPages.Content = new RolesPage();
                        break;
                    case AdminTables.Accounts:
                        AdminPages.Content = new AccountsPage();
                        break;
                    case AdminTables.Employees:
                        AdminPages.Content = new EmployeesPage();
                        break;
                }
            }
        }
    }
}

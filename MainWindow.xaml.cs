using Final_Practice.ViewModels;
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

namespace Final_Practice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public LoginViewModel loginVM { get; set; }
         public MainWindow()
        {
            loginVM = new LoginViewModel();
            InitializeComponent();
        }

        private void OnAuthorize_Click(object sender, RoutedEventArgs e)
        {
            if (!loginVM.LogIn())
                MessageBox.Show("Неправильный логин или пароль. Проверьте введенные значения на предмет ошибок.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                if (loginVM.LoggedRole.ID_Role == 1)
                {
                    AdminWindow adminWindow = new AdminWindow();
                    Close();
                    adminWindow.Show();
                }
                else if (loginVM.LoggedRole.Role_Name.ToLower() == "cashierrole")
                {
                    CashierWindow cashWin = new CashierWindow();
                    Close();
                    cashWin.Show();
                }
                else if (loginVM.LoggedRole.Role_Name.ToLower() == "storagemanagerrole")
                {
                    StorageWindow storageWin = new StorageWindow();
                    Close();
                    storageWin.Show();
                }
            }
        }
    }
}

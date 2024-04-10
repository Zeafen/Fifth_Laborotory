using Final_Practice.Pages.CashierPages;
using Final_Practice.Pages.CashierPages.ExistingOrdersPage;
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
    /// Interaction logic for CashierPage.xaml
    /// </summary>
    public partial class CashierWindow : Window
    {
        private OrdersPage ordersPage;
        public CashierWindow()
        {
            ordersPage = new OrdersPage();
            InitializeComponent();
            CashierPages.Content = ordersPage;
        }

        private void ProcessNewOrder_Click(object sender, RoutedEventArgs e)
        {
            if (CashierPages.Content == ordersPage)
            {
                ordersPage = null;
                CashierPages.Content = new CashPage(() =>
                {
                    ordersPage = new OrdersPage();
                    CashierPages.Content = ordersPage;
                });
            }
        }
    }
}

using Final_Practice.database;
using Final_Practice.ViewModels.CashierPagesViewModels;
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

namespace Final_Practice.Pages.CashierPages.ExistingOrdersPage
{
    /// <summary>
    /// Interaction logic for OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {

        public OrdersViewModel ordersVM { get; set; }
        public OrdersPage()
        {
            ordersVM = new OrdersViewModel();
            InitializeComponent();
        }

        private void OnClearInfo_Click(object sender, RoutedEventArgs e)
        {
            ordersVM.ClearInfo();
        }

        private void OrdersDGr_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ordersVM.CurrentOrder != null)
            {
                OrderInfoWindow orderInfo = new OrderInfoWindow(ordersVM);
                orderInfo.ShowDialog();
            }
        }
    }
}

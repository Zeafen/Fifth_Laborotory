using Final_Practice.database;
using Final_Practice.Pages.CashierPages.ExistingOrdersPage;
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

namespace Final_Practice.Pages.CashierPages
{
    /// <summary>
    /// Interaction logic for CashPage.xaml
    /// </summary>
    public partial class CashPage : Page
    {
        private Action OnFinishOrder;
        public CashierPageViewModel cashVM { get; set; }
        public CashPage(Action onFinishOrder)
        {
            OnFinishOrder = onFinishOrder;
            cashVM = new CashierPageViewModel();
            InitializeComponent();
            CupsSelectionMI.Tag = OrderType.Cup;
            PackagesSelectionMI.Tag = OrderType.Package;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if(sender  is MenuItem mi)
                cashVM.CurrentOrderType = (OrderType)mi.Tag;
        }

        private void OnGoToOrder_Click(object sender, RoutedEventArgs e)
        {
            var vm = new OrdersViewModel();
            vm.CurrentOrder = cashVM.NewOrder;
            var config = new OrderInfoWindow(vm);
            config.ShowDialog();
            if(!cashVM.CheckIfNewOrderExists())
                OnFinishOrder();
        }

        private void OnFinishOrder_Click(object sender, RoutedEventArgs e)
        {
            var payWindow = new PayingWindow(cashVM, OnFinishOrder);
            payWindow.ShowDialog();
        }
    }
}

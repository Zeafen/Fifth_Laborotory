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
using System.Windows.Shapes;

namespace Final_Practice.Pages.CashierPages.ExistingOrdersPage
{
    /// <summary>
    /// Interaction logic for PayingWindow.xaml
    /// </summary>
    public partial class PayingWindow : Window
    {
        private Action OnFinishPaying;
        public CashierPageViewModel cashVM { get; set; } 
        public PayingWindow(CashierPageViewModel _cashViewModel, Action onFinishPaying)
        {
            this.cashVM = _cashViewModel;
            InitializeComponent();
            this.Closing += PayingWindow_Closing;
            this.Closed += PayingWindow_Closed;
            OnFinishPaying = onFinishPaying;
        }

        private void PayingWindow_Closed(object sender, EventArgs e)
        {
            if (cashVM.NeedPay)
                return;
        }

        private void PayingWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (cashVM.NeedPay)
                return;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!cashVM.NeedPay)
            {
                DialogResult = true;
                OnFinishPaying();
            }
            else
                MessageBox.Show("Pay for the order at first!!!!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}

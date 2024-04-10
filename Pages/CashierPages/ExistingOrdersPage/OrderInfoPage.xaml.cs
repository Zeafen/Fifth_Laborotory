using Final_Practice.ViewModels.CashierPagesViewModels;
using Final_Practice.ViewModels.StoragePagesViewModels;
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
    /// Interaction logic for OrderInfoPage.xaml
    /// </summary>
    public partial class OrderInfoWindow : Window
    {
        public PackOrderTeasViewModel packOrdersVM { get; set; }
        public Cups_OrderTeasViewModel cupOrdersVM { get; set; }
        public Collection_OrderTeasViewModel collectionOrdersVM { get; set; }
        public OrdersViewModel ordersVM { get; set; }
        public OrderInfoWindow(OrdersViewModel _ordersVM)
        {
            ordersVM = _ordersVM; 
            packOrdersVM = new PackOrderTeasViewModel(ordersVM.CurrentOrder.ID_Order);
            cupOrdersVM = new Cups_OrderTeasViewModel(ordersVM.CurrentOrder.ID_Order);
            collectionOrdersVM = new Collection_OrderTeasViewModel(ordersVM.CurrentOrder.ID_Order);
            InitializeComponent();
        }

        private void OnGoBack_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void PacksDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (PackagesGr.RowDefinitions[1].Height.Equals(new GridLength(0, GridUnitType.Pixel)) && PackagesGr.RowDefinitions[2].Height.Equals(new GridLength(0, GridUnitType.Pixel)))
            {
                PackagesGr.RowDefinitions[0].Height = new GridLength(5, GridUnitType.Star);
                PackagesGr.RowDefinitions[1].Height = new GridLength(10, GridUnitType.Pixel);
                PackagesGr.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
            }
            else
                PackagesGr.RowDefinitions[2].Height = PackagesGr.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Pixel);
        }
        private void CupDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (CupsGr.RowDefinitions[1].Height.Equals(new GridLength(0, GridUnitType.Pixel)) && CupsGr.RowDefinitions[2].Height.Equals(new GridLength(0, GridUnitType.Pixel)))
            {
                CupsGr.RowDefinitions[0].Height = new GridLength(5, GridUnitType.Star);
                CupsGr.RowDefinitions[1].Height = new GridLength(10, GridUnitType.Pixel);
                CupsGr.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
            }
            else
                CupsGr.RowDefinitions[2].Height = CupsGr.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Pixel);
        }
        private void CollectionDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (CollectionsGr.RowDefinitions[1].Height.Equals(new GridLength(0, GridUnitType.Pixel)) && CollectionsGr.RowDefinitions[2].Height.Equals(new GridLength(0, GridUnitType.Pixel)))
            {
                CollectionsGr.RowDefinitions[0].Height = new GridLength(5, GridUnitType.Star);
                CollectionsGr.RowDefinitions[1].Height = new GridLength(10, GridUnitType.Pixel);
                CollectionsGr.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
            }
            else
                CollectionsGr.RowDefinitions[2].Height = CollectionsGr.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Pixel);
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            ordersVM.UpdateOrderPayBack();
        }
    }
}

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

namespace Final_Practice.Pages.StoragePages.Collections
{
    /// <summary>
    /// Interaction logic for CollectionsPage.xaml
    /// </summary>
    public partial class CollectionsPage : Page
    {
        public CollectionsViewModel collectionsVM { get; set; }
        public CollectionsPage()
        {
            collectionsVM = new CollectionsViewModel();
            InitializeComponent();
        }

        private void OnClearInfo_Click(object sender, RoutedEventArgs e)
        {
            collectionsVM.ClearInfo();
        }

        private void Button_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            collectionsVM.UpdateImage();
        }

        private void OnSaveData_Click(object sender, RoutedEventArgs e)
        {
            collectionsVM.Loadcollections();
        }
    }
}

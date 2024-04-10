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

namespace Final_Practice.Pages.StoragePages.Forms
{
    /// <summary>
    /// Interaction logic for CollectionFormsPage.xaml
    /// </summary>
    public partial class CollectionFormsPage : Page
    {
        public CollectionFormsViewModel collectionFormsVM { get; set; }
        public CollectionFormsPage()
        {
            collectionFormsVM = new CollectionFormsViewModel();
            InitializeComponent();
        }

        private void OnClearInfo_Click(object sender, RoutedEventArgs e)
        {
            collectionFormsVM.ClearInfo();
        }
    }
}

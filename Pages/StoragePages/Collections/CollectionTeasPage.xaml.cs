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
    /// Interaction logic for CollectionTeasPage.xaml
    /// </summary>
    public partial class CollectionTeasPage : Page
    {
        public CollectionsTeasViewModel collectionTeasVM { get; set; }
        public CollectionTeasPage()
        {
            collectionTeasVM = new CollectionsTeasViewModel();
            InitializeComponent();
            
        }

        private void OnClearInfo_Click(object sender, RoutedEventArgs e)
        {
            collectionTeasVM.ClearInfo();
        }
    }
}

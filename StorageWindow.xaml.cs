using Final_Practice.Pages.StoragePages;
using Final_Practice.Pages.StoragePages.Collections;
using Final_Practice.Pages.StoragePages.Forms;
using Final_Practice.Pages.StoragePages.Storages;
using Final_Practice.Pages.Storages.StoragePages;
using Final_Practice.Pages.Teas.StoragePages;
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
    /// Interaction logic for StorageWindow.xaml
    /// </summary>
    public partial class StorageWindow : Window
    {
        public StorageWindow()
        {
            InitializeComponent();
            PageSelection.ItemsSource = new object[]
{
                StorageTables.Packages,
                StorageTables.Cups,
                StorageTables.Storage,
                StorageTables.TeaSorts,
                StorageTables.TeaTypes,
                StorageTables.Producers,
                StorageTables.CollectionForms,
                StorageTables.CollectionsStorage,
                StorageTables.Collections,
                StorageTables.CollectionTeas,
};
        }

        private void onSelectedPageChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PageSelection.SelectedItem != null && PageSelection.SelectedItem is StorageTables)
            {
                switch (PageSelection.SelectedItem)
                {
                    case StorageTables.Packages:
                        Pages.Content = new PackagesPage();
                        break;
                    case StorageTables.Cups:
                        Pages.Content = new CupsPage();
                        break;
                    case StorageTables.Storage:
                        Pages.Content = new StoragePage();
                        break;
                    case StorageTables.TeaSorts:
                        Pages.Content = new TeaSortsPage();
                        break;
                    case StorageTables.TeaTypes:
                        Pages.Content = new TeaTypesPage();
                        break;
                    case StorageTables.Producers:
                        Pages.Content = new ProducersPage();
                        break;
                    case StorageTables.CollectionForms:
                        Pages.Content = new CollectionFormsPage();
                        break;
                    case StorageTables.CollectionsStorage:
                        Pages.Content = new CollectionsStoragePage();
                        break;
                    case StorageTables.Collections:
                        Pages.Content = new CollectionsPage();
                        break;
                    case StorageTables.CollectionTeas:
                        Pages.Content = new CollectionTeasPage();
                        break;
                }
            }
        }
    }
}

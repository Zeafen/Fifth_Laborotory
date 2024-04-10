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
    /// Interaction logic for CupsPage.xaml
    /// </summary>
    public partial class CupsPage : Page
    {
        public CupsViewModel cupsVM { get; set; }
        public CupsPage()
        {
            cupsVM = new CupsViewModel();
            InitializeComponent();
        }

        private void OnClearInfo_Click(object sender, RoutedEventArgs e)
        {
            cupsVM.ClearInfo();
        }
    }
}

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

namespace Final_Practice.Pages.Teas.StoragePages
{
    /// <summary>
    /// Interaction logic for TeaSortsPage.xaml
    /// </summary>
    public partial class TeaSortsPage : Page
    {
        public TeaSortsViewModel teasVM { get; set; }
        public TeaSortsPage()
        {
            teasVM = new TeaSortsViewModel();
            InitializeComponent();
        }

        private void OnClearInfo_Click(object sender, RoutedEventArgs e)
        {
            teasVM.ClearInfo();
        }

        private void Button_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            teasVM.UpdateImage();
        }

        private void OnSaveData_Click(object sender, RoutedEventArgs e)
        {
            teasVM.LoadTeaSorts();
        }
    }
}

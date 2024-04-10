using Final_Practice.Commands;
using Final_Practice.database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Final_Practice.ViewModels.CashierPagesViewModels
{
    public class PackOrderTeasViewModel : INotifyPropertyChanged
    {

        private RelayCommand<Pack_OrdersTeas> _deletePack_OrdersTeasCommand = null;
        public RelayCommand<Pack_OrdersTeas> deletePack_OrdersTeasCommand => _deletePack_OrdersTeasCommand ?? 
            (_deletePack_OrdersTeasCommand = new RelayCommand<Pack_OrdersTeas>(DeletePack_OrdersTeas, (packOrder) => packOrder != null && Pack_OrdersTeas.FirstOrDefault(c => c.ID_PackOrderTeas == packOrder?.ID_PackOrderTeas) != null));

        private RelayCommand<Pack_OrdersTeas> _updatePack_OrdersTeasCommand = null;
        public RelayCommand<Pack_OrdersTeas> updatePack_OrdersTeasCommand => _updatePack_OrdersTeasCommand ?? 
            (_updatePack_OrdersTeasCommand = new RelayCommand<Pack_OrdersTeas>(UpdatePack_OrdersTeas, (packOrder) => packOrder != null && Pack_OrdersTeas.FirstOrDefault(c => c.ID_PackOrderTeas == packOrder?.ID_PackOrderTeas) != null));


        private TeaShopDBContext ctx;

        public ObservableCollection<Pack_OrdersTeas> Pack_OrdersTeas { get; set; }
        public ObservableCollection<Packages> Packages { get; set; }
        public ObservableCollection<TeaSorts> TeaSorts { get; set; }

        private Pack_OrdersTeas _CurrentPack_OrdersTeas { get; set; } = new Pack_OrdersTeas();
        public Pack_OrdersTeas CurrentPack_OrdersTeas
        {
            get => _CurrentPack_OrdersTeas;
            set
            {
                _CurrentPack_OrdersTeas = value;
                OnPropertyChanged();
            }
        }

        public PackOrderTeasViewModel()
        {
            ctx = new TeaShopDBContext();
            Pack_OrdersTeas = new ObservableCollection<Pack_OrdersTeas>(ctx.Pack_OrdersTeas.ToList());
            Packages = new ObservableCollection<Packages>(ctx.Packages.ToList());
            TeaSorts = new ObservableCollection<TeaSorts>(ctx.TeaSorts.ToList());
        }
        public PackOrderTeasViewModel(int OrderID)
        {
            ctx = new TeaShopDBContext();
            Pack_OrdersTeas = new ObservableCollection<Pack_OrdersTeas>(ctx.Pack_OrdersTeas.ToList().Where(p => p.Order_ID == OrderID));
            Packages = new ObservableCollection<Packages>(ctx.Packages.ToList());
            TeaSorts = new ObservableCollection<TeaSorts>(ctx.TeaSorts.ToList());
        }


        public void ClearInfo()
        {
            CurrentPack_OrdersTeas = new Pack_OrdersTeas();
        }

        public void UpdatePack_OrdersTeas(Pack_OrdersTeas pack_OrdersTeas)
        {
            try
            {
                if (pack_OrdersTeas == null) throw new ArgumentNullException("Cannot add null value");
                var updatedPack_OrdersTeas = ctx.Pack_OrdersTeas.FirstOrDefault(c => c.ID_PackOrderTeas == pack_OrdersTeas.ID_PackOrderTeas);
                var arrayUpdatedPack_OrdersTeas = Pack_OrdersTeas.FirstOrDefault(c => c.ID_PackOrderTeas == pack_OrdersTeas.ID_PackOrderTeas);
                if (updatedPack_OrdersTeas != null)
                {
                    updatedPack_OrdersTeas = pack_OrdersTeas;
                    ctx.SaveChanges();
                    arrayUpdatedPack_OrdersTeas = pack_OrdersTeas;
                }
                CurrentPack_OrdersTeas = new Pack_OrdersTeas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void DeletePack_OrdersTeas(Pack_OrdersTeas pack_OrdersTeas)
        {
            try
            {
                if (pack_OrdersTeas == null) throw new ArgumentNullException("Cannot delete null value");
                ctx.Pack_OrdersTeas.Remove(pack_OrdersTeas);
                ctx.SaveChanges();
                Pack_OrdersTeas.Remove(pack_OrdersTeas);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

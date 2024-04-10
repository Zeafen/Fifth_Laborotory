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
    public class Cups_OrderTeasViewModel : INotifyPropertyChanged
    {

        private RelayCommand<Cup_OrdersTeas> _deleteCup_OrdersTeasCommand = null;
        public RelayCommand<Cup_OrdersTeas> deleteCup_OrdersTeasCommand => _deleteCup_OrdersTeasCommand ??
            (_deleteCup_OrdersTeasCommand = new RelayCommand<Cup_OrdersTeas>(DeleteCup_OrdersTeas, (cup_OrdersTeas) => cup_OrdersTeas != null && Cup_OrdersTeas.FirstOrDefault(c => c.ID_CupOrderTeas == cup_OrdersTeas?.ID_CupOrderTeas) != null));

        private RelayCommand<Cup_OrdersTeas> _updateCup_OrdersTeasCommand = null;
        public RelayCommand<Cup_OrdersTeas> updateCup_OrdersTeasCommand => _updateCup_OrdersTeasCommand ??
            (_updateCup_OrdersTeasCommand = new RelayCommand<Cup_OrdersTeas>(UpdateCup_OrdersTeas, (cup_OrdersTeas) => cup_OrdersTeas != null && Cup_OrdersTeas.FirstOrDefault(c => c.ID_CupOrderTeas == cup_OrdersTeas?.ID_CupOrderTeas) != null));


        private TeaShopDBContext ctx;

        public ObservableCollection<Cup_OrdersTeas> Cup_OrdersTeas { get; set; }
        public ObservableCollection<Cups> Cups { get; set; }
        public ObservableCollection<TeaSorts> TeaSorts { get; set; }

        private Cup_OrdersTeas _CurrentCup_OrdersTeas { get; set; } = new Cup_OrdersTeas();
        public Cup_OrdersTeas CurrentCup_OrdersTeas
        {
            get => _CurrentCup_OrdersTeas;
            set
            {
                _CurrentCup_OrdersTeas = value;
                OnPropertyChanged();
            }
        }

        public Cups_OrderTeasViewModel()
        {
            ctx = new TeaShopDBContext();
            Cups = new ObservableCollection<Cups>(ctx.Cups.ToList());
            Cup_OrdersTeas = new ObservableCollection<Cup_OrdersTeas>(ctx.Cup_OrdersTeas.ToList());
            TeaSorts = new ObservableCollection<TeaSorts>(ctx.TeaSorts.ToList());
        }

        public Cups_OrderTeasViewModel(int OrderID)
        {
            ctx = new TeaShopDBContext();
            Cups = new ObservableCollection<Cups>(ctx.Cups.ToList());
            Cup_OrdersTeas = new ObservableCollection<Cup_OrdersTeas>(ctx.Cup_OrdersTeas.ToList().Where(c => c.Order_ID == OrderID));
            TeaSorts = new ObservableCollection<TeaSorts>(ctx.TeaSorts.ToList());
        }


        public void ClearInfo()
        {
            CurrentCup_OrdersTeas = new Cup_OrdersTeas();
        }
        public void UpdateCup_OrdersTeas(Cup_OrdersTeas cup_OrdersTeas)
        {
            try
            {
                if (cup_OrdersTeas == null) throw new ArgumentNullException("Cannot add null value");
                var updatedCup_OrdersTeas = ctx.Cup_OrdersTeas.FirstOrDefault(c => c.ID_CupOrderTeas == cup_OrdersTeas.ID_CupOrderTeas);
                var arrayUpdatedCup_OrdersTeas = Cup_OrdersTeas.FirstOrDefault(c => c.ID_CupOrderTeas == cup_OrdersTeas.ID_CupOrderTeas);
                if (updatedCup_OrdersTeas != null)
                {
                    updatedCup_OrdersTeas = cup_OrdersTeas;
                    ctx.SaveChanges();
                    arrayUpdatedCup_OrdersTeas = cup_OrdersTeas;
                }
                CurrentCup_OrdersTeas = new Cup_OrdersTeas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void DeleteCup_OrdersTeas(Cup_OrdersTeas cup_OrdersTeas)
        {
            try
            {
                if (cup_OrdersTeas == null) throw new ArgumentNullException("Cannot delete null value");
                ctx.Cup_OrdersTeas.Remove(cup_OrdersTeas);
                ctx.SaveChanges();
                Cup_OrdersTeas.Remove(cup_OrdersTeas);
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

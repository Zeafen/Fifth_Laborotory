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

namespace Final_Practice.ViewModels.StoragePagesViewModels
{
    public class CupsViewModel : INotifyPropertyChanged
    {
        private RelayCommand<Cups> _addCupCommand = null;
        public RelayCommand<Cups> addCupCommand => _addCupCommand ?? (_addCupCommand = new RelayCommand<Cups>(AddCup,
            (cup) => !string.IsNullOrEmpty(cup?.Cup_Name) &&
            Cups.FirstOrDefault(c => c.Cup_Name == cup?.Cup_Name) == null && Cups.FirstOrDefault(c => c.Cup_AmountTea == cup?.Cup_AmountTea) == null));

        private RelayCommand<Cups> _deleteCupCommand = null;
        public RelayCommand<Cups> deleteCupCommand => _deleteCupCommand ?? (_deleteCupCommand = new RelayCommand<Cups>(DeleteCup, (cup) => cup != null && Cups.FirstOrDefault(c => c.ID_Cup == cup?.ID_Cup) != null));

        private RelayCommand<Cups> _updateCupCommand = null;
        public RelayCommand<Cups> updateCupCommand => _updateCupCommand ?? (_updateCupCommand = new RelayCommand<Cups>(UpdateCup, (cup) => cup != null && Cups.FirstOrDefault(c => c.ID_Cup == cup?.ID_Cup) != null));


        private TeaShopDBContext ctx;

        public ObservableCollection<Cups> Cups { get; set; }

        private Cups _CurrentCup { get; set; } = new Cups();
        public Cups CurrentCup
        {
            get => _CurrentCup;
            set
            {
                _CurrentCup = value;
                OnPropertyChanged();
            }
        }

        public CupsViewModel()
        {
            ctx = new TeaShopDBContext();
            Cups = new ObservableCollection<Cups>(ctx.Cups.ToList());
        }


        public void ClearInfo()
        {
            CurrentCup = new Cups();
        }
        public void AddCup(Cups newCup)
        {
            try
            {
                if (newCup == null) throw new ArgumentNullException("Cannot add null value");
                ctx.Cups.Add(newCup);
                ctx.SaveChanges();
                Cups.Add(newCup);
                CurrentCup = new Cups();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void UpdateCup(Cups cup)
        {
            try
            {
                if (cup == null) throw new ArgumentNullException("Cannot add null value");
                var updatedCup = ctx.Cups.FirstOrDefault(c => c.ID_Cup == cup.ID_Cup);
                var arrayUpdatedCup = Cups.FirstOrDefault(c => c.ID_Cup == cup.ID_Cup);
                if (updatedCup != null)
                {
                    updatedCup = cup;
                    ctx.SaveChanges();
                    arrayUpdatedCup = cup;
                }
                CurrentCup = new Cups();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void DeleteCup(Cups cup)
        {
            try
            {
                if (cup == null) throw new ArgumentNullException("Cannot delete null value");
                ctx.Cups.Remove(cup);
                ctx.SaveChanges();
                Cups.Remove(cup);
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

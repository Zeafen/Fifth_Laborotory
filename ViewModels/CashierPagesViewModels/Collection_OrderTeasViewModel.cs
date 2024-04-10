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
    public class Collection_OrderTeasViewModel : INotifyPropertyChanged
    {

        private RelayCommand<Collection_OrdersTeas> _deleteCollection_OrdersTeasCommand = null;
        public RelayCommand<Collection_OrdersTeas> deleteCollection_OrdersTeasCommand => _deleteCollection_OrdersTeasCommand ?? 
            (_deleteCollection_OrdersTeasCommand = new RelayCommand<Collection_OrdersTeas>(DeleteCollection_OrdersTeas, (collection_OrdersTeas) => collection_OrdersTeas != null && Collection_OrdersTeas.FirstOrDefault(c => c.ID_CollectionOrderTea == collection_OrdersTeas?.ID_CollectionOrderTea) != null));

        private RelayCommand<Collection_OrdersTeas> _updateCollection_OrdersTeasCommand = null;
        public RelayCommand<Collection_OrdersTeas> updateCollection_OrdersTeasCommand => _updateCollection_OrdersTeasCommand ?? 
            (_updateCollection_OrdersTeasCommand = new RelayCommand<Collection_OrdersTeas>(UpdateCollection_OrdersTeas, (collection_OrdersTeas) => collection_OrdersTeas != null && Collection_OrdersTeas.FirstOrDefault(c => c.ID_CollectionOrderTea == collection_OrdersTeas?.ID_CollectionOrderTea) != null));


        private TeaShopDBContext ctx;

        public ObservableCollection<Collection_OrdersTeas> Collection_OrdersTeas { get; set; }
        public ObservableCollection<Collections> Collections { get; set; }
        public ObservableCollection<TeaSorts> Teas { get; set; }

        private Collection_OrdersTeas _CurrentCollection_OrdersTeas { get; set; } = new Collection_OrdersTeas();
        public Collection_OrdersTeas CurrentCollection_OrdersTeas
        {
            get => _CurrentCollection_OrdersTeas;
            set
            {
                _CurrentCollection_OrdersTeas = value;
                OnPropertyChanged();
            }
        }


        public Collection_OrderTeasViewModel()
        {
            ctx = new TeaShopDBContext();
            Collection_OrdersTeas = new ObservableCollection<Collection_OrdersTeas>(ctx.Collection_OrdersTeas.ToList());
            Collections = new ObservableCollection<Collections>(ctx.Collections.ToList());
            Teas = new ObservableCollection<TeaSorts>(ctx.TeaSorts.ToList());
        }
        public Collection_OrderTeasViewModel(int OrderID) 
        {
            ctx = new TeaShopDBContext();
            Collection_OrdersTeas = new ObservableCollection<Collection_OrdersTeas>(ctx.Collection_OrdersTeas.ToList().Where(c => c.Order_ID == OrderID));
            Collections = new ObservableCollection<Collections>(ctx.Collections.ToList());
            Teas = new ObservableCollection<TeaSorts>(ctx.TeaSorts.ToList());
        }


        public void ClearInfo()
        {
            CurrentCollection_OrdersTeas = new Collection_OrdersTeas();
        }
        public void UpdateCollection_OrdersTeas(Collection_OrdersTeas collection_OrdersTeas)
        {
            try
            {
                if (collection_OrdersTeas == null) throw new ArgumentNullException("Cannot add null value");
                var updatedCollection_OrdersTeas = ctx.Collection_OrdersTeas.FirstOrDefault(c => c.ID_CollectionOrderTea == collection_OrdersTeas.ID_CollectionOrderTea);
                var arrayUpdatedCollection_OrdersTeas = Collection_OrdersTeas.FirstOrDefault(c => c.ID_CollectionOrderTea == collection_OrdersTeas.ID_CollectionOrderTea);
                if (updatedCollection_OrdersTeas != null)
                {
                    updatedCollection_OrdersTeas = collection_OrdersTeas;
                    ctx.SaveChanges();
                    arrayUpdatedCollection_OrdersTeas = collection_OrdersTeas;
                }
                CurrentCollection_OrdersTeas = new Collection_OrdersTeas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void DeleteCollection_OrdersTeas(Collection_OrdersTeas collection_OrdersTeas)
        {
            try
            {
                if (collection_OrdersTeas == null) throw new ArgumentNullException("Cannot delete null value");
                ctx.Collection_OrdersTeas.Remove(collection_OrdersTeas);
                ctx.SaveChanges();
                Collection_OrdersTeas.Remove(collection_OrdersTeas);
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

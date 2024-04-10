using Final_Practice.Commands;
using Final_Practice.database;
using System;
using System.CodeDom;
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
    public class CollectionsTeasViewModel : INotifyPropertyChanged
    {
        private RelayCommand<CollectionTeas> _addCollectionTeasCommand = null;
        public RelayCommand<CollectionTeas> addCollectionTeasCommand => _addCollectionTeasCommand ?? (_addCollectionTeasCommand = new RelayCommand<CollectionTeas>(AddCollectionTeas,
            (collectionTeas) =>
            collectionTeas!= null && collectionTeas?.Collections != null &&
            CollectionTeas.FirstOrDefault(c => c.Collection_ID == collectionTeas?.Collection_ID && collectionTeas?.TeaSort_ID == c.TeaSort_ID) == null));

        private RelayCommand<CollectionTeas> _deleteCollectionTeasCommand = null;
        public RelayCommand<CollectionTeas> deleteCollectionTeasCommand => _deleteCollectionTeasCommand ?? (_deleteCollectionTeasCommand = new RelayCommand<CollectionTeas>(DeleteCup, (cup) => cup != null && CollectionTeas.FirstOrDefault(c => c.ID_CollectionTeas == cup?.ID_CollectionTeas) != null));

        private RelayCommand<CollectionTeas> _updateCollectionTeasCommand = null;
        public RelayCommand<CollectionTeas> updateCollectionTeasCommand => _updateCollectionTeasCommand ?? (_updateCollectionTeasCommand = new RelayCommand<CollectionTeas>(UpdateCollectionTeas, (cup) => cup != null && CollectionTeas.FirstOrDefault(c => c.ID_CollectionTeas == cup?.ID_CollectionTeas) != null));


        private TeaShopDBContext ctx;

        public ObservableCollection<CollectionTeas> CollectionTeas { get; set; }
        public ObservableCollection<Collections> Collections { get; set; }
        public ObservableCollection<TeaSorts> TeaSorts { get; set; }

        private CollectionTeas _CurrentCollectionTeas { get; set; } = new CollectionTeas();
        public CollectionTeas CurrentCollectionTeas
        {
            get => _CurrentCollectionTeas;
            set
            {
                _CurrentCollectionTeas = value;
                OnPropertyChanged();
            }
        }

        public CollectionsTeasViewModel()
        {
            ctx = new TeaShopDBContext();
            CollectionTeas = new ObservableCollection<CollectionTeas>(ctx.CollectionTeas.ToList());
            Collections = new ObservableCollection<Collections>(ctx.Collections.ToList());
            TeaSorts = new ObservableCollection<TeaSorts>(ctx.TeaSorts.ToList());
        }
        private bool EnoughFreeCells(CollectionTeas collection)
        {
            int FreeCount = collection.Collections.CollectionForms.Collection_CellAmount;
            foreach (var item in CollectionTeas)
                if (item.Collection_ID == collection?.ID_CollectionTeas)
                    FreeCount -= item.CollectionTeas_AmountTeas;
            return FreeCount - collection.CollectionTeas_AmountTeas > 0;
        }


        public void ClearInfo()
        {
            CurrentCollectionTeas = new CollectionTeas();
        }
        public void AddCollectionTeas(CollectionTeas newCollectionTeas)
        {
            try
            {
                if (newCollectionTeas == null) throw new ArgumentNullException("Cannot add null value");
                if (EnoughFreeCells(newCollectionTeas)) throw new Exception("Form doesn`t contain so much cells");
                ctx.CollectionTeas.Add(newCollectionTeas);
                ctx.SaveChanges();
                CollectionTeas.Add(newCollectionTeas);
                CurrentCollectionTeas = new CollectionTeas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void UpdateCollectionTeas(CollectionTeas collectionTeas)
        {
            try
            {
                if (collectionTeas == null) throw new ArgumentNullException("Cannot add null value");
                var updatedCollectionTeas = ctx.CollectionTeas.FirstOrDefault(c => c.ID_CollectionTeas == collectionTeas.ID_CollectionTeas);
                var arrayUpdatedCollectionTeas = CollectionTeas.FirstOrDefault(c => c.ID_CollectionTeas == collectionTeas.ID_CollectionTeas);
                if (updatedCollectionTeas != null)
                {
                    updatedCollectionTeas = collectionTeas;
                    ctx.SaveChanges();
                    arrayUpdatedCollectionTeas = collectionTeas;
                }
                CurrentCollectionTeas = new CollectionTeas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void DeleteCup(CollectionTeas collectionTeas)
        {
            try
            {
                if (collectionTeas == null) throw new ArgumentNullException("Cannot delete null value");
                ctx.CollectionTeas.Remove(collectionTeas);
                ctx.SaveChanges();
                CollectionTeas.Remove(collectionTeas);
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

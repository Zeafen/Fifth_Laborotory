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
    public class CollectionsStorageViewModel : INotifyPropertyChanged
    {
        private RelayCommand<CollectionsStorage> _addCollectionsStorageCommand = null;
        public RelayCommand<CollectionsStorage> addCollectionsStorageCommand => _addCollectionsStorageCommand ?? (_addCollectionsStorageCommand = new RelayCommand<CollectionsStorage>(AddCollectionsStorage,
            (collectionsStorage) => collectionsStorage?.Collections != null &&
            CollectionsStorage.FirstOrDefault(c => c.Collection_ID == collectionsStorage?.Collection_ID) == null));

        private RelayCommand<CollectionsStorage> _deleteCollectionsStorageCommand = null;
        public RelayCommand<CollectionsStorage> deleteCollectionsStorageCommand => _deleteCollectionsStorageCommand ?? 
            (_deleteCollectionsStorageCommand = new RelayCommand<CollectionsStorage>(DeleteCup, (collectionsStorage) => collectionsStorage != null && CollectionsStorage.FirstOrDefault(c => c.ID_CollectionStorage == collectionsStorage?.ID_CollectionStorage) != null));

        private RelayCommand<CollectionsStorage> _updateCollectionsStorageCommand = null;
        public RelayCommand<CollectionsStorage> updateCollectionsStorageCommand => _updateCollectionsStorageCommand ??
            (_updateCollectionsStorageCommand = new RelayCommand<CollectionsStorage>(UpdateCollectionsStorage, (collectionsStorage) => collectionsStorage != null && CollectionsStorage.FirstOrDefault(c => c.ID_CollectionStorage == collectionsStorage?.ID_CollectionStorage) != null));


        private TeaShopDBContext ctx;

        public ObservableCollection<CollectionsStorage> CollectionsStorage { get; set; }
        public ObservableCollection<Collections> Collections { get; set; }

        private CollectionsStorage _CurrentCollectionsStorage { get; set; } = new CollectionsStorage();
        public CollectionsStorage CurrentCollectionsStorage
        {
            get => _CurrentCollectionsStorage;
            set
            {
                _CurrentCollectionsStorage = value;
                OnPropertyChanged();
            }
        }

        public CollectionsStorageViewModel()
        {
            ctx = new TeaShopDBContext();
            CollectionsStorage = new ObservableCollection<CollectionsStorage>(ctx.CollectionsStorage.ToList());
            Collections = new ObservableCollection<Collections>(ctx.Collections.ToList());
        }


        public void ClearInfo()
        {
            CurrentCollectionsStorage = new CollectionsStorage();
        }
        public void AddCollectionsStorage(CollectionsStorage newCollectionsStorage)
        {
            try
            {
                if (newCollectionsStorage == null) throw new ArgumentNullException("Cannot add null value");
                ctx.CollectionsStorage.Add(newCollectionsStorage);
                ctx.SaveChanges();
                CollectionsStorage.Add(newCollectionsStorage);
                CurrentCollectionsStorage = new CollectionsStorage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void UpdateCollectionsStorage(CollectionsStorage collectionsStorage)
        {
            try
            {
                if (collectionsStorage == null) throw new ArgumentNullException("Cannot add null value");
                var updatedCollectionsStorage = ctx.CollectionsStorage.FirstOrDefault(c => c.ID_CollectionStorage == collectionsStorage.ID_CollectionStorage);
                var arrayUpdatedCollectionsStorage = CollectionsStorage.FirstOrDefault(c => c.ID_CollectionStorage == collectionsStorage.ID_CollectionStorage);
                if (updatedCollectionsStorage != null)
                {
                    updatedCollectionsStorage = collectionsStorage;
                    ctx.SaveChanges();
                    arrayUpdatedCollectionsStorage = collectionsStorage;
                }
                CurrentCollectionsStorage = new CollectionsStorage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void DeleteCup(CollectionsStorage collectionsStorage)
        {
            try
            {
                if (collectionsStorage == null) throw new ArgumentNullException("Cannot delete null value");
                ctx.CollectionsStorage.Remove(collectionsStorage);
                ctx.SaveChanges();
                CollectionsStorage.Remove(collectionsStorage);
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

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
    public class StorageViewModel : INotifyPropertyChanged
    {

        private RelayCommand<Storage> _addStorageCommand = null;
        public RelayCommand<Storage> addStorageCommand => _addStorageCommand ?? (_addStorageCommand = new RelayCommand<Storage>(AddTeaType, (storage) => storage?.TeaSorts != null && Storage.FirstOrDefault(s => s.Tea_ID == storage?.Tea_ID) == null));

        private RelayCommand<Storage> _deleteStorageCommand = null;
        public RelayCommand<Storage> deleteStorageCommand => _deleteStorageCommand ?? (_deleteStorageCommand = new RelayCommand<Storage>(DeleteTeaType, (storage) => storage != null && Storage.FirstOrDefault(s => s.ID_Storage == storage?.ID_Storage) != null));

        private RelayCommand<Storage> _updateStorageCommand = null;
        public RelayCommand<Storage> updateStorageCommand => _updateStorageCommand ?? (_updateStorageCommand = new RelayCommand<Storage>(UpdateTeaType, (storage) => storage != null && Storage.FirstOrDefault(s => s.ID_Storage == storage?.ID_Storage) != null));


        private TeaShopDBContext ctx;

        public ObservableCollection<Storage> Storage { get; set; }
        public ObservableCollection<TeaSorts> TeaSorts { get; set; }

        private Storage _CurrentStorage { get; set; } = new Storage();
        public Storage CurrentStorage
        {
            get => _CurrentStorage;
            set
            {
                _CurrentStorage = value;
                OnPropertyChanged();
            }
        }

        public StorageViewModel()
        {
            ctx = new TeaShopDBContext();
            Storage = new ObservableCollection<Storage>(ctx.Storage.ToList());
            TeaSorts = new ObservableCollection<TeaSorts>(ctx.TeaSorts.ToList());
        }


        public void ClearInfo()
        {
            CurrentStorage = new Storage();
        }
        public void AddTeaType(Storage newStorage)
        {
            try
            {
                if (newStorage == null) throw new ArgumentNullException("Cannot add null value");
                ctx.Storage.Add(newStorage);
                ctx.SaveChanges();
                Storage.Add(newStorage);
                CurrentStorage = new Storage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void UpdateTeaType(Storage storage)
        {
            try
            {
                if (storage == null) throw new ArgumentNullException("Cannot add null value");
                var updatedStorage = ctx.Storage.FirstOrDefault(s => s. ID_Storage == storage.ID_Storage);
                var arrayUpdatedStorage = Storage.FirstOrDefault(s => s.ID_Storage == storage.ID_Storage);
                if (updatedStorage != null)
                {
                    updatedStorage = storage;
                    ctx.SaveChanges();
                    arrayUpdatedStorage = storage;
                }
                var stor = ctx.Storage.FirstOrDefault(s => s.Storage_CountTea == storage.ID_Storage);
                CurrentStorage = new Storage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void DeleteTeaType(Storage storage)
        {
            try
            {
                if (storage == null) throw new ArgumentNullException("Cannot add null value");
                ctx.Storage.Remove(storage);
                ctx.SaveChanges();
                Storage.Remove(storage);
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

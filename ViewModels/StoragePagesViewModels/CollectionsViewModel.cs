using Final_Practice.Commands;
using Final_Practice.database;
using Final_Practice.serializers;
using Microsoft.Win32;
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
    public class CollectionsViewModel : INotifyPropertyChanged
    {
        private RelayCommand<ICollection<Collections>> _saveCollectionsCommand = null;
        public RelayCommand<ICollection<Collections>> saveCollectionsCommand => _saveCollectionsCommand ?? (_saveCollectionsCommand = new RelayCommand<ICollection<Collections>>(SaveCollections, (collections) => collections != null && collections.Count > 0));

        private RelayCommand<Collections> _addCollectionCommand = null;
        public RelayCommand<Collections> addCollectionCommand => _addCollectionCommand ?? (_addCollectionCommand = new RelayCommand<Collections>(AddCollection,
            (collection) => !string.IsNullOrEmpty(collection?.Collection_Name) && collection?.CollectionForms != null &&
            Collections.FirstOrDefault(c => c.Collection_Name == collection?.Collection_Name) == null));

        private RelayCommand<Collections> _deleteCollectionCommand = null;
        public RelayCommand<Collections> deleteCollectionCommand => _deleteCollectionCommand ?? (_deleteCollectionCommand = 
            new RelayCommand<Collections>(DeleteCollection, (collection) => collection != null && Collections.FirstOrDefault(c => c.ID_Collection == collection?.ID_Collection) != null));

        private RelayCommand<Collections> _updateCollectionCommand = null;
        public RelayCommand<Collections> updateCollectionCommand => _updateCollectionCommand ?? (_updateCollectionCommand = 
            new RelayCommand<Collections>(UpdateCollection, (collection) => collection != null && Collections.FirstOrDefault(c => c.ID_Collection == collection?.ID_Collection) != null));


        private TeaShopDBContext ctx;

        public readonly Employees CurrentUser = Application.Current.Resources["Current_Account"] as Employees;
        public ObservableCollection<Collections> Collections { get; set; }
        public ObservableCollection<CollectionForms> CollectionForms { get; set; }
        public ObservableCollection<Employees> Employees { get; set; }

        private Collections _CurrentCollection { get; set; } = new Collections();
        public Collections CurrentCollection
        {
            get => _CurrentCollection;
            set
            {
                _CurrentCollection = value;
                OnPropertyChanged();
            }
        }

        public CollectionsViewModel()
        {
            ctx = new TeaShopDBContext();
            Collections = new ObservableCollection<Collections>(ctx.Collections.ToList());
            CollectionForms = new ObservableCollection<CollectionForms>(ctx.CollectionForms.ToList());
            Employees = new ObservableCollection<Employees>(ctx.Employees.ToList());
        }

        public void UpdateImage()
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "Image files(*.bmp,*.jpg)|*.bmp;*.jpg|*.png;|*.tif)";
            if (dlg.ShowDialog() ?? false)
                if (dlg.FileName == string.Empty) return;
            CurrentCollection.Collection_ImageUrl = dlg.FileName;
        }

        public void SaveCollections(ICollection<Collections> collections)
        {
            try
            {CollectionsSerializer.Serialize(collections, Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\Collections.json");
                MessageBox.Show("Successfully saved");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void Loadcollections()
        {
            try
            {
                var dlg = new OpenFileDialog();
                if (dlg.ShowDialog() ?? false)
                    if (dlg.FileName == string.Empty || !dlg.FileName.EndsWith(".json")) return;
                string path = dlg.FileName;
                var collections = CollectionsSerializer.Deserialize(path);
                if (collections == default) throw new ArgumentException("There aren't required data in this file");
                foreach (var collection in collections)
                {
                    var NewCollection = new Collections()
                    {
                        Collection_Name = collection.Collection_Name,
                        Collection_Description = collection.Collection_Description,
                        Collection_Price = collection.Collection_Price,
                        Collection_ImageUrl = collection.Collection_ImageUrl,
                    };

                    var form = ctx.CollectionForms.FirstOrDefault(f => f.Collection_CellAmount == collection.CollectionForm.CollectionForm_CellAmount &&
                    f.Collection_Size == collection.CollectionForm.CollectionForm_Size);

                    if (form == null || ctx.Collections.FirstOrDefault(c => c.Collection_Name == NewCollection.Collection_Name) != null)
                        continue;
                    else {

                        NewCollection.CollectionForm_ID = form.ID_CollectionForm;
                        ctx.Collections.Add(NewCollection);
                        ctx.SaveChanges();
                        foreach (var collectionTea in collection.CollectionTeas)
                        {
                            var teaSort = ctx.TeaSorts.FirstOrDefault(t => t.TeaSort_Name == collectionTea.TeaSort_Name);
                            if (teaSort == null)
                                continue;

                            ctx.CollectionTeas.Add(new database.CollectionTeas()
                            {
                                Collection_ID = NewCollection.ID_Collection,
                                TeaSort_ID = teaSort.ID_TeaSort,
                                CollectionTeas_AmountTeas = collectionTea.CollectionTeas_Count
                            });
                        }
                        ctx.SaveChanges();
                        Collections.Add(NewCollection);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public void ClearInfo()
        {
            CurrentCollection = new Collections();
        }
        public void AddCollection(Collections newCollection)
        {
            try
            {
                if (newCollection == null) throw new ArgumentNullException("Cannot add null value");
                var currentAccount = Application.Current.Resources["CurrentUser"] as Accounts;
                if(currentAccount != null)
                    newCollection.Employees = currentAccount.Employees as Employees??null;
                ctx.Collections.Add(newCollection);
                ctx.SaveChanges();
                Collections.Add(newCollection);
                CurrentCollection = new Collections();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void UpdateCollection(Collections collection)
        {
            try
            {
                if (collection == null) throw new ArgumentNullException("Cannot add null value");
                var updatedCollection = ctx.Collections.FirstOrDefault(c => c.ID_Collection == collection.ID_Collection);
                var arrayUpdatedCollection = Collections.FirstOrDefault(c => c.ID_Collection == collection.ID_Collection);
                if (updatedCollection != null)
                {
                    updatedCollection = collection;
                    ctx.SaveChanges();
                    arrayUpdatedCollection = collection;
                }
                CurrentCollection = new Collections();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void DeleteCollection(Collections collection)
        {
            try
            {
                if (collection == null) throw new ArgumentNullException("Cannot delete null value");
                ctx.Collections.Remove(collection);
                ctx.SaveChanges();
                Collections.Remove(collection);
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

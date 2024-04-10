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
    public class CollectionFormsViewModel : INotifyPropertyChanged
    {

        private RelayCommand<CollectionForms> _addCollectionFormCommand = null;
        public RelayCommand<CollectionForms> addCollectionFormCommand => _addCollectionFormCommand ?? (_addCollectionFormCommand = new RelayCommand<CollectionForms>(AddCollectionForm,
            (collectionForm) => 
            CollectionForms.FirstOrDefault(c => c.Collection_CellAmount == collectionForm?.Collection_CellAmount && c.Collection_Size == collectionForm.Collection_Size) == null));

        private RelayCommand<CollectionForms> _deleteCollectionFormCommand = null;
        public RelayCommand<CollectionForms> deleteCollectionFormCommand => _deleteCollectionFormCommand ?? (_deleteCollectionFormCommand = new RelayCommand<CollectionForms>(DeleteCollectionForm, (collectionForm) => collectionForm != null && CollectionForms.FirstOrDefault(c => c.ID_CollectionForm == collectionForm?.ID_CollectionForm) != null));

        private RelayCommand<CollectionForms> _updateCollectionFormCommand = null;
        public RelayCommand<CollectionForms> updateCollectionFormCommand => _updateCollectionFormCommand ?? (_updateCollectionFormCommand = new RelayCommand<CollectionForms>(UpdateCollectionForm, (collectionForm) => collectionForm != null && CollectionForms.FirstOrDefault(c => c.ID_CollectionForm == collectionForm?.ID_CollectionForm) != null));


        private TeaShopDBContext ctx;

        public ObservableCollection<CollectionForms> CollectionForms { get; set; }

        private CollectionForms _CurrentCollectionForm { get; set; } = new CollectionForms();
        public CollectionForms CurrentCollectionForm
        {
            get => _CurrentCollectionForm;
            set
            {
                _CurrentCollectionForm = value;
                OnPropertyChanged();
            }
        }

        public CollectionFormsViewModel()
        {
            ctx = new TeaShopDBContext();
            CollectionForms = new ObservableCollection<CollectionForms>(ctx.CollectionForms.ToList());
        }


        public void ClearInfo()
        {
            CurrentCollectionForm = new CollectionForms();
        }
        public void AddCollectionForm(CollectionForms newCollectionForm)
        {
            try
            {
                if (newCollectionForm == null) throw new ArgumentNullException("Cannot add null value");
                ctx.CollectionForms.Add(newCollectionForm);
                ctx.SaveChanges();
                CollectionForms.Add(newCollectionForm);
                CurrentCollectionForm = new CollectionForms();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void UpdateCollectionForm(CollectionForms collectionForm)
        {
            try
            {
                if (collectionForm == null) throw new ArgumentNullException("Cannot add null value");
                var updatedCollectionForm = ctx.CollectionForms.FirstOrDefault(c => c.ID_CollectionForm == collectionForm.ID_CollectionForm);
                var arrayUpdatedCollectionForms = CollectionForms.FirstOrDefault(c => c.ID_CollectionForm == collectionForm.ID_CollectionForm);
                if (updatedCollectionForm != null)
                {
                    updatedCollectionForm = collectionForm;
                    ctx.SaveChanges();
                    arrayUpdatedCollectionForms = collectionForm;
                }
                CurrentCollectionForm = new CollectionForms();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void DeleteCollectionForm(CollectionForms collectionForm)
        {
            try
            {
                if (collectionForm == null) throw new ArgumentNullException("Cannot delete null value");
                ctx.CollectionForms.Remove(collectionForm);
                ctx.SaveChanges();
                CollectionForms.Remove(collectionForm);
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

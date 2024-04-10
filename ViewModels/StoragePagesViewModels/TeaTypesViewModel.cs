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
    public class TeaTypesViewModel : INotifyPropertyChanged
    {
        private RelayCommand<TeaTypes> _addTeaTypeCommand = null;
        public RelayCommand<TeaTypes> addTeaTypeCommand => _addTeaTypeCommand ?? (_addTeaTypeCommand = new RelayCommand<TeaTypes>(AddTeaType, (teaType) => !string.IsNullOrEmpty(teaType?.TeaType_Name) && TeaTypes.FirstOrDefault(r => r.TeaType_Name == teaType?.TeaType_Name) == null));

        private RelayCommand<TeaTypes> _deleteTeaTypeCommand = null;
        public RelayCommand<TeaTypes> deleteTeaTypeCommand => _deleteTeaTypeCommand ?? (_deleteTeaTypeCommand = new RelayCommand<TeaTypes>(DeleteTeaType, (teaType) => teaType != null && TeaTypes.FirstOrDefault(r => r.ID_TeaType == teaType?.ID_TeaType) != null));

        private RelayCommand<TeaTypes> _updateTeaTypeCommand = null;
        public RelayCommand<TeaTypes> updateTeaTypeCommand => _updateTeaTypeCommand ?? (_updateTeaTypeCommand = new RelayCommand<TeaTypes>(UpdateTeaType, (teaType) => teaType != null && TeaTypes.FirstOrDefault(r => r.ID_TeaType == teaType?.ID_TeaType) != null));


        private TeaShopDBContext ctx;

        public ObservableCollection<TeaTypes> TeaTypes { get; set; }

        private TeaTypes _CurrentTeaType { get; set; } = new TeaTypes();
        public TeaTypes CurrentTeaType
        {
            get => _CurrentTeaType;
            set
            {
                _CurrentTeaType = value;
                OnPropertyChanged();
            }
        }

        public TeaTypesViewModel()
        {
            ctx = new TeaShopDBContext();
            TeaTypes = new ObservableCollection<TeaTypes>(ctx.TeaTypes.ToList());
        }


        public void ClearInfo()
        {
            CurrentTeaType = new TeaTypes();
        }
        public void AddTeaType(TeaTypes newTeaType)
        {
            try
            {
                if (newTeaType == null) throw new ArgumentNullException("Cannot add null value");
                ctx.TeaTypes.Add(newTeaType);
                ctx.SaveChanges();
                TeaTypes.Add(newTeaType);
                CurrentTeaType = new TeaTypes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void UpdateTeaType(TeaTypes teaType)
        {
            try
            {
                if (teaType == null) throw new ArgumentNullException("Cannot add null value");
                var updatedTeaType = ctx.TeaTypes.FirstOrDefault(r => r.ID_TeaType == teaType.ID_TeaType);
                var arrayUpdatedTeaType = TeaTypes.FirstOrDefault(r => r.ID_TeaType == teaType.ID_TeaType);
                if (updatedTeaType != null)
                {
                    updatedTeaType = teaType;
                    ctx.SaveChanges();
                    arrayUpdatedTeaType = teaType;
                }
                CurrentTeaType = new TeaTypes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void DeleteTeaType(TeaTypes teaType)
        {
            try
            {
                if (teaType == null) throw new ArgumentNullException("Cannot add null value");
                ctx.TeaTypes.Remove(teaType);
                ctx.SaveChanges();
                TeaTypes.Remove(teaType);
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

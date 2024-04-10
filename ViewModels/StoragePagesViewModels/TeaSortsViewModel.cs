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
    public class TeaSortsViewModel : INotifyPropertyChanged
    {

        private RelayCommand<ICollection<TeaSorts>> _saveTeaSortsCommand = null;
        public RelayCommand<ICollection<TeaSorts>> saveTeaSortsCommand => _saveTeaSortsCommand ?? (_saveTeaSortsCommand = new RelayCommand<ICollection<TeaSorts>>(SaveTeaSorts, (teaSorts) => teaSorts != null && teaSorts.Count > 0));

        private RelayCommand<TeaSorts> _addTeaSortCommand = null;
        public RelayCommand<TeaSorts> addTeaSortCommand => _addTeaSortCommand ?? (_addTeaSortCommand = new RelayCommand<TeaSorts>(AddTeaSort, (teaSort) => !string.IsNullOrEmpty(teaSort?.TeaSort_Name) && teaSort.TeaTypes != null && TeaSorts.FirstOrDefault(r => r.TeaSort_Name == teaSort?.TeaSort_Name) == null));

        private RelayCommand<TeaSorts> _deleteTeaSortCommand = null;
        public RelayCommand<TeaSorts> deleteTeaSortCommand => _deleteTeaSortCommand ?? (_deleteTeaSortCommand = new RelayCommand<TeaSorts>(DeleteTeaSort, (teaSort) => teaSort != null && TeaSorts.FirstOrDefault(r => r.ID_TeaSort == teaSort?.ID_TeaSort) != null));

        private RelayCommand<TeaSorts> _updateTeaSortCommand = null;
        public RelayCommand<TeaSorts> updateTeaSortCommand => _updateTeaSortCommand ?? (_updateTeaSortCommand = new RelayCommand<TeaSorts>(UpdateTeaSort, (teaSort) => teaSort != null && TeaSorts.FirstOrDefault(r => r.ID_TeaSort == teaSort?.ID_TeaSort) != null));


        private TeaShopDBContext ctx;

        public ObservableCollection<TeaSorts> TeaSorts { get; set; }
        public ObservableCollection<TeaTypes> TeaTypes { get; set; }
        public ObservableCollection<Producers> Producers { get; set; }


        private TeaSorts _CurrentTeaSort {  get; set; } = new TeaSorts();
        public TeaSorts CurrentTeaSort
        {
            get => _CurrentTeaSort;
            set
            {
                _CurrentTeaSort = value;
                OnPropertyChanged();
            }
        }

        public TeaSortsViewModel() { 
            ctx = new TeaShopDBContext();

            TeaTypes = new ObservableCollection<TeaTypes>(ctx.TeaTypes.ToList());
            Producers = new ObservableCollection<Producers>(ctx.Producers.ToList());
            TeaSorts = new ObservableCollection<TeaSorts>(ctx.TeaSorts.ToList());
        }
        public void SaveTeaSorts(ICollection<TeaSorts> teaSorts)
        {
            try
            {
                TeaSortSerializer.Serialize(teaSorts, Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\TeaSorts.json");
                MessageBox.Show("Successfully saved");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void LoadTeaSorts()
        {
            try
            {
                var dlg = new OpenFileDialog();
                if (dlg.ShowDialog() ?? false)
                    if (dlg.FileName == string.Empty || !dlg.FileName.EndsWith(".json")) return;
                string path = dlg.FileName;
                var teaSorts = TeaSortSerializer.Deserialize(path);

                if (teaSorts == default) throw new ArgumentException("There aren't required data in this file");
                foreach (var teaSort in teaSorts)

                {
                    if (ctx.TeaSorts.FirstOrDefault(a => a.TeaSort_Name == teaSort.TeaSort_Name && a.TeaType_ID == teaSort.TeaType_ID) == null)
                        AddTeaSort(teaSort);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void UpdateImage()
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "Image files(*.bmp,*.jpg)|*.bmp;*.jpg|*.png;|*.tif)";
            if (dlg.ShowDialog() ?? false)
                if (dlg.FileName == string.Empty) return;
            CurrentTeaSort.TeaSort_ImageUrl = dlg.FileName;
        }


        public void ClearInfo()
        {
            CurrentTeaSort = new TeaSorts();
        }
        public void AddTeaSort(TeaSorts newTeaSort)
        {
            try
            {
                if (newTeaSort == null) throw new ArgumentNullException("Cannot add null value");
                ctx.TeaSorts.Add(newTeaSort);
                ctx.SaveChanges();
                TeaSorts.Add(newTeaSort);
                CurrentTeaSort = new TeaSorts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void UpdateTeaSort(TeaSorts teaSort)
        {
            try
            {
                if (teaSort == null) throw new ArgumentNullException("Cannot add null value");
                var updatedTeaSort = ctx.TeaSorts.FirstOrDefault(r => r.ID_TeaSort == teaSort.ID_TeaSort);
                var arrayUpdatedTeaSort = TeaSorts.FirstOrDefault(r => r.ID_TeaSort == teaSort.ID_TeaSort);
                if (updatedTeaSort != null)
                {
                    updatedTeaSort = teaSort;
                    ctx.SaveChanges();
                    arrayUpdatedTeaSort = teaSort;
                }
                CurrentTeaSort = new TeaSorts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void DeleteTeaSort(TeaSorts teaSort)
        {
            try
            {
                if (teaSort == null) throw new ArgumentNullException("Cannot add null value");
                ctx.TeaSorts.Remove(teaSort);
                ctx.SaveChanges();
                TeaSorts.Remove(teaSort);
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

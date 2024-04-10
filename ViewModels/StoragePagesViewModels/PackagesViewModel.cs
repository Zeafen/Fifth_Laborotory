using Final_Practice.Commands;
using Final_Practice.database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Packaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Final_Practice.ViewModels.StoragePagesViewModels
{
    public class PackagesViewModel : INotifyPropertyChanged
    {

        private RelayCommand<Packages> _addPackageCommand = null;
        public RelayCommand<Packages> addPackageCommand => _addPackageCommand ?? (_addPackageCommand = new RelayCommand<Packages>(AddPackage,
            (package) => !string.IsNullOrEmpty(package?.Package_Name) &&
            Packages.FirstOrDefault(p => p.Package_Name == package?.Package_Name) == null && Packages.FirstOrDefault(p => p.Package_AmountTea == package?.Package_AmountTea) == null));

        private RelayCommand<Packages> _deletePackageCommand = null;
        public RelayCommand<Packages> deletePackageCommand => _deletePackageCommand ?? (_deletePackageCommand = new RelayCommand<Packages>(DeletePackage, (teaType) => teaType != null && Packages.FirstOrDefault(r => r.ID_Package == teaType?.ID_Package) != null));

        private RelayCommand<Packages> _updatePackageCommand = null;
        public RelayCommand<Packages> updatePackageCommand => _updatePackageCommand ?? (_updatePackageCommand = new RelayCommand<Packages>(UpdatePackage, (teaType) => teaType != null && Packages.FirstOrDefault(r => r.ID_Package == teaType?.ID_Package) != null));


        private TeaShopDBContext ctx;

        public ObservableCollection<Packages> Packages { get; set; }

        private Packages _CurrentPackage { get; set; } = new Packages();
        public Packages CurrentPackage
        {
            get => _CurrentPackage;
            set
            {
                _CurrentPackage = value;
                OnPropertyChanged();
            }
        }

        public PackagesViewModel()
        {
            ctx = new TeaShopDBContext();
            Packages = new ObservableCollection<Packages>(ctx.Packages.ToList());
        }


        public void ClearInfo()
        {
            CurrentPackage = new Packages();
        }
        public void AddPackage(Packages newPackage)
        {
            try
            {
                if (newPackage == null) throw new ArgumentNullException("Cannot add null value");
                ctx.Packages.Add(newPackage);
                ctx.SaveChanges();
                Packages.Add(newPackage);
                CurrentPackage = new Packages();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void UpdatePackage(Packages package)
        {
            try
            {
                if (package == null) throw new ArgumentNullException("Cannot add null value");
                var updatedPackage = ctx.Packages.FirstOrDefault(p => p.ID_Package == package.ID_Package);
                var arrayUpdatedPackage = Packages.FirstOrDefault(p => p.ID_Package == package.ID_Package);
                if (updatedPackage != null)
                {
                    updatedPackage = package;
                    ctx.SaveChanges();
                    arrayUpdatedPackage = package;
                }
                CurrentPackage = new Packages();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void DeletePackage(Packages package)
        {
            try
            {
                if (package == null) throw new ArgumentNullException("Cannot delete null value");
                ctx.Packages.Remove(package);
                ctx.SaveChanges();
                Packages.Remove(package);
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

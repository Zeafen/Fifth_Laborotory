using Final_Practice.Commands;
using Final_Practice.database;
using Final_Practice.serializers;
using MaterialDesignThemes.Wpf.Converters;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Final_Practice.ViewModels.AdminPagesViewModels
{
    public class RolesViewModel : INotifyPropertyChanged
    {
        private RelayCommand<Roles> _addRoleCommand = null;
        public RelayCommand<Roles> addRoleCommand => _addRoleCommand ?? (_addRoleCommand = new RelayCommand<Roles>(AddRole, (role) => !string.IsNullOrEmpty(role?.Role_Name) && Roles.FirstOrDefault(r => r.Role_Name == role?.Role_Name) == null));

        private RelayCommand<Roles> _deleteRoleCommand = null;
        public RelayCommand<Roles> deleteRoleCommand => _deleteRoleCommand ?? (_deleteRoleCommand = new RelayCommand<Roles>(DeleteRole, (role) => role != null && Roles.FirstOrDefault(r => r.ID_Role == role?.ID_Role) != null));

        private RelayCommand<Roles> _updateRoleCommand = null;
        public RelayCommand<Roles> updateRoleCommand => _updateRoleCommand ?? (_updateRoleCommand = new RelayCommand<Roles>(UpdateRole, (role) => role != null && Roles.FirstOrDefault(r => r.ID_Role == role?.ID_Role) != null));


        private RelayCommand<ICollection<Roles>> _saveRolesCommand = null;
        public RelayCommand<ICollection<Roles>> saveRolesCommand => _saveRolesCommand ?? (_saveRolesCommand = new RelayCommand<ICollection<Roles>>(SaveRoles, (roles) => roles != null && roles.Count > 0));

        private TeaShopDBContext ctx;
        private Roles _CurrentRole { get; set; }
        public Roles CurrentRole
        {
            get => _CurrentRole;
            set { 
                _CurrentRole = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Roles> Roles { get; set; }

        public RolesViewModel()
        {
            ctx = new TeaShopDBContext();
            Roles = new ObservableCollection<Roles>(ctx.Roles.ToList());
        }

        public void ClearInfo()
        {
            CurrentRole = new Roles();
        }
        public void AddRole(Roles newRole)
        {
            try
            {
                if (newRole == null) throw new ArgumentNullException("Cannot add null value");
                ctx.Roles.Add(newRole);
                ctx.SaveChanges();
                Roles.Add(newRole);
                CurrentRole = new Roles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void UpdateRole(Roles role)
        {
            try
            {
                if (role == null) throw new ArgumentNullException("Cannot add null value");
                var updatedRole = ctx.Roles.FirstOrDefault(r => r.ID_Role == role.ID_Role);
                var arrayUpdatedRole = Roles.FirstOrDefault(r => r.ID_Role == role.ID_Role);
                if (updatedRole != null)
                {
                    updatedRole = role;
                    ctx.SaveChanges();
                    arrayUpdatedRole = role;
                }
                CurrentRole = new Roles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void DeleteRole(Roles role)
        {
            try
            {
                if (role == null) throw new ArgumentNullException("Cannot add null value");
                foreach (var item in ctx.Accounts)
                {
                    if (item.Role_ID == role.ID_Role)
                        ctx.Accounts.FirstOrDefault(a => a.ID_Account == item.ID_Account).Role_ID = null;
                }
                ctx.SaveChanges ();
                ctx.Roles.Remove(role);
                ctx.SaveChanges();
                Roles.Remove(role);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SaveRoles(ICollection<Roles> roles)
        {
            try
            {
                RolesSerializer.Serialize(roles, Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\Roles.json");
                MessageBox.Show("Successfully saved");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void LoadRoles()
        {
            try
            {
                var dlg = new OpenFileDialog();
                if (dlg.ShowDialog() ?? false)
                    if (dlg.FileName == string.Empty || !dlg.FileName.EndsWith(".json")) return;
                string path = dlg.FileName;
                var roles = RolesSerializer.Deserialize(path);
                if (roles == default) throw new ArgumentException("There aren't required data in this file");
                foreach(var role in roles)
                {
                    if(ctx.Roles.FirstOrDefault(r => r.Role_Name == role.Role_Name) == null)
                        AddRole(role);
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

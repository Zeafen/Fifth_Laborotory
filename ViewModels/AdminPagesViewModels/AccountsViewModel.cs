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
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Final_Practice.ViewModels.AdminPagesViewModels
{
    public class AccountsViewModel : INotifyPropertyChanged
    {
        private RelayCommand<ICollection<Accounts>> _saveAccountsCommand = null;
        public RelayCommand<ICollection<Accounts>> saveAccountsCommand => _saveAccountsCommand ?? (_saveAccountsCommand = new RelayCommand<ICollection<Accounts>>(SaveAccounts, (accounts) => accounts != null && accounts.Count > 0));

        private RelayCommand<Accounts> _addAccountCommand = null;
        public RelayCommand<Accounts> addAccountCommand => _addAccountCommand ?? (_addAccountCommand = new RelayCommand<Accounts>(AddAccount, (account) => !string.IsNullOrEmpty(account?.Account_Login) && Accounts.FirstOrDefault(a => a.Account_Login == account?.Account_Login) == null));

        private RelayCommand<Accounts> _deleteAccountCommand = null;
        public RelayCommand<Accounts> deleteAccountCommand => _deleteAccountCommand ?? (_deleteAccountCommand = new RelayCommand<Accounts>(DeleteAccount, (account) => account != null && Accounts.FirstOrDefault(a => a.ID_Account == account?.ID_Account) != null));

        private RelayCommand<Accounts> _updateAccountCommand = null;
        public RelayCommand<Accounts> updateAccountCommand => _updateAccountCommand ?? (_updateAccountCommand = new RelayCommand<Accounts>(UpdateAccount, (account) => account != null && Accounts.FirstOrDefault(a => a.ID_Account == account?.ID_Account) != null));



        private Accounts _CurrentAccount {  get; set; }
        public Accounts CurrentAccount
        {
            get => _CurrentAccount;
            set
            {
                _CurrentAccount = value;
                OnPropertyChanged();
            }
        }

        private TeaShopDBContext ctx;
        public ObservableCollection<Accounts> Accounts { get; set; }
        public ObservableCollection<Roles> Roles { get; set; }
        public AccountsViewModel()
        {
            ctx = new TeaShopDBContext();
            Accounts = new ObservableCollection<Accounts>(ctx.Accounts.ToList());
            Roles = new ObservableCollection<Roles>(ctx.Roles.ToList());
        }

        public void SaveAccounts(ICollection<Accounts> accounts)
        {
            try
            {
                AccountsSerializer.Serialize(accounts, Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\Accounts.json");
                MessageBox.Show("Successfully saved");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void LoadAccounts()
        {
            try
            {
                var dlg = new OpenFileDialog();
                if (dlg.ShowDialog() ?? false)
                    if (dlg.FileName == string.Empty || !dlg.FileName.EndsWith(".json")) return;
                string path = dlg.FileName;
                var accounts = AccountsSerializer.Deserialize(path);

                if (accounts == default) throw new ArgumentException("There aren't required data in this file");
                foreach (var account in accounts)
                {
                    if (ctx.Accounts.FirstOrDefault(a => a.Account_Login == account.Account_Login) == null)
                    {
                        ctx.Accounts.Add(account);
                        Accounts.Add(account);
                    }
                }
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ClearInfo()
        {
            CurrentAccount = new Accounts();
        }
        public void AddAccount(Accounts newAccount)
        {
            try
            {
                if (newAccount == null) throw new ArgumentNullException("Cannot add null value");
                newAccount.Account_Password = PasswordHashier.ByteArrayToString(PasswordHashier.PasswordToHash(newAccount.Account_Password));
                ctx.Accounts.Add(newAccount);
                ctx.SaveChanges();
                Accounts.Add(newAccount);
                CurrentAccount = new Accounts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void UpdateAccount(Accounts account)
        {
            try
            {
                if (account == null) throw new ArgumentNullException("Cannot add null value");
                account.Account_Password = PasswordHashier.ByteArrayToString(PasswordHashier.PasswordToHash(account.Account_Password));
                var updatedAccount = ctx.Accounts.FirstOrDefault(r => r.ID_Account == account.ID_Account);
                var arrayUpdatedAccount = Accounts.FirstOrDefault(r => r.ID_Account == account.ID_Account);
                if (updatedAccount != null)
                {
                    updatedAccount = account;
                    ctx.SaveChanges();
                    arrayUpdatedAccount = account;
                }
                CurrentAccount = new Accounts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void DeleteAccount(Accounts account)
        {
            try
            {
                if (account == null) throw new ArgumentNullException("Cannot add null value");
                foreach (var item in ctx.Employees)
                {
                    if (item.Account_ID == account.ID_Account)
                        ctx.Employees.FirstOrDefault(a => a.ID_Employee == item.ID_Employee).Account_ID = null;
                }
                ctx.Accounts.Remove(account);
                ctx.SaveChanges();
                Accounts.Remove(account);
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

using Final_Practice.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Final_Practice.ViewModels
{
    public class LoginViewModel
    {
        private TeaShopDBContext ctx;
        public string account_Login { get; set; } = string.Empty;
        public string account_Password { get; set; } = string.Empty;

        public Roles LoggedRole { get; private set; }

        public LoginViewModel()
        {
            ctx = new TeaShopDBContext();
        }
        private void SaveAccount(Accounts acc)
        {
            if (!Application.Current.Resources.Contains("Current_Account"))
                Application.Current.Resources.Add("Current_Account", acc.Employees.First());
            else Application.Current.Resources["Current_Account"] = acc.Employees.First();
        }

        public bool LogIn()
        {
            if(string.IsNullOrEmpty(account_Login)|| string.IsNullOrEmpty(account_Password))
                return false;
            var account = ctx.Accounts.FirstOrDefault(ac => ac.Account_Login == account_Login);
            if(account != null)
                if (PasswordHashier.ByteArrayToString(PasswordHashier.PasswordToHash(account_Password)).Equals(account.Account_Password))
                {
                    SaveAccount(account);
                    LoggedRole = account.Roles;
                    return true;
                }
            return false;
        }
    }
}

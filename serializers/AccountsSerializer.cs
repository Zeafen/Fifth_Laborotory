using Final_Practice.database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Practice.serializers
{
    public class AccountsSerializer
    {
        public static void Serialize(ICollection<Accounts> record, string filePath)
        {
            if (File.Exists(filePath))
                using (var resource = File.Open(filePath, FileMode.Open))
                {
                    resource.SetLength(0);
                }
            else
                File.Create(filePath).Close();

            List<SerializedAccount> roles = new List<SerializedAccount>();
            foreach (var item in record)
                roles.Add(new SerializedAccount() { Account_Login = item.Account_Login, Account_Password = item.Account_Password, Account_Role_Name = item.Roles.Role_Name});

            using (StreamWriter writer = new StreamWriter(File.Open(filePath, FileMode.Open)))
            {
                writer.WriteLine(JsonConvert.SerializeObject(roles));
            }
        }

        public static ICollection<Accounts> Deserialize(string filePath)
        {
            var ctx = new TeaShopDBContext();

            List<Accounts> roles = new List<Accounts>();
            FileInfo fie = new FileInfo(filePath);
            if (new FileInfo(filePath).Length == 0)
                return default;
            using (StreamReader Jfile = new StreamReader(File.Open(filePath, FileMode.OpenOrCreate)))
            {
                var json = Convert.ToString(Jfile.ReadToEnd());
                var records = JsonConvert.DeserializeObject<ICollection<SerializedAccount>>(json);
                foreach (var item in records)
                    roles.Add(new Accounts() { Account_Login = item.Account_Login, Account_Password = item.Account_Password, Role_ID = ctx?.Roles?.FirstOrDefault(r => r.Role_Name == item.Account_Role_Name).ID_Role});
            }
            return roles;
        }
    }

    internal class SerializedAccount
    {
        public string Account_Login { get; set; }
        public string Account_Password { get; set; }
        public string Account_Role_Name {  get; set; }
    }
}

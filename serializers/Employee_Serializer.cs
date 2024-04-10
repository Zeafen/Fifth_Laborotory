using Final_Practice.database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Practice.serializers
{
    static class Employee_Serializer
    {
        public static void Serialize(ICollection<Employees> record, string filePath)
        {
            if (File.Exists(filePath))
                using (var resource = File.Open(filePath, FileMode.Open))
                {
                    resource.SetLength(0);
                }
            else
                File.Create(filePath).Close();

            List<SerializedEmployee> employees = new List<SerializedEmployee>();
            foreach (var item in record)
                employees.Add(new SerializedEmployee()
                {
                    Employee_Name = item.Employee_Name,
                    Employee_Surname = item.Employee_Surname,
                    Employee_Patronymic = item.Employee_Patronymic,
                    Employee_Age = item.Employee_Age,
                    Account_Login = item?.Accounts?.Account_Login ?? null
                });

            using (StreamWriter writer = new StreamWriter(File.Open(filePath, FileMode.Open)))
            {
                writer.WriteLine(JsonConvert.SerializeObject(employees));
            }
        }

        public static ICollection<Employees> Deserialize(string filePath)
        {
            var ctx = new TeaShopDBContext();

            List<Employees> employees = new List<Employees>();

            FileInfo fie = new FileInfo(filePath);
            if (new FileInfo(filePath).Length == 0)
                return default;

            using (StreamReader Jfile = new StreamReader(File.Open(filePath, FileMode.OpenOrCreate)))
            {
                var json = Convert.ToString(Jfile.ReadToEnd());
                var records = JsonConvert.DeserializeObject<ICollection<SerializedEmployee>>(json);
                foreach (var item in records)
                    employees.Add(new Employees()
                    {
                        Employee_Name = item.Employee_Name,
                        Employee_Surname = item.Employee_Surname,
                        Employee_Patronymic = item.Employee_Patronymic,
                        Employee_Age = item.Employee_Age,
                        Account_ID = ctx.Accounts.FirstOrDefault(a => a.Account_Login == item.Account_Login).ID_Account,
                    });
            }
            return employees;
        }
    }

    internal class SerializedEmployee
    {
        public string Employee_Name { get; set; }
        public string Employee_Surname { get; set; }
        public string Employee_Patronymic { get; set; }
        public int Employee_Age { get; set; }
        public string Account_Login { get; set; }
    }
}

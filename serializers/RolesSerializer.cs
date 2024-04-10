using Final_Practice.database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Final_Practice.serializers
{
    public static class RolesSerializer
    {
        public static void Serialize(ICollection<Roles> record, string filePath)
        {
            if (File.Exists(filePath))
                using (var resource = File.Open(filePath, FileMode.Open))
                {
                    resource.SetLength(0);
                }
            else
                File.Create(filePath).Close();

            List<SerializedRole> roles = new List<SerializedRole>();
            foreach (var item in record)
                roles.Add(new SerializedRole() { Role_Name = item.Role_Name, Role_Description = item.Role_Description});

            using (StreamWriter writer = new StreamWriter(File.Open(filePath, FileMode.Open)))
            {
                writer.WriteLine(JsonConvert.SerializeObject(roles));
            }
        }

        public static ICollection<Roles> Deserialize(string filePath)
        {
            List<Roles> roles = new List<Roles>();
            FileInfo fie = new FileInfo(filePath);
            if (new FileInfo(filePath).Length == 0)
                return default;
            using (StreamReader Jfile = new StreamReader(File.Open(filePath, FileMode.OpenOrCreate)))
            {
                var json = Convert.ToString(Jfile.ReadToEnd());
                var records = JsonConvert.DeserializeObject<ICollection<SerializedRole>>(json);
                foreach (var item in records)
                    roles.Add(new Roles() { Role_Name = item.Role_Name, Role_Description = item.Role_Description });
            }
            return roles;
        }
    }

        internal class SerializedRole
        {
            public string Role_Name {  get; set; }
            public string Role_Description {  get; set; }
        }
}

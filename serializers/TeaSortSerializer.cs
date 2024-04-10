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
    static class TeaSortSerializer
    {
        public static void Serialize(ICollection<TeaSorts> record, string filePath)
        {
            if (File.Exists(filePath))
                using (var resource = File.Open(filePath, FileMode.Open))
                {
                    resource.SetLength(0);
                }
            else
                File.Create(filePath).Close();

            List<SerializedTeaSort> teaSorts = new List<SerializedTeaSort>();
            foreach (var item in record)
                teaSorts.Add(new SerializedTeaSort() {
                    TeaSort_Name = item.TeaSort_Name,
                    TeaSort_Description = item.TeaSort_Description,
                    TeaSort_CupPrice = item.TeaSort_CupPrice,
                    TeaSort_FermentatinPeriod = item.TeaSort_FermentatinPeriod,
                    TeaSort_ImageUrl = item.TeaSort_ImageUrl,
                    TeaSort_PackPrice = item.TeaSort_PackPrice,
                    TeaType_Name = item.TeaTypes.TeaType_Name
                });

            using (StreamWriter writer = new StreamWriter(File.Open(filePath, FileMode.Open)))
            {
                writer.WriteLine(JsonConvert.SerializeObject(teaSorts));
            }
        }

        public static ICollection<TeaSorts> Deserialize(string filePath)
        {
            var ctx = new TeaShopDBContext();

            List<TeaSorts> roles = new List<TeaSorts>();
            FileInfo fie = new FileInfo(filePath);
            if (new FileInfo(filePath).Length == 0)
                return default;
            using (StreamReader Jfile = new StreamReader(File.Open(filePath, FileMode.OpenOrCreate)))
            {
                var json = Convert.ToString(Jfile.ReadToEnd());
                var records = JsonConvert.DeserializeObject<ICollection<SerializedTeaSort>>(json);
                foreach (var item in records)
                    roles.Add(new TeaSorts()
                    {
                        TeaSort_Name = item.TeaSort_Name,
                        TeaSort_Description = item.TeaSort_Description,
                        TeaSort_CupPrice = item.TeaSort_CupPrice,
                        TeaSort_FermentatinPeriod = item.TeaSort_FermentatinPeriod,
                        TeaSort_ImageUrl = item.TeaSort_ImageUrl,
                        TeaSort_PackPrice = item.TeaSort_PackPrice,
                        TeaType_ID = ctx.TeaTypes.FirstOrDefault(ts => ts.TeaType_Name == item.TeaType_Name).ID_TeaType
                    });
            }
            return roles;
        }
    }
    internal class SerializedTeaSort
    {
        public string TeaSort_Name { get; set; }
        public string TeaSort_Description { get; set; }
        public decimal TeaSort_FermentatinPeriod { get; set; }
        public decimal TeaSort_PackPrice { get; set; }
        public decimal TeaSort_CupPrice { get; set; }
        public string TeaSort_ImageUrl { get; set; }
        public string TeaType_Name { get; set; }
    }
}

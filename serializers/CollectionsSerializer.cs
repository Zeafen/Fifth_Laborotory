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
    static class CollectionsSerializer
    {
        public static void Serialize(ICollection<Collections> record, string filePath)
        {
            if (File.Exists(filePath))
                using (var resource = File.Open(filePath, FileMode.Open))
                {
                    resource.SetLength(0);
                }
            else
                File.Create(filePath).Close();

            List<SerializedCollection> collections = new List<SerializedCollection>();
            foreach (var item in record)
            {

                var SerCollection = new SerializedCollection()
                {
                    Collection_Name = item.Collection_Name,
                    Collection_Description = item.Collection_Description,
                    Collection_Price = item.Collection_Price,
                    Collection_ImageUrl = item.Collection_ImageUrl,
                    CollectionTeas = new List<SerializedCollectionTeas>(),
                    CollectionForm = new SerializedCollectionForm()
                    {
                        CollectionForm_CellAmount = item.CollectionForms.Collection_CellAmount,
                        CollectionForm_Size = item.CollectionForms.Collection_Size,
                    }
                };
                foreach (var collectionTea in item.CollectionTeas)
                    SerCollection.CollectionTeas.Add(new SerializedCollectionTeas()
                    {
                        Collection_Name = SerCollection.Collection_Name,
                        TeaSort_Name = collectionTea.TeaSorts.TeaSort_Name,
                        CollectionTeas_Count = collectionTea.CollectionTeas_AmountTeas
                    });
                collections.Add(SerCollection);
            }

            using (StreamWriter writer = new StreamWriter(File.Open(filePath, FileMode.Open)))
            {
                writer.WriteLine(JsonConvert.SerializeObject(collections));
            }
        }

        public static ICollection<SerializedCollection> Deserialize(string filePath)
        {
            List<SerializedCollection> records;
            FileInfo fie = new FileInfo(filePath);
            if (new FileInfo(filePath).Length == 0)
                return default;
            using (StreamReader Jfile = new StreamReader(File.Open(filePath, FileMode.OpenOrCreate)))
            {
                var json = Convert.ToString(Jfile.ReadToEnd());
                records = JsonConvert.DeserializeObject<ICollection<SerializedCollection>>(json).ToList();
            }
            return records;
        }
    }
    public class SerializedCollection
    {
        public string Collection_Name { get; set; }
        public string Collection_Description { get; set; }
        public decimal Collection_Price { get; set; }
        public string Collection_ImageUrl { get; set; }
        public SerializedCollectionForm CollectionForm { get; set; }
        public ICollection<SerializedCollectionTeas> CollectionTeas { get; set; }
    }

    public class SerializedCollectionTeas
    {
        public string Collection_Name { get; set; }
        public string TeaSort_Name { get; set; }
        public int CollectionTeas_Count { get; set; }
    }
    public class SerializedCollectionForm
    {
        public int CollectionForm_CellAmount { get; set; }
        public int CollectionForm_Size { get; set; }
    }
}

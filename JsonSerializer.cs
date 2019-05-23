using ShowRoomCars.Plugins;
using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Windows;

namespace ShowRoomCars
{
    public class JsonSerializer
    {
        private object _orderToSerialize;
        private string _path;

        public JsonSerializer(object order, string path)
        {
            _orderToSerialize = order;
            _path = path;
        }

        public JsonSerializer(string path)
        {
            _path = path;
        }

        public void JsonSerializeObject(int cypher)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Order));
            using (FileStream fs = new FileStream(_path, FileMode.OpenOrCreate))
            {
                if (cypher == 2)
                {
                    jsonFormatter.WriteObject(fs, _orderToSerialize);
                }
                else if (cypher == 0)
                {
                    DESCrypt des = new DESCrypt();
                    des.Serialize(fs, _orderToSerialize, _path, null, null, jsonFormatter);
                }
                else if (cypher == 1)
                {
                    AESCrypt aes = new AESCrypt();
                    aes.Serialize(fs, _orderToSerialize, _path, null, null, jsonFormatter);
                }
                MessageBox.Show("Object successfully serialized.");
            }

        }

        public object JsonDeserializeObject(int mode)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Order));
            using (FileStream fs = new FileStream(_path, FileMode.Open, FileAccess.Read))
            {
                if (mode == 2)
                {
                    object ord = jsonFormatter.ReadObject(fs);
                    MessageBox.Show("Object successfully deserialized");
                    return ord;
                }
                else if (mode == 0)
                {
                    DESCrypt des = new DESCrypt();
                    object ord = des.Deserialize(fs, _path, null, null, jsonFormatter);
                    MessageBox.Show("Object successfully deserialized");
                    return ord;
                }
                else if (mode == 1)
                {
                    AESCrypt aes = new AESCrypt();
                    object ord = aes.Deserialize(fs, _path, null, null, jsonFormatter);
                    MessageBox.Show("Object successfully deserialized");
                    return ord;
                }
                return null;
            }
        }


    }
}

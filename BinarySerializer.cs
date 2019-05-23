using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Windows;
using ShowRoomCars.Plugins;

namespace ShowRoomCars
{
    public class BinarySerializer
    {
        private object ObjectToSerialize;
        private string Path;

        public BinarySerializer(object orderToSerialize, string path)
        {
            this.ObjectToSerialize = orderToSerialize;
            Path = path;
        }

        public BinarySerializer(string path)
        {
            Path = path;
        }

        public void BinarySerializeObject(int cypherAlgo)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(Path, FileMode.OpenOrCreate))
            {
                if (cypherAlgo == 2)
                {
                    formatter.Serialize(fs, ObjectToSerialize);
                }
                else
                {
                    if (cypherAlgo == 0)
                    {
                        DESCrypt des = new DESCrypt();
                        des.Serialize(fs, ObjectToSerialize, Path, null, formatter, null);
                    }
                    else if (cypherAlgo == 1)
                    {
                        AESCrypt aes = new AESCrypt();
                        aes.Serialize(fs, ObjectToSerialize, Path, null, formatter, null);
                    }
                }
                MessageBox.Show("Object successfully serialized");
            }
        }

        public object BinaryDeserializeObject(int mode)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(Path, FileMode.Open, FileAccess.Read))
            {
                if (mode == 2)
                {
                    object obj = formatter.Deserialize(fs);
                    MessageBox.Show("Object successfully deserialized");
                    return obj;
                }
                else if (mode == 0)
                {
                    DESCrypt des = new DESCrypt();
                    object ord = des.Deserialize(fs, Path, null, formatter, null);
                    MessageBox.Show("Object successfully deserialized");
                    return ord;
                    
                }
                else if (mode == 1)
                {
                    AESCrypt aes = new AESCrypt();
                    object ord = aes.Deserialize(fs, Path, null, formatter, null);
                    MessageBox.Show("Object successfully deserialized");
                    return ord;
                }
                return null;
            }
        }
    }
}

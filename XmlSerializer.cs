using System.IO;
using System.Security.Cryptography;
using System.Windows;
using System.Xml.Serialization;
using ShowRoomCars.Plugins;

namespace ShowRoomCars
{
    public class XmlSerialize
    {
        private object OrderToSerialize;
        private string Path;

        public XmlSerialize(object orderToSerialize, string path)
        {
            this.OrderToSerialize = orderToSerialize;
            this.Path = path;
        }

        public XmlSerialize(string path)
        {
            this.Path = path;
        }


        public void SerializeXmlObject(int cyher)
        {
            XmlSerializer sl = new XmlSerializer(typeof(Order));
            using (FileStream fs = new FileStream(Path, FileMode.OpenOrCreate))
            {
                if (cyher == 2)
                {
                    sl.Serialize(fs, OrderToSerialize);
                }
                else if (cyher == 0)
                {
                    DESCrypt des = new DESCrypt();
                    des.Serialize(fs, OrderToSerialize, Path, sl, null, null);
                }
                else if (cyher == 1)
                {
                    AESCrypt aes = new AESCrypt();
                    aes.Serialize(fs, OrderToSerialize, Path, sl, null, null);
                }
                MessageBox.Show("Object successfully serialized");
            }
        }

        public object DeserializeXmlObject(int cyphermode)
        {
            XmlSerializer sl = new XmlSerializer(typeof(Order));
            using (FileStream fs = new FileStream(Path, FileMode.OpenOrCreate))
            {
                if (cyphermode == 2)
                {
                    object ord = sl.Deserialize(fs);
                    MessageBox.Show("Object successfully deserialized");
                    return ord;
                }
                else if (cyphermode == 0)
                {
                    DESCrypt des = new DESCrypt();
                    object ord = des.Deserialize(fs, Path, sl, null);
                    MessageBox.Show("Object successfully deserialized");
                    return ord;

                }
                else if (cyphermode == 1)
                {
                    AESCrypt aes = new AESCrypt();
                    object ord = aes.Deserialize(fs, Path, sl, null);
                    MessageBox.Show("Object successfully deserialized");
                    return ord;
                }
                return null;
            }
        }
        
    }
}

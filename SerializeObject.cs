using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowRoomCars
{
    public class SerializeObject
    {
        private string extension;
        private string fullFilePath;

        public SerializeObject(string ext, string fullFilePath)
        {
            extension = ext;
            this.fullFilePath = fullFilePath;
        }

        public void ExecuteSerialization(object orderToSerialize, int cypher = 2)
        {
            if (extension.Equals(".xml"))
            {
                XmlSerialize xml = new XmlSerialize(orderToSerialize, fullFilePath);
                xml.SerializeXmlObject(cypher);
            }
            else if (extension.Equals(".bin"))
            {
                BinarySerializer bin = new BinarySerializer(orderToSerialize, fullFilePath);
                bin.BinarySerializeObject(cypher);
            }
            else if (extension.Equals(".json"))
            {
                JsonSerializer json = new JsonSerializer(orderToSerialize, fullFilePath);
                json.JsonSerializeObject(cypher);
            }
        }

        public object ExecuteDeserialization(int cypherMode = 2)
        {
            if (extension.Equals(".xml"))
            {
                XmlSerialize xml = new XmlSerialize(fullFilePath);
                return xml.DeserializeXmlObject(cypherMode);             
            }
            else if (extension.Equals(".bin"))
            {
                BinarySerializer bin = new BinarySerializer(fullFilePath);
                return bin.BinaryDeserializeObject(cypherMode);
            }
            else if (extension.Equals(".json"))
            {
                JsonSerializer json = new JsonSerializer(fullFilePath);
                return json.JsonDeserializeObject(cypherMode);
            }
            return null;
        }

    }
}
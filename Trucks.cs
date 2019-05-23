using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowRoomCars
{
    [Serializable]
    public class Trucks : AllCars
    {
        public int LoadCapacity { get; set; }
        
        public Trucks(string brand, string year, int load):base()
        {
            this.Brand = brand;
            this.Year = year;
            this.LoadCapacity = load;
        }

        public Trucks()
        {

        }       

    }
}

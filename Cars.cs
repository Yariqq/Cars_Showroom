using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowRoomCars
{
    [Serializable]
    public class Cars : AllCars
    {
        public string Color { get; set; }

        public int Power { get; set; }

        public int OrderNum { get; set; }

        public Cars(int OrderNum, string brand, string year, string color, int power):base()
        {
            this.OrderNum = OrderNum;
            Brand = brand;
            Year = year;
            Color = color;
            Power = power;
        }
        
        public Cars()
        {

        }

    }
}

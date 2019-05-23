using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowRoomCars
{
    [Serializable]
    public class Order
    {
        public int NumOrder { get; set; }

        public DateTime Date { get; set; }

        public string PaymentType { get; set; }

        private List<Cars> car;
        private List<Trucks> truck;

        public Order(int numOrder, string paymentType, DateTime timeOrder, List<Cars> cars = null, List<Trucks> trucks = null)
        {
            this.NumOrder = numOrder;
            this.Date = timeOrder;
            this.PaymentType = paymentType;
            if (cars != null)
            {
                car = new List<Cars>(cars);
            }
            if (trucks != null)
            {
                truck = new List<Trucks>(trucks);
            }
        } 

        public Order()
        {

        }

        public List<Cars> returnCars { get { return car; } set { car = value; } }

        public List<Trucks> returnTrucks { get { return truck; } set { truck = value; } }

    }
}

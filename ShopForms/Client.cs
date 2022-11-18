using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopForms
{
    public class Client
    {
        public int Id;
        public string Name;
        public DateTime RegistrationDate;
        public string PickupPoint;

        public Client() { }

        public Client(int Id, string Name, DateTime RegistrationDate, string PickupPoint)
        {
            this.Id = Id;
            this.Name = Name;
            this.RegistrationDate = RegistrationDate;
            this.PickupPoint = PickupPoint;
        }
    }
}

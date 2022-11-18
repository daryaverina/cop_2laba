using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopContracts.ViewModels
{
    public class ClientOutput
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PickupPoint { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Order1 { get; set; }
        public string Order2 { get; set; }
        public string Order3 { get; set; }
        public string Order4 { get; set; }
    }
}

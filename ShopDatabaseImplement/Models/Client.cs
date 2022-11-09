using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDatabaseImplement.Models
{
    public class Client
    {
        [Required]
        public string Name { get; set; }
        public int Id { get; set; }

        [Required]
        public string PickupPoint { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string Order1 { get; set; }
        public string Order2 { get; set; }
        public string Order3 { get; set; }
        public string Order4 { get; set; }
    }
}

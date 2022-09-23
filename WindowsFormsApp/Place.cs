using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp
{
    internal class Place
    {
        public String country;
        public String city;
        public String district;

        public Place() {}

        public Place(String country, String city, String district)
        {
            this.country = country;
            this.city = city;
            this.district = district;
        }
    }
}

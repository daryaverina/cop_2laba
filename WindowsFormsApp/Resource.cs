using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp
{
    public class Resource
    {
        public string ResName;
        public int year_2003;
        public int year_2004;
        public int year_2005;


        public Resource() { }

        public Resource(string ResName, int year_2003, int year_2004, int year_2005)
        {
            this.ResName = ResName;
            this.year_2003 = year_2003;
            this.year_2004 = year_2004;
            this.year_2005 = year_2005;
        }
    }
}

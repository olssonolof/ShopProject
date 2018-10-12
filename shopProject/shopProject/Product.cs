using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace shopProject
{
    class Product
    {
        public string Name;
        public int Year;
        public int Price;



        public Product(string formated)
        {
            string [] x = formated.Split(',');
          
            Name = x[0];
            Year = int.Parse(x[1]);
            Price = int.Parse(x[2]);
                                            
        }

        
    }
   
}

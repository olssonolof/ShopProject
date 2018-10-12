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



        public Product[] GetData()
        {
            string[] nonFormated = File.ReadAllLines("products.csv");
            Product[] formated = new Product[nonFormated.Length];
            for (int i = 0; i < nonFormated.Length; i++)
            {
                string[] x = nonFormated[i].Split(',');
                formated[i].Name = x[0];
                formated[i].Year = int.Parse(x[1]);
                formated[i].Price = int.Parse(x[2]);
            }
            return formated;            
        }
    }
   
}

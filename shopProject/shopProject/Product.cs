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
    class Customer
    {
        public List<Product> Cart;
        public int TotalPrise;
        public int TotalNrOfProduct;
        public int Discount;


        public void AddProductToCard(Product product, int amount)
        {
            for (int i = 1; i < amount; i++)
            {
                Cart.Add(product);
                TotalPrise += product.Price;
            }
        }
    }
   
}

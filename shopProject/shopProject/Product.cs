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
        public Dictionary<String, int> Cart = new Dictionary<String, int> { };
        public int TotalPrise;
        public int TotalNrOfProduct;
        public int Discount;


        public Customer()
        {
            ReadSaveCart();
        }

        public void AddProductToCart(Product product)
        {
            if (Cart.ContainsKey(product.Name))
            {
                Cart[product.Name]++;
            }
            else
            {
                Cart.Add(product.Name, 1);
            }
            TotalPrise += product.Price;

            
        }
        public void CountTotalAmount()
        {
            TotalNrOfProduct = 0;
            foreach (KeyValuePair<string, int> nr in Cart)
            {
              
                TotalNrOfProduct += nr.Value;
            }
        }

        public void ReadSaveCart()
        {
            string[] nonFormated = File.ReadAllLines(@"C:\Windows\Temp\shop.txt");
            
            foreach (var item in nonFormated)
            {
                string[] formated = item.Split(',');
                Cart.Add(formated[0], int.Parse(formated[1]));
                TotalNrOfProduct += int.Parse(formated[1]);
                TotalPrise += int.Parse(formated[2]);

            }
        }

       
    }
   
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace shopProject
{
    class Product
    {
        public string Name;
        public int Year;
        public int Price;
        public Image Pic;
        public string Summary;

        public Product(string formated)
        {
            string[] pic = new string[] { @"shoppics\Battlefield.jpg", @"shoppics\Black.jpg", @"shoppics\Diablo.jpg", @"shoppics\GTA.jpg", @"shoppics\Lemmings.jpg", @"shoppics\Minecraft.jpg",
            @"shoppics\Modern.jpg", @"shoppics\Need.jpg", @"shoppics\Battleground.jpg", @"shoppics\Sims.jpg", @"shoppics\Tetris.jpg", @"shoppics\Witcher.jpg", @"shoppics\WoW.jpg"};
            string[] x = formated.Split(',');

            Name = x[0];
            Year = int.Parse(x[1]);
            Price = int.Parse(x[2]);

            foreach (string p in pic)
            {
                int y = p.IndexOf('.');
                string z = p.Substring(9, y - 9);
                if (Name.Contains(z))
                {
                    Pic = Image.FromFile(p);
                    Summary = File.ReadAllText(@"summarys\" + z + ".txt");
                }
            }
        }
    }
    class Customer
    {
        public Dictionary<String, int> Cart = new Dictionary<String, int> { };
        public int TotalPrise;
        public int TotalNrOfProduct;
        public Double Discount = 1;


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
            TotalNrOfProduct = Cart.Values.Sum(x => x);
        }

        public void ReadSaveCart()
        {
            try
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
            catch
            {
                string x = "";
                File.WriteAllText(@"C:\Windows\Temp\shop.txt", x);
            }
        }
        public void RemoveFromCart(DataGridView cartGrid)
        {
            if (cartGrid.RowCount > 0)
            {
                string x = cartGrid.CurrentRow.Cells[0].Value.ToString();
                int y = int.Parse(cartGrid.CurrentRow.Cells[2].Value.ToString());
                Cart.Remove(x);
                TotalPrise -= y;
                CountTotalAmount();
            }

        }
        public bool ReadDiscount(string text)
        {
            string text2 = text.ToUpper();
            string[] discount = File.ReadAllLines(@"discount.txt");
            foreach (string item in discount)
            {
                string[] formated = item.Split(',');
                if (text2.Contains(formated[0])) //(formated[0].Contains(text2) && text.Length > 3)
                {
                    Discount = 1 - (double.Parse(formated[1]) / 100);
                    return true;
                }

            }
            return false;
        }

    }

}

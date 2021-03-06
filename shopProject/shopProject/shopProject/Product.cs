﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace shopProject
{
    public class Product
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public int Price { get; set; }
        public Image Pic { get; set; }
        public string Summary { get; set; }

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

    public class Customer
    {
        public Dictionary<String, int> Cart = new Dictionary<String, int> { };
        public int TotalPrice { get; set; }
        public int TotalNrOfProduct { get; set; }
        private double discount = 1;
        public Double Discount
        {
            get
            {
                return discount;
            }
            set
            {
                discount = value;
            }
        }

        // Different path only used for testing 
        public Customer(string path = @"C:\Windows\Temp\shop.txt")
        {
            ReadSaveCart(path);
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
            TotalPrice += product.Price;
        }

        public void CountTotalAmount()
        {
            TotalNrOfProduct = Cart.Values.Sum(x => x);
        }

        public void ReadSaveCart(string path)
        {
            try
            {
                string[] nonFormated = File.ReadAllLines(path);

                foreach (var item in nonFormated)
                {
                    string[] formated = item.Split(',');
                    Cart.Add(formated[0], int.Parse(formated[1]));
                    TotalNrOfProduct += int.Parse(formated[1]);
                    TotalPrice += int.Parse(formated[2]);
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
                string nameOfProduct = cartGrid.CurrentRow.Cells[0].Value.ToString();
                int priceOfProduct = int.Parse(cartGrid.CurrentRow.Cells[2].Value.ToString());
                Cart.Remove(nameOfProduct);
                TotalPrice -= priceOfProduct;
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
                if ((text2.Contains(formated[0]) && Discount == 1) || (text2.Contains(formated[0]) && (100 - Double.Parse(formated[1])) < Discount * 100))
                {
                    Discount = 1 - (double.Parse(formated[1]) / 100);                 
                    return true;
                }
                else if (text2.Contains(formated[0]) && (100 - double.Parse(formated[1])) > Discount * 100)
                {
                    MessageBox.Show("You already have a active discount code with a higher value.");
                }
            }
            return false;
        }

    }

}

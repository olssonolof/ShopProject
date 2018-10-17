using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;


namespace shopProject
{
    class MyForm : Form
    {


        TableLayoutPanel container;
        DataGridView data1;
        Panel infoContainer;
        Panel header;
        Panel footer;
        DataGridView dataGridCart;
        TableLayoutPanel infoContainerTable;
        NumericUpDown amountToBuy;

        Customer customer;


        string[] nonformated;
        List<Product> products;

        public MyForm()
        {
            #region UIControls
            nonformated = GetData();
            customer = new Customer();
            products = new List<Product> { };
            foreach (string s in nonformated)
            {
                products.Add(new Product(s));
            }

            Text = "Game Shop";
            Size = new Size(1200, 800);
            Font = new Font("Arial", 10);


            container = new TableLayoutPanel
            {
                RowCount = 3,
                ColumnCount = 3,
                Dock = DockStyle.Fill
            };
            Controls.Add(container);

            header = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Gray
            };

            footer = new Panel
            {
                BackColor = Color.Gray,
                Dock = DockStyle.Fill
            };

            data1 = new DataGridView
            {
                ColumnCount = 3,
                Dock = DockStyle.Fill,
                RowHeadersVisible = false,
                GridColor = SystemColors.GrayText,
                CellBorderStyle = DataGridViewCellBorderStyle.None,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Font = new Font("Arial", 8),
                AllowUserToAddRows = false,
                BackgroundColor = SystemColors.Control,
                MultiSelect = false,
                AllowUserToResizeRows = false,               
            };
            data1.Columns[0].FillWeight = 180;
            NameDataGrid(data1);

            infoContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = SystemColors.ControlDark

            };
            AddData(data1);

            dataGridCart = new DataGridView
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowHeadersVisible = false,
                CellBorderStyle = DataGridViewCellBorderStyle.None,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Font = new Font("Arial", 8),
                BackgroundColor = SystemColors.Control,
                AllowUserToAddRows = false,
                MultiSelect = false,
                AllowUserToResizeRows = false,
            };
            dataGridCart.Columns[0].FillWeight = 180;

            NameDataGridCart(dataGridCart);
            infoContainerTable = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 3,
                Dock = DockStyle.Fill
            };


            amountToBuy = new NumericUpDown
            {
                Dock = DockStyle.Top,
                Minimum = 1,
            };

            Button buy = new Button
            {
                Text = "Buy >>",

            };
            Button remove = new Button
            {
                Text = "Remove <<"
            };

            PictureBox productPicture = new PictureBox
            {
                Dock = DockStyle.Fill,
                //Image = "test",
                BackColor = Color.Black,
            };
            Label productInfo = new Label
            {
                Text = "test!",
                Dock = DockStyle.Fill,
                
            };
            Label HeaderInfo = new Label
            {
                Dock = DockStyle.Bottom,
                Font =new Font("Arial", 15),
                Text = "Game information: ",
            };


            #endregion  //GUI
            //            amountToBuy.Location = CenterToScreen();

            container.Controls.Add(header);
            container.Controls.Add(footer, 2, 2);
            container.Controls.Add(data1, 0, 1);
            container.Controls.Add(infoContainer, 1, 1);
            container.Controls.Add(dataGridCart, 2, 1);

            container.SetColumnSpan(header, 100);
            container.SetColumnSpan(footer, 100);

            infoContainer.Controls.Add(infoContainerTable);

            infoContainerTable.Controls.Add(HeaderInfo, 1, 0);

            infoContainerTable.Controls.Add(productPicture, 0, 1);
            infoContainerTable.Controls.Add(productInfo, 1, 1);

            infoContainerTable.Controls.Add(buy, 1, 2);
            infoContainerTable.Controls.Add(remove, 1, 2);



            infoContainerTable.SetColumnSpan(amountToBuy, 100);
            infoContainerTable.RowStyles.Add(new RowStyle(SizeType.Percent, 22));
            infoContainerTable.RowStyles.Add(new RowStyle(SizeType.Percent, 46));
            infoContainerTable.RowStyles.Add(new RowStyle(SizeType.Percent, 32));
            infoContainerTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            infoContainerTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));



            remove.Click += Remove_Clicked;
            buy.Click += BuyClicked;

            //infoContainer.Controls.Add(infoContainerTable);
            //infoContainer.Controls.Add(infoContainerTable);


            container.RowStyles.Add(new RowStyle(SizeType.Percent, 15));
            container.RowStyles.Add(new RowStyle(SizeType.Percent, 70));
            container.RowStyles.Add(new RowStyle(SizeType.Percent, 15));
            container.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32));
            container.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36));
            container.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32));
            UpdateCart();
        }

        private void Remove_Clicked(object sender, EventArgs e)
        {
            customer.RemoveFromCart(dataGridCart);
            dataGridCart.Rows.Clear();
            UpdateCart();
            UpdateData(dataGridCart);
        }

        static void NameDataGrid(DataGridView data)
        {
            data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            data.Columns[0].Name = "Game";
            data.Columns[1].Name = "Release year";
            data.Columns[2].Name = "Price";

        }
        static void NameDataGridCart(DataGridView data)
        {
            data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            data.Columns[0].Name = "Game";
            data.Columns[1].Name = "Amount";
            data.Columns[2].Name = "Price";
        }

        static string[] GetData()
        {

            return File.ReadAllLines("products.csv");
        }
        public void AddData(DataGridView data)
        {
            foreach (Product x in products)
            {
                data1.Rows.Add(x.Name, x.Year, x.Price);
            }
        }


        public void BuyClicked(object sender, EventArgs e)
        {
            string x = data1.CurrentRow.Cells[0].Value.ToString();
            foreach (Product game in products)
            {
                if (x == game.Name)
                {
                    customer.AddProductToCart(game);

                    dataGridCart.Rows.Clear();

                    UpdateCart();
                    amountToBuy.Value = 1;
                    UpdateData(dataGridCart);
                    break;

                }
            }
        }

        public void UpdateCart()
        {
            foreach (KeyValuePair<string, int> x in customer.Cart)
            {
                int z = 0;
                for (int i = 0; i < products.Count; i++)
                {
                    if (products[i].Name == x.Key)
                    {
                        z = products[i].Price;
                        break;
                    }
                }
                dataGridCart.Rows.Add(x.Key, x.Value, x.Value * z);
                customer.CountTotalAmount();

            }
        }
        public void UpdateData(DataGridView cart)
        {
            string cartCSV; ;
            List<string> listFormated = new List<string> { };
            foreach (DataGridViewRow row in cart.Rows)
            {
                cartCSV = row.Cells[0].Value.ToString().Trim() + ',';
                cartCSV += row.Cells[1].Value.ToString().Trim() + ',';
                cartCSV += row.Cells[2].Value.ToString().Trim();
                listFormated.Add(cartCSV);
                File.WriteAllLines(@"C:\Windows\Temp\shop.txt", listFormated);
            }
            if (cart.RowCount == 0)
            {
                string x = "";
                File.WriteAllText(@"C:\Windows\Temp\shop.txt", x);
            }
        }



    }

}



//selectionChanged
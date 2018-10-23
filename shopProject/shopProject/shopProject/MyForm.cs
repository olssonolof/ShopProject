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
        PictureBox productPicture = new PictureBox();
        Button remove;
        Button buy;
        Button clearCart;
        Button checkout;
        Label productInfo = new Label();
        PictureBox gradient;
        Customer customer;
        TextBox discountTextBox;
        Label TotalPriceLabel;

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
            Panel xz = header;
            CreateBackgroundGradient(xz);


            footer = new Panel
            {
                BackColor = Color.Gray,
                Dock = DockStyle.Fill,
            };
            Panel xy = footer;
            CreateBackgroundGradient(xy);
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
                AllowUserToDeleteRows = false,
                ReadOnly = true,
            };
            data1.Columns[0].FillWeight = 180;
            NameDataGrid(data1);
            data1.SelectionChanged += ChangedSelektion;

            infoContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = SystemColors.Control,

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
                AllowUserToDeleteRows = false,
                ReadOnly = true,
            };
            dataGridCart.Columns[0].FillWeight = 180;

            NameDataGridCart(dataGridCart);
            infoContainerTable = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 3,
                Dock = DockStyle.Fill
            };



            TableLayoutPanel buttonHandlerPanel = new TableLayoutPanel
            {
                RowCount = 3,
                Dock = DockStyle.Fill,
                ColumnCount = 3,

            };


            buy = new Button
            {
                Dock = DockStyle.Fill,
                FlatStyle = FlatStyle.Flat,
                Text = "Buy >>",
                AutoSize = true,

            };
            remove = new Button
            {
                Size = new Size(150, 15),
                FlatStyle = FlatStyle.Flat,
                Text = "Remove <<",
                AutoSize = true,
                Dock = DockStyle.Fill
            };
            clearCart = new Button
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                FlatStyle = FlatStyle.Flat,
                Text = "Clear Cart",
            };

            checkout = new Button
            {
                Dock = DockStyle.Fill,
                Text = "Checkout",

            };

            productPicture = new PictureBox
            {
                Dock = DockStyle.Top,
                BackColor = SystemColors.Control,
                Size = new Size(200, 150),
                SizeMode = PictureBoxSizeMode.Zoom,

            };
            productInfo = new Label
            {
                Text = "test!",
                Dock = DockStyle.Fill,
                Font = new Font("arial", 9),

            };
            Label HeaderInfo = new Label
            {
                Dock = DockStyle.Bottom,
                Font = new Font("Arial", 15),
                Text = "Game information: ",

            };
            TableLayoutPanel cartPanel = new TableLayoutPanel
            {
                RowCount = 3,
                ColumnCount = 2,
                Dock = DockStyle.Fill,
            };
            discountTextBox = new TextBox
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                Text = "Discount Code",

            };
            discountTextBox.Click += DiscountTextSelectAll_OnClick;
            TotalPriceLabel = new Label
            {
                AutoSize = true,
                Text = "Total Price: " + customer.TotalPrise * customer.Discount,
            };

            #endregion  //GUI


            container.Controls.Add(header);
            container.Controls.Add(footer, 2, 2);
            container.Controls.Add(data1, 0, 1);
            container.Controls.Add(infoContainer, 1, 1);
            container.Controls.Add(cartPanel, 2, 1);


            cartPanel.Controls.Add(dataGridCart, 0, 0);
            cartPanel.Controls.Add(discountTextBox, 0, 1);
            cartPanel.Controls.Add(TotalPriceLabel, 1, 1);
            cartPanel.Controls.Add(checkout, 0, 2);
            cartPanel.SetColumnSpan(dataGridCart, 100);
            cartPanel.SetColumnSpan(checkout, 100);


            container.SetColumnSpan(header, 100);
            container.SetColumnSpan(footer, 100);
            infoContainerTable.SetColumnSpan(buttonHandlerPanel, 100);


            infoContainer.Controls.Add(infoContainerTable);

            infoContainerTable.Controls.Add(HeaderInfo, 1, 0);

            infoContainerTable.Controls.Add(productPicture, 0, 1);
            infoContainerTable.Controls.Add(productInfo, 1, 1);

            infoContainerTable.Controls.Add(buttonHandlerPanel, 1, 2);
            buttonHandlerPanel.Controls.Add(buy, 1, 0);
            buttonHandlerPanel.Controls.Add(remove, 1, 1);
            buttonHandlerPanel.Controls.Add(clearCart, 1, 2);



            infoContainerTable.RowStyles.Add(new RowStyle(SizeType.Percent, 12));
            infoContainerTable.RowStyles.Add(new RowStyle(SizeType.Percent, 56));
            infoContainerTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            infoContainerTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));



            cartPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 85));
            cartPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 7));
            cartPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            cartPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));



            buttonHandlerPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33));
            buttonHandlerPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33));
            buttonHandlerPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33));
            buttonHandlerPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            buttonHandlerPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            buttonHandlerPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));

            remove.MouseEnter += MouseOverButton;
            buy.MouseEnter += MouseOverButton;
            clearCart.MouseEnter += MouseOverButton;
            checkout.MouseEnter += MouseOverButton;
            remove.Click += Remove_Clicked;
            buy.Click += BuyClicked;
            data1.CellDoubleClick += BuyClicked;
            dataGridCart.DoubleClick += Remove_Clicked;
            clearCart.Click += ClearCartPressed;
            data1.KeyDown += Data1_KeyUp;
            checkout.Click += Checkout_Click;
            discountTextBox.TextChanged += DiscountTextBox_TextChanged;
            this.FormClosed += ClosedWindow;




            container.RowStyles.Add(new RowStyle(SizeType.Percent, 15));
            container.RowStyles.Add(new RowStyle(SizeType.Percent, 70));
            container.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32));
            container.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36));
            container.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32));


            UpdateCart();
        }

        private void DiscountTextBox_TextChanged(object sender, EventArgs e)
        {
            bool isDiscount = customer.ReadDiscount(discountTextBox.Text);
            TotalPrice();
            if (isDiscount)
            {
                discountTextBox.BackColor = Color.Green;
            }
        }

        private void DiscountTextSelectAll_OnClick(object sender, EventArgs e)
        {
            discountTextBox.SelectAll();
        }

        private void Checkout_Click(object sender, EventArgs e)
        {
            if (dataGridCart.Rows.Count > 0)
            {
                if (MessageBox.Show("Are You sure You want to complete the checkout? ?", "Confirm buy", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string total = "";
                    foreach (KeyValuePair<string, int> game in customer.Cart)
                    {
                        total += game.Key + ": " + game.Value + "\n";
                    }

                    MessageBox.Show("*****Your receipt:*****\n" + total + "\nTotal number of products: " + customer.TotalNrOfProduct + "\nTotal cost: $" + customer.TotalPrise * customer.Discount, "Receipt");
                    string x = "";
                    File.WriteAllText(@"C:\Windows\Temp\shop.txt", x);
                    customer = new Customer();
                    customer.Discount = 1;
                    discountTextBox.BackColor = SystemColors.Control;
                    discountTextBox.Text = "Discount Code";

                    UpdateCart();

                }
            }
        }

        private void Data1_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter) BuyProduct();
        }

        private void ClosedWindow(object sender, FormClosedEventArgs e)
        {
            if (dataGridCart.Rows.Count > 0)
            {
                if (MessageBox.Show("Do You want to save the cart? ?", "Save cart", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    string x = "";
                    File.WriteAllText(@"C:\Windows\Temp\shop.txt", x);

                }
            }

        }
        private void ClearCartPressed(object sender, EventArgs e)
        {
            string x = "";
            File.WriteAllText(@"C:\Windows\Temp\shop.txt", x);
            customer = new Customer();
            UpdateCart();
            TotalPrice();
        }

        private void MouseOverButton(object sender, EventArgs e)
        {
            ToolTip tp = new ToolTip
            {
                ShowAlways = true,
                ReshowDelay = 500,
            };
            if (sender == buy) tp.SetToolTip(buy, "Click here to add product to cart");
            else if (sender == remove) tp.SetToolTip(remove, "Click here to remove product from cart");
            else if (sender == clearCart) tp.SetToolTip(clearCart, "Click here to clear the cart");
            else if (sender == checkout) tp.SetToolTip(checkout, "Click here to checkout");
        }

        private void CreateBackgroundGradient(Panel x)
        {
            gradient = new PictureBox
            {
                Dock = DockStyle.Fill,
                ImageLocation = "https://wallpapers.wallhaven.cc/wallpapers/full/wallhaven-507917.jpg",
                SizeMode = PictureBoxSizeMode.StretchImage,
            };
            x.Controls.Add(gradient);
        }

        private void ChangedSelektion(object sender, EventArgs e)
        {
            string x = data1.CurrentRow.Cells[0].Value.ToString();
            foreach (Product item in products)
            {
                if (item.Name == x)
                {
                    productPicture.Image = item.Pic;
                    productInfo.Text = item.Summary;
                }
            }

        }

        private void Remove_Clicked(object sender, EventArgs e)
        {
            customer.RemoveFromCart(dataGridCart);
            dataGridCart.Rows.Clear();
            UpdateCart();
            TotalPrice();

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
            BuyProduct();
           
        }

        public void UpdateCart()
        {
            dataGridCart.Rows.Clear();

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
            TotalPrice();

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
        public void GetImages()
        {
            string x = data1.CurrentRow.Cells[0].Value.ToString();

        }

        public void BuyProduct()
        {
            string x = data1.CurrentRow.Cells[0].Value.ToString();
            foreach (Product game in products)
            {
                if (x == game.Name)
                {
                    customer.AddProductToCart(game);

                    //   dataGridCart.Rows.Clear();

                    UpdateCart();
                    UpdateData(dataGridCart);
                    break;

                }
            }
        }
        public void TotalPrice()
        {
            TotalPriceLabel.Text = "Total Price: " + (customer.TotalPrise * customer.Discount);
        }

    }

}

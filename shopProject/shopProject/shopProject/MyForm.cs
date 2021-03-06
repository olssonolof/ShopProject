﻿using System;
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
        private TableLayoutPanel container;
        private Panel infoContainer;
        private Panel header;
        private Panel footer;
        private DataGridView dataGridProduct;
        private DataGridView dataGridCart;
        private TableLayoutPanel infoContainerTable;
        private PictureBox productPicture;
        private Button remove;
        private Button buy;
        private Button clearCart;
        private Button checkout;
        private Panel secretPanel;
        private Label productInfo;
        private TextBox discountTextBox;
        private Label totalPriceLabel;
        private Form receipt;

        private Customer customer;
        string[] nonformated;
        List<Product> products;

        public MyForm()
        {
            #region UIControls

            Icon = new Icon(@"shoppics\Mario.ico");
            nonformated = File.ReadAllLines("products.csv");
            customer = new Customer();
            products = new List<Product> { };
            foreach (string s in nonformated)
            {
                products.Add(new Product(s));
            }

            Text = "Game Shop";
            Size = new Size(1200, 800);
            Font = new Font("Arial", 10);
            StartPosition = FormStartPosition.CenterScreen;

            container = new TableLayoutPanel
            {
                RowCount = 3,
                ColumnCount = 3,
                Dock = DockStyle.Fill
            };
            
            header = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Gray,
                BackgroundImage = Image.FromFile(@"shoppics\Background.jpg"),
                BackgroundImageLayout = ImageLayout.Stretch,
            };

            footer = new Panel
            {
                BackColor = Color.Gray,
                Dock = DockStyle.Fill,
                BackgroundImage = Image.FromFile(@"shoppics\Background.jpg"),
                BackgroundImageLayout = ImageLayout.Stretch,
            };

            dataGridProduct = new DataGridView
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
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            };

            dataGridProduct.Columns[0].Name = "Game";
            dataGridProduct.Columns[1].Name = "Release year";
            dataGridProduct.Columns[2].Name = "Price";
            dataGridProduct.Columns[0].FillWeight = 180;
            dataGridProduct.SelectionChanged += ChangedSelektion;

            infoContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = SystemColors.Control,
            };

            AddDataToDGVProducts();

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

            secretPanel = new Panel
            {
                Size = new Size(10, 10),
                BackgroundImageLayout = ImageLayout.Zoom,
            };

            checkout = new Button
            {
                Dock = DockStyle.Fill,
                Text = "Checkout"
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
            totalPriceLabel = new Label
            {
                AutoSize = true,
                Text = "Total Price: $" + customer.TotalPrice * customer.Discount,
            };


            Controls.Add(container);
            container.Controls.Add(header);
            container.Controls.Add(footer, 2, 2);
            container.Controls.Add(dataGridProduct, 0, 1);
            container.Controls.Add(infoContainer, 1, 1);
            container.Controls.Add(cartPanel, 2, 1);

            cartPanel.Controls.Add(dataGridCart, 0, 0);
            cartPanel.Controls.Add(discountTextBox, 0, 1);
            cartPanel.Controls.Add(totalPriceLabel, 1, 1);
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
            buttonHandlerPanel.Controls.Add(secretPanel, 0, 0);
            buttonHandlerPanel.Controls.Add(clearCart, 1, 2);
            buttonHandlerPanel.SetRowSpan(secretPanel, 100);

            infoContainerTable.RowStyles.Add(new RowStyle(SizeType.Percent, 12));
            infoContainerTable.RowStyles.Add(new RowStyle(SizeType.Percent, 56));
            infoContainerTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            infoContainerTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            cartPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 85));
            cartPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 7));
            cartPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            cartPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            for (int i = 0; i < 3; i++)
            {
                buttonHandlerPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33));
                buttonHandlerPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            }

            container.RowStyles.Add(new RowStyle(SizeType.Percent, 15));
            container.RowStyles.Add(new RowStyle(SizeType.Percent, 70));
            container.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32));
            container.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36));
            container.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32));

            #endregion  //GUI
            
            remove.MouseEnter += MouseOverButton;
            buy.MouseEnter += MouseOverButton;
            secretPanel.MouseEnter += MouseOverButton;
            secretPanel.MouseLeave += SecretPanel_MouseLeave;
            clearCart.MouseEnter += MouseOverButton;
            checkout.MouseEnter += MouseOverButton;
            remove.Click += Remove_Clicked;
            buy.Click += BuyClicked;
            dataGridProduct.CellDoubleClick += BuyClicked;
            dataGridCart.DoubleClick += Remove_Clicked;
            clearCart.Click += ClearCartPressed;
            dataGridProduct.KeyDown += DataGridProduct_KeyUp;
            checkout.Click += Checkout_Click;
            discountTextBox.TextChanged += DiscountTextBox_TextChanged;
            this.FormClosed += ClosedWindow;

            UpdateCart();
        }

        private void SecretPanel_MouseLeave(object sender, EventArgs e)
        {
            secretPanel.BackgroundImage = null;
            secretPanel.Dock = DockStyle.None;
            secretPanel.Size = new Size(10, 10);
        }

        private void DiscountTextBox_TextChanged(object sender, EventArgs e)
        {
            bool isDiscount = customer.ReadDiscount(discountTextBox.Text);
            TotalPrice();
            if (isDiscount)
            {
                discountTextBox.BackColor = Color.Green;
                MessageBox.Show($"You have {(100 - (customer.Discount * 100))}% discount!");
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
                    CreateReceipt();
                }
            }
        }

        private void DataGridProduct_KeyUp(object sender, KeyEventArgs e)
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
            double discount = 1;
            string x = "";
            if (customer.Discount != discount)
            {
                discount = customer.Discount;
            }
            File.WriteAllText(@"C:\Windows\Temp\shop.txt", x);
            customer = new Customer();
            customer.Discount = discount;
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
            else if (sender == secretPanel)
            {
                tp.SetToolTip(secretPanel, "Meow!");
                secretPanel.BackgroundImage = Image.FromFile(@"shoppics\catPic.jpg");
                secretPanel.Dock = DockStyle.Fill;
            }
        }

        #region reciptMaker
        private void CreateReceipt()
        {

            receipt = new Form
            {
                Size = new Size(350, 600),
                Font = new Font("Arial", 10),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                StartPosition = FormStartPosition.CenterScreen,
            };

            TableLayoutPanel receiptContainer = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = SystemColors.Control,
                RowCount = 6
            };

            Label receiptLabel = new Label
            {
                Dock = DockStyle.Fill,
                Text = "-- Receipt --",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 16),
                AutoSize = true,
            };

            Label totalPriceLabelReceipt = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = true,
                Text = totalPriceLabel.Text,
            };

            Label discountLabelReceipt = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = true,
                Text = $"Total discount: ${Math.Round(customer.TotalPrice - (customer.TotalPrice * customer.Discount), 2)}",
            };
            Label totalNrOfProducts = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = true,
                Text = $"Total nr of products {customer.TotalNrOfProduct}",
            };

            Button closeReceipt = new Button
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Close Receipt",
            };

            DataGridView dataCart = new DataGridView
            {
                Dock = DockStyle.Fill,
                RowHeadersVisible = false,
                CellBorderStyle = DataGridViewCellBorderStyle.None,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Font = new Font("Arial", 8),
                BackgroundColor = SystemColors.Control,
                AllowUserToAddRows = false,
                MultiSelect = false,
                Enabled = false,
                AllowUserToResizeRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
            };


            //Copy values of dataGridCart to a new dataGridView for receipt
            foreach (DataGridViewCell cell in dataGridCart.Rows[0].Cells)
                dataCart.Columns.Add(new DataGridViewColumn(cell));

            dataCart.Rows.Add(dataGridCart.Rows.Count);

            for (int row = 0; row < dataGridCart.Rows.Count; row++)
            {
                for (int col = 0; col < dataGridCart.Columns.Count; col++)
                {
                    dataCart[col, row].Value = dataGridCart[col, row].Value;
                }
            }
            NameDataGridCart(dataCart);

            receiptContainer.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            receiptContainer.RowStyles.Add(new RowStyle(SizeType.Percent, 60));
            receiptContainer.RowStyles.Add(new RowStyle(SizeType.Percent, 5));
            receiptContainer.RowStyles.Add(new RowStyle(SizeType.Percent, 5));
            receiptContainer.RowStyles.Add(new RowStyle(SizeType.Percent, 5));
            receiptContainer.RowStyles.Add(new RowStyle(SizeType.Percent, 10));




            receipt.Show();
            this.Enabled = false;
            receipt.Controls.Add(receiptContainer);
            receiptContainer.Controls.Add(receiptLabel, 0, 0);
            receiptContainer.Controls.Add(dataCart, 0, 1);
            receiptContainer.Controls.Add(totalNrOfProducts, 0, 2);
            receiptContainer.Controls.Add(discountLabelReceipt, 0, 3);
            receiptContainer.Controls.Add(totalPriceLabelReceipt, 0, 4);
            receiptContainer.Controls.Add(closeReceipt, 0, 5);


            closeReceipt.Click += Receipt_ClosedX;
            receipt.FormClosed += Receipt_Closed;
        }

        private void Receipt_Closed(object sender, EventArgs e)
        {
            this.Enabled = true;
            string x = "";
            File.WriteAllText(@"C:\Windows\Temp\shop.txt", x);
            customer = new Customer();
            customer.Discount = 1;
            discountTextBox.BackColor = Color.White;
            discountTextBox.Text = "Discount Code";
            TotalPrice();
            UpdateCart();
        }

        private void Receipt_ClosedX(object sender, EventArgs e)
        {
            receipt.Close();
        }
        #endregion

        private void ChangedSelektion(object sender, EventArgs e)
        {
            string x = dataGridProduct.CurrentRow.Cells[0].Value.ToString();
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

        public void BuyClicked(object sender, EventArgs e)
        {
            BuyProduct();
        }

        // Both Cart and Receipe uses this method
        static void NameDataGridCart(DataGridView data)
        {
            data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            data.Columns[0].Name = "Game";
            data.Columns[1].Name = "Amount";
            data.Columns[2].Name = "Price";
        }


        public void AddDataToDGVProducts()
        {
            foreach (Product items in products)
            {
                dataGridProduct.Rows.Add(items.Name, items.Year, items.Price);
            }
        }


        public void UpdateCart()
        {
            dataGridCart.Rows.Clear();

            foreach (KeyValuePair<string, int> row in customer.Cart)
            {
                int price = 0;
                bool found = false;
                for (int i = 0; i < products.Count || !found; i++)
                {
                    // Lägger till rätt pris i datagridview(cart).
                    if (products[i].Name == row.Key)
                    {
                        price = products[i].Price;
                        found = true;
                    }
                }
                dataGridCart.Rows.Add(row.Key, row.Value, row.Value * price);
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

        public void BuyProduct()
        {
            string gameName = dataGridProduct.CurrentRow.Cells[0].Value.ToString();
            foreach (Product game in products)
            {
                if (gameName == game.Name)
                {
                    customer.AddProductToCart(game);
                    UpdateCart();
                    UpdateData(dataGridCart);
                    break;
                }
            }
        }

        public void TotalPrice()
        {
            totalPriceLabel.Text = $"Total Price: ${customer.TotalPrice * customer.Discount}";
        }

    }

}

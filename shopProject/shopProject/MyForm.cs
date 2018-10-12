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

        

        string[] nonformated;
        List<Product> products;

        public MyForm()
        {

            nonformated = GetData();

            products = new List<Product> { };
            foreach (string s in nonformated)
            {
                products.Add(new Product(s));
            }

            Text = "Game Shop";
            Size = new Size(800, 800);
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
                CellBorderStyle = DataGridViewCellBorderStyle.None
            };
            NameDataGrid(data1);

            infoContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Red

            };
            AddData(data1);

            dataGridCart = new DataGridView
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                
            };

            infoContainerTable = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount= 3,
                Dock = DockStyle.Fill
            };


            amountToBuy = new NumericUpDown
            {
                Dock = DockStyle.Top
            };

            Button buy = new Button
            {

            };

//            amountToBuy.Location = CenterToScreen();

            container.Controls.Add(header);
            container.Controls.Add(footer, 2, 2);
            container.Controls.Add(data1, 0, 1);
            container.Controls.Add(infoContainer, 1, 1);
            container.Controls.Add(dataGridCart, 2, 1);
            container.SetColumnSpan(header, 100);
            container.SetColumnSpan(footer, 100);

            infoContainer.Controls.Add(infoContainerTable);
            infoContainerTable.Controls.Add(amountToBuy);
            infoContainerTable.SetColumnSpan(amountToBuy, 100);
            infoContainerTable.RowStyles.Add(new RowStyle(SizeType.Percent, 32));
            infoContainerTable.RowStyles.Add(new RowStyle(SizeType.Percent, 36));
            infoContainerTable.RowStyles.Add(new RowStyle(SizeType.Percent, 32));

            infoContainerTable.Controls.Add(buy, 2, 1);

            //infoContainer.Controls.Add(infoContainerTable);
            //infoContainer.Controls.Add(infoContainerTable);


            container.RowStyles.Add(new RowStyle(SizeType.Percent, 15));
            container.RowStyles.Add(new RowStyle(SizeType.Percent, 70));
            container.RowStyles.Add(new RowStyle(SizeType.Percent, 15));
            container.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32));
            container.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36));
            container.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32));

        }

        static void NameDataGrid(DataGridView data)
        {
            data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            data.Columns[0].Name = "Game";
            data.Columns[1].Name = "Release year";
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
    }
}

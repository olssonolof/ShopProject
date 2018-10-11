using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace shopProject
{
    class MyForm : Form
    {
        public MyForm()
        {
            Text = "Game Shop";
            Size = new Size(800, 800);
            Font = new Font("Arial", 14);


            TableLayoutPanel container = new TableLayoutPanel
            {
                RowCount = 3,
                ColumnCount = 3,
                Dock = DockStyle.Fill
            };
            Controls.Add(container);

            Panel header = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Gray
            };
            container.Controls.Add(header);
        }
    }
}

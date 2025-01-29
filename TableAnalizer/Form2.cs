using System;
using System.Windows.Forms;

namespace TableAnalizer
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public DataGridView DataGridView1 { get; set; }
        public DataGridView DataGridView2 { get; set; }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (DataGridView1 != null)
            {
                this.Controls.Add(DataGridView1);
                DataGridView1.Dock = DockStyle.Top;
            }

            if (DataGridView2 != null)
            {
                this.Controls.Add(DataGridView2);
                DataGridView2.Dock = DockStyle.Bottom;
            }
        }
    }
}
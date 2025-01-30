using System;
using System.Windows.Forms;

namespace TableAnalizer
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

        }

        public DataGridView DataGridView1 { get; set; }
        public DataGridView DataGridView2 { get; set; }

        private void Form2_Load(object sender, EventArgs e)
        {
            DataGridView1.Visible = true;

            if (DataGridView1 != null)
            {
                this.Controls.Add(DataGridView1);
                DataGridView1.Dock = DockStyle.Top;
                DataGridView1.Size = new System.Drawing.Size(318, 800);
            }

            if (DataGridView2 != null)
            {
                this.Controls.Add(DataGridView2);
                DataGridView2.Dock = DockStyle.Top;
                DataGridView1.Size = new System.Drawing.Size(318, 800);

            }

            if (checkBox1.Checked)
            {
                checkBox1.Text =  "Mostrar todos los datos";
            }else
            {
                checkBox1.Text = "Mostrar datos relevantes";

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox1.Text = "Mostrar todos los datos";
                DataGridView1.Visible = false;
                DataGridView2.Visible = true;

            }
            else
            {
                checkBox1.Text = "Mostrar datos relevantes";
                DataGridView2.Visible = false;
                DataGridView1.Visible = true;


            }
        }
    }
}
using System;
using System.Windows.Forms;

namespace TableAnalizer
{
    public partial class Form2 : Form
    {
        private Form1 form1Instance;

        public Form2(Form1 form1)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.form1Instance = form1;
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
                DataGridView2.Size = new System.Drawing.Size(318, 800);

            }

            if (checkBox1.Checked)
            {
                checkBox1.Text = "Mostrar datos relevantes";
            }
            else
            {
                checkBox1.Text = "Mostrar todos los datos";
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var dataTable = form1Instance.LoadExcelData(form1Instance.FilePath, checkBox1.Checked, checkBox1.Checked);

            if (checkBox1.Checked)
            {
                checkBox1.Text = "Mostrar datos relevantes";
                DataGridView1.Visible = false;
                DataGridView2.Visible = true;
                DataGridView2.DataSource = dataTable;
            }
            else
            {
                checkBox1.Text = "Mostrar todos los datos";
                DataGridView2.Visible = false;
                DataGridView1.Visible = true;
                DataGridView1.DataSource = dataTable;
            }
        }
    }
}
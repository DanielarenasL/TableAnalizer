using System;
using System.Windows.Forms;

namespace TableAnalizer
{
    public partial class Form2 : Form
    {
        private Form1 form1Instance;

        //Constructor
        public Form2(Form1 form1)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.form1Instance = form1; ///Trae datos del form1
            this.Text = "Show Tables"; // Cambiar el título de la ventana

        }

        //Trae las dos tablas del form1
        public DataGridView DataGridView1 { get; set; }
        public DataGridView DataGridView2 { get; set; }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (DataGridView1 != null)
            {
                this.Controls.Add(DataGridView1);
                DataGridView1.Dock = DockStyle.Top;
                DataGridView1.Size = new System.Drawing.Size(318, 800);
                DataGridView1.Visible = true; // Asegurarse de que DataGridView1 esté visible
                DataGridView1.DataSource = form1Instance.LoadExcelData(form1Instance.FilePath, applyDateFilter: true); // Cargar datos filtrados por fecha
            }

            if (DataGridView2 != null)
            {
                this.Controls.Add(DataGridView2);
                DataGridView2.Dock = DockStyle.Top;
                DataGridView2.Size = new System.Drawing.Size(318, 800);
                DataGridView2.Visible = false; // Inicialmente oculto
            }

            if (checkBox1.Checked)
            {
                checkBox1.Text = "Mostrar todos los lotes";

                DataGridView1.Visible = false;
                DataGridView2.Visible = true;
                DataGridView2.DataSource = form1Instance.LoadExcelData(form1Instance.FilePath, filterFailed: true, applyDateFilter: true); // Cargar datos fallidos filtrados por fecha
            }
            else
            {
                checkBox1.Text = "Mostrar lotes fallidos";
            }
        }


        //Cambia de vista entre tablas al darle al checkbox
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var dataTable = form1Instance.LoadExcelData(form1Instance.FilePath, filterFailed: checkBox1.Checked, applyDateFilter: true);

            if (checkBox1.Checked)
            {
                checkBox1.Text = "Mostrar todos los lotes";
                DataGridView1.Visible = false;
                DataGridView2.Visible = true;
                DataGridView2.DataSource = dataTable;
            }
            else
            {
                checkBox1.Text = "Mostrar lotes fallidos";
                DataGridView2.Visible = false;
                DataGridView1.Visible = true;
                DataGridView1.DataSource = dataTable;
            }
        }


        //Reinicia las instancias de las tablas 
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (DataGridView1 != null)
            {
                DataGridView1.DataSource = null;
                this.Controls.Remove(DataGridView1);
            }
            if (DataGridView2 != null)
            {
                DataGridView2.DataSource = null;
                this.Controls.Remove(DataGridView2);
            }
        }
    }
}
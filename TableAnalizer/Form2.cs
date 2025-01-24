using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TableAnalizer
{
    public partial class Form2 : Form
    {
        public Form2(DataTable data)
        {
            InitializeComponent();

            // Asigna el DataTable al DataGridView
            dataGridView1.DataSource = data;

            
            this.WindowState = FormWindowState.Maximized;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // Puedes ajustar algunas configuraciones del DataGridView si es necesario
            dataGridView1.AutoResizeColumns();
        }

        private void Form2_Load_1(object sender, EventArgs e)
        {

        }
    }
}

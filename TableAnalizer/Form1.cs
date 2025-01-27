using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml; // Agrega la referencia a EPPlus

namespace TableAnalizer
{
    public partial class Form1 : Form
    {   
        
        private int totalLotes = 0;
        private int lotesPassed = 0;
        private int lotesFailed = 0;
        private bool state = false;
        private string filePath = string.Empty;



        //Constructor 
        public Form1()
        {
            //Usar licencia gratuita
            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            InitializeComponent();
            
            openFileDialog1 = new OpenFileDialog();         //Crea la funcion para abrir el archivo

            this.WindowState = FormWindowState.Maximized;       //Al ejecutar, la ventana siempre esta maximizada

            // Suscribirse al evento de formato de celda
            dataGridView1.CellFormatting += DataGridView_CellFormatting;
            dataGridView2.CellFormatting += DataGridView_CellFormatting;


            View();

        }

        private void View()
        {
            if(SelectDocument.Visible == Visible)
            {
                label1.Visible = false;
                label2.Visible = false;
            }
            else
            {
                label1.Visible = true;
                label2.Visible = true;

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Cambia el estilo de las cabeceras de columna a negrita
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);

            // Opcionalmente, también puedes cambiar el color de fondo o el color del texto de las cabeceras:
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }


        private void SelectDocument_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Archivos de Excel (*.xlsx)|*.xlsx";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
                var dataTable = LoadExcelData(filePath, filterColumns: checkBox1.Checked, filterFailed: checkBox1.Checked);

                if (checkBox1.Checked)
                {
                    dataGridView2.DataSource = dataTable;
                    dataGridView2.Visible = true;
                    dataGridView1.Visible = false;
                }
                else
                {
                    dataGridView1.DataSource = dataTable;
                    dataGridView1.Visible = true;
                    dataGridView2.Visible = false;
                }

                CountBatch(dataTable);
            }
        }

        private System.Data.DataTable LoadExcelData(string filePath, bool filterColumns = false, bool filterFailed = false)
        {
            var dataTable = new System.Data.DataTable();

            var originalColumns = new List<string>
            {
                "Batch Id", "Dyelot Date", "Orders", "Shade Name", "Max Colour Diff", "Batch Status",
                "Substr Code", "Count/Ply", "Thread Quality", "Fibre Type", "Dyeing Method", "Recipe Status",
                "Delta L", "Delta c", "Delta h", "Machine Name", "Machine Vol", "Total Cheeses", "Total Batch Weight",
                "Failure Reason", "Dyeclass(es)", "Dye Triangle 1", "Dye Code 1", "Dye Code 2", "Dye Code 3",
                "Total Dye Conc Stage 1", "Dye Triangle 2", "Dye Code 4", "Dye Code 5", "Dye Code 6",
                "Total Dye Conc Stage 2", "Worker", "Shift", "Machine In", "Machine Out", "Dyelot Comments",
                "Shade Desc", "Shade Card", "Recipe Type", "Material Code", "Customers", "Lub Type", "Unfinished Stnd Type",
                "L", "A", "B", "Chroma", "Hue", "Recipe Type Code", "No. of Passed Cheeses", "Producer", "Finish Type",
                "Fastness Type", "Dyed From", "Comment", "Colour Category", "Article", "Source Dyehouse", "Speedline Priority"
            };

            var columnsToShow = new List<string>
            {
                "Batch Id", "Dyelot Date", "Orders", "Substr Code", "Batch Status", "Fibre Type", "Dyeing Method",
                "Recipe Status", "Machine Name", "Failure Reason", "Dyeclass(es)", "Dye", "Triangle 1",
                "Worker", "Article", "Machine In", "Machine Out"
            };

            using (var package = new ExcelPackage(new System.IO.FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var startRow = worksheet.Dimension.Start.Row;
                var endRow = worksheet.Dimension.End.Row;
                var startCol = worksheet.Dimension.Start.Column;
                var endCol = worksheet.Dimension.End.Column;

                foreach (var columnName in originalColumns)
                {
                    if (!filterColumns || columnsToShow.Contains(columnName))
                    {
                        dataTable.Columns.Add(columnName);
                    }
                }

                for (int row = startRow + 1; row <= endRow; row++)
                {
                    var rowData = dataTable.NewRow();
                    bool addRow = true;
                    for (int col = startCol; col <= endCol; col++)
                    {
                        string columnName = worksheet.Cells[1, col].Text;
                        if (dataTable.Columns.Contains(columnName))
                        {
                            rowData[columnName] = worksheet.Cells[row, col].Text;
                            if (filterFailed && columnName == "Batch Status" && worksheet.Cells[row, col].Text != "FAILED")
                            {
                                addRow = false;
                                break;
                            }
                        }
                    }
                    if (addRow)
                    {
                        dataTable.Rows.Add(rowData);
                    }
                }
            }

            SelectDocument.Visible = false;
            SelectDocument.Enabled = false;
            View();

            return dataTable;
        }

        

        private void DataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var dataGridView = sender as DataGridView;

            // Verifica que estamos en la columna correcta
            if (dataGridView.Columns[e.ColumnIndex].Name == "Batch Status")
            {
                // Verifica si el valor de la celda no es nulo y es un número
                if (e.Value != null && e.Value is string valor)
                {
                    // Si el valor es mayor a 100, cambia el fondo de la celda a rojo
                    if (valor == "FAILED")
                    {
                        dataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                        dataGridView.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                    }
                    else
                    {
                        e.CellStyle.BackColor = Color.White; // Fondo blanco por defecto
                        e.CellStyle.ForeColor = Color.Black; // Texto negro por defecto
                    }
                }
            }
        }

        private void CountBatch(DataTable dataTable)
        {
            totalLotes = dataTable.Rows.Count;
            lotesFailed = 0;
            lotesPassed = 0;
            foreach (DataRow row in dataTable.Rows) 
            {
                string batchValue = row["Batch Status"].ToString();

                if (batchValue == "FAILED")
                {
                    lotesFailed++;
                }
                else if(batchValue == "PASSED"){
                    lotesPassed++;
                }
            }


            double passedPercentage = (100.0 * lotesPassed) / totalLotes;
            double failedPercentage = (100.0 * lotesFailed) / totalLotes;

            label1.Text = $"Total: {totalLotes} \n\nPassed: {lotesPassed} \n\nFailed: {lotesFailed}";
            label2.Text = $"Passed Percentage: \n{passedPercentage:F2}% \n\nFailed Percentage: \n{failedPercentage:F2}%";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                var dataTable = LoadExcelData(filePath, filterColumns: checkBox1.Checked, filterFailed: checkBox1.Checked);

                if (checkBox1.Checked)
                {
                    dataGridView2.DataSource = dataTable;
                    dataGridView2.Visible = true;
                    dataGridView1.Visible = false;
                }
                else
                {
                    dataGridView1.DataSource = dataTable;
                    dataGridView1.Visible = true;
                    dataGridView2.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("No se ha seleccionado un archivo Excel.");
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
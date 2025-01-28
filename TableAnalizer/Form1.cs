using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using System.Net.Http;
using System.Security.Policy;
using static TableAnalizer.Form1;
using System.Net.Http.Headers;

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

            dataGridView1.CellFormatting += DataGridView_CellFormatting;
            dataGridView2.CellFormatting += DataGridView_CellFormatting;


            View();

        }

        private void View()
        {

            if (SelectDocument.Visible == Visible)
            {
                label1.Visible = false;
                Limpiar.Visible = false;
                label2.Visible = false;
            }
            else
            {
                label1.Visible = true;
                label2.Visible = true;
                Limpiar.Visible = true;


            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }


        //Seleccionar documento
        private void SelectDocument_Click(object sender, EventArgs e)
        {
            //Evita que se agreguen archivos que no sean ecel
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

            //Define las columnas originales
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

            //Define las columnas "especiales"
            var columnsToShow = new List<string>
            {
                "Batch Id", "Dyelot Date", "Shade Name", "Max Colour Diff", "Substr Code", "Batch Status", "Count/Ply",
                "Fibre Type", "Dyeing Method", "Recipe Status", "Delta L", "Delta c", "Delta h", "Machine Name", "Machine Vol",
                "Failure Reason", "Dyeclass(es)", "Worker", "Article", "Material Code"
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

        
        //Pinta de rojo los que no pasaron
        private void DataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var dataGridView = sender as DataGridView;

            
            if (dataGridView.Columns[e.ColumnIndex].Name == "Batch Status")
            {
                
                if (e.Value != null && e.Value is string valor)
                {
                    
                    if (valor == "FAILED")
                    {
                        dataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                        dataGridView.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                    }
                    else
                    {
                        e.CellStyle.BackColor = Color.White; 
                        e.CellStyle.ForeColor = Color.Black; 
                    }
                }
            }
        }

        //Calcula las estadisticas
        private void CountBatch(DataTable dataTable)
        {
            totalLotes = dataTable.Rows.Count;
            lotesFailed = 0;
            lotesPassed = 0;

            var counters = new Dictionary<string, Dictionary<string, int>>
            {
                {"Substr Code", new Dictionary<string, int>()},
                {"Count/Ply", new Dictionary<string, int>()},
                {"Fibre Type", new Dictionary<string, int>()},
                {"Dyeing Method", new Dictionary<string, int>()},
                {"Recipe Status", new Dictionary<string, int>()},
                {"Machine Name", new Dictionary<string, int>()},
                {"Dyeclass(es)", new Dictionary<string, int>()},
                {"Worker", new Dictionary<string, int>()},
                {"Material Code", new Dictionary<string, int>()},
                {"Article", new Dictionary<string, int>()}
            };

            foreach (DataRow row in dataTable.Rows)
            {
                string batchValue = row["Batch Status"].ToString();
                if (batchValue == "FAILED")
                {
                    lotesFailed++;

                    foreach (var column in counters.Keys)
                    {
                        string value = row[column].ToString();
                        if (!string.IsNullOrWhiteSpace(value))
                        {
                            if (!counters[column].ContainsKey(value))
                            {
                                counters[column][value] = 0;
                            }
                            counters[column][value]++;
                        }
                    }
                }
                else if (batchValue == "PASSED")
                {
                    lotesPassed++;
                }
            }

            double passedPercentage = (100.0 * lotesPassed) / totalLotes;
            double failedPercentage = (100.0 * lotesFailed) / totalLotes;

            label1.Text = $"Total: {totalLotes} \n\nPassed: {lotesPassed} \n\nFailed: {lotesFailed}";
            label2.Text = $"Passed Percentage: \n{passedPercentage:F2}% \n\nFailed Percentage: \n{failedPercentage:F2}%";

            // Build the output string for label3 with the 3 most common values
            StringBuilder output = new StringBuilder();
            foreach (var column in counters.Keys)
            {
                output.Append($"{column}:   ");
                foreach (var value in counters[column].OrderByDescending(kv => kv.Value).Take(3))
                {
                    
                    
                     output.Append($"{value.Key}({value.Value})      ");
                    
                }
                output.Append("\n\n");
            }
            label3.Text = output.ToString();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //Verifica el estado del Checkbox
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
                checkBox1 .Checked = false;
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Limpiar_Click(object sender, EventArgs e)
        {
            // Limpiar los DataGrid
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;

            // Reiniciar contadores y porcentajes
            totalLotes = 0;
            lotesFailed = 0;
            lotesPassed = 0;
            label1.Text = "Total: 0 \n\nPassed: 0 \n\nFailed: 0";
            label2.Text = "Passed Percentage: \n0.00% \n\nFailed Percentage: \n0.00%";
            label3.Text = string.Empty;

            // Permitir seleccionar otro archivo
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
                var dataTable = LoadExcelData(filePath, filterColumns: checkBox1.Checked, filterFailed: checkBox1.Checked);

                // Asignar el DataTable al DataGrid correspondiente
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

                // Calcular las estadísticas
                CountBatch(dataTable);
            }
        }
    }
}
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
            dataGridView1.CellFormatting += DataGridView1_CellFormatting;


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

            // Opcionalmente, también puedes cambiar el color de fondo o el color del texto de las cabeceras:
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }


        private void SelectDocument_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Archivos de Excel (*.xlsx)|*.xlsx";

            // Muestra el diálogo de apertura de archivo
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                
                // Obtiene la ruta del archivo seleccionado
                filePath = openFileDialog1.FileName;

                // Llama a un método para cargar los datos de Excel
                var dataTable = LoadExcelData(filePath);

                // Asigna el DataTable al DataGridView
                dataGridView1.DataSource = dataTable;

                CountBatch(dataTable);

            }
        }
        //private System.Data.DataTable LoadExcelData(string filePath)
        //{
        //    var dataTable = new System.Data.DataTable();

        //    // Lista de columnas a mostrar si el CheckBox está marcado
        //    var columnsToShow = new List<string>
        //    {
        //        "Batch Id",
        //        "Dyelot Date",
        //        "Orders",
        //        "Substr Code",
        //        "Fibre Type",
        //        "Dyeing Method",
        //        "Recipe Status",
        //        "Machine Name",
        //        "Failure Reason",
        //        "Dyeclass(es)",
        //        "Dye",
        //        "Triangle 1",
        //        "Worker",
        //        "Article",
        //        "Machine In",
        //        "Machine Out"
        //    };


        //    // Abre el archivo Excel y lee las celdas
        //    using (var package = new ExcelPackage(new System.IO.FileInfo(filePath)))
        //    {
        //        var worksheet = package.Workbook.Worksheets[0]; // Lee la primera hoja
        //        var startRow = worksheet.Dimension.Start.Row;
        //        var endRow = worksheet.Dimension.End.Row;
        //        var startCol = worksheet.Dimension.Start.Column;
        //        var endCol = worksheet.Dimension.End.Column;

        //        // Crea las columnas del DataTable
        //        for (int col = startCol; col <= endCol; col++)
        //        {
        //            string columnName = worksheet.Cells[1, col].Text;
        //            if (checkBox1.Checked)
        //            {
        //                if (columnsToShow.Contains(columnName))
        //                {
        //                    dataTable.Columns.Add(columnName);
        //                }
        //            }
        //            else
        //            {
        //                dataTable.Columns.Add(columnName);
        //            }
        //        }

        //        // Rellena las filas del DataTable con los datos del Excel
        //        for (int row = startRow + 1; row <= endRow; row++)
        //        {
        //            var rowData = dataTable.NewRow();
        //            for (int col = startCol; col <= endCol; col++)
        //            {
        //                string columnName = worksheet.Cells[1, col].Text;

        //                if (checkBox1.Checked && columnsToShow.Contains(columnName))
        //                {
        //                    rowData[columnName] = worksheet.Cells[row, col].Text;
        //                }
        //                else if (!checkBox1.Checked)
        //                {
        //                    rowData[columnName] = worksheet.Cells[row, col].Text;
        //                }
        //            }
        //            dataTable.Rows.Add(rowData);
        //        }
        //    }
        //    SelectDocument.Visible = false;
        //    SelectDocument.Enabled = false;
        //    View();



        //    return dataTable;
        //}




        private System.Data.DataTable LoadExcelData(string filePath)
        {
            var dataTable = new System.Data.DataTable();

            // Lista de todas las columnas originales en el orden correcto
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

            // Lista de columnas a mostrar si el CheckBox está marcado
            var columnsToShow = new List<string>
            {
                "Batch Id", "Dyelot Date", "Orders", "Substr Code", "Fibre Type", "Dyeing Method",
                "Recipe Status", "Machine Name", "Failure Reason", "Dyeclass(es)", "Dye", "Triangle 1",
                "Worker", "Article", "Machine In", "Machine Out"
            };
            Console.WriteLine("ColumnToshow: " + columnsToShow);
            // Abre el archivo Excel y lee las celdas
            using (var package = new ExcelPackage(new System.IO.FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Lee la primera hoja
                var startRow = worksheet.Dimension.Start.Row;
                var endRow = worksheet.Dimension.End.Row;
                var startCol = worksheet.Dimension.Start.Column;
                var endCol = worksheet.Dimension.End.Column;

                // Crea las columnas del DataTable en el orden original o las columnas seleccionadas
                foreach (var columnName in originalColumns)
                {
                    if (!checkBox1.Checked || columnsToShow.Contains(columnName))
                    {
                        dataTable.Columns.Add(columnName);
                    }
                }
                // Rellena las filas del DataTable con los datos del Excel
                for (int row = startRow + 1; row <= endRow; row++)
                {

                    var rowData = dataTable.NewRow();
                    for (int col = startCol; col <= endCol; col++)
                    {
                        string columnName = worksheet.Cells[1, col].Text;
                        if (dataTable.Columns.Contains(columnName))
                        {
                            rowData[columnName] = worksheet.Cells[row, col].Text;
                        }
                    }

                    dataTable.Rows.Add(rowData);
                }
            }

            SelectDocument.Visible = false;
            SelectDocument.Enabled = false;
            View();

            return dataTable;
        }



        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            // Verifica que estamos en la columna correcta
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Batch Status")
            {
                // Verifica si el valor de la celda no es nulo y es un número
                if (e.Value != null && e.Value is string valor)
                {
                    // Si el valor es mayor a 100, cambia el fondo de la celda a rojo
                    if (valor == "FAILED")
                    {
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
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
            Console.WriteLine(checkBox1.CheckState);

            if (checkBox1.Checked)
            {
                state = true;
            }else
            {
                state = false;

            }

            if (!string.IsNullOrEmpty(filePath))
            {
                var dataTable = LoadExcelData(filePath);  // Usa la ruta almacenada
                dataGridView1.DataSource = dataTable;     // Actualiza el DataGridView
            }
            else
            {
                MessageBox.Show("No se ha seleccionado un archivo Excel.");
            }

        }
    }
}
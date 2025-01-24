using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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



        public Form1()
        {
            // Establece el contexto de licencia a "NonCommercial" (no comercial)
            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            InitializeComponent();
            openFileDialog1 = new OpenFileDialog();

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }


        private void SelectDocument_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Archivos de Excel (*.xlsx)|*.xlsx";

            // Muestra el diálogo de apertura de archivo
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Obtiene la ruta del archivo seleccionado
                string filePath = openFileDialog1.FileName;

                // Llama a un método para cargar los datos de Excel
                var dataTable = LoadExcelData(filePath);

                Form2 form2 = new Form2(dataTable);
                form2.Show();
                // Aquí puedes agregar código adicional para leer o procesar el archivo

            }
        }
        private System.Data.DataTable LoadExcelData(string filePath)
        {
            var dataTable = new System.Data.DataTable();

            // Abre el archivo Excel y lee las celdas
            using (var package = new ExcelPackage(new System.IO.FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Lee la primera hoja
                var startRow = worksheet.Dimension.Start.Row;
                var endRow = worksheet.Dimension.End.Row;
                var startCol = worksheet.Dimension.Start.Column;
                var endCol = worksheet.Dimension.End.Column;

                // Crea las columnas del DataTable
                for (int col = startCol; col <= endCol; col++)
                {
                    dataTable.Columns.Add(worksheet.Cells[1, col].Text); // Títulos de las columnas
                }

                // Rellena las filas del DataTable con los datos del Excel
                for (int row = startRow + 1; row <= endRow; row++)
                {
                    var rowData = dataTable.NewRow();
                    for (int col = startCol; col <= endCol; col++)
                    {
                        rowData[col - 1] = worksheet.Cells[row, col].Text;
                    }
                    dataTable.Rows.Add(rowData);
                }
            }

            return dataTable;
        }
    }
}

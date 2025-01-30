﻿using System;
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
using System.Windows.Forms.DataVisualization.Charting;

namespace TableAnalizer
{
    public partial class Form1 : Form
    {
        private int totalLotes = 0;
        private int lotesPassed = 0;
        private int lotesFailed = 0;
        private bool state = false;
        private string filePath = string.Empty;
        private int page = 1;

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
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;


            View();
        }

        private void View()
        {
            if (SelectDocument.Visible == Visible)
            {
                label1.Visible = false;
                Limpiar.Visible = false;
            }
            else
            {
                label1.Visible = true;
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
            // Evita que se agreguen archivos que no sean excel
            openFileDialog1.Filter = "Archivos de Excel (*.xlsx)|*.xlsx";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
                var dataTable = LoadExcelData(filePath);

                dataGridView1.DataSource = dataTable;
                dataGridView1.Visible = false;
                dataGridView2.Visible = false;

                CountBatch(dataTable);

                ShowGraphics();
            }
        }
        private void ShowGraphics()
        {
            var dataTable = LoadExcelData(filePath);


            // Filtrar las filas donde el Batch Status sea "FAILED"
            var failedDataTable = dataTable.AsEnumerable()
                .Where(row => row["Batch Status"].ToString() == "FAILED")
                .CopyToDataTable();

            var columnsToShow = new List<string>
            {
                "Shade Name", "Max Colour Diff", "Substr Code", "Count/Ply",
                "Fibre Type", "Dyeing Method", "Recipe Status", "Machine Name", "Machine Vol",
                "Failure Reason", "Dyeclass(es)", "Worker", "Article", "Material Code"
            };

            ShowPieCharts(failedDataTable, columnsToShow);
            
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

            foreach (DataRow row in dataTable.Rows)
            {
                string batchValue = row["Batch Status"].ToString();
                if (batchValue == "FAILED")
                {
                    lotesFailed++;
                }
                else if (batchValue == "PASSED")
                {
                    lotesPassed++;
                }
            }

            double passedPercentage = (100.0 * lotesPassed) / totalLotes;
            double failedPercentage = (100.0 * lotesFailed) / totalLotes;

            label1.Text = $"Total: {totalLotes} \n\nPassed: {lotesPassed} ({passedPercentage:F2}%) \n\nFailed: {lotesFailed} ({failedPercentage:F2}%)";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
            label1.Text = "Total: 0  \n\nPassed: 0 (0.00%) \n\nFailed: 0 (0.00%)";
            label3.Text = string.Empty;

            // Permitir seleccionar otro archivo
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
                var dataTable = LoadExcelData(filePath);

                // Asignar el DataTable al DataGrid correspondiente
                dataGridView1.DataSource = dataTable;
                dataGridView1.Visible = true;
                dataGridView2.Visible = false;

                // Calcular las estadísticas
                CountBatch(dataTable);

                // Regenerar las gráficas
                ShowGraphics();
            }
        }



        private void ShowPieCharts(DataTable dataTable, List<string> columnsToShow )
        {
            var panels = new List<Panel>
            {
                chartPanel1, chartPanel2, chartPanel3, chartPanel4, chartPanel5, chartPanel6, chartPanel7, chartPanel8,
                chartPanel9, chartPanel10, chartPanel11, chartPanel12, chartPanel13, chartPanel14
            };

            var position1 = new Point(0, 3);
            var position2 = new Point(401, 3);
            var position3 = new Point(801, 3);
            var position4 = new Point(2, 427);
            var position5 = new Point(401, 427);
            var defaultPosition = new Point(2000, 2000);



            if (page == 1)
            {
                chartPanel1.Location = position1;
                chartPanel2.Location = position2;
                chartPanel3.Location = position3;
                chartPanel4.Location = position4;
                chartPanel5.Location = position5;
                chartPanel6.Location = defaultPosition;
                chartPanel7.Location = defaultPosition;
                chartPanel8.Location = defaultPosition;
                chartPanel9.Location = defaultPosition;
                chartPanel10.Location = defaultPosition;
                chartPanel11.Location = defaultPosition;
                chartPanel12.Location = defaultPosition;
                chartPanel13.Location = defaultPosition;
                chartPanel14.Location = defaultPosition;


            }
            else if(page == 2)
            {
                chartPanel1.Location = defaultPosition;
                chartPanel2.Location = defaultPosition;
                chartPanel3.Location = defaultPosition;
                chartPanel4.Location = defaultPosition;
                chartPanel5.Location = defaultPosition;
                chartPanel6.Location = position1;
                chartPanel7.Location = position2;
                chartPanel8.Location = position3;
                chartPanel9.Location = position4;
                chartPanel10.Location = position5;
                chartPanel11.Location = defaultPosition;
                chartPanel12.Location = defaultPosition;
                chartPanel13.Location = defaultPosition;
                chartPanel14.Location = defaultPosition;
            }
            else
            {
                chartPanel1.Location = defaultPosition;
                chartPanel2.Location = defaultPosition;
                chartPanel3.Location = defaultPosition;
                chartPanel4.Location = defaultPosition;
                chartPanel5.Location = defaultPosition;
                chartPanel6.Location = defaultPosition;
                chartPanel7.Location = defaultPosition;
                chartPanel8.Location = defaultPosition;
                chartPanel9.Location = defaultPosition;
                chartPanel10.Location = defaultPosition;
                chartPanel11.Location = position1;
                chartPanel12.Location = position2;
                chartPanel13.Location = position3;
                chartPanel14.Location = position4;
            }

            foreach (var panel in panels)
            {
                panel.Controls.Clear(); // Limpiar el contenido del panel
                panel.Size = new System.Drawing.Size(410, 430);
                panel.BringToFront();
            }


            for (int i = 0; i < columnsToShow.Count && i < panels.Count; i++)
            {
                var columnName = columnsToShow[i];
                var chart = new Chart();
                chart.Dock = DockStyle.Fill;
                chart.ChartAreas.Add(new ChartArea());

                // Agregar el título del gráfico
                chart.Titles.Add(columnName);

                panels[i].Controls.Add(chart);

                UpdateChart(dataTable, columnName, chart);
            }
        }

        private void UpdateChart(DataTable dataTable, string columnName, Chart chart)
        {
            chart.Series.Clear();

            var series = new Series
            {
                ChartType = SeriesChartType.Pie,
                IsValueShownAsLabel = true,
                Label = "#VALX (#PERCENT{P0})" // Mostrar nombre y porcentaje
            };

            var data = dataTable.AsEnumerable()
                .Where(row => !row.IsNull(columnName))
                .GroupBy(row => row[columnName].ToString())
                .Select(group => new { Value = group.Key, Count = group.Count() })
                .ToList();

            foreach (var item in data)
            {
                series.Points.AddXY(item.Value, item.Count);
            }

            chart.Series.Add(series);
        }

        private void openDataGridViewButton_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.DataGridView1 = this.dataGridView1;
            form2.DataGridView2 = this.dataGridView2;
            form2.Show();

        }

        private void Next_Click(object sender, EventArgs e)
        {
            if (page <= 2)
            {
                page++;
                ShowGraphics();

            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            if (page >= 2 )
            {
                page--;
                ShowGraphics();

            }
        }
    }
}
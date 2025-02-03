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
        private bool isFromDateSet = false;
        private bool isToDateSet = false;

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
            dataGridView1.Visible = true;
            dataGridView2.Visible = false;


            View();
        }


        //Vistas 
        private void View()
        {
            if (SelectDocument.Visible == Visible)
            {
                Limpiar.Visible = false;
                Next.Visible = false;
                Back.Visible = false;
                dataGridView3.Visible = false;
                openDataGridViewButton.Visible = false;
                label1.Visible = false;
                label2.Visible = false;
                FromDate.Visible = false;
                ToDate.Visible = false;
            }
            else
            {
                Limpiar.Visible = true;
                Next.Visible = true;
                Back.Visible = true;
                dataGridView3.Visible = true;
                openDataGridViewButton.Visible = true;
                label1.Visible = true;
                label2.Visible = true;
                FromDate.Visible = true;
                ToDate.Visible = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e) 
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
            dataGridView3.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView3.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView3.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;

        }


        //Seleccionar documento
        private void SelectDocument_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Archivos de Excel (*.xlsx)|*.xlsx";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
                var dataTable = LoadExcelData(filePath);

                dataGridView1.DataSource = dataTable;
                dataGridView1.Visible = true;
                dataGridView2.Visible = true;

                CountBatch(dataTable);
                ShowGraphics();
            }
        }

        //Definir diagramas
        private void ShowGraphics()
        {
            try
            {
                var dataTable = LoadExcelData(filePath);

                // Filtrar las filas donde el Batch Status sea "FAILED"
                var failedDataTable = dataTable.AsEnumerable()
                    .Where(row => row["Batch Status"].ToString() == "FAILED")
                    .CopyToDataTable();

                var columnsToShow = new List<string>
        {
            "Machine Name", "Total Cheeses", "Fibre Type", "Colour Group", "Substr Code",
            "Count/Ply", "Dyeclass(es)", "Total Dye Conc Stage 1", "Total Dye Conc Stage 2", "Dyeing Method", "Recipe Status",
            "Recipe Type", "Colour Category", "Prescreen User", "Prescreen Procedure Path", "Shift", "Worker"
        };

                ShowPieCharts(failedDataTable, columnsToShow);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al mostrar los gráficos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string FilePath => filePath;


        //Carga los datos de las tablas
        public System.Data.DataTable LoadExcelData(string filePath, bool filterColumns = false, bool filterFailed = false)
        {
            var dataTable = new System.Data.DataTable();
            DateTime fromDate = FromDate.Value;
            DateTime toDate = ToDate.Value;

            // Define las columnas originales
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
        "Fastness Type", "Dyed From", "Comment", "Colour Category", "Article", "Source Dyehouse", "Speedline Priority",
        "Unlevel", "Recipe Version No Minor", "Recipe Version No", "Update Date", "Updated By", "Update Method",
        "Thread Group", "Prescreen User", "Prescreen Procedure Path", "Pass Fail Date", "Order Created", "Colour Group",
        "Delta Hue Descriptor", "Actual Dye Conc 6", "Actual Dye Conc 5", "Actual Dye Conc 4", "Actual Dye Conc 3",
        "Actual Dye Conc 2", "Actual Dye Conc 1"
    };

            // Define las columnas "especiales"
            var columnsToShow = new List<string>
    {
        "Batch Id", "Machine Name", "Total Cheeses", "Fibre Type", "Colour Group", "Batch Status", "Substr Code",
        "Count/Ply", "Dyeclass(es)", "Total Dye Conc Stage 1", "Total Dye Conc Stage 2", "Dyeing Method", "Recipe Status",
        "Recipe Type", "Colour Category", "Prescreen User", "Prescreen Procedure Path", "Shift", "Worker", "Machine Out"
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

                            if (isFromDateSet && isToDateSet && columnName == "Machine Out")
                            {
                                DateTime machineOutDate = DateTime.Parse(worksheet.Cells[row, col].Text);
                                if (machineOutDate < fromDate || machineOutDate > toDate)
                                {
                                    addRow = false;
                                    break;
                                }
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
            try
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
                    else if (batchValue == "PASSED")
                    {
                        lotesPassed++;
                    }
                }

                double passedPercentage = totalLotes > 0 ? (100.0 * lotesPassed) / totalLotes : 0;
                double failedPercentage = totalLotes > 0 ? (100.0 * lotesFailed) / totalLotes : 0;

                DataTable countBatchTable = new DataTable();
                countBatchTable.Columns.Add("Description");
                countBatchTable.Columns.Add("Cant");
                countBatchTable.Columns.Add("Percentage");

                countBatchTable.Rows.Add("Total", totalLotes, "100%");
                countBatchTable.Rows.Add("Passed", lotesPassed, $"{passedPercentage:F2}%");
                countBatchTable.Rows.Add("Failed", lotesFailed, $"{failedPercentage:F2}%");

                dataGridView3.DataSource = countBatchTable;
                dataGridView3.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al contar los lotes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        //Limpia todo al seleccionar otro archivo
        private void Limpiar_Click(object sender, EventArgs e)
        {
            // Limpiar los DataGrid
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;

            // Reiniciar contadores y porcentajes
            totalLotes = 0;
            lotesFailed = 0;
            lotesPassed = 0;

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


        //Mostrar diagramas
        private void ShowPieCharts(DataTable dataTable, List<string> columnsToShow )
        {
            //Define todos los paneles que se van a usar para los diagramas
            var panels = new List<Panel>
            {
                chartPanel1, chartPanel2, chartPanel3, chartPanel4, chartPanel5, chartPanel6, chartPanel7, chartPanel8,
                chartPanel9, chartPanel10, chartPanel11, chartPanel12, chartPanel13, chartPanel14, chartPanel15, chartPanel16, chartPanel17
            };

            var position1 = new Point(0, 3);
            var position2 = new Point(401, 3);
            var position3 = new Point(2, 480);
            var position4 = new Point(401, 480);
            var defaultPosition = new Point(2000, 2000);

            
            //Los acomoda en sus respectivos lugares
            if (page == 1)
            {
                chartPanel1.Location = position1;
                chartPanel2.Location = position2;
                chartPanel3.Location = position3;
                chartPanel4.Location = position4;
                chartPanel5.Location = defaultPosition;
                chartPanel6.Location = defaultPosition;
                chartPanel7.Location = defaultPosition;
                chartPanel8.Location = defaultPosition;
                chartPanel9.Location = defaultPosition;
                chartPanel10.Location = defaultPosition;
                chartPanel11.Location = defaultPosition;
                chartPanel12.Location = defaultPosition;
                chartPanel13.Location = defaultPosition;
                chartPanel14.Location = defaultPosition;
                chartPanel15.Location = defaultPosition;
                chartPanel16.Location = defaultPosition;
                chartPanel17.Location = defaultPosition;


            }
            else if(page == 2)
            {
                chartPanel1.Location = defaultPosition;
                chartPanel2.Location = defaultPosition;
                chartPanel3.Location = defaultPosition;
                chartPanel4.Location = defaultPosition;
                chartPanel5.Location = position1;
                chartPanel6.Location = position2;
                chartPanel7.Location = position3;
                chartPanel8.Location = position4;
                chartPanel9.Location = defaultPosition;
                chartPanel10.Location = defaultPosition;
                chartPanel11.Location = defaultPosition;
                chartPanel12.Location = defaultPosition;
                chartPanel13.Location = defaultPosition;
                chartPanel14.Location = defaultPosition;
                chartPanel15.Location = defaultPosition;
                chartPanel16.Location = defaultPosition;
                chartPanel17.Location = defaultPosition;

            }
            else if( page == 3) 
            {
                chartPanel1.Location = defaultPosition;
                chartPanel2.Location = defaultPosition;
                chartPanel3.Location = defaultPosition;
                chartPanel4.Location = defaultPosition;
                chartPanel5.Location = defaultPosition;
                chartPanel6.Location = defaultPosition;
                chartPanel7.Location = defaultPosition;
                chartPanel8.Location = defaultPosition;
                chartPanel9.Location = position1;
                chartPanel10.Location = position2;
                chartPanel11.Location = position3;
                chartPanel12.Location = position4;
                chartPanel13.Location = defaultPosition;
                chartPanel14.Location = defaultPosition;
                chartPanel15.Location = defaultPosition;
                chartPanel16.Location = defaultPosition;
                chartPanel17.Location = defaultPosition;

            }else if( page == 4) 
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
                chartPanel11.Location = defaultPosition;
                chartPanel12.Location = defaultPosition;
                chartPanel13.Location = position1;
                chartPanel14.Location = position2;
                chartPanel15.Location = position3;
                chartPanel16.Location = position4;
                chartPanel17.Location = defaultPosition;
            } else
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
                chartPanel11.Location = defaultPosition;
                chartPanel12.Location = defaultPosition;
                chartPanel13.Location = defaultPosition;
                chartPanel14.Location = defaultPosition;
                chartPanel15.Location = defaultPosition;
                chartPanel16.Location = defaultPosition;
                chartPanel17.Location = position1;
            }

            foreach (var panel in panels)
            {
                panel.Controls.Clear(); // Limpiar el contenido del panel
                panel.Size = new System.Drawing.Size(410, 480);
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


        //Le da colores y estilos a los diagramas
        private void UpdateChart(DataTable dataTable, string columnName, Chart chart)
        {
            chart.Series.Clear();
            chart.Legends.Clear();  // Clear any existing legends
            chart.Legends.Add(new Legend("Legend"));  // Add a new legend

            var series = new Series
            {
                ChartType = SeriesChartType.Pie,
                IsValueShownAsLabel = true,
                Label = "#PERCENT{P0}" // Mostrar nombre y porcentaje
            };

            var data = dataTable.AsEnumerable()
                .Where(row => !row.IsNull(columnName))
                .GroupBy(row => row[columnName].ToString())
                .Select(group => new { Value = group.Key, Count = group.Count() })
                .ToList();

            double maxPercentage = data.Max(item => (item.Count / (double)dataTable.Rows.Count) * 100);
            bool allEqual = data.All(d => (d.Count / (double)dataTable.Rows.Count) * 100 == maxPercentage);

            // Customize the color palette to include 15 tones
            var pastelColors = new List<Color>
            {
                Color.FromArgb(119, 221, 119),  // Darker Pastel Green
                Color.FromArgb(255, 105, 97),   // Darker Pastel Pink
                Color.FromArgb(255, 179, 71),   // Darker Pastel Orange
                Color.FromArgb(253, 253, 150),  // Darker Pastel Yellow
                Color.FromArgb(119, 158, 203),  // Darker Pastel Blue
                Color.FromArgb(204, 153, 255),  // Darker Pastel Purple
                Color.FromArgb(255, 204, 204),  // Darker Pastel Peach
                Color.FromArgb(255, 255, 204),  // Darker Pastel Lemon
                Color.FromArgb(204, 255, 204),  // Darker Pastel Mint
                Color.FromArgb(204, 229, 255),  // Darker Pastel Sky Blue
                Color.FromArgb(255, 204, 229),  // Darker Pastel Magenta
                Color.FromArgb(255, 229, 204),  // Darker Pastel Coral
                Color.FromArgb(229, 204, 255),  // Darker Pastel Lavender
                Color.FromArgb(204, 255, 229),  // Darker Pastel Aqua
                Color.FromArgb(255, 255, 229)   // Darker Pastel Cream
            };

            for (int i = 0; i < data.Count; i++)
            {
                var item = data[i];
                var point = new DataPoint
                {
                    AxisLabel = item.Value,
                    YValues = new double[] { item.Count },
                    Color = pastelColors[i % pastelColors.Count]
                };

                double percentage = (item.Count / (double)dataTable.Rows.Count) * 100;
                if (!allEqual && percentage == maxPercentage)
                {
                    point["Exploded"] = "true"; // Separar el sector con mayor porcentaje
                    point.Color = Color.FromArgb(135, 206, 250); 
                }

                series.Points.Add(point);
            }

            chart.Series.Add(series);

            foreach (var point in series.Points)
            {
                double percentage = (point.YValues[0] / dataTable.Rows.Count) * 100;
                if (columnName == "Max Colour Diff" || columnName == "Substr Code"
                    || columnName == "Fibre Type" || columnName == "Dyeing Method" || columnName == "Machine Vol")
                {
                    point.Label = $"{point.AxisLabel} {percentage:F0}%";
                    var chartArea = new ChartArea();
                    chart.ChartAreas.Clear();
                    chart.Legends.Clear();
                    chart.ChartAreas.Add(chartArea);
                    chartArea.Area3DStyle.Enable3D = true;
                    chartArea.Position = new ElementPosition(0, 0, 105, 105); // Adjust the position and size
                    chartArea.InnerPlotPosition = new ElementPosition(10, 10, 85, 85); 
                }
                else
                {
                    point.LegendText = $"{point.AxisLabel} ";  
                    var legend = chart.Legends["Legend"];
                    legend.Docking = Docking.Bottom; // Position the legend on the right
                    legend.AutoFitMinFontSize = 5; // Set minimum font size to auto fit
                    legend.Font = new Font("Arial", 8); // Adjust the font size to make it smaller
                    legend.IsTextAutoFit = true;

                    var chartArea = new ChartArea();
                    chart.ChartAreas.Clear();
                    chart.ChartAreas.Add(chartArea);
                    chartArea.Area3DStyle.Enable3D = true; 
                    chartArea.Position = new ElementPosition(0, 0, 95, 95); // Adjust the position and size
                    chartArea.InnerPlotPosition = new ElementPosition(10, 10, 80, 80); 
                }
            }
        }


        //Llamar al Form2 al darle al boton
        private void openDataGridViewButton_Click(object sender, EventArgs e)
        {

            Form2 form2 = new Form2(this);
            form2.DataGridView1 = this.dataGridView1;
            form2.DataGridView2 = this.dataGridView2;
            form2.Show();
        }

        private void Next_Click(object sender, EventArgs e)
        {
            if (page <= 4)
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

        private void FromDate_ValueChanged(object sender, EventArgs e)
        {
            isFromDateSet = true;
            ApplyDateFilterIfReady();
        }

        private void ToDate_ValueChanged(object sender, EventArgs e)
        {
            isToDateSet = true;
            ApplyDateFilterIfReady();
        }

        private void ApplyDateFilterIfReady()
        {
            if (isFromDateSet && isToDateSet)
            {
                var dataTable = LoadExcelData(filePath, filterColumns: true, filterFailed: true);
                dataGridView1.DataSource = dataTable;
                CountBatch(dataTable);
                ShowGraphics();
            }
        }
    }
}
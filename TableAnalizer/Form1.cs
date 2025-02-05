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
        public string FilePath => filePath;

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
                BtnShowStatistics.Visible = false;
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
                BtnShowStatistics.Visible = true;
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

                var dataTable = LoadExcelData(filePath, filterColumns: false, filterFailed: false, applyDateFilter: true);
                dataGridView1.DataSource = dataTable;
                dataGridView1.Visible = false;

                var failedDataTable = LoadExcelData(filePath, filterColumns: false, filterFailed: true, applyDateFilter: true);
                dataGridView2.DataSource = failedDataTable;
                dataGridView2.Visible = false;

                CountBatch(dataTable); // Usar todos los datos para el conteo
                ShowGraphics(failedDataTable); // Mostrar gráficos solo con fallidos
            }
        }


        //Definir diagramas
        private void ShowGraphics(DataTable dataTable)
        {
            try
            {
                // Filtrar las filas donde el Batch Status sea "FAILED"
                var failedDataTable = dataTable.AsEnumerable()
                    .Where(row => row["Batch Status"].ToString() == "FAILED")
                    .CopyToDataTable();

                var columnsToShow = new List<string>
                {
                    "Substr Code", "Count/Ply", "Dyeclass(es)", "Total Dye Conc Stage 2", "Machine Name", "Total Cheeses", "Fibre Type", "Colour Group", 
                    "Dyeing Method", "Recipe Status", "Recipe Type", "Colour Category", "Prescreen User", "Prescreen Procedure Path", 
                    "Shift", "Worker", "Failure Reason"
                };

                ShowPieCharts(failedDataTable, columnsToShow);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al mostrar los gráficos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        //Carga los datos de las tablas
        public System.Data.DataTable LoadExcelData(string filePath, bool filterColumns = false, bool filterFailed = false, bool applyDateFilter = false)
        {
            var dataTable = new System.Data.DataTable();
            DateTime fromDate = FromDate.Value.Date;
            DateTime toDate = ToDate.Value.Date;

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

            var columnsToShow = new List<string>
            {
                "Batch Id", "Machine Name", "Total Cheeses", "Fibre Type", "Colour Group", "Batch Status", "Substr Code",
                "Count/Ply", "Dyeclass(es)", "Total Dye Conc Stage 1", "Total Dye Conc Stage 2", "Dyeing Method", "Recipe Status",
                "Recipe Type", "Colour Category", "Prescreen User", "Prescreen Procedure Path", "Shift", "Worker", "Machine Out", "Failure Reason"
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

                            if (applyDateFilter && columnName == "Machine Out")
                            {
                                DateTime machineOutDate = DateTime.Parse(worksheet.Cells[row, col].Text);
                                if (!(machineOutDate.Date >= fromDate && machineOutDate.Date <= toDate))
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
                ShowGraphics(dataTable);
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
            chartPanel17.Size = new System.Drawing.Size(820, 960);

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
            chart.Legends.Clear();
            chart.Legends.Add(new Legend("Legend"));

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

            var pastelColors = new List<Color>
            {
                Color.FromArgb(119, 221, 119),  // Pastel Green
                Color.FromArgb(174, 198, 207),  // Pastel Blue
                Color.FromArgb(253, 253, 150),  // Pastel Yellow
                Color.FromArgb(207, 207, 255),  // Pastel Purple
                Color.FromArgb(170, 240, 209),  // Pastel Mint
                Color.FromArgb(178, 223, 238),  // Pastel Aqua
                Color.FromArgb(255, 218, 185),  // Pastel Peach
                Color.FromArgb(230, 230, 250),  // Pastel Lavender
                Color.FromArgb(118, 215, 196),  // Pastel Turquoise
                Color.FromArgb(217, 234, 211),  // Pastel Lime
                Color.FromArgb(135, 206, 235),  // Pastel Sky Blue
                Color.FromArgb(255, 182, 193),  // Light Pink
                Color.FromArgb(255, 250, 205),  // Pastel Lemon
                Color.FromArgb(240, 255, 255),  // Pastel Azure
                Color.FromArgb(255, 221, 193),  // Pastel Coral
                Color.FromArgb(153, 204, 204),  // Pastel Teal
                Color.FromArgb(178, 190, 181),  // Pastel Olive
                Color.FromArgb(245, 245, 220),  // Pastel Beige
                Color.FromArgb(255, 255, 240),  // Pastel Ivory
                Color.FromArgb(159, 226, 191)   // Pastel Seafoam
            };

            List<DataPoint> maxPoints = new List<DataPoint>();

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
                    point.Color = Color.FromArgb(255, 136, 2);
                    maxPoints.Add(point);  // Guardar los puntos con el porcentaje máximo para el evento hover
                }

                series.Points.Add(point);
            }

            chart.Series.Add(series);

            ToolTip tooltip = new ToolTip();

            if (maxPoints.Count > 0)
            {
                chart.MouseMove += (sender, e) =>
                {
                    var result = chart.HitTest(e.X, e.Y);
                    if (result.ChartElementType == ChartElementType.DataPoint && maxPoints.Contains(result.Object as DataPoint))
                    {
                        var point = result.Object as DataPoint;
                        tooltip.SetToolTip(chart, $"{point.AxisLabel}  Cantidad: {point.YValues[0]}/{dataTable.Rows.Count}");
                        
                    }
                    else
                    {
                        tooltip.SetToolTip(chart, string.Empty);
                    }
                };
            }

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
                    chartArea.Position = new ElementPosition(0, 0, 105, 105);
                    chartArea.InnerPlotPosition = new ElementPosition(10, 10, 85, 85);
                }
                else
                {
                    point.LegendText = $"{point.AxisLabel} ";
                    var legend = chart.Legends["Legend"];
                    legend.Docking = Docking.Bottom;
                    legend.AutoFitMinFontSize = 5;
                    legend.Font = new Font("Arial", 8);
                    legend.IsTextAutoFit = true;

                    var chartArea = new ChartArea();
                    chart.ChartAreas.Clear();
                    chart.ChartAreas.Add(chartArea);
                    chartArea.Area3DStyle.Enable3D = true;
                    chartArea.Position = new ElementPosition(0, 0, 95, 95);
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

                var failedDataTable = LoadExcelData(filePath, filterColumns: false, filterFailed: true, applyDateFilter: true);
                ShowGraphics(failedDataTable);
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            if (page >= 2 )
            {
                page--;
                var failedDataTable = LoadExcelData(filePath, filterColumns: false, filterFailed: true, applyDateFilter: true);
                ShowGraphics(failedDataTable);

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
                var dataTable = LoadExcelData(filePath, filterColumns: false, filterFailed: false, applyDateFilter: true);
                dataGridView1.DataSource = dataTable;

                var failedDataTable = LoadExcelData(filePath, filterColumns: false, filterFailed: true, applyDateFilter: true);
                dataGridView2.DataSource = failedDataTable;

                CountBatch(dataTable); 
                ShowGraphics(failedDataTable); 
            }
        }




        private void ShowStatisticsMessage(DataTable dataTable)
        {
            try
            {
                // Crear un formulario personalizado para mostrar el mensaje
                void ShowCustomMessageBox(string message)
                {
                    Form form = new Form();
                    Label messageLabel = new Label();
                    Button okButton = new Button();

                    form.Text = "Mensaje";
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.ClientSize = new Size(500, 400);

                    messageLabel.Text = message;
                    messageLabel.Font = new Font("Arial", 14); // Cambia el tamaño de la fuente aquí
                    messageLabel.AutoSize = true;
                    messageLabel.Location = new Point(10, 10);
                    messageLabel.MaximumSize = new Size(480, 0);

                    okButton.Text = "OK";
                    okButton.Size = new Size(80, 30);
                    okButton.Location = new Point(210, 350);
                    okButton.Click += (sender, e) => { form.Close(); };

                    form.Controls.Add(messageLabel);
                    form.Controls.Add(okButton);
                    form.ShowDialog();
                }

                // Calcular los porcentajes y conteos para "Substr Code"
                var substrGroups = dataTable.AsEnumerable()
                    .GroupBy(row => row["Substr Code"].ToString())
                    .Select(group => new
                    {
                        SubstrCode = group.Key,
                        Count = group.Count(),
                        Percentage = (double)group.Count() / dataTable.Rows.Count * 100
                    })
                    .OrderByDescending(x => x.Count)
                    .ToList();

                if (substrGroups.Count > 0)
                {
                    var mostFailedSubstr = substrGroups.First();
                    string message = $"El Substrato que más falló fue: {mostFailedSubstr.SubstrCode} con {mostFailedSubstr.Count} fallos ({mostFailedSubstr.Percentage:F2}%).\n";

                    // Calcular los porcentajes y conteos para "Count/Ply" dentro del Substr que más falló
                    var countPlyGroups = dataTable.AsEnumerable()
                        .Where(row => row["Substr Code"].ToString() == mostFailedSubstr.SubstrCode)
                        .GroupBy(row => row["Count/Ply"].ToString())
                        .Select(group => new
                        {
                            CountPly = group.Key,
                            Count = group.Count(),
                            Percentage = (double)group.Count() / mostFailedSubstr.Count * 100
                        })
                        .OrderByDescending(x => x.Count)
                        .ToList();

                    if (countPlyGroups.Count > 0)
                    {
                        var mostCommonCountPly = countPlyGroups.First();
                        message += $"De esos {mostFailedSubstr.SubstrCode}, {mostCommonCountPly.Count}({mostCommonCountPly.Percentage:F2})% fueron {mostCommonCountPly.CountPly}.\n";

                        // Calcular los porcentajes y conteos para "Fiber Type" dentro del Count/Ply más común
                        var fiberTypeGroups = dataTable.AsEnumerable()
                            .Where(row => row["Substr Code"].ToString() == mostFailedSubstr.SubstrCode && row["Count/Ply"].ToString() == mostCommonCountPly.CountPly)
                            .GroupBy(row => row["Fibre Type"].ToString())
                            .Select(group => new
                            {
                                FiberType = group.Key,
                                Count = group.Count(),
                                Percentage = (double)group.Count() / mostCommonCountPly.Count * 100
                            })
                            .OrderByDescending(x => x.Count)
                            .ToList();

                        if (fiberTypeGroups.Count > 0)
                        {
                            message += "\nDesglose por tipo de fibra:\n\n";
                            foreach (var fiber in fiberTypeGroups)
                            {
                                message += $"  - {fiber.FiberType}: {fiber.Count} ({fiber.Percentage:F2}%)\n";
                            }
                            message += "\n";
                        }

                        // Calcular los porcentajes y conteos para "Dyeing Method" dentro del Count/Ply más común
                        var dyeingMethodGroups = dataTable.AsEnumerable()
                            .Where(row => row["Substr Code"].ToString() == mostFailedSubstr.SubstrCode && row["Count/Ply"].ToString() == mostCommonCountPly.CountPly)
                            .GroupBy(row => row["Dyeing Method"].ToString())
                            .Select(group => new
                            {
                                DyeingMethod = group.Key,
                                Count = group.Count(),
                                Percentage = (double)group.Count() / mostCommonCountPly.Count * 100
                            })
                            .OrderByDescending(x => x.Count)
                            .ToList();

                        if (dyeingMethodGroups.Count > 0)
                        {
                            message += "Desglose por método de teñido:\n\n";
                            foreach (var method in dyeingMethodGroups)
                            {
                                message += $"  - {method.DyeingMethod}: {method.Count} ({method.Percentage:F2}%)\n";
                            }
                            message += "\n";

                        }
                    }

                    ShowCustomMessageBox(message);
                }
                else
                {
                    ShowCustomMessageBox("No hay datos de fallos disponibles.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al calcular las estadísticas detalladas: {ex.Message}");
            }
        }



        private void BtnShowStatistics_Click(object sender, EventArgs e)
        {
            // Obtener los datos del dataGridView2 (que ya están filtrados como erróneos)
            DataTable failedDataTable = dataGridView2.DataSource as DataTable;

            if (failedDataTable != null)
            {
                ShowStatisticsMessage(failedDataTable); // Mostrar estadísticas detalladas
            }
            else
            {
                MessageBox.Show("No hay datos disponibles en dataGridView2.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
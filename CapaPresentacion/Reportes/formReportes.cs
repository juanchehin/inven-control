using CapaNegocio;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CapaPresentacion.Reportes
{
    public partial class formReportes : Form
    {
        CN_Productos objetoCN_productos = new CN_Productos();
        private PrintDocument printDocument1 = new PrintDocument();


        public formReportes()
        {
            InitializeComponent();
            cargar_ingresos_egresos();
            ConfigurarGraficoTorta();
            configurar_ingresos();
            configurar_egresos();
        }

        // =============================
        // Obtiene los ingresos y egresos del mes actual
        // =============================
        private void cargar_ingresos_egresos()
        {
            DataSet data_unidades_ing_egresos = objetoCN_productos.cargar_ingresos_egresos();

            if (data_unidades_ing_egresos.Tables.Count > 0)
            {
                // Iterar sobre las filas de la primera tabla del DataSet

                foreach (DataRow row in data_unidades_ing_egresos.Tables[0].Rows)
                {
                    lbl_egresos_mes.Text = row["cant_egresada"].ToString() + " unidades";
                }

                foreach (DataRow row in data_unidades_ing_egresos.Tables[1].Rows)
                {
                    lbl_ingresos_mes.Text = row["cant_ingresada"].ToString() + " unidades";
                }

            }
        }

        // =============================
        /* entregue los 3 racks con mas productos   */
        // =============================
        private void ConfigurarGraficoTorta()
        {
            // Limpia cualquier serie anterior en el Chart
            ct_torta_racks.Series.Clear();

            // Crea una nueva serie y configura el tipo de gráfico
            Series series = new Series
            {
                Name = "Racks",
                IsValueShownAsLabel = true,
                ChartType = SeriesChartType.Pie
            };

            // Añadir la serie al gráfico
            ct_torta_racks.Series.Add(series);

            DataSet dataRacks = objetoCN_productos.productos_rack();

            // Iterar sobre las filas de la primera tabla del DataSet
            foreach (DataRow row in dataRacks.Tables[0].Rows)
            {
                // Agrega puntos de datos a la serie
                series.Points.AddXY("Rack : " + row["nro_rack"].ToString(), row["Porcentaje"].ToString()); //

            }

            // Opcional: Configura el título del gráfico
            ct_torta_racks.Titles.Clear();
            ct_torta_racks.Titles.Add("Distribución de Productos por Rack");

            // Configura la apariencia del gráfico
            series.ChartType = SeriesChartType.Pie;
            series["PieLabelStyle"] = "Inside";
            series["PieStartAngle"] = "0";

            // Configura el diseño de los puntos de datos
            foreach (DataPoint point in series.Points)
            {
                point.Label = string.Format("{0} ({1:P1})", point.AxisLabel, point.YValues[0] / 100);
            }
        }

        // =============================
        /*  Top 5 productos mas ingresados  */
        // =============================
        private void configurar_ingresos()
        {
            DataSet ingresos_prod = objetoCN_productos.top_ingresos_productos();

            // Limpia cualquier serie anterior en el Chart
            top_ingresos.Series.Clear();

            // Crea una nueva serie para el gráfico de barras
            Series series = new Series
            {
                Name = "Productos",
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true // Mostrar valores en las barras
            };

            // Añadir la serie al Chart
            top_ingresos.Series.Add(series);

            // Iterar sobre las filas de la primera tabla del DataSet
            foreach (DataRow row in ingresos_prod.Tables[0].Rows)
            {
                // Agrega puntos de datos a la serie
                series.Points.AddXY(row["Producto"].ToString(), row["cantidad"].ToString()); //

            }

            // Opcional: Configurar el título del gráfico
            top_ingresos.Titles.Clear();
            top_ingresos.Titles.Add("Productos que mas ingresaron");

            // Configuración adicional de la serie
            series.Color = System.Drawing.Color.Blue;
            series.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);

            // Configurar las propiedades del ChartArea (opcional)
            ChartArea chartArea = top_ingresos.ChartAreas[0];
            chartArea.AxisX.Title = "Productos";
            chartArea.AxisY.Title = "Nro Ingresos";
            chartArea.AxisX.Interval = 1;
        }

        // =============================
        /* Top 5 productos mas egresados  */
        // =============================

        private void configurar_egresos()
        {
            DataSet egresos_prod = objetoCN_productos.top_egresos_productos();

            // Limpia cualquier serie anterior en el Chart
            top_egresos.Series.Clear();

            // Crea una nueva serie para el gráfico de barras
            Series series = new Series
            {
                Name = "Productos",
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true // Mostrar valores en las barras
            };

            // Añadir la serie al Chart
            top_egresos.Series.Add(series);

            // Iterar sobre las filas de la primera tabla del DataSet
            foreach (DataRow row in egresos_prod.Tables[0].Rows)
            {
                // Agrega puntos de datos a la serie
                series.Points.AddXY(row["Producto"].ToString(), row["cantidad"].ToString()); //
            }

            // Opcional: Configurar el título del gráfico
            top_egresos.Titles.Clear();
            top_egresos.Titles.Add("Productos que mas egresaron");

            // Configuración adicional de la serie
            series.Color = System.Drawing.Color.Blue;
            series.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);

            // Configurar las propiedades del ChartArea (opcional)
            ChartArea chartArea = top_egresos.ChartAreas[0];
            chartArea.AxisX.Title = "Productos";
            chartArea.AxisY.Title = "Nro Egresos";
            chartArea.AxisX.Interval = 1;
        }

        private Bitmap bitmapToInsert;


        private void btnPrinter_Click_1(object sender, EventArgs e)
        {
            // Calcula el área del cliente (contenido) excluyendo la barra de título y bordes
            int titleBarHeight = this.RectangleToScreen(this.ClientRectangle).Top - this.Top;
            int borderWidth = (this.Width - this.ClientSize.Width) / 2;

            // Crear un bitmap solo del contenido, excluyendo bordes y barra de título
            Bitmap bitmapToInsert = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            this.DrawToBitmap(bitmapToInsert, new Rectangle(borderWidth, titleBarHeight, this.ClientSize.Width, this.ClientSize.Height));


            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Crear un nuevo paquete de Excel
            using (ExcelPackage package = new ExcelPackage())
            {
                // Crear una hoja de trabajo en el paquete
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Productos");

                // Estilo para las celdas de encabezado
                var headerStyle = worksheet.Cells["A1:F1"].Style;
                headerStyle.Font.Size = 12;
                headerStyle.Font.Bold = true;
                headerStyle.Font.Color.SetColor(Color.Black);
                headerStyle.Fill.PatternType = ExcelFillStyle.Solid;
                headerStyle.Fill.BackgroundColor.SetColor(Color.LightGray);

                // Establecer los encabezados
                worksheet.Cells[1, 1].Value = "Item/Codigo";
                worksheet.Cells[1, 2].Value = "Producto";
                worksheet.Cells[1, 3].Value = "Medida";
                worksheet.Cells[1, 4].Value = "Existencia inicial";
                worksheet.Cells[1, 5].Value = "Stock minimo";
                worksheet.Cells[1, 6].Value = "Stock fisico";

                DataSet dataListadoTodosProductos = objetoCN_productos.ListarTodosProductos();

                if (dataListadoTodosProductos.Tables.Count > 0)
                {
                    int i = 2;
                    foreach (DataRow row in dataListadoTodosProductos.Tables[0].Rows)
                    {
                        worksheet.Cells[i, 1].Value = row["Codigo"].ToString();
                        worksheet.Cells[i, 2].Value = row["Producto"].ToString();
                        worksheet.Cells[i, 3].Value = row["unidad"].ToString();
                        worksheet.Cells[i, 4].Value = row["stock_inicial"].ToString();
                        worksheet.Cells[i, 5].Value = row["stock_alerta"].ToString();
                        worksheet.Cells[i, 6].Value = row["Stock"].ToString();
                        i++;
                    }
                }

                // Ajustar las columnas al contenido
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Insertar la imagen capturada del formulario en el Excel
                if (bitmapToInsert != null)
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        bitmapToInsert.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                        memoryStream.Position = 0;

                        // Insertar la imagen en la hoja de Excel
                        var picture = worksheet.Drawings.AddPicture("FormImage", memoryStream);
                        picture.SetPosition(0, 0, worksheet.Dimension.End.Column, 0); // Ajustar la posición
                        picture.SetSize(700, 400); // Ajustar el tamaño según sea necesario
                    }
                }

                // Mostrar el SaveFileDialog para que el usuario elija la ruta y el nombre del archivo
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Files|*.xlsx";
                saveFileDialog.Title = "Guardar archivo excel";

                string fechaActual = DateTime.Now.ToString("dd-MM-yyyy");
                saveFileDialog.FileName = $"productos-{fechaActual}.xlsx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileInfo fileInfo = new FileInfo(saveFileDialog.FileName);
                    package.SaveAs(fileInfo);
                    MessageBox.Show("Archivo guardado exitosamente en: " + fileInfo.FullName);
                }
            }
        }

    }
}

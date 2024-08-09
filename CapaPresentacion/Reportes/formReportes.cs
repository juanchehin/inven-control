using CapaNegocio;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CapaPresentacion.Reportes
{
    public partial class formReportes : Form
    {
        CN_Productos objetoCN_productos = new CN_Productos();

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
    }
}

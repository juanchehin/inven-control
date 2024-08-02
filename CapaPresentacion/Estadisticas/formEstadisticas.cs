using CapaNegocio;
using System;
using System.Windows.Forms;

namespace CapaPresentacion.Estadisticas
{
    public partial class formEstadisticas : Form
    {
        CN_Estadisticas objetoCN = new CN_Estadisticas();

        public formEstadisticas()
        {
            InitializeComponent();
            this.cargarCBClasificacion();
        }

        private void cargarCBClasificacion()
        {
            cbClasificacion.Items.Add("Productos mas vendidos");
            cbClasificacion.Items.Add("Ventas por Usuario");
            cbClasificacion.Items.Add("Articulos comprados");
            cbClasificacion.Items.Add("Compras a proveedores");
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            var añoInicio = dtpFechaInicio.Value.Year;
            var mesInicio = dtpFechaInicio.Value.Month;
            var diaInicio = dtpFechaInicio.Value.Day;
            var fechaInicio = añoInicio + "-" + mesInicio + "-" + diaInicio;


            var añoFin = dtpFechaFin.Value.Year;
            var mesFin = dtpFechaFin.Value.Month;
            var diaFin = dtpFechaFin.Value.Day;
            var fechaFin = añoFin + "-" + mesFin + "-" + diaFin;

            var opt = cbClasificacion.Text;
            switch (opt)
            {
                case "Productos mas vendidos":
                    dataListadoEstadisticas.DataSource = objetoCN.dameProductosMasVendidos(fechaInicio, fechaFin);
                    break;
                case "Ventas por Usuario":
                    dataListadoEstadisticas.DataSource = objetoCN.dameVentasVendedor(fechaInicio, fechaFin);
                    break;
                case "Articulos comprados":
                    dataListadoEstadisticas.DataSource = objetoCN.dameArticulosComprados(fechaInicio, fechaFin);
                    break;
                case "Compras a proveedores":
                    dataListadoEstadisticas.DataSource = objetoCN.dameComprasProveedor();
                    break;
                default:
                    break;
            }
        }
    }
}

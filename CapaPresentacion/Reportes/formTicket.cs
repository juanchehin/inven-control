using Microsoft.Reporting.WinForms;
using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace CapaPresentacion.Reportes
{
    public partial class formTicket : Form
    {
        DataGridView dataListadoProductos = new DataGridView();
        decimal precio_total;
        public formTicket(int pIdUsuario, int IdCliente, DataGridView pProductos, decimal p_precio_total)
        {
            InitializeComponent();
            this.dataListadoProductos = pProductos;
            this.precio_total = p_precio_total;
        }


        private void formTicket_Load(object sender, EventArgs e)
        {
            var ticketVenta = new ObservableCollection<TicketVenta>();

            foreach (DataGridViewRow dr in this.dataListadoProductos.Rows)
            {
                string producto = dr.Cells[2].Value.ToString();
                string cantidad = dr.Cells[3].Value.ToString();
                decimal precio = Convert.ToDecimal(dr.Cells[5].Value);
                //decimal peso_balanza = Convert.ToDecimal(dr.Cells[6].Value);

                ticketVenta.Add(new TicketVenta { Producto = producto, Cantidad = cantidad, Precio = precio, PrecioTotal = this.precio_total });
            }

            // this.reportViewer1.LocalReport.ReportPath = "../../Reportes/TicketVenta.rdlc";
            // this.reportViewer1.LocalReport.ReportPath = "TicketVenta.rdlc"; // Para publicacion del soft
            // Obtener la ruta del directorio donde se encuentra el ensamblado actual (la clase desde donde se llama)

            // string directorioActual = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            // Construir la ruta completa del archivo "TicketVenta.rdlc"

            /*
            string rutaCompleta = Path.Combine(directorioActual, "TicketVenta.rdlc");
            this.reportViewer1.LocalReport.ReportPath = rutaCompleta;
            */

            this.reportViewer1.LocalReport.ReportPath = "TicketVenta.rdlc"; // Para publicacion del soft


            ReportDataSource source = new ReportDataSource("DataSetTicketVenta", ticketVenta);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(source);
            this.reportViewer1.RefreshReport();

        }
    }
}

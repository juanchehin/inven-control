using CapaNegocio;
using System;
using System.Data;
using System.Windows.Forms;

namespace CapaPresentacion.Caja
{
    public partial class formDetalleVenta : Form
    {
        private int id_venta;
        private string tipo_pago = "";

        DataSet respuesta;
        CN_Ventas objetoCN = new CN_Ventas();


        public formDetalleVenta(int p_id_venta)
        {
            this.id_venta = p_id_venta;
            InitializeComponent();
        }

        private void formDetalleVenta_Load(object sender, EventArgs e)
        {
            listar_detalle_venta(this.id_venta);
        }

        // Carga los valores
        private void listar_detalle_venta(int id_venta)
        {
            if (id_venta <= 0)
            {
                MessageBox.Show("No se selecciono ninguna venta", "InvenControl", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            respuesta = objetoCN.listar_detalle_venta(id_venta);

            dataListadoDetalleVenta.DataSource = respuesta.Tables[0];

            if (respuesta.Tables.Count <= 0)
            {
                MessageBox.Show("No se selecciono ninguna venta", "InvenControl", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {

                if (respuesta.Tables[1].Rows.Count > 0)
                {
                    DataRow firstRow = respuesta.Tables[1].Rows[0];

                    object value = firstRow[0]; // Puedes cambiar el índice 0 por el nombre de la columna si conoces el nombre

                    this.tipo_pago = value.ToString();
                }
            }

            this.lbl_nro_transaccion.Text = "# " + id_venta;

            lblTotalLineasVenta.Text = "Total de Registros: " + Convert.ToString(dataListadoDetalleVenta.Rows.Count);
            lbl_tipo_pago.Text = "Tipo de pago: " + this.tipo_pago;


        }
    }
}

using CapaNegocio;
using System;
using System.Data;
using System.Windows.Forms;

namespace CapaPresentacion.Compras
{
    public partial class formDetalleCompra : Form
    {
        private int id_compra;

        DataSet respuesta;
        CN_Compras objetoCN = new CN_Compras();

        public formDetalleCompra(int p_id_compra)
        {
            this.id_compra = p_id_compra;
            InitializeComponent();
        }

        private void formDetalleCompra_Load(object sender, EventArgs e)
        {
            listar_detalle_compra(this.id_compra);
        }
        // Carga los valores
        private void listar_detalle_compra(int id_compra)
        {
            respuesta = objetoCN.listar_detalle_compra(id_compra);

            dataListadoDetalleCompra.DataSource = respuesta.Tables[0];

            if (respuesta.Tables[1].Rows.Count > 0)
            {
                DataRow firstRow = respuesta.Tables[1].Rows[0];

                object value = firstRow[0]; // Puedes cambiar el índice 0 por el nombre de la columna si conoces el nombre

            }
            else
            {
                Console.WriteLine("La tabla está vacía");
            }

            this.lbl_nro_transaccion.Text = "# " + id_compra;

            lblTotalLineasCompra.Text = "Total de Registros: " + Convert.ToString(dataListadoDetalleCompra.Rows.Count);


        }

    }
}

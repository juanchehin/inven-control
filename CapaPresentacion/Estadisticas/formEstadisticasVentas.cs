using System;
using System.Windows.Forms;

using CapaNegocio;

namespace CapaPresentacion
{
    public partial class formEstadisticasVentas : Form
    {
        CN_Ventas objetoCN = new CN_Ventas();

        private int IdVenta;


        public formEstadisticasVentas()
        {
            InitializeComponent();
            MostrarVentas();
        }


        public void MostrarVentas()
        {
            dataListadoVentas.DataSource = objetoCN.MostrarVentas();
            dataListadoVentas.Columns[0].Visible = false;
            lblTotalVentas.Text = "Total de Registros: " + Convert.ToString(dataListadoVentas.Rows.Count);
        }


        //Mostrar Mensaje de Confirmación generico
        private void MensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje, "InvenControl", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnNuevaVenta_Click(object sender, EventArgs e)
        {
            //formNuevaVenta frm = new formNuevaVenta(this.IdVenta, true);
            //frm.MdiParent = this.MdiParent;
            //frm.Show();
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Realmente Desea Eliminar la venta", "InvenControl", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (Opcion == DialogResult.OK)
                {
                    CN_Ventas.Eliminar(this.IdVenta);
                }
                this.MensajeOk("Se elimino de forma correcta el registro");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
            this.MostrarVentas();
        }

        private void botonEditarListado_Click_1(object sender, EventArgs e)
        {
            //formNuevaVenta frm = new formNuevaVenta(this.IdVenta, false);
            //frm.MdiParent = this.MdiParent;
            //frm.Show();
        }

        private void dataListadoVentas_SelectionChanged(object sender, EventArgs e)
        {
            if (dataListadoVentas.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataListadoVentas.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataListadoVentas.Rows[selectedrowindex];
                this.IdVenta = Convert.ToInt32(selectedRow.Cells["IdVenta"].Value);
            }
        }

        private void formVentas_Load(object sender, EventArgs e)
        {
            this.MostrarVentas();
        }

        private void btnRefrescar_Click_1(object sender, EventArgs e)
        {
            this.MostrarVentas();
            this.Refresh();
        }
    }
}

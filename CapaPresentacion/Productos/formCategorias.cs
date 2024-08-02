using CapaNegocio;
using System;
using System.Windows.Forms;

namespace CapaPresentacion.Productos
{
    public partial class formCategorias : Form
    {
        CN_Productos objetoCN = new CN_Productos();
        int IdCategoria;
        public formCategorias()
        {
            InitializeComponent();
        }

        private void formCategorias_Load(object sender, EventArgs e)
        {
            MostrarCategorias();
        }
        public void MostrarCategorias()
        {
            dataListadoCategorias.DataSource = objetoCN.DameCategorias();
            lblTotalProductos.Text = "Total de Registros: " + Convert.ToString(dataListadoCategorias.Rows.Count);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BuscarCategoria();
        }

        private void botonEditarListado_Click(object sender, EventArgs e)
        {
            formNuevoEditarCategoria frm = new formNuevoEditarCategoria(this.IdCategoria, false);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void BuscarCategoria()
        {
            this.dataListadoCategorias.DataSource = objetoCN.BuscarCategoria(this.txtBuscar.Text);
            lblTotalProductos.Text = "Total de Registros: " + Convert.ToString(dataListadoCategorias.Rows.Count);
        }


        //Mostrar Mensaje de Confirmación
        private void MensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje, "InvenControl", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }


        //Mostrar Mensaje de Error
        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "InvenControl", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnNuevaCategoria_Click(object sender, EventArgs e)
        {
            formNuevoEditarCategoria frm = new formNuevoEditarCategoria(this.IdCategoria, true);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void btnRefrescar_Click_1(object sender, EventArgs e)
        {
            this.MostrarCategorias();
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Realmente Desea Eliminar la categoria", "InvenControl", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (Opcion == DialogResult.OK)
                {
                    if(CN_Productos.EliminarCategoria(this.IdCategoria).Equals("Ok"))
                    {
                        this.MensajeOk("Se elimino de forma correcta el producto");
                        this.MostrarCategorias();
                    }
                    else
                    {
                        this.MensajeError("Ocurrio un problema al eliminar");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void dataListadoCategorias_SelectionChanged_1(object sender, EventArgs e)
        {

            if (dataListadoCategorias.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataListadoCategorias.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataListadoCategorias.Rows[selectedrowindex];
                this.IdCategoria = Convert.ToInt32(selectedRow.Cells["IdCategoria"].Value);
            }
        }
    }
}

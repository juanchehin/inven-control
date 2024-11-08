using CapaNegocio;
using System;
using System.Data;
using System.Windows.Forms;

namespace CapaPresentacion.Compras
{
    public partial class formCompras : Form
    {
        CN_Compras objeto_compras = new CN_Compras();
        DataSet transacciones_ingresos;

        int IdUsuario;
        string Usuario;
        int desde = 0;
        string total_ingresos;
        int p_id_compra;


        public formCompras(int IdUsuario, string Usuario)
        {
            InitializeComponent();
            this.IdUsuario = IdUsuario;
            this.Usuario = Usuario;
            listarIngresos();
        }

        private void listarIngresos()
        {
            var añoInicio = dtFechaInicio.Value.Year;
            var mesInicio = dtFechaInicio.Value.Month;
            var diaInicio = dtFechaInicio.Value.Day;
            var fechaInicio = añoInicio + "-" + mesInicio + "-" + diaInicio;


            var añoFin = dtFechaFin.Value.Year;
            var mesFin = dtFechaFin.Value.Month;
            var diaFin = dtFechaFin.Value.Day;
            var fechaFin = añoFin + "-" + mesFin + "-" + diaFin;

            transacciones_ingresos = objeto_compras.listar_ingresos(this.desde, fechaInicio, fechaFin);

            dataListadoIngresos.DataSource = transacciones_ingresos.Tables[0];

            if (transacciones_ingresos.Tables[1].Rows.Count > 0)
            {
                DataRow firstRow = transacciones_ingresos.Tables[1].Rows[0];

                object value = firstRow[0];

                this.total_ingresos = value.ToString();
                lblTotalVentas.Text = "Total de registros : " + this.total_ingresos;
            }
            else
            {
                Console.WriteLine("La tabla está vacía");
            }

        }

        private void btnNuevaCompra_Click(object sender, EventArgs e)
        {
            formNuevaCompra frm = new formNuevaCompra(this.IdUsuario, this.Usuario);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void dtFechaInicio_ValueChanged(object sender, EventArgs e)
        {
            listarIngresos();
        }

        private void dtFechaFin_ValueChanged(object sender, EventArgs e)
        {
            listarIngresos();
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            listarIngresos();
        }

        private void btn_detalle_compra_Click(object sender, EventArgs e)
        {
            formDetalleCompra frm = new formDetalleCompra(this.p_id_compra);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void dataListadoIngresos_SelectionChanged(object sender, EventArgs e)
        {
            if (dataListadoIngresos.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataListadoIngresos.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataListadoIngresos.Rows[selectedrowindex];
                this.p_id_compra = Convert.ToInt32(selectedRow.Cells["IdTransaccion"].Value);
            }
        }
    }
}

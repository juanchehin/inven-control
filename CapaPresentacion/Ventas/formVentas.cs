using CapaNegocio;
using CapaPresentacion.Caja;
using System;
using System.Data;
using System.Windows.Forms;

namespace CapaPresentacion.Ventas
{
    public partial class formVentas : Form
    {
        CN_Ventas objeto_ventas = new CN_Ventas();
        DataSet transacciones;

        int IdUsuario;
        string Usuario;
        int desde = 0;
        string total_ventas;
        int p_id_venta;
        int p_id_tipo_pago_seleccionado = 0;

        public formVentas(int IdUsuario,string Usuario)
        {
            InitializeComponent();

            this.KeyDown += formVentas_KeyDown;
            this.KeyPreview = true;

            this.IdUsuario = IdUsuario;
            this.Usuario = Usuario;
            llenar_cb_tipos_pago();
            listarTransacciones();
        }

        private void btnNuevaVenta_Click_1(object sender, EventArgs e)
        {
            formNuevaVenta frm = new formNuevaVenta(this.IdUsuario, this.Usuario);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void listarTransacciones()
        {
            var añoInicio = dtFechaInicio.Value.Year;
            var mesInicio = dtFechaInicio.Value.Month;
            var diaInicio = dtFechaInicio.Value.Day;
            var fechaInicio = añoInicio + "-" + mesInicio + "-" + diaInicio;


            var añoFin = dtFechaFin.Value.Year;
            var mesFin = dtFechaFin.Value.Month;
            var diaFin = dtFechaFin.Value.Day;
            var fechaFin = añoFin + "-" + mesFin + "-" + diaFin;

            transacciones = objeto_ventas.listarTransacciones(this.desde, fechaInicio, fechaFin,this.p_id_tipo_pago_seleccionado);

            dataListadoVentas.DataSource = transacciones.Tables[0];

            if (transacciones.Tables[1].Rows.Count > 0)
            {
                DataRow firstRow = transacciones.Tables[1].Rows[0];

                object value = firstRow[0];

                this.total_ventas = value.ToString();
                lblTotalVentas.Text = "Total de registros : " + this.total_ventas;
            }
            else
            {
                Console.WriteLine("La tabla está vacía");
            }

            //
            if (transacciones.Tables[2].Rows.Count > 0)
            {
                DataRow firstRow = transacciones.Tables[2].Rows[0];

                object value = firstRow[0];

                if (!string.IsNullOrWhiteSpace(value.ToString()) && (this.p_id_tipo_pago_seleccionado == 1 || this.p_id_tipo_pago_seleccionado == 0))
                {
                    lbl_efectivo.Text = "Ventas efectivo : $" + value.ToString();

                }
                else
                {
                    lbl_efectivo.Text = "Ventas efectivo : $ 0";
                }

            }
            else
            {
                Console.WriteLine("La tabla está vacía");
            }

            //
            if (transacciones.Tables[3].Rows.Count > 0)
            {
                DataRow firstRow = transacciones.Tables[3].Rows[0];

                object value = firstRow[0];

                if (!string.IsNullOrWhiteSpace(value.ToString()) && (this.p_id_tipo_pago_seleccionado == 2 || this.p_id_tipo_pago_seleccionado == 0))
                {
                    lbl_tarjeta.Text = "Ventas tarjeta : $" + value.ToString();

                }
                else
                {
                    lbl_tarjeta.Text = "Ventas tarjeta : $ 0";
                }

            }
            else
            {
                Console.WriteLine("La tabla está vacía");
            }

            //
            if (transacciones.Tables[4].Rows.Count > 0)
            {
                DataRow firstRow = transacciones.Tables[4].Rows[0];

                object value = firstRow[0];

                if (!string.IsNullOrWhiteSpace(value.ToString()) && (this.p_id_tipo_pago_seleccionado == 3 || this.p_id_tipo_pago_seleccionado == 0))
                {
                    lbl_mp.Text = "Ventas mp : $" + value.ToString();

                }
                else
                {
                    lbl_mp.Text = "Ventas mp : $ 0";
                }

            }
            else
            {
                Console.WriteLine("La tabla está vacía");
            }

            //
            if (transacciones.Tables[5].Rows.Count > 0)
            {
                DataRow firstRow = transacciones.Tables[5].Rows[0];

                object value = firstRow[0];

                if (!string.IsNullOrWhiteSpace(value.ToString()) && (this.p_id_tipo_pago_seleccionado == 4 || this.p_id_tipo_pago_seleccionado == 0))
                {
                    lbl_transferencia.Text = "Ventas transferencia : $" + value.ToString();

                }
                else
                {
                    lbl_transferencia.Text = "Ventas transferencia : $ 0";
                }
            }
            else
            {
                Console.WriteLine("La tabla está vacía");
            }

        }

        private void llenar_cb_tipos_pago()
        {
            cbTiposPago.Items.Add("Todos");
            cbTiposPago.Items.Add("Efectivo");
            cbTiposPago.Items.Add("Tarjeta");
            cbTiposPago.Items.Add("Mercado pago");
            cbTiposPago.Items.Add("Transferencia");

            cbTiposPago.SelectedItem = "Todos";

        }

        private void dtFechaFin_ValueChanged(object sender, EventArgs e)
        {
            listarTransacciones();
        }

        private void dtFechaInicio_ValueChanged(object sender, EventArgs e)
        {
            listarTransacciones();

        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if ((desde + 20) >= Convert.ToInt32(this.total_ventas))
            {
                return;
            }

            if (desde < 0)
            {
                return;
            }

            this.desde += 20;
            listarTransacciones();
        }

        private void btn_detalle_venta_Click(object sender, EventArgs e)
        {
            if((p_id_venta == null) || p_id_venta.Equals("") || (p_id_venta <= 0))
            {
                MessageBox.Show("No se selecciono ninguna venta", "InvenControl", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            formDetalleVenta frm = new formDetalleVenta(this.p_id_venta);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if ((desde - 20) >= Convert.ToInt32(this.total_ventas))
            {
                return;
            }

            if (desde < 0)
            {
                return;
            }

            this.desde -= 20;
            listarTransacciones();
        }

        private void dataListadoCaja_SelectionChanged(object sender, EventArgs e)
        {
            if (dataListadoVentas.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataListadoVentas.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataListadoVentas.Rows[selectedrowindex];
                this.p_id_venta = Convert.ToInt32(selectedRow.Cells["IdTransaccion"].Value);
            }
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            dtFechaInicio.Value = DateTime.Today;
            dtFechaFin.Value = DateTime.Today;

            this.desde = 0;
            listarTransacciones();
        }

        private void cbTiposPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el valor seleccionado
            string valorSeleccionado = cbTiposPago.SelectedItem.ToString();

            switch (valorSeleccionado)
            {
                case "Efectivo":
                    this.p_id_tipo_pago_seleccionado = 1;
                    break;
                case "Tarjeta":
                    this.p_id_tipo_pago_seleccionado = 2;
                    break;
                case "Mercado pago":
                    this.p_id_tipo_pago_seleccionado = 3;
                    break;
                case "Transferencia":
                    this.p_id_tipo_pago_seleccionado = 5;
                    break;
                default:
                    this.p_id_tipo_pago_seleccionado = 0;
                    break;
            }

            listarTransacciones();

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Realmente Desea Eliminar la venta", "InvenControl", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (Opcion == DialogResult.OK)
                {
                    if ((p_id_venta == null) || p_id_venta.Equals("") || (p_id_venta <= 0))
                    {
                        MessageBox.Show("No se selecciono ninguna venta", "InvenControl", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    formValidacionEliminacionVenta frm = new formValidacionEliminacionVenta(p_id_venta);
                    frm.MdiParent = this.MdiParent;
                    frm.Show();

                    //CN_Ventas.Eliminar(p_id_venta);
                    listarTransacciones();

                    //MessageBox.Show("Se elimino de forma correcta la venta", "InvenControl", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void formVentas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2 && !e.Handled)
            {
                // Llamar directamente al método que quieres ejecutar en lugar de usar PerformClick
                BotonClickManual();

                // Marcar el evento como manejado para que no se procese de nuevo
                e.Handled = true;
            }
        }

        private void BotonClickManual()
        {
            formNuevaVenta frm = new formNuevaVenta(this.IdUsuario, this.Usuario);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }
    }
}

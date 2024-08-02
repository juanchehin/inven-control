using CapaNegocio;
using System;
using System.Data;
using System.Windows.Forms;

namespace CapaPresentacion.Gastos
{
    public partial class formGastos : Form
    {
        CN_Gastos objeto_gastos = new CN_Gastos();
        DataSet transacciones;
        int desde = 0;
        string total_gastos = "0";
        string suma_gastos = "0";

        public formGastos()
        {
            InitializeComponent();
            listar_gastos_paginado();
        }

        private void btnNuevoGasto_Click(object sender, EventArgs e)
        {
            formNuevoGasto frm = new formNuevoGasto();
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void listar_gastos_paginado()
        {
            var añoInicio = dtFechaInicio.Value.Year;
            var mesInicio = dtFechaInicio.Value.Month;
            var diaInicio = dtFechaInicio.Value.Day;
            var fechaInicio = añoInicio + "-" + mesInicio + "-" + diaInicio;


            var añoFin = dtFechaFin.Value.Year;
            var mesFin = dtFechaFin.Value.Month;
            var diaFin = dtFechaFin.Value.Day;
            var fechaFin = añoFin + "-" + mesFin + "-" + diaFin;

            transacciones = objeto_gastos.listar_gastos_paginado(this.desde, fechaInicio, fechaFin);

            dataListadoVentas.DataSource = transacciones.Tables[0];

            if (transacciones.Tables[1].Rows.Count > 0)
            {
                DataRow firstRow = transacciones.Tables[1].Rows[0];

                object value = firstRow[0];

                this.total_gastos = value.ToString();
                lblTotalVentas.Text = "Total de registros : " + this.total_gastos;
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

                this.suma_gastos = value.ToString();
                lblMontoTotal.Text = "Monto total: $" + this.suma_gastos;
            }
            else
            {
                Console.WriteLine("La tabla está vacía");
            }

        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            this.desde = 0;
            listar_gastos_paginado();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if ((desde + 20) >= Convert.ToInt32(this.total_gastos))
            {
                return;
            }

            if (desde < 0)
            {
                return;
            }

            this.desde += 20;
            this.listar_gastos_paginado();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (desde <= 0)
            {
                return;
            }

            this.desde -= 20;
            this.listar_gastos_paginado();
        }

        private void dtFechaFin_ValueChanged(object sender, EventArgs e)
        {
            this.listar_gastos_paginado();
        }

        private void dtFechaInicio_ValueChanged(object sender, EventArgs e)
        {
            this.listar_gastos_paginado();
        }
    }
}

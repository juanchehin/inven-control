using CapaDatos;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Clientes
{
    public partial class formCuentasCorrientes : Form
    {
        int id_cliente;
        string apellido;
        string nombre;
        string dni;

        DataSet dsCuentas = new DataSet();
        CN_Clientes objetoCN = new CN_Clientes();
        int desde = 0;
        int totalMovimientos = 0;
        decimal saldo = 0;

        public formCuentasCorrientes(int p_id_cliente)
        {
            this.id_cliente = p_id_cliente;
            

            InitializeComponent();

            
            cargar_movimientos_cuenta();
        }

        private void cargar_movimientos_cuenta()
        {
            dsCuentas = objetoCN.cargar_movimientos_cuenta(id_cliente, this.desde);

            dataListadoCC.DataSource = dsCuentas.Tables[1];

            saldo = Convert.ToDecimal(dsCuentas.Tables[3].Rows[0][0]);
            totalMovimientos = Convert.ToInt32(dsCuentas.Tables[2].Rows[0][0]);

            this.apellido = Convert.ToString(dsCuentas.Tables[0].Rows[0][0]);
            this.nombre = Convert.ToString(dsCuentas.Tables[0].Rows[0][1]);
            this.dni = Convert.ToString(dsCuentas.Tables[0].Rows[0][2]);

            lblApellidNombre.Text = apellido + ", " + nombre + " - DNI: " + dni;

            // dataListadoCC.Columns["IdPersona"].Visible = false;

            lblTotalMovimientosCC.Text = "Total de Registros : " + totalMovimientos.ToString();
            lblSaldo.Text = "Saldo : " + saldo.ToString();
        }

        private void btnNuevoDeposito_Click(object sender, EventArgs e)
        {
            formNuevoDeposito frm = new formNuevoDeposito(this.id_cliente);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            this.desde = 0;
            this.cargar_movimientos_cuenta();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if ((desde + 20) >= Convert.ToInt32(lblTotalMovimientosCC))
            {
                return;
            }

            if (desde < 0)
            {
                return;
            }

            this.desde += 20;
            this.cargar_movimientos_cuenta();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (desde <= 0)
            {
                return;
            }

            this.desde -= 20;
            this.cargar_movimientos_cuenta();
        }
    }
}

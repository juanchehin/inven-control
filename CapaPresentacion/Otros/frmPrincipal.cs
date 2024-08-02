using CapaNegocio;
using CapaPresentacion.Caja;
using CapaPresentacion.Compras;
using CapaPresentacion.Estadisticas;
using CapaPresentacion.Gastos;
using CapaPresentacion.Otros;
using CapaPresentacion.Pedidos;
using CapaPresentacion.Ventas;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmPrincipal : Form
    {
        string Usuario;
        int IdRol;
        int IdUsuario;

        CN_Configuraciones objetoCN_configuraciones = new CN_Configuraciones();
        DataTable resp;
        string rutaImagen = "";

        public frmPrincipal(int IdUsuario,string usuario,int IdRol)
        {
            InitializeComponent();
            this.Usuario = usuario;
            lblUsuario.Text = usuario;
            this.IdRol = IdRol;
            this.IdUsuario = IdUsuario;
            cargarDatosEmpresa();
            // Chequear permisos y ocultar botones
            if (this.IdRol != 1) // ¿Es admin?
            {
                this.btnUsuarios.Visible = false;
                this.lblUsuarios.Visible = false;
                this.btnEstadisticas.Visible = false;
                this.lblEstadisticas.Visible = false;
            }
        }

        private void cargarDatosEmpresa()
        {
            resp = objetoCN_configuraciones.dameDatosEmpresa();

            foreach (DataRow row in resp.Rows)
            {
                // pbImagenEmpresa.ImageLocation = Convert.ToString(row["imagen"]);
                // this.rutaImagen = Convert.ToString(row["imagen"]);
            }

            if (!Directory.Exists(this.rutaImagen))
            {
                //pbImagenEmpresa.Image = Properties.Resources.store;
            }
        }


        private void txtSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }


        private void btnCalculadora_Click(object sender, EventArgs e)
        {
            formCalculadora frm = new formCalculadora();
            frm.Show();
        }

        // **
        private void btnProductos_Click(object sender, EventArgs e)
        {
            formProductos frm = new formProductos();
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            formClientes frm = new formClientes();
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void btnProveedores_Click_1(object sender, EventArgs e)
        {
            formProveedores frm = new formProveedores();
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void btnCompras_Click_1(object sender, EventArgs e)
        {
            formCompras frm = new formCompras(this.IdUsuario, this.Usuario);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void btnVentas_Click_1(object sender, EventArgs e)
        {
            formVentas frm = new formVentas(this.IdUsuario, this.Usuario);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void btnConfiguracion_Click_1(object sender, EventArgs e)
        {
            formConfiguraciones frm = new formConfiguraciones();
            frm.Show();
        }

        private void btnEstadisticas_Click_1(object sender, EventArgs e)
        {
            formEstadisticas frm = new formEstadisticas();
            frm.Show();
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            formUsuarios frm = new formUsuarios();
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void btnAyuda_Click(object sender, EventArgs e)
        {
            formInformacion frm = new formInformacion();
            frm.Show();
        }

        private void btnPedidos_Click(object sender, EventArgs e)
        {
            formPedidos frm = new formPedidos();
            frm.Show();
        }

        private void formGasto_Click(object sender, EventArgs e)
        {
            formGastos frm = new formGastos();
            frm.Show();
        }
    }
}

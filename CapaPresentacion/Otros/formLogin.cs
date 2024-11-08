using CapaPresentacion.Otros;
using System;
using System.Data;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class formLogin : Form
    {
        string usuario;
        public int IdUsuario;
        int IdRol;
        DataTable datos;
        string activado = "";
        public formLogin()
        {
            // ValidarConexion();
            InitializeComponent();
            txtUsuario.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            this.activado = CapaNegocio.CN_Usuarios.chequear_activacion();

            if (this.activado != "Ok")
            {
                formActivacion frm5 = new formActivacion();
                frm5.MdiParent = this.MdiParent;
                frm5.Show();

                // MessageBox.Show("Programa no activado o periodo de prueba caduco, contactese con el administrador", "Estetica", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                this.datos = CapaNegocio.CN_Usuarios.Login(this.txtUsuario.Text, this.txtPassword.Text);
                //Evaluar si existe el Usuario
                if (this.datos.Rows[0][0].ToString() != "Ok")
                {
                    MessageBox.Show("Error de login", "InvenControl", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    this.IdUsuario = Convert.ToInt32(this.datos.Rows[0][1].ToString());
                    this.IdRol = Convert.ToInt32(this.datos.Rows[0][3].ToString());
                    this.usuario = this.datos.Rows[0][2].ToString();

                    frmPrincipal frm = new frmPrincipal(this.IdUsuario, this.usuario, this.IdRol);
                    frm.Show();
                    this.Hide();
                }
            }
        }

        private void formLogin_Load(object sender, EventArgs e)
        {
            txtUsuario.Select();
            // ValidarConexion();
        }

        private void ValidarConexion()
        {
            if (!CapaNegocio.CN_Configuraciones.testConexion())
            {
                instalarServidorScripts();
            }
        }

        private void instalarServidorScripts()
        {
            try
            {
                // instalar mysql.exe
                System.Diagnostics.Process.Start("mysql-installer.exe");
                // ejecutar scripts
                if (CapaNegocio.CN_Configuraciones.ejecutarScript() == "Ok")
                {
                    MessageBox.Show("Instalacion correcta, vuelva a abrir el programa", "InvenControl", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ocurrio un error, contactese con el administrador", "InvenControl", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Ocurrio un error, contactese con el administrador", "InvenControl", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            formInformacion frm = new formInformacion();
            frm.Show();
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            this.ttAyuda.SetToolTip(btnAyuda, "Ayuda");
        }
    }
}

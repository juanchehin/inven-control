using CapaNegocio;
using System;
using System.Windows.Forms;

namespace CapaPresentacion.Movimientos
{
    public partial class formMisVentas : Form
    {
        int IdEmpleado; // Traer de formLogin
        CN_Productos objetoCN = new CN_Productos();
        public formMisVentas()
        {
            InitializeComponent();
            var instanciaLogin = new formLogin();
            //anInstanceofMyClass.TestCall();

            this.IdEmpleado = instanciaLogin.IdUsuario;
        }

        private void formMisVentas_Load(object sender, EventArgs e)
        {
            MostrarMisVentas();
        }

        public void MostrarMisVentas()
        {
            //dgvMisVentas.DataSource = objetoCN.MostrarMisVentas();
        }
    }
}

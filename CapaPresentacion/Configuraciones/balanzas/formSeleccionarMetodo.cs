using System;
using System.Windows.Forms;

namespace CapaPresentacion.Configuraciones.balanzas
{
    public partial class formSeleccionarMetodo : Form
    {
        public formSeleccionarMetodo()
        {
            InitializeComponent();
        }

        private void btnEnviarRecibir_Click(object sender, EventArgs e)
        {
            formEnviarRecibir frm = new formEnviarRecibir();
            frm.Show();
        }

        private void btnLeerDatos_Click(object sender, EventArgs e)
        {
            formRecibir frm = new formRecibir();
            frm.Show();
        }

        private void btnSystel_Click(object sender, EventArgs e)
        {
            formSystelCuora frm = new formSystelCuora();
            frm.Show();
        }

        private void btnConfigBalanza_Click(object sender, EventArgs e)
        {
            formConfiguracionValores frm = new formConfiguracionValores();
            frm.Show();
        }
    }
}

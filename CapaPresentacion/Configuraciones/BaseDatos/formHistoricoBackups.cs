using System;
using System.Windows.Forms;

namespace CapaPresentacion.Configuraciones.BaseDatos
{
    public partial class formHistoricoBackups : Form
    {
        public formHistoricoBackups()
        {
            InitializeComponent();
        }

        private void btnNuevoBackup_Click(object sender, EventArgs e)
        {
            formBackup frm = new formBackup();
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }
    }
}

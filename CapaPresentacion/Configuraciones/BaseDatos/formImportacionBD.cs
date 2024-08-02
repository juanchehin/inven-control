using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CapaPresentacion.Configuraciones
{
    public partial class formImportacionBD : Form
    {
        public formImportacionBD()
        {
            InitializeComponent();
            panelCargando.Visible = false;
        }

        private void txtRuta_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "SQL Files|*.sql";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtRuta.Text = openFileDialog.FileName;
            }
        }

        private void btnImportacion_Click(object sender, EventArgs e)
        {
            string message = "Esta operacion reemplazara los datos cargados actualmente. ¿Desea continuar?";
            string caption = "Advertencia";

            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            
            if (!string.IsNullOrEmpty(txtRuta.Text))
            {
                if (!File.Exists(txtRuta.Text))
                {
                    MensajeError("Archivo inexistente");
                    return;
                }

                //
                result = MessageBox.Show(message, caption, buttons);
                if (result == DialogResult.No)
                {
                    this.Close();
                }

                // 
                panelCargando.Visible = true;
                this.executa();
                this.Close();
                panelCargando.Visible = false;
            }
            else
            {
                MessageBox.Show("Selecciona una ruta donde se encuentra la copia de seguridad", "InvenControl", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRuta.Focus();
                txtRuta.BackColor = Color.FromArgb(255, 255, 192);
            }
        }

        public void executa()
        {
            

            string ruta_completa = txtRuta.Text;

            try
            {
                if (CapaNegocio.CN_Configuraciones.Restore(ruta_completa) == "Ok")
                {
                    MensajeOk("Importacion exitosa");
                }
            }
            catch (Exception ex)
            {
                MensajeError(ex.Message);
            }
        }

        private void MensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje, "InvenControl", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        //Mostrar Mensaje de Error
        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "InvenControl", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

using CapaNegocio;
using System;
using System.Data;
using System.Windows.Forms;

namespace CapaPresentacion.Configuraciones.Empresa
{
    public partial class formEmpresa : Form
    {
        CN_Configuraciones objetoCN_configuraciones = new CN_Configuraciones();
        DataTable resp;
        string rutaImagen = "";

        public formEmpresa()
        {
            InitializeComponent();
            cargarDatosEmpresa();
        }

        private void cargarDatosEmpresa()
        {
            resp = objetoCN_configuraciones.dameDatosEmpresa();

            foreach (DataRow row in resp.Rows)
            {
                txtNombreEmpresa.Text = Convert.ToString(row["empresa"]);
                txtDomicilio.Text = Convert.ToString(row["direccion_empresa"]);
                txtTelefono.Text = Convert.ToString(row["telefono_empresa"]);
                txtCUIT.Text = Convert.ToString(row["CUIT"]);
                txtIngBrutos.Text = Convert.ToString(row["ing_brutos"]);
                pbEmpresa.ImageLocation = Convert.ToString(row["imagen"]);
                this.rutaImagen = Convert.ToString(row["imagen"]);
                // imagen
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";

                rpta = CN_Configuraciones.InsertarDatosEmpresa(this.txtNombreEmpresa.Text.Trim(), this.rutaImagen, this.txtDomicilio.Text.Trim(), this.txtTelefono.Text.Trim(), this.txtCUIT.Text.Trim(), this.txtIngBrutos.Text.Trim());


                if (rpta.Equals("Ok"))
                {
                    this.MensajeOk("Se Insertó de forma correcta el registro");
                }
                else
                {
                    this.MensajeError(rpta);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
            this.Close();
        }

        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "InvenControl", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void MensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje, "InvenControl", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnCargarFoto_Click(object sender, EventArgs e)
        {

            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "jpg files(.*jpg)|*.jpg| PNG files(.*png)|*.png| All Files(*.*)|*.*";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.rutaImagen = dialog.FileName;

                    pbEmpresa.ImageLocation = this.rutaImagen;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void formEmpresa_Load(object sender, EventArgs e)
        {

        }
    }
}

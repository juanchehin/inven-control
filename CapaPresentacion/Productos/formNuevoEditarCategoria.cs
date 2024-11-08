using CapaNegocio;
using System;
using System.Windows.Forms;

namespace CapaPresentacion.Productos
{
    public partial class formNuevoEditarCategoria : Form
    {
        public formNuevoEditarCategoria()
        {
            InitializeComponent();
        }

        CN_Productos objetoCN = new CN_Productos();
        string respuesta;
        bool bandera;
        bool IsNuevo = false;
        bool IsEditar = false;

        private int IdCategoria;

        public formNuevoEditarCategoria(int parametro, bool IsNuevoEditar)
        {
            InitializeComponent();
            this.IdCategoria = parametro;
            this.bandera = IsNuevoEditar;
        }


        private void DameCategoriaPorId(int IdCategoria)
        {
            //respuesta = objetoCN.DameCategoriaPorId(IdCategoria);

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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void formNuevoEditarCategoria_Load_1(object sender, EventArgs e)
        {
            this.ActiveControl = txtNombre;
            if (this.bandera)
            {
                lblEditarNuevo.Text = "Nuevo";
                this.IsNuevo = true;
                this.IsEditar = false;
            }
            else
            {
                lblEditarNuevo.Text = "Editar";
                this.IsNuevo = false;
                this.IsEditar = true;
                this.DameCategoriaPorId(this.IdCategoria);
            }
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (this.txtNombre.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos");
                }
                else
                {
                    if (this.IsNuevo)
                    {
                        rpta = CN_Productos.AltaCategoria(this.txtNombre.Text.Trim());
                    }
                    else
                    {
                        rpta = CN_Productos.EditarCategoria(this.IdCategoria, this.txtNombre.Text.Trim());
                    }

                    if (rpta.Equals("OK"))
                    {
                        if (this.IsNuevo)
                        {
                            this.MensajeOk("Se Insertó de forma correcta el registro");
                        }
                        else
                        {
                            this.MensajeOk("Se Actualizó de forma correcta el registro");
                        }
                    }
                    else
                    {
                        this.MensajeError(rpta);
                    }
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
    }
}

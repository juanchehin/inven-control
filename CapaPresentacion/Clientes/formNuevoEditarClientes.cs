using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CapaNegocio;

namespace CapaPresentacion
{
    public partial class formNuevoEditarClientes : Form
    {
        CN_Clientes objetoCN = new CN_Clientes();
        DataTable respuesta;
        // int parametroActual;
        bool bandera;
        bool IsNuevo = false;
        bool IsEditar = false;

        private int IdCliente;
        private string Apellidos;
        private string Nombres;
        private string Telefono;
        private string DNI;
        private string Email;
        private string Direccion;
        private string FechaNac;
        private string Ciudad;
        private string FechaAlta;
        private string FechaModificacion;

        public formNuevoEditarClientes(int parametro, bool IsNuevoEditar)
        {
            InitializeComponent();
            this.IdCliente = parametro;
            this.bandera = IsNuevoEditar;
        }

        private void formNuevoEditarClientes_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtNombres;
            if (this.bandera)
            {
                lblEditarNuevo.Text = "Nuevo";
                // this.MostrarProducto(this.IdProducto);
                this.IsNuevo = true;
                this.IsEditar = false;
                this.lblFechaAlta.Visible = false;
                this.lblFechaModificacion.Visible = false;
            }
            else
            {
                lblEditarNuevo.Text = "Editar";
                this.IsNuevo = false;
                this.IsEditar = true;
                this.lblFechaAlta.Visible = true;
                this.lblFechaModificacion.Visible = true;
                this.MostrarCliente(this.IdCliente);
            }
        }

        // Carga los valores en los campos de texto del formulario para que se modifiquen los que se desean
        private void MostrarCliente(int IdCliente)
        {
            respuesta = objetoCN.MostrarCliente(IdCliente);

            foreach (DataRow row in respuesta.Rows)
            {
                IdCliente = Convert.ToInt32(row["IdPersona"]);
                Apellidos = Convert.ToString(row["Apellidos"]);
                Nombres = Convert.ToString(row["Nombres"]);
                Telefono = Convert.ToString(row["Telefono"]);
                DNI = Convert.ToString(row["DNI"]);
                Email = Convert.ToString(row["Email"]);
                Direccion = Convert.ToString(row["Direccion"]);
                FechaNac = Convert.ToString(row["FechaNac"]);
                Ciudad = Convert.ToString(row["Ciudad"]);
                FechaAlta = Convert.ToString(row["FechaAlta"]);
                FechaModificacion = Convert.ToString(row["FechaModificacion"]);

                txtApellidos.Text = Apellidos;
                txtNombres.Text = Nombres;
                txtTelefono.Text = Telefono;
                txtDNI.Text = DNI;
                txtCorreo.Text = Email;
                txtDireccion.Text = Direccion;
                dtpFechaNac.Text = FechaNac;
                txtCiudad.Text = Ciudad;
                lblFechaAlta1.Text = FechaAlta;
                lblFechaModificacion1.Text = FechaModificacion;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (this.txtNombres.Text == string.Empty || this.txtApellidos.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos");
                }
                else
                {
                    if (this.IsNuevo)
                    {
                        rpta = CN_Clientes.Insertar(this.txtApellidos.Text.Trim(),this.txtNombres.Text.Trim(), this.txtCorreo.Text.Trim());
                    }
                    else
                    {
                        rpta = CN_Clientes.Editar(this.IdCliente, this.txtApellidos.Text.Trim(), this.txtNombres.Text.Trim(), this.txtCorreo.Text.Trim());
                    }

                    if (rpta.Equals("Ok"))
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

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
            this.Close();
        }
        //Mostrar Mensaje de Error
        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "InvenControl", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void MensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje, "InvenControl", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

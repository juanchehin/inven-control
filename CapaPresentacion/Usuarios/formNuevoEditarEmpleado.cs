using CapaNegocio;
using System;
using System.Data;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class formNuevoEditarEmpleado : Form
    {
        CN_Empleados objetoCN = new CN_Empleados();
        DataTable respuesta;
        // int parametroActual;
        bool bandera;
        bool IsNuevo = false;
        bool IsEditar = false;

        private int IdEmpleado;
        private string Nombres;
        private string Apellidos;
        private string DNI;
        private string Direccion;
        private string Telefono;
        private string Email;
        private string Usuario;
        private DateTime FechaNac;

        public formNuevoEditarEmpleado(int parametro, bool IsNuevoEditar)
        {
            InitializeComponent();
            this.IdEmpleado = parametro;
            this.bandera = IsNuevoEditar;
            this.CargarRolesComboBox();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MostrarEmpleado(int IdEmpleado)
        {
            respuesta = objetoCN.MostrarEmpleado(IdEmpleado);

            foreach (DataRow row in respuesta.Rows)
            {
                //IdEmpleado = Convert.ToInt32(row["IdEmpleado"]);
                Nombres = Convert.ToString(row["Nombres"]);
                Apellidos = Convert.ToString(row["Apellidos"]);
                DNI = Convert.ToString(row["DNI"]);
                Direccion = Convert.ToString(row["Direccion"]);
                Telefono = Convert.ToString(row["Telefono"]);
                Email = Convert.ToString(row["Email"]);
                Usuario = Convert.ToString(row["Usuario"]);

                if (row["Fecha de nacimiento"] == DBNull.Value)
                {
                    FechaNac = Convert.ToDateTime("2010-12-25");
                    dtFechaNac.Value = FechaNac;
                }
                else
                {
                    FechaNac = Convert.ToDateTime(row["Fecha de nacimiento"]);
                    dtFechaNac.Value = FechaNac;

                }

                txtNombre.Text = Nombres;
                txtApellidos.Text = Apellidos;
                txtDNI.Text = DNI;
                txtDireccion.Text = Direccion;
                txtTelefono.Text = Telefono;
                txtEmail.Text = Email;
                txtUsuario.Text = Usuario;
            }

            //this.CargarRolesComboBox();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (this.txtNombre.Text == string.Empty || this.txtApellidos.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos");
                }
                else
                {
                    if (this.IsNuevo)
                    {
                        var año = this.dtFechaNac.Value.Year;
                        var mes = this.dtFechaNac.Value.Month;
                        var dia = this.dtFechaNac.Value.Day;
                        var fechaNac = año + "-" + mes + "-" + dia;

                        rpta = CN_Empleados.InsertarUsuario(this.txtNombre.Text.Trim(), this.txtApellidos.Text.Trim(), this.txtDNI.Text.Trim(),
                            this.txtDireccion.Text.Trim(), this.txtTelefono.Text.Trim(), fechaNac, this.txtUsuario.Text.Trim(), txtPassword.Text.Trim()
                            , txtEmail.Text.Trim(), cbRoles.Text.Trim());
                    }
                    else
                    {
                        var año = this.dtFechaNac.Value.Year;
                        var mes = this.dtFechaNac.Value.Month;
                        var dia = this.dtFechaNac.Value.Day;
                        var fecha = año + "-" + mes + "-" + dia;

                        rpta = CN_Empleados.Editar(this.IdEmpleado, this.txtNombre.Text.Trim(), this.txtApellidos.Text.Trim(),
                            this.txtDNI.Text.Trim(), this.txtDireccion.Text.Trim(), this.txtTelefono.Text.Trim(), fecha);
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
                        this.Close();
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

        }

        private void CargarRolesComboBox()
        {
            DataTable roles;
            roles = objetoCN.MostrarRoles();

            cbRoles.DataSource = roles;

            cbRoles.DisplayMember = "Rol";
            cbRoles.ValueMember = "IdRol";
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

        private void formNuevoEditarEmpleado_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtNombre;
            if (this.bandera)
            {
                lblEditarNuevo.Text = "Nuevo";
                // this.MostrarProducto(this.IdProducto);
                this.IsNuevo = true;
                this.IsEditar = false;
            }
            else
            {
                lblEditarNuevo.Text = "Editar";
                this.IsNuevo = false;
                this.IsEditar = true;
                this.MostrarEmpleado(this.IdEmpleado);
            }
        }
    }
}

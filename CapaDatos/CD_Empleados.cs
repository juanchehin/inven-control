using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace CapaDatos
{
    public class CD_Empleados
    {
        private int _IdEmpleado;
        private string _Nombre;
        private string _Apellidos;
        private string _DNI;
        private string _EstadoEmp;
        private string _Direccion;
        private string _Telefono;
        private string _FechaNac;

        private string _TextoBuscar;



        public int IdEmpleado { get => _IdEmpleado; set => _IdEmpleado = value; }
        public string Nombre { get => _Nombre; set => _Nombre = value; }
        public string Apellidos { get => _Apellidos; set => _Apellidos = value; }
        public string DNI { get => _DNI; set => _DNI = value; }
        public string EstadoEmp { get => _EstadoEmp; set => _EstadoEmp = value; }
        public string Direccion { get => _Direccion; set => _Direccion = value; }
        public string Telefono { get => _Telefono; set => _Telefono = value; }
        public string TextoBuscar { get => _TextoBuscar; set => _TextoBuscar = value; }
        public string FechaNac { get => _FechaNac; set => _FechaNac = value; }

        //Constructores
        public CD_Empleados()
        {

        }

        public CD_Empleados(int IdEmpleado, string Nombre, string Apellidos, string DNI, string EstadoEmp, string Direccion,string Telefono, string FechaNac)
        {
            this.IdEmpleado = IdEmpleado;
            this.Nombre = Nombre;
            this.Apellidos = Apellidos;
            this.Apellidos = Apellidos;
            this.DNI = DNI;
            this.EstadoEmp = EstadoEmp;
            this.Direccion = Direccion;
            this.Telefono = Telefono;
            this.FechaNac = FechaNac;

        }

        // ==================================================
        //  Permite devolver todos los empleados activos de la BD
        // ==================================================
        private CD_Conexion conexion = new CD_Conexion();

        MySqlDataReader leer;
        DataTable tabla = new DataTable();
        MySqlCommand comando = new MySqlCommand();


        public DataTable Mostrar()
        {

            comando.Connection = conexion.AbrirConexion();
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "bsp_dame_empleados";

            tabla.Clear();
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            comando.Parameters.Clear();// si no ponerlo al comienzo de esta funcion

            conexion.CerrarConexion();
            return tabla;

        }

        //Métodos
        //Insertar
        public string InsertarUsuario(string Nombre, string Apellidos, string DNI,
                            string Direccion, string Telefono, string fechaNac, string Usuario, string Password,
                            string Email, string rol)
        {
            string rpta = "";
            comando.Parameters.Clear();// si no ponerlo al comienzo de esta funcion
            try
            {

                comando.Connection = conexion.AbrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "bsp_alta_usuario";

                MySqlParameter pRol = new MySqlParameter();
                pRol.ParameterName = "@pRol";
                pRol.MySqlDbType = MySqlDbType.VarChar;
                pRol.Size = 60;
                pRol.Value = rol;
                comando.Parameters.Add(pRol);

                MySqlParameter pNombres = new MySqlParameter();
                pNombres.ParameterName = "@pNombres";
                pNombres.MySqlDbType = MySqlDbType.VarChar;
                pNombres.Size = 60;
                pNombres.Value = Nombre;
                comando.Parameters.Add(pNombres);

                MySqlParameter pApellidos = new MySqlParameter();
                pApellidos.ParameterName = "@pApellidos";
                pApellidos.MySqlDbType = MySqlDbType.VarChar;
                pApellidos.Size = 60;
                pApellidos.Value = Apellidos;
                comando.Parameters.Add(pApellidos);

                MySqlParameter pDNI = new MySqlParameter();
                pDNI.ParameterName = "@pDNI";
                pDNI.MySqlDbType = MySqlDbType.VarChar;
                pDNI.Size = 45;
                pDNI.Value = DNI;
                comando.Parameters.Add(pDNI);

                MySqlParameter pEmail = new MySqlParameter();
                pEmail.ParameterName = "@pEmail";
                pEmail.MySqlDbType = MySqlDbType.VarChar;
                pEmail.Size = 60;
                pEmail.Value = Email;
                comando.Parameters.Add(pEmail);

                MySqlParameter pDireccion = new MySqlParameter();
                pDireccion.ParameterName = "@pDireccion";
                pDireccion.MySqlDbType = MySqlDbType.VarChar;
                pDireccion.Size = 60;
                pDireccion.Value = Direccion;
                comando.Parameters.Add(pDireccion);

                MySqlParameter pFechaNac = new MySqlParameter();
                pFechaNac.ParameterName = "@pFechaNac";
                pFechaNac.MySqlDbType = MySqlDbType.VarChar;
                pFechaNac.Size = 60;
                pFechaNac.Value = fechaNac;
                comando.Parameters.Add(pFechaNac);

                MySqlParameter pTelefono = new MySqlParameter();
                pTelefono.ParameterName = "@pTelefono";
                pTelefono.MySqlDbType = MySqlDbType.VarChar;
                pTelefono.Size = 15;
                pTelefono.Value = Telefono;
                comando.Parameters.Add(pTelefono);

                MySqlParameter pUsuario = new MySqlParameter();
                pUsuario.ParameterName = "@pUsuario";
                pUsuario.MySqlDbType = MySqlDbType.VarChar;
                pUsuario.Size = 60;
                pUsuario.Value = Usuario;
                comando.Parameters.Add(pUsuario);

                MySqlParameter pPassword = new MySqlParameter();
                pPassword.ParameterName = "@pPassword";
                pPassword.MySqlDbType = MySqlDbType.VarChar;
                pPassword.Size = 60;
                pPassword.Value = Password;
                comando.Parameters.Add(pPassword);                       

                // Console.WriteLine("rpta es : " + rpta);

                rpta = (string)comando.ExecuteScalar();

                if (rpta == "OK")
                {
                    rpta = "Ok";
                }
                else
                {
                    rpta = "NO se Ingreso el Registro";
                }
                // == "Ok" ? "OK" : "NO se Ingreso el Registro";

            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                conexion.CerrarConexion();
            }

            return rpta;

        }

        // Metodo ELIMINAR Empleado (da de baja)
        public string Eliminar(CD_Empleados Empleado)
        {
            string rpta = "";
            try
            {


                comando.Connection = conexion.AbrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "bsp_eliminar_empleado";

                MySqlParameter pIdEmpleado = new MySqlParameter();
                pIdEmpleado.ParameterName = "@pIdEmpleado";
                pIdEmpleado.MySqlDbType = MySqlDbType.Int32;
                // pIdEmpleado.Size = 60;
                pIdEmpleado.Value = Empleado.IdEmpleado;
                comando.Parameters.Add(pIdEmpleado);

                rpta = comando.ExecuteNonQuery() == 1 ? "OK" : "NO se Elimino el Registro";


            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                //if (conexion. == ConnectionState.Open) 
                conexion.CerrarConexion();
            }
            return rpta;
        }

        public string Editar(CD_Empleados Empleado)
        {
            string rpta = "";
            comando.Parameters.Clear();// si no ponerlo al comienzo de esta funcion
            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "bsp_editar_empleado";

                MySqlParameter pIdEmpleado = new MySqlParameter();
                pIdEmpleado.ParameterName = "@pIdEmpleado";
                pIdEmpleado.MySqlDbType = MySqlDbType.Int32;
                // pIdEmpleado.Size = 60;
                pIdEmpleado.Value = Empleado.IdEmpleado;
                comando.Parameters.Add(pIdEmpleado);

                MySqlParameter pNombre = new MySqlParameter();
                pNombre.ParameterName = "@pNombre";
                pNombre.MySqlDbType = MySqlDbType.VarChar;
                pNombre.Size = 60;
                pNombre.Value = Empleado.Nombre;
                comando.Parameters.Add(pNombre);

                MySqlParameter pApellidos = new MySqlParameter();
                pApellidos.ParameterName = "@pApellidos";
                pApellidos.MySqlDbType = MySqlDbType.VarChar;
                pApellidos.Size = 60;
                pApellidos.Value = Empleado.Apellidos;
                comando.Parameters.Add(pApellidos);

                MySqlParameter pDNI = new MySqlParameter();
                pDNI.ParameterName = "@pDNI";
                pDNI.MySqlDbType = MySqlDbType.VarChar;
                pDNI.Size = 45;
                pDNI.Value = Empleado.DNI;
                comando.Parameters.Add(pDNI);

                MySqlParameter pDireccion = new MySqlParameter();
                pDireccion.ParameterName = "@pDireccion";
                pDireccion.MySqlDbType = MySqlDbType.VarChar;
                pDireccion.Size = 60;
                pDireccion.Value = Empleado.Direccion;
                comando.Parameters.Add(pDireccion);

                MySqlParameter pTelefono = new MySqlParameter();
                pTelefono.ParameterName = "@pTelefono";
                pTelefono.MySqlDbType = MySqlDbType.VarChar;
                pTelefono.Size = 15;
                pTelefono.Value = Empleado.Telefono;
                comando.Parameters.Add(pTelefono);

                MySqlParameter pFechaNac = new MySqlParameter();
                pFechaNac.ParameterName = "@pFechaNac";
                pFechaNac.MySqlDbType = MySqlDbType.VarChar;
                pFechaNac.Size = 60;
                pFechaNac.Value = Empleado.FechaNac;
                comando.Parameters.Add(pFechaNac);

                rpta = comando.ExecuteScalar().ToString() == "Ok" ? "OK" : "No se edito el Registro";



            }
            catch (Exception ex)
            {

                rpta = ex.Message;
                Console.WriteLine("rpta es : " + rpta);
            }
            finally
            {
                //if (conexion. == ConnectionState.Open) 
                conexion.CerrarConexion();
            }
            comando.Parameters.Clear();
            return rpta;
        }

        public DataTable BuscarEmpleado(CD_Empleados Empleado)
        {
            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "bsp_buscar_empleado";

                MySqlParameter pTextoBuscar = new MySqlParameter();
                pTextoBuscar.ParameterName = "@pTextoBuscar";
                pTextoBuscar.MySqlDbType = MySqlDbType.VarChar;
                pTextoBuscar.Size = 30;
                pTextoBuscar.Value = Empleado.TextoBuscar;
                comando.Parameters.Add(pTextoBuscar);

                leer = comando.ExecuteReader();
                tabla.Load(leer);
                comando.Parameters.Clear();
                conexion.CerrarConexion();

                // return tabla;
            }
            catch (Exception ex)
            {
                tabla = null;
            }
            return tabla;

        }

        // Devuelve un solo usuario
        public DataTable MostrarUsuario(int IdPersona)
        {
            comando.Parameters.Clear();

            comando.Connection = conexion.AbrirConexion();
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "bsp_dame_usuario";

            MySqlParameter pIdPersona = new MySqlParameter();
            pIdPersona.ParameterName = "@pIdPersona";
            pIdPersona.MySqlDbType = MySqlDbType.Int32;
            pIdPersona.Value = IdPersona;
            comando.Parameters.Add(pIdPersona);

            tabla.Clear();
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conexion.CerrarConexion();

            return tabla;

        }

        public DataTable MostrarRoles()
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "bsp_dame_roles";

            leer = comando.ExecuteReader();
            tabla.Load(leer);
            comando.Parameters.Clear();
            conexion.CerrarConexion();

            return tabla;

        }


    }
}

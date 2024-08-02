using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Proveedores
    {
        private int _IdProveedor;
        private string _Proveedor;
        private string _CUIL;
        private string _Direccion;
        private string _Telefono;

        private string _TextoBuscar;


        public int IdProveedor { get => _IdProveedor; set => _IdProveedor = value; }
        public string Proveedor { get => _Proveedor; set => _Proveedor = value; }
        public string CUIL { get => _CUIL; set => _CUIL = value; }
        public string Direccion { get => _Direccion; set => _Direccion = value; }
        public string Telefono { get => _Telefono; set => _Telefono = value; }
        public string TextoBuscar { get => _TextoBuscar; set => _TextoBuscar = value; }

        //Constructores
        public CD_Proveedores()
        {

        }

        public CD_Proveedores(int IdProveedor, string Proveedor, string CUIL, string Direccion, string Telefono)
        {
            this.IdProveedor = IdProveedor;
            this.Proveedor = Proveedor;
            this.CUIL = CUIL;
            this.Direccion = Direccion;
            this.Telefono = Telefono;
        }

        // ==================================================
        //  Variables para la conexion y devolucion de datos
        // ==================================================
        private CD_Conexion conexion = new CD_Conexion();

        MySqlDataReader leer;
        DataTable tabla = new DataTable();
        MySqlCommand comando = new MySqlCommand();

        // ==================================================
        //  Permite devolver todos los proveedores de la BD
        // ==================================================
        public DataTable Mostrar()
        {

            comando.Connection = conexion.AbrirConexion();
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "bsp_dame_proveedores";

            tabla.Clear();
            leer = comando.ExecuteReader();
            tabla.Load(leer);

            return tabla;

        }

        // Devuelve un solo proveedor dado un ID
        public DataTable MostrarProveedor(int IdProveedor)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "bsp_dame_proveedor";

            MySqlParameter pIdProveedor = new MySqlParameter();
            pIdProveedor.ParameterName = "@pIdProveedor";
            pIdProveedor.MySqlDbType = MySqlDbType.Int32;
            // pIdProducto.Size = 60;
            pIdProveedor.Value = IdProveedor;
            comando.Parameters.Add(pIdProveedor);

            tabla.Clear();
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            comando.Parameters.Clear();
            conexion.CerrarConexion();

            return tabla;

        }

        //Métodos
        //Insertar
        public string InsertarProveedor(string Proveedor, string Nombres, string Apellidos, string CUIL, string Direccion, string Telefono,
                            string DNI, string Email)
        {
            string rpta = "";
            try
            {
                comando.Parameters.Clear();
                comando.Connection = conexion.AbrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "bsp_alta_proveedor";

                MySqlParameter pNombres = new MySqlParameter();
                pNombres.ParameterName = "@pNombres";
                pNombres.MySqlDbType = MySqlDbType.VarChar;
                pNombres.Size = 60;
                pNombres.Value = Nombres;
                comando.Parameters.Add(pNombres);

                MySqlParameter pProveedor = new MySqlParameter();
                pProveedor.ParameterName = "@pProveedor";
                pProveedor.MySqlDbType = MySqlDbType.VarChar;
                pProveedor.Size = 60;
                pProveedor.Value = Proveedor;
                comando.Parameters.Add(pProveedor);

                MySqlParameter pApellidos = new MySqlParameter();
                pApellidos.ParameterName = "@pApellidos";
                pApellidos.MySqlDbType = MySqlDbType.VarChar;
                pApellidos.Size = 60;
                pApellidos.Value = Apellidos;
                comando.Parameters.Add(pApellidos);

                MySqlParameter pCUIL = new MySqlParameter();
                pCUIL.ParameterName = "@pCUIL";
                pCUIL.MySqlDbType = MySqlDbType.VarChar;
                pCUIL.Size = 60;
                pCUIL.Value = CUIL;
                comando.Parameters.Add(pCUIL);

                MySqlParameter pDireccion = new MySqlParameter();
                pDireccion.ParameterName = "@pDireccion";
                pDireccion.MySqlDbType = MySqlDbType.VarChar;
                pDireccion.Size = 60;
                pDireccion.Value = Direccion;
                comando.Parameters.Add(pDireccion);

                MySqlParameter pTelefono = new MySqlParameter();
                pTelefono.ParameterName = "@pTelefono";
                pTelefono.MySqlDbType = MySqlDbType.VarChar;
                pTelefono.Size = 60;
                pTelefono.Value = Telefono;
                comando.Parameters.Add(pTelefono);

                MySqlParameter pDNI = new MySqlParameter();
                pDNI.ParameterName = "@pDNI";
                pDNI.MySqlDbType = MySqlDbType.VarChar;
                pDNI.Size = 60;
                pDNI.Value = DNI;
                comando.Parameters.Add(pDNI);

                MySqlParameter pEmail = new MySqlParameter();
                pEmail.ParameterName = "@pEmail";
                pEmail.MySqlDbType = MySqlDbType.VarChar;
                pEmail.Size = 60;
                pEmail.Value = Email;
                comando.Parameters.Add(pEmail);

                // Console.WriteLine("rpta es : " + rpta);

                rpta = (string)comando.ExecuteScalar();

                if(rpta == "Ya existe un proveedor con ese CUIL")
                {
                    rpta = "Ya existe un proveedor con ese CUIL";
                    return rpta;
                }
                else
                {
                    if (rpta == "El Proveedor es obligatorio.")
                    {
                        rpta = "El Proveedor es obligatorio.";
                        return rpta;
                    }
                }
                rpta = "Ok";
                comando.Parameters.Clear();
                return rpta;

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
        public string Eliminar(CD_Proveedores Proveedor)
        {
            string rpta = "";
            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "bsp_baja_proveedor";

                MySqlParameter pIdProveedor = new MySqlParameter();
                pIdProveedor.ParameterName = "@pIdProveedor";
                pIdProveedor.MySqlDbType = MySqlDbType.Int32;
                pIdProveedor.Value = Proveedor.IdProveedor;
                comando.Parameters.Add(pIdProveedor);

                rpta = comando.ExecuteNonQuery() == 1 ? "Ok" : "NO se Elimino el Registro";

            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                conexion.CerrarConexion();
            }
            comando.Parameters.Clear();
            return rpta;
        }

        public string EditarProveedor(int IdProveedor,string Proveedor, string Nombres, string Apellidos, string CUIL, string Direccion, string Telefono,
                            string DNI, string Email)
        {
            string rpta = "";
            comando.Parameters.Clear();// si no ponerlo al comienzo de esta funcion
            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "bsp_editar_proveedor";

                MySqlParameter pIdProveedor = new MySqlParameter();
                pIdProveedor.ParameterName = "@pIdProveedor";
                pIdProveedor.MySqlDbType = MySqlDbType.Int32;
                pIdProveedor.Value = IdProveedor;
                comando.Parameters.Add(pIdProveedor);

                MySqlParameter pProveedor = new MySqlParameter();
                pProveedor.ParameterName = "@pProveedor";
                pProveedor.MySqlDbType = MySqlDbType.VarChar;
                pProveedor.Size = 60;
                pProveedor.Value = Proveedor;
                comando.Parameters.Add(pProveedor);

                MySqlParameter pNombres = new MySqlParameter();
                pNombres.ParameterName = "@pNombres";
                pNombres.MySqlDbType = MySqlDbType.VarChar;
                pNombres.Size = 60;
                pNombres.Value = Nombres;
                comando.Parameters.Add(pNombres);                

                MySqlParameter pApellidos = new MySqlParameter();
                pApellidos.ParameterName = "@pApellidos";
                pApellidos.MySqlDbType = MySqlDbType.VarChar;
                pApellidos.Size = 60;
                pApellidos.Value = Apellidos;
                comando.Parameters.Add(pApellidos);

                MySqlParameter pCUIL = new MySqlParameter();
                pCUIL.ParameterName = "@pCUIL";
                pCUIL.MySqlDbType = MySqlDbType.VarChar;
                pCUIL.Size = 60;
                pCUIL.Value = CUIL;
                comando.Parameters.Add(pCUIL);

                MySqlParameter pDireccion = new MySqlParameter();
                pDireccion.ParameterName = "@pDireccion";
                pDireccion.MySqlDbType = MySqlDbType.VarChar;
                pDireccion.Size = 60;
                pDireccion.Value = Direccion;
                comando.Parameters.Add(pDireccion);

                MySqlParameter pTelefono = new MySqlParameter();
                pTelefono.ParameterName = "@pTelefono";
                pTelefono.MySqlDbType = MySqlDbType.VarChar;
                pTelefono.Size = 60;
                pTelefono.Value = Telefono;
                comando.Parameters.Add(pTelefono);

                MySqlParameter pDNI = new MySqlParameter();
                pDNI.ParameterName = "@pDNI";
                pDNI.MySqlDbType = MySqlDbType.VarChar;
                pDNI.Size = 60;
                pDNI.Value = DNI;
                comando.Parameters.Add(pDNI);

                MySqlParameter pEmail = new MySqlParameter();
                pEmail.ParameterName = "@pEmail";
                pEmail.MySqlDbType = MySqlDbType.VarChar;
                pEmail.Size = 60;
                pEmail.Value = Email;
                comando.Parameters.Add(pEmail);

                //Ejecutamos nuestro comando

                rpta = (string)comando.ExecuteScalar();

            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                conexion.CerrarConexion();
            }
            comando.Parameters.Clear();
            return rpta;
        }

        public DataTable BuscarProveedor(CD_Proveedores Proveedor)
        {
            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "bsp_buscar_proveedor";

                MySqlParameter pTextoBuscar = new MySqlParameter();
                pTextoBuscar.ParameterName = "@pTextoBuscar";
                pTextoBuscar.MySqlDbType = MySqlDbType.VarChar;
                pTextoBuscar.Size = 30;
                pTextoBuscar.Value = Proveedor.TextoBuscar;
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
    }
}

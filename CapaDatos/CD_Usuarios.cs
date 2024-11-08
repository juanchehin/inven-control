using MySql.Data.MySqlClient;
using System.Data;
// Agregados

namespace CapaDatos
{
    public class CD_Usuarios
    {
        private int _IdUsuario;
        private string _Usuario;
        private string _Password;
        private string _Estado;



        public int IdUsuario { get => _IdUsuario; set => _IdUsuario = value; }
        public string Usuario { get => _Usuario; set => _Usuario = value; }
        public string Password { get => _Password; set => _Password = value; }
        public string Estado { get => _Estado; set => _Estado = value; }

        //Constructores
        public CD_Usuarios()
        {

        }

        public CD_Usuarios(int IdUsuario, string Usuario, string Password, string Estado)
        {
            this.IdUsuario = IdUsuario;
            this.Usuario = Usuario;
            this.Password = Password;
            this.Estado = Estado;

        }
        private CD_Conexion conexion = new CD_Conexion();


        MySqlCommand comando = new MySqlCommand();
        MySqlDataReader leer;
        DataTable tabla = new DataTable();

        public DataTable Login(CD_Usuarios Usuario)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "bsp_login";

            MySqlParameter pUsuario = new MySqlParameter();
            pUsuario.ParameterName = "@pUsuario";
            pUsuario.MySqlDbType = MySqlDbType.VarChar;
            pUsuario.Size = 30;
            pUsuario.Value = Usuario.Usuario;
            comando.Parameters.Add(pUsuario);

            MySqlParameter pPassword = new MySqlParameter();
            pPassword.ParameterName = "@pPassword";
            pPassword.MySqlDbType = MySqlDbType.VarChar;
            pPassword.Size = 30;
            pPassword.Value = Usuario.Password;
            comando.Parameters.Add(pPassword);


            tabla.Clear();
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conexion.CerrarConexion();
            return tabla;

        }

        public string chequear_activacion()
        {
            string rpta;

            comando.Connection = conexion.AbrirConexion();
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "bsp_chequear_activacion";

            rpta = comando.ExecuteScalar().ToString() == "Ok" ? "Ok" : "Requiere activacion";

            return rpta;
        }

        public string activar_producto(string codigo)
        {
            string rpta;

            MySqlParameter pCodigo = new MySqlParameter();
            pCodigo.ParameterName = "@pCodigo";
            pCodigo.MySqlDbType = MySqlDbType.VarChar;
            pCodigo.Size = 30;
            pCodigo.Value = codigo;
            comando.Parameters.Add(pCodigo);

            comando.Connection = conexion.AbrirConexion();
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "bsp_activar_producto";

            rpta = comando.ExecuteScalar().ToString() == "Ok" ? "Ok" : "Requiere activacion";

            return rpta;
        }


    }
}

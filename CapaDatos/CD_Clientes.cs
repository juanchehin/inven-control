using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace CapaDatos
{
    public class CD_Clientes
    {
        private int _IdCliente;
        private string _Transporte;
        private string _Titular;
        private string _Telefono;

        private string _TextoBuscar;


        public int IdCliente { get => _IdCliente; set => _IdCliente = value; }
        public string Transporte { get => _Transporte; set => _Transporte = value; }
        public string Titular { get => _Titular; set => _Titular = value; }
        public string Telefono { get => _Telefono; set => _Telefono = value; }
        public string TextoBuscar { get => _TextoBuscar; set => _TextoBuscar = value; }

        //Constructores
        public CD_Clientes()
        {

        }

        public CD_Clientes(int IdCliente, string Transporte, string Titular, string Telefono)
        {
            this.IdCliente = IdCliente;
            this.Transporte = Transporte;
            this.Titular = Titular;
            this.Telefono = Telefono;
        }

        // ==================================================
        //  Permite devolver todos los clientes activos de la BD
        // ==================================================
        private CD_Conexion conexion = new CD_Conexion();

        MySqlDataReader leer;
        DataTable tabla = new DataTable();
        MySqlCommand comando = new MySqlCommand();

        public DataSet buscar_clientes_paginado(string p_pTextoBuscar, int p_pDesde)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "bsp_buscar_clientes_paginado";

            MySqlParameter pTextoBuscar = new MySqlParameter();
            pTextoBuscar.ParameterName = "@pTextoBuscar";
            pTextoBuscar.MySqlDbType = MySqlDbType.VarChar;
            pTextoBuscar.Value = p_pTextoBuscar;
            comando.Parameters.Add(pTextoBuscar);

            MySqlParameter desde = new MySqlParameter();
            desde.ParameterName = "@pDesde";
            desde.MySqlDbType = MySqlDbType.Int32;
            desde.Value = p_pDesde;
            comando.Parameters.Add(desde);

            MySqlDataAdapter da = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            da.Fill(ds);

            comando.Parameters.Clear();

            conexion.CerrarConexion();

            return ds;

        }

        public DataTable Mostrar()
        {

            comando.Connection = conexion.AbrirConexion();
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "bsp_dame_clientes";

            tabla.Clear();
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conexion.CerrarConexion();
            return tabla;

        }

        // devuelve solo 1 cliente de la BD
        public DataTable MostrarCliente(int IdCliente)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "bsp_dame_cliente";

            MySqlParameter pIdCliente = new MySqlParameter();
            pIdCliente.ParameterName = "@pIdCliente";
            pIdCliente.MySqlDbType = MySqlDbType.Int32;
            pIdCliente.Value = IdCliente;
            comando.Parameters.Add(pIdCliente);

            tabla.Clear();
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            comando.Parameters.Clear();
            conexion.CerrarConexion();

            return tabla;

        }

        public string Editar(CD_Clientes Cliente)
        {
            string rpta = "";
            comando.Parameters.Clear();// si no ponerlo al comienzo de esta funcion
            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "bsp_editar_cliente";

                MySqlParameter pIdCliente = new MySqlParameter();
                pIdCliente.ParameterName = "@pIdCliente";
                pIdCliente.MySqlDbType = MySqlDbType.Int32;
                pIdCliente.Value = Cliente.IdCliente;
                comando.Parameters.Add(pIdCliente);

                MySqlParameter pTransporte = new MySqlParameter();
                pTransporte.ParameterName = "@pTransporte";
                pTransporte.MySqlDbType = MySqlDbType.VarChar;
                pTransporte.Size = 60;
                pTransporte.Value = Cliente.Transporte;
                comando.Parameters.Add(pTransporte);

                MySqlParameter pTitular = new MySqlParameter();
                pTitular.ParameterName = "@pTitular";
                pTitular.MySqlDbType = MySqlDbType.VarChar;
                pTitular.Size = 30;
                pTitular.Value = Cliente.Titular;
                comando.Parameters.Add(pTitular);

                MySqlParameter pTelefono = new MySqlParameter();
                pTelefono.ParameterName = "@pTelefono";
                pTelefono.MySqlDbType = MySqlDbType.VarChar;
                pTelefono.Size = 15;
                pTelefono.Value = Cliente.Telefono;
                comando.Parameters.Add(pTelefono);

                rpta = comando.ExecuteScalar().ToString() == "Ok" ? "Ok" : "No se edito el Registro";

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

        //Métodos
        //Insertar
        public string Insertar(CD_Clientes Cliente)
        {
            string rpta = "";
            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "bsp_alta_cliente";

                MySqlParameter pTitular = new MySqlParameter();
                pTitular.ParameterName = "@pTitular";
                pTitular.MySqlDbType = MySqlDbType.VarChar;
                pTitular.Size = 60;
                pTitular.Value = Cliente.Titular;
                comando.Parameters.Add(pTitular);

                MySqlParameter pTransporte = new MySqlParameter();
                pTransporte.ParameterName = "@pTransporte";
                pTransporte.MySqlDbType = MySqlDbType.VarChar;
                pTransporte.Size = 60;
                pTransporte.Value = Cliente.Transporte;
                comando.Parameters.Add(pTransporte);

                MySqlParameter pTelefono = new MySqlParameter();
                pTelefono.ParameterName = "@pTelefono";
                pTelefono.MySqlDbType = MySqlDbType.VarChar;
                pTelefono.Size = 15;
                pTelefono.Value = Cliente.Telefono;
                comando.Parameters.Add(pTelefono);

                rpta = comando.ExecuteScalar().ToString();


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
        public string Eliminar(CD_Clientes Cliente)
        {
            string rpta = "";
            try
            {

                comando.Connection = conexion.AbrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "bsp_eliminar_cliente";

                MySqlParameter pIdCliente = new MySqlParameter();
                pIdCliente.ParameterName = "@pIdCliente";
                pIdCliente.MySqlDbType = MySqlDbType.Int32;
                pIdCliente.Value = Cliente.IdCliente;
                comando.Parameters.Add(pIdCliente);

                //Ejecutamos nuestro comando
                rpta = comando.ExecuteNonQuery() == 1 ? "OK" : "NO se Elimino el Registro";


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

        public DataTable BuscarCliente(CD_Clientes Cliente)
        {
            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "bsp_buscar_cliente";

                MySqlParameter pTextoBuscar = new MySqlParameter();
                pTextoBuscar.ParameterName = "@pTextoBuscar";
                pTextoBuscar.MySqlDbType = MySqlDbType.VarChar;
                pTextoBuscar.Size = 30;
                pTextoBuscar.Value = Cliente.TextoBuscar;
                comando.Parameters.Add(pTextoBuscar);

                leer = comando.ExecuteReader();
                tabla.Load(leer);
                comando.Parameters.Clear();
                conexion.CerrarConexion();

            }
            catch (Exception ex)
            {
                tabla = null;
            }
            return tabla;

        }

        public DataSet cargar_movimientos_cuenta(int p_id_cliente, int p_pDesde)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "bsp_listar_movimientos_cuenta";

            MySqlParameter pIdCliente = new MySqlParameter();
            pIdCliente.ParameterName = "@pIdCliente";
            pIdCliente.MySqlDbType = MySqlDbType.Int32;
            pIdCliente.Value = p_id_cliente;
            comando.Parameters.Add(pIdCliente);

            MySqlParameter desde = new MySqlParameter();
            desde.ParameterName = "@pDesde";
            desde.MySqlDbType = MySqlDbType.Int32;
            desde.Value = p_pDesde;
            comando.Parameters.Add(desde);

            MySqlDataAdapter da = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            da.Fill(ds);

            comando.Parameters.Clear();

            conexion.CerrarConexion();

            return ds;

        }

        public string alta_deposito(int p_id_cliente, string p_monto_gasto, string p_fecha_gasto, int p_id_tipo_pago, string p_concepto)
        {
            string rpta = "";
            comando.Parameters.Clear();// si no ponerlo al comienzo de esta funcion
            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "bsp_alta_deposito";

                MySqlParameter pIdCliente = new MySqlParameter();
                pIdCliente.ParameterName = "@pIdCliente";
                pIdCliente.MySqlDbType = MySqlDbType.Int24;
                // pIdTipoPago.Size = 2;
                pIdCliente.Value = p_id_cliente;
                comando.Parameters.Add(pIdCliente);

                MySqlParameter pIdTipoPago = new MySqlParameter();
                pIdTipoPago.ParameterName = "@pIdTipoPago";
                pIdTipoPago.MySqlDbType = MySqlDbType.Int24;
                // pIdTipoPago.Size = 2;
                pIdTipoPago.Value = p_id_tipo_pago;
                comando.Parameters.Add(pIdTipoPago);

                MySqlParameter pMonto = new MySqlParameter();
                pMonto.ParameterName = "@pMonto";
                pMonto.MySqlDbType = MySqlDbType.Decimal;
                pMonto.Size = 10;
                pMonto.Value = p_monto_gasto;
                comando.Parameters.Add(pMonto);

                MySqlParameter pFechaGasto = new MySqlParameter();
                pFechaGasto.ParameterName = "@pFechaGasto";
                pFechaGasto.MySqlDbType = MySqlDbType.VarChar;
                pFechaGasto.Size = 60;
                pFechaGasto.Value = p_fecha_gasto;
                comando.Parameters.Add(pFechaGasto);

                MySqlParameter pDescripcion = new MySqlParameter();
                pDescripcion.ParameterName = "@pDescripcion";
                pDescripcion.MySqlDbType = MySqlDbType.VarChar;
                pDescripcion.Size = 255;
                pDescripcion.Value = p_concepto;
                comando.Parameters.Add(pDescripcion);

                rpta = comando.ExecuteScalar().ToString() == "ok" ? "Ok" : "No se completo la operacion. Contactese con el administrador";

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

    }
}

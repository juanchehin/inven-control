using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace CapaDatos
{
    public class CD_Pedidos
    {
        // ==================================================
        //  Permite devolver todos las compras de la BD ordenada por fecha
        // ==================================================
        private CD_Conexion conexion = new CD_Conexion();

        MySqlDataReader leer;
        DataTable tabla = new DataTable();
        DataSet tabla_dataset = new DataSet();
        MySqlCommand comando = new MySqlCommand();


        // bsp_listar_ventas
        public DataSet listar_pedidos(int pDesde, string FechaInicio, string FechaFin, string p_estado_pedido)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.Parameters.Clear();// si no ponerlo al comienzo de esta funcion
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "bsp_listar_pedidos";

            MySqlParameter desde = new MySqlParameter();
            desde.ParameterName = "@pDesde";
            desde.MySqlDbType = MySqlDbType.Int32;
            desde.Value = pDesde;
            comando.Parameters.Add(desde);

            MySqlParameter pFechaInicio = new MySqlParameter();
            pFechaInicio.ParameterName = "@pFechaInicio";
            pFechaInicio.MySqlDbType = MySqlDbType.String;
            pFechaInicio.Value = FechaInicio;
            comando.Parameters.Add(pFechaInicio);

            MySqlParameter pFechaFin = new MySqlParameter();
            pFechaFin.ParameterName = "@pFechaFin";
            pFechaFin.MySqlDbType = MySqlDbType.String;
            pFechaFin.Value = FechaFin;
            comando.Parameters.Add(pFechaFin);

            MySqlParameter estado_pedido = new MySqlParameter();
            estado_pedido.ParameterName = "@pEstadoPedido";
            estado_pedido.MySqlDbType = MySqlDbType.VarChar;
            estado_pedido.Value = p_estado_pedido;
            comando.Parameters.Add(estado_pedido);

            MySqlDataAdapter da = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            da.Fill(ds);

            comando.Parameters.Clear();

            conexion.CerrarConexion();

            return ds;

        }

        public string alta_pedido(string fechaPedido, string p_direccion_envio, string p_cliente,
                    string p_estado_pedido, int p_tipo_pago, string p_descripcion)
        {
            string rpta = "";
            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "bsp_alta_pedido";

                MySqlParameter pFecha = new MySqlParameter();
                pFecha.ParameterName = "@pFecha";
                pFecha.MySqlDbType = MySqlDbType.VarChar;
                pFecha.Size = 60;
                pFecha.Value = fechaPedido;
                comando.Parameters.Add(pFecha);

                MySqlParameter pDireccion = new MySqlParameter();
                pDireccion.ParameterName = "@pDireccion";
                pDireccion.MySqlDbType = MySqlDbType.VarChar;
                pDireccion.Size = 60;
                pDireccion.Value = p_direccion_envio;
                comando.Parameters.Add(pDireccion);

                MySqlParameter pCliente = new MySqlParameter();
                pCliente.ParameterName = "@pCliente";
                pCliente.MySqlDbType = MySqlDbType.VarChar;
                pCliente.Size = 60;
                pCliente.Value = p_cliente;
                comando.Parameters.Add(pCliente);

                MySqlParameter pEstadoPedido = new MySqlParameter();
                pEstadoPedido.ParameterName = "@pEstadoPedido";
                pEstadoPedido.MySqlDbType = MySqlDbType.VarChar;
                pEstadoPedido.Size = 1;
                pEstadoPedido.Value = p_estado_pedido;
                comando.Parameters.Add(pEstadoPedido);

                MySqlParameter pIdTipoPago = new MySqlParameter();
                pIdTipoPago.ParameterName = "@pIdTipoPago";
                pIdTipoPago.MySqlDbType = MySqlDbType.Int16;
                pIdTipoPago.Value = p_tipo_pago;
                comando.Parameters.Add(pIdTipoPago);

                MySqlParameter pDescripcion = new MySqlParameter();
                pDescripcion.ParameterName = "@pDescripcion";
                pDescripcion.MySqlDbType = MySqlDbType.VarChar;
                pDescripcion.Size = 250;
                pDescripcion.Value = p_descripcion;
                comando.Parameters.Add(pDescripcion);

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
        public string confirmar_pedido(int p_id_pedido)
        {
            string rpta = "";
            try
            {

                comando.Connection = conexion.AbrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "bsp_confirmar_pedido";

                MySqlParameter pIdPedido = new MySqlParameter();
                pIdPedido.ParameterName = "@pIdTransaccion";
                pIdPedido.MySqlDbType = MySqlDbType.Int32;
                pIdPedido.Value = p_id_pedido;
                comando.Parameters.Add(pIdPedido);

                //Ejecutamos nuestro comando
                rpta = comando.ExecuteNonQuery() == 1 ? "OK" : "Error. Contactese con el administrador";


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


    }
}

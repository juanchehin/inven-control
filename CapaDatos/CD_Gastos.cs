using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace CapaDatos
{
    public class CD_Gastos
    {
        // ==================================================
        //  Permite devolver todos los clientes activos de la BD
        // ==================================================
        private CD_Conexion conexion = new CD_Conexion();

        MySqlDataReader leer;
        DataTable tabla = new DataTable();
        MySqlCommand comando = new MySqlCommand();

        //Métodos
        //Insertar
        public string alta_gasto(string p_monto_gasto, string p_fecha_gasto, int p_id_tipo_pago, string p_concepto)
        {
            string rpta = "";
            comando.Parameters.Clear();// si no ponerlo al comienzo de esta funcion
            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "bsp_alta_gasto";

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

        public DataSet listar_gastos_paginado(int pDesde, string FechaInicio, string FechaFin)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "bsp_listar_gastos_paginado";

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

            MySqlDataAdapter da = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            da.Fill(ds);

            comando.Parameters.Clear();

            conexion.CerrarConexion();

            return ds;

        }
    }
}

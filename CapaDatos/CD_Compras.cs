using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace CapaDatos
{
    public class CD_Compras
    {
        private int _IdCompra;
        private string _Producto;
        private string _Proveedor;
        private string _Cantidad;
        private string _TipoPago;
        private string _Descripcion;

        public int IdCompra { get => _IdCompra; set => _IdCompra = value; }
        public string Producto { get => _Producto; set => _Producto = value; }
        public string Proveedor { get => _Proveedor; set => _Proveedor = value; }
        public string Cantidad { get => _Cantidad; set => _Cantidad = value; }
        public string TipoPago { get => _TipoPago; set => _TipoPago = value; }
        public string Descripcion { get => _Descripcion; set => _Descripcion = value; }

        // private string _TextoBuscar;
        //Constructores
        public CD_Compras()
        {

        }

        // ==================================================
        //  Permite devolver todos las compras de la BD ordenada por fecha
        // ==================================================
        private CD_Conexion conexion = new CD_Conexion();

        MySqlDataReader leer;
        DataTable tabla = new DataTable();
        MySqlCommand comando = new MySqlCommand();

        public DataSet listar_ingresos(int pDesde, string FechaInicio, string FechaFin)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.Parameters.Clear();// si no ponerlo al comienzo de esta funcion
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "bsp_listar_compras_paginado";

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

        // Devuelve un solo compra dado un ID
        public DataSet listar_detalle_compra(int p_id_compra)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "bsp_listar_detalle_compra";

            MySqlParameter pIdCompra = new MySqlParameter();
            pIdCompra.ParameterName = "@pIdTransaccion";
            pIdCompra.MySqlDbType = MySqlDbType.Int32;
            // pIdProducto.Size = 60;
            pIdCompra.Value = p_id_compra;
            comando.Parameters.Add(pIdCompra);


            MySqlDataAdapter da = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            da.Fill(ds);

            comando.Parameters.Clear();

            conexion.CerrarConexion();

            return ds;

        }

        public DataTable MostrarCompras(String FechaInicio, String FechaFin)
        {

            comando.Connection = conexion.AbrirConexion();
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "bsp_listar_compras";

            comando.Parameters.Clear();

            MySqlParameter pFechaInicio = new MySqlParameter();
            pFechaInicio.ParameterName = "@pFechaInicio";
            pFechaInicio.MySqlDbType = MySqlDbType.Date;
            pFechaInicio.Size = 30;
            pFechaInicio.Value = FechaInicio;
            comando.Parameters.Add(pFechaInicio);

            MySqlParameter pFechaFin = new MySqlParameter();
            pFechaFin.ParameterName = "@pFechaFin";
            pFechaFin.MySqlDbType = MySqlDbType.Date;
            pFechaFin.Size = 30;
            pFechaFin.Value = FechaFin;
            comando.Parameters.Add(pFechaFin);

            tabla.Clear();
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conexion.CerrarConexion();
            return tabla;
        }


        // Devuelve un solo compra dado un ID
        public DataTable MostrarCompra(int IdCompra)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "bsp_dame_compra";

            MySqlParameter pIdCompra = new MySqlParameter();
            pIdCompra.ParameterName = "@pIdCompra";
            pIdCompra.MySqlDbType = MySqlDbType.Int32;
            pIdCompra.Value = IdCompra;
            comando.Parameters.Add(pIdCompra);

            leer = comando.ExecuteReader();
            tabla.Load(leer);
            comando.Parameters.Clear();
            conexion.CerrarConexion();

            return tabla;

        }

        //Métodos
        //Insertar
        public string AltaCompra(int pIdUsuario, int pIdProveedor, string pTipoPago, DataTable pListadoProductos, decimal pMontoTotal)
        {
            int idVenta;
            string rpta = "";
            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "bsp_alta_compra";

                MySqlParameter IdUsuario = new MySqlParameter();
                IdUsuario.ParameterName = "@pIdUsuario";
                IdUsuario.MySqlDbType = MySqlDbType.Int32;
                IdUsuario.Value = pIdUsuario;   // IdEmpleado / cajero
                comando.Parameters.Add(IdUsuario);

                MySqlParameter IdProveedor = new MySqlParameter();
                IdProveedor.ParameterName = "@pIdProveedor";
                IdProveedor.MySqlDbType = MySqlDbType.Int32;
                IdProveedor.Value = pIdProveedor;
                comando.Parameters.Add(IdProveedor);

                MySqlParameter TipoPago = new MySqlParameter();
                TipoPago.ParameterName = "@pTipoPago";
                TipoPago.MySqlDbType = MySqlDbType.VarChar;
                TipoPago.Value = pTipoPago;
                comando.Parameters.Add(TipoPago);

                MySqlParameter MontoTotal = new MySqlParameter();
                MontoTotal.ParameterName = "@pMontoTotal";
                MontoTotal.MySqlDbType = MySqlDbType.Decimal;
                MontoTotal.Value = pMontoTotal;
                comando.Parameters.Add(MontoTotal);

                MySqlParameter Descripcion = new MySqlParameter();
                Descripcion.ParameterName = "@pDescripcion";
                Descripcion.MySqlDbType = MySqlDbType.VarChar;
                Descripcion.Value = "Compra";
                comando.Parameters.Add(Descripcion);

                IdCompra = Convert.ToInt32(comando.ExecuteScalar());
                comando.Parameters.Clear();

            }
            catch (Exception ex)
            {
                rpta = ex.Message;
                conexion.CerrarConexion();
                return rpta;
            }

            try
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "bsp_alta_linea_compra";

                for (int curRow = 0; curRow < pListadoProductos.Rows.Count; curRow++)
                {
                    MySqlParameter pIdCompra = new MySqlParameter();
                    pIdCompra.ParameterName = "@pIdCompra";
                    pIdCompra.MySqlDbType = MySqlDbType.Int32;
                    pIdCompra.Value = IdCompra;
                    comando.Parameters.Add(pIdCompra);

                    MySqlParameter pIdEmpleado = new MySqlParameter();
                    pIdEmpleado.ParameterName = "@pIdProducto";
                    pIdEmpleado.MySqlDbType = MySqlDbType.Int32;
                    pIdEmpleado.Value = pListadoProductos.Rows[curRow][0];
                    comando.Parameters.Add(pIdEmpleado);

                    MySqlParameter pCantidad = new MySqlParameter();
                    pCantidad.ParameterName = "@pCantidad";
                    pCantidad.MySqlDbType = MySqlDbType.Decimal;  // Ver por que esta definido como string
                    pCantidad.Value = pListadoProductos.Rows[curRow][1];
                    comando.Parameters.Add(pCantidad);

                    rpta = (string)comando.ExecuteScalar();//  == "Ok";//  : "NO se Ingreso el Registro";
                    comando.Parameters.Clear();

                }
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
        public string Eliminar(CD_Compras Compra)
        {
            string rpta = "";
            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "bsp_eliminar_compra";

                MySqlParameter pIdCompra = new MySqlParameter();
                pIdCompra.ParameterName = "@pIdCompra";
                pIdCompra.MySqlDbType = MySqlDbType.Int32;
                pIdCompra.Value = Compra.IdCompra;
                comando.Parameters.Add(pIdCompra);

                //Ejecutamos nuestro comando

                rpta = comando.ExecuteNonQuery() == 1 ? "OK" : "NO se Elimino el Registro";
                Console.WriteLine("rpta es ; " + rpta);

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

        public string Editar(CD_Compras Compra)
        {
            string rpta = "";
            comando.Parameters.Clear();// si no ponerlo al comienzo de esta funcion
            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "bsp_editar_compra";

                MySqlParameter pIdCompra = new MySqlParameter();
                pIdCompra.ParameterName = "@pIdCompra";
                pIdCompra.MySqlDbType = MySqlDbType.Int32;
                // pIdEmpleado.Size = 60;
                pIdCompra.Value = Compra.IdCompra;
                comando.Parameters.Add(pIdCompra);

                MySqlParameter pProducto = new MySqlParameter();
                pProducto.ParameterName = "@pProducto";
                pProducto.MySqlDbType = MySqlDbType.VarChar;
                pProducto.Size = 60;
                pProducto.Value = Compra.Producto;
                comando.Parameters.Add(pProducto);

                MySqlParameter pProveedor = new MySqlParameter();
                pProveedor.ParameterName = "@pProveedor";
                pProveedor.MySqlDbType = MySqlDbType.VarChar;
                pProveedor.Size = 30;
                pProveedor.Value = Compra.Proveedor;
                comando.Parameters.Add(pProveedor);

                MySqlParameter pCantidad = new MySqlParameter();
                pCantidad.ParameterName = "@pCantidad";
                pCantidad.MySqlDbType = MySqlDbType.Int32;
                // pPrecioCompra.Size = 60;
                pCantidad.Value = Compra.Cantidad;
                comando.Parameters.Add(pCantidad);

                MySqlParameter pDescripcion = new MySqlParameter();
                pDescripcion.ParameterName = "@pDescripcion";
                pDescripcion.MySqlDbType = MySqlDbType.VarChar;
                pDescripcion.Size = 60;
                pDescripcion.Value = Compra.Descripcion;
                comando.Parameters.Add(pDescripcion);

                //Ejecutamos nuestro comando

                rpta = comando.ExecuteScalar().ToString() == "Ok" ? "Ok" : "No se edito el Registro";



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


    }
}

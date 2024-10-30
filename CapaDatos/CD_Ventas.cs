using Devart.Common;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;
using System.Windows;

namespace CapaDatos
{
    public class CD_Ventas
    {
        private int _IdVenta;
        private string _Producto;
        private int _IdEmpleado;
        private string _Cantidad;

        public int IdVenta { get => _IdVenta; set => _IdVenta = value; }
        public int IdEmpleado { get => _IdEmpleado; set => _IdEmpleado = value; }
        public string Cantidad { get => _Cantidad; set => _Cantidad = value; }
        public string Producto { get => _Producto; set => _Producto = value; }

        //Constructores
        public CD_Ventas()
        {

        }
        // ==================================================
        //  Permite devolver todos las compras de la BD ordenada por fecha
        // ==================================================
        private CD_Conexion conexion = new CD_Conexion();

        MySqlDataReader leer;
        DataTable tabla = new DataTable();
        DataSet tabla_dataset = new DataSet();
        MySqlCommand comando = new MySqlCommand();


        public DataTable DameTiposPago()
        {

            comando.Connection = conexion.AbrirConexion();
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "bsp_dame_tipos_pago";

            tabla.Clear();
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conexion.CerrarConexion();
            return tabla;

        }
        // bsp_listar_ventas
        public DataSet ListarTransacciones(int pDesde, string FechaInicio, string FechaFin,int p_id_tipo_pago_seleccionado)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.Parameters.Clear();// si no ponerlo al comienzo de esta funcion
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "bsp_listar_ventas";

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

            MySqlParameter id_tipoPago = new MySqlParameter();
            id_tipoPago.ParameterName = "@pIdTipoPago";
            id_tipoPago.MySqlDbType = MySqlDbType.Int32;
            id_tipoPago.Value = p_id_tipo_pago_seleccionado;
            comando.Parameters.Add(id_tipoPago);

            MySqlDataAdapter da = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            da.Fill(ds);

            comando.Parameters.Clear();

            conexion.CerrarConexion();

            return ds;

        }

        public DataSet dame_credencial_afip()
        {
            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.Parameters.Clear();// si no ponerlo al comienzo de esta funcion
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "bsp_dame_credencial_afip";

                MySqlDataAdapter da = new MySqlDataAdapter(comando);
                DataSet ds = new DataSet();
                da.Fill(ds);

                comando.Parameters.Clear();

                conexion.CerrarConexion();

                return ds;
            }
            catch (Exception ex)
            {
                alta_log("Exception dame_credencial_afip - " + ex.Message);

                conexion.CerrarConexion();
                return null;
            }


        }
        public DataTable MostrarVentas()
        {

            comando.Connection = conexion.AbrirConexion();
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "bsp_dame_ventas";

            tabla.Clear();
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conexion.CerrarConexion();
            return tabla;

        }

        // Devuelve un solo compra dado un ID
        public DataTable MostrarVenta(int IdVenta)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "bsp_dame_venta";

            MySqlParameter pIdVenta = new MySqlParameter();
            pIdVenta.ParameterName = "@pIdVenta";
            pIdVenta.MySqlDbType = MySqlDbType.Int32;
            // pIdProducto.Size = 60;
            pIdVenta.Value = IdVenta;
            comando.Parameters.Add(pIdVenta);

            leer = comando.ExecuteReader();
            tabla.Load(leer);
            comando.Parameters.Clear();
            conexion.CerrarConexion();

            return tabla;

        }
        
        // Devuelve un solo compra dado un ID
        public DataSet listar_detalle_venta(int IdVenta)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "bsp_listar_detalle_venta";

            MySqlParameter pIdVenta = new MySqlParameter();
            pIdVenta.ParameterName = "@pIdTransaccion";
            pIdVenta.MySqlDbType = MySqlDbType.Int32;
            // pIdProducto.Size = 60;
            pIdVenta.Value = IdVenta;
            comando.Parameters.Add(pIdVenta);


            MySqlDataAdapter da = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            da.Fill(ds);

            comando.Parameters.Clear();

            conexion.CerrarConexion();

            return ds;

        }
        //Métodos
        //Insertar
        public string AltaVenta(int pIdUsuario, int pIdCliente,string pTipoPago, DataTable pListadoProductos,decimal pMontoTotal)
        {
            int idVenta;
            string rpta = "";
            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "bsp_alta_venta";

                MySqlParameter IdUsuario = new MySqlParameter();
                IdUsuario.ParameterName = "@pIdUsuario";
                IdUsuario.MySqlDbType = MySqlDbType.Int32;
                IdUsuario.Value = pIdUsuario;   // IdEmpleado / cajero
                comando.Parameters.Add(IdUsuario);

                MySqlParameter IdCliente = new MySqlParameter();
                IdCliente.ParameterName = "@pIdCliente";
                IdCliente.MySqlDbType = MySqlDbType.Int32;
                IdCliente.Value = pIdCliente;
                comando.Parameters.Add(IdCliente);

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
                Descripcion.Value = "Venta";
                comando.Parameters.Add(Descripcion);

                idVenta = Convert.ToInt32(comando.ExecuteScalar());
                comando.Parameters.Clear();


               
            }
            catch(Exception ex)
            {
                rpta = ex.Message;
                conexion.CerrarConexion();
                return rpta;
            }

            // alta_linea_venta
            try
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "bsp_alta_linea_venta";

                for (int curRow = 0; curRow < pListadoProductos.Rows.Count; curRow++)
                {
                    MySqlParameter pIdVenta = new MySqlParameter();
                    pIdVenta.ParameterName = "@pIdVenta";
                    pIdVenta.MySqlDbType = MySqlDbType.Int32;
                    pIdVenta.Value = idVenta;
                    comando.Parameters.Add(pIdVenta);

                    MySqlParameter pIdEmpleado = new MySqlParameter();
                    pIdEmpleado.ParameterName = "@pIdProducto";
                    pIdEmpleado.MySqlDbType = MySqlDbType.Int32;
                    pIdEmpleado.Value = pListadoProductos.Rows[curRow][0];
                    comando.Parameters.Add(pIdEmpleado);

                    MySqlParameter pCantidad = new MySqlParameter();
                    pCantidad.ParameterName = "@pCantidad";
                    pCantidad.MySqlDbType = MySqlDbType.Decimal;
                    pCantidad.Value = pListadoProductos.Rows[curRow][2];
                    comando.Parameters.Add(pCantidad);

                    MySqlParameter pPeso = new MySqlParameter();
                    pPeso.ParameterName = "@pPeso";
                    pPeso.MySqlDbType = MySqlDbType.Decimal;
                    pPeso.Value = pListadoProductos.Rows[curRow][3];
                    comando.Parameters.Add(pPeso);

                    MySqlParameter pPrecioVenta = new MySqlParameter();
                    pPrecioVenta.ParameterName = "@pPrecioVenta";
                    pPrecioVenta.MySqlDbType = MySqlDbType.Decimal;
                    pPrecioVenta.Value = pListadoProductos.Rows[curRow][4];
                    comando.Parameters.Add(pPrecioVenta);

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

        public string alta_credencial_afip(string unique_id, string token, string sign, string expiration_time, string generation_time)
        {
            string rpta = "";

            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "bsp_alta_credencial_afip";

                MySqlParameter UniqueID = new MySqlParameter();
                UniqueID.ParameterName = "@pUniqueID";
                UniqueID.MySqlDbType = MySqlDbType.VarChar;
                UniqueID.Value = unique_id;
                comando.Parameters.Add(UniqueID);

                MySqlParameter Token = new MySqlParameter();
                Token.ParameterName = "@pToken";
                Token.MySqlDbType = MySqlDbType.VarChar;
                Token.Value = token;
                comando.Parameters.Add(UniqueID);

                MySqlParameter Sign = new MySqlParameter();
                Sign.ParameterName = "@pSign";
                Sign.MySqlDbType = MySqlDbType.VarChar;
                Sign.Value = sign;
                comando.Parameters.Add(UniqueID);

                MySqlParameter ExpTime = new MySqlParameter();
                ExpTime.ParameterName = "@pExpTime";
                ExpTime.MySqlDbType = MySqlDbType.VarChar;
                ExpTime.Value = expiration_time;
                comando.Parameters.Add(UniqueID);

                MySqlParameter GenerationTime = new MySqlParameter();
                GenerationTime.ParameterName = "@pGenerationTime";
                GenerationTime.MySqlDbType = MySqlDbType.VarChar;
                GenerationTime.Value = generation_time;
                comando.Parameters.Add(GenerationTime);

                comando.Parameters.Clear();

            }
            catch (Exception ex)
            {
                rpta = ex.Message;
                conexion.CerrarConexion();
                return rpta;
            }

            return rpta;
        }

        // Metodo ELIMINAR venta
        public string Eliminar(CD_Ventas Venta)
        {
            string rpta = "";
            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "bsp_baja_transaccion";

                MySqlParameter pIdTransaccion = new MySqlParameter();
                pIdTransaccion.ParameterName = "@pIdTransaccion";
                pIdTransaccion.MySqlDbType = MySqlDbType.Int32;
                pIdTransaccion.Value = Venta.IdVenta;
                comando.Parameters.Add(pIdTransaccion);

                //Ejecutamos nuestro comando

                rpta = comando.ExecuteNonQuery() == 1 ? "Ok" : "NO se Elimino el Registro";

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
            comando.Parameters.Clear();
            return rpta;
        }

        
        // Metodo
        public string check_expiration_time()
        {
            string rpta = "";
            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "bsp_check_expiration_time";


                //Ejecutamos nuestro comando

                rpta = comando.ExecuteScalar().ToString() == "Ok" ? "OK" : "No se edito el Registro";

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
            comando.Parameters.Clear();
            return rpta;
        }

        public string Editar(CD_Ventas Venta)
        {
            string rpta = "";
            comando.Parameters.Clear();// si no ponerlo al comienzo de esta funcion
            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "bsp_editar_venta";

                MySqlParameter pIdVenta = new MySqlParameter();
                pIdVenta.ParameterName = "@pIdVenta";
                pIdVenta.MySqlDbType = MySqlDbType.Int32;
                pIdVenta.Value = Venta.IdVenta;
                comando.Parameters.Add(pIdVenta);

                MySqlParameter pProducto = new MySqlParameter();
                pProducto.ParameterName = "@pProducto";
                pProducto.MySqlDbType = MySqlDbType.VarChar;
                pProducto.Size = 60;
                pProducto.Value = Venta.Producto;
                comando.Parameters.Add(pProducto);

                MySqlParameter pIdEmpleado = new MySqlParameter();
                pIdEmpleado.ParameterName = "@pIdEmpleado";
                pIdEmpleado.MySqlDbType = MySqlDbType.Int32;
                pIdEmpleado.Value = Venta.IdEmpleado;
                comando.Parameters.Add(pIdEmpleado);

                MySqlParameter pCantidad = new MySqlParameter();
                pCantidad.ParameterName = "@pCantidad";
                pCantidad.MySqlDbType = MySqlDbType.Int32;
                pCantidad.Value = Venta.Cantidad;
                comando.Parameters.Add(pCantidad);


                //Ejecutamos nuestro comando

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

        private void alta_log(string mensaje)
        {
            try
            {
                // Obtiene la ruta de acceso a la carpeta AppData del usuario actual
                string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                // Crea la ruta completa para tu archivo dentro de la carpeta AppData
                string filePath = System.IO.Path.Combine(appDataFolder, "store-soft", "logs_cd_ventas.txt");

                // Verifica si el directorio del archivo existe, si no, lo crea
                string directoryPath = System.IO.Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }


                // Verifica si el archivo existe
                if (!File.Exists(filePath))
                {
                    // Si el archivo no existe, lo crea y escribe contenido
                    using (StreamWriter writer = File.CreateText(filePath))
                    {
                        string mensaje_creacion = "Este es un nuevo archivo creado.";

                        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // Obtiene la fecha y hora actual
                        writer.WriteLine($"[{timestamp}] - {mensaje_creacion}");
                    }

                    Console.WriteLine("Archivo creado exitosamente.");
                }

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // Obtiene la fecha y hora actual
                    writer.WriteLine($"[{timestamp}] - {mensaje}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error al crear el archivo logs");
            }

        }
    }
}

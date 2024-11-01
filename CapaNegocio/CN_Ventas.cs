using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;

namespace CapaNegocio
{
    public class CN_Ventas
    {
        private CD_Ventas objetoCD = new CD_Ventas();

        //Método Insertar que llama al método Insertar de la clase
        //de la CapaDatos

        public DataSet dame_info_contribuyente()
        {
            DataSet tabla = new DataSet();
            tabla = objetoCD.dame_info_contribuyente();
            return tabla;
        }
        public static string AltaVenta(int pIdUsuario, int pIdCliente,string tipoPago,DataTable pListadoProductos,decimal pMontoTotal)
        {
            CD_Ventas Obj = new CD_Ventas();

            return Obj.AltaVenta(pIdUsuario,pIdCliente,tipoPago, pListadoProductos, pMontoTotal);
            //return null;
        }

        public static string alta_credencial_afip(string unique_id,string token,string sign,string expiration_time,string generation_time)
        {
            CD_Ventas Obj = new CD_Ventas();

            return Obj.alta_credencial_afip(unique_id, token, sign, expiration_time, generation_time);
            //return null;
        }
        
        public static string check_expiration_time()
        {
            CD_Ventas Obj = new CD_Ventas();

            return Obj.check_expiration_time();
            //return null;
        }

        public DataSet listarTransacciones(int pDesde, string pFechaInicio, string pFechaFin, int p_id_tipo_pago_seleccionado)
        {
            DataSet tabla = new DataSet();
            tabla = objetoCD.ListarTransacciones(pDesde, pFechaInicio, pFechaFin, p_id_tipo_pago_seleccionado);
            return tabla;
        }

        public DataSet dame_credencial_afip()
        {
            DataSet tabla = new DataSet();
            tabla = objetoCD.dame_credencial_afip();
            return tabla;
        }

        // Devuelve todas las compras habidas y por haber
        public DataTable MostrarVentas()
        {

            DataTable tabla = new DataTable();
            tabla = objetoCD.MostrarVentas();
            return tabla;
        }

        public DataTable DameTiposPago()
        {

            DataTable tabla = new DataTable();
            tabla = objetoCD.DameTiposPago();
            return tabla;
        }
        // Devuelve una compra (unica) dado un Id
        public DataTable MostrarVenta(int IdVenta)
        {

            DataTable tabla = new DataTable();
            tabla = objetoCD.MostrarVenta(IdVenta);
            return tabla;
        }
        public static string Eliminar(int IdVenta)
        {
            CD_Ventas Obj = new CD_Ventas();
            Obj.IdVenta = IdVenta;
            return Obj.Eliminar(Obj);
        }
        public DataSet listar_detalle_venta(int IdVenta)
        {
            DataSet tabla = new DataSet();
            tabla = objetoCD.listar_detalle_venta(IdVenta);
            return tabla;
        }

        public static string Editar(int IdVenta, string Producto, string Titular, int IdEmpleado, string cantidad)
        {
            CD_Ventas Obj = new CD_Ventas();
            Obj.IdVenta = IdVenta;

            Obj.Producto = Producto;
            Obj.IdEmpleado = IdEmpleado;
            Obj.Cantidad = cantidad;

            return Obj.Editar(Obj);
        }
    }
}

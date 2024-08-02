using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    public class CN_Compras
    {
        private CD_Compras objetoCD = new CD_Compras();

        //Método Insertar que llama al método Insertar de la clase
        //de la CapaDatos
        public static string AltaCompra(int IdUsuario,int IdProveedor,string TipoPago, DataTable pListadoProductos,decimal MontoTotal)
        {
            CD_Compras Obj = new CD_Compras();

            return Obj.AltaCompra(IdUsuario, IdProveedor, TipoPago, pListadoProductos,MontoTotal);
        }

        // Devuelve todas las compras habidas y por haber
        public DataTable MostrarCompras(string FechaInicio,string FechaFin)
        {

            DataTable tabla = new DataTable();
            tabla = objetoCD.MostrarCompras(FechaInicio,FechaFin);
            return tabla;
        }
        // Devuelve una compra (unica) dado un Id
        public DataTable MostrarCompra(int IdCompra)
        {

            DataTable tabla = new DataTable();
            tabla = objetoCD.MostrarCompra(IdCompra);
            return tabla;
        }
        public static string Eliminar(int IdCompra)
        {
            CD_Compras Obj = new CD_Compras();
            Obj.IdCompra = IdCompra;
            return Obj.Eliminar(Obj);
        }

        public DataSet listar_ingresos(int pDesde, string pFechaInicio, string pFechaFin)
        {
            DataSet tabla = new DataSet();
            tabla = objetoCD.listar_ingresos(pDesde, pFechaInicio, pFechaFin);
            return tabla;
        }
        public DataSet listar_detalle_compra(int id_compra)
        {
            DataSet tabla = new DataSet();
            tabla = objetoCD.listar_detalle_compra(id_compra);
            return tabla;
        }

        public static string Editar(int IdCompra, string Producto, string Proveedor, string Cantidad, string Descripcion)
        {
            CD_Compras Obj = new CD_Compras();
            Obj.IdCompra = IdCompra;

            Obj.Producto = Producto;
            Obj.Proveedor = Proveedor;
            Obj.Cantidad = Cantidad;
            Obj.Descripcion = Descripcion;


            return Obj.Editar(Obj);
        }
    }
}

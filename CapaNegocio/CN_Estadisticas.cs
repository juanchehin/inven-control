using CapaDatos;
using System.Data;

namespace CapaNegocio
{
    public class CN_Estadisticas
    {
        public DataTable dameVentasVendedor(string pFechaInicio, string pFechaFin )
        {
            CD_Estadisticas Obj = new CD_Estadisticas();

            return Obj.ventasVendedor(pFechaInicio, pFechaFin);
        }
        public DataTable dameProductosMasVendidos(string pFechaInicio, string pFechaFin)
        {
            CD_Estadisticas Obj = new CD_Estadisticas();

            return Obj.dameProductosMasVendidos(pFechaInicio, pFechaFin);
        }

        public DataTable dameArticulosComprados(string pFechaInicio, string pFechaFin)
        {
            CD_Estadisticas Obj = new CD_Estadisticas();

            return Obj.dameArticulosComprados(pFechaInicio, pFechaFin);
        }

        public DataTable dameComprasProveedor()
        {
            CD_Estadisticas Obj = new CD_Estadisticas();

            return Obj.dameComprasProveedor();
        }
    }
}

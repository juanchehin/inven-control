using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Gastos
    {
        private CD_Gastos objetoCD = new CD_Gastos();

        //Método Insertar que llama al método Insertar de la clase
        //de la CapaDatos
        public static string alta_gasto(string p_gasto, string p_fecha_gasto,int p_id_tipo_pago,string p_concepto)
        {
            CD_Gastos Obj = new CD_Gastos();

            return Obj.alta_gasto(p_gasto, p_fecha_gasto, p_id_tipo_pago, p_concepto);
            //return null;
        }

        public DataSet listar_gastos_paginado(int pDesde, string pFechaInicio, string pFechaFin)
        {
            DataSet tabla = new DataSet();
            tabla = objetoCD.listar_gastos_paginado(pDesde, pFechaInicio, pFechaFin);
            return tabla;
        }
    }
}

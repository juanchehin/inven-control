using CapaDatos;
using System.Data;

namespace CapaNegocio
{
    public class CN_Pedidos
    {
        private CD_Pedidos objetoCD = new CD_Pedidos();

        public DataSet listar_pedidos(int pDesde, string pFechaInicio, string pFechaFin, string p_estado_pedido)
        {
            DataSet tabla = new DataSet();
            tabla = objetoCD.listar_pedidos(pDesde, pFechaInicio, pFechaFin, p_estado_pedido);
            return tabla;
        }

        public static string alta_pedido(string fechaPedido, string p_direccion_envio, string p_cliente,
                    string p_estado_pedido, int p_tipo_pago, string descripcion)
        {
            CD_Pedidos Obj = new CD_Pedidos();

            return Obj.alta_pedido(fechaPedido, p_direccion_envio, p_cliente, p_estado_pedido, p_tipo_pago, descripcion);
            //return null;
        }

        public static string confirmar_pedido(int p_id_pedido)
        {
            CD_Pedidos Obj = new CD_Pedidos();

            return Obj.confirmar_pedido(p_id_pedido);
            //return null;
        }

    }
}

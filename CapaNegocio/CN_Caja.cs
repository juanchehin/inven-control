using System;
using System.Data;

using CapaDatos;

namespace CapaNegocio
{
    public class CN_Caja
    {
        private CD_Caja objetoCD_caja = new CD_Caja();
        string estadoCaja;
        string resp = "";

        public static string Eliminar(int IdCliente)
        {
            CD_Clientes Obj = new CD_Clientes();
            Obj.IdCliente = IdCliente;
            return Obj.Eliminar(Obj);
        }


        public static string Editar(int IdCliente, string Transporte, string Titular, string Telefono)
        {

            CD_Clientes Obj = new CD_Clientes();
            Obj.IdCliente = IdCliente;

            Obj.Transporte = Transporte;
            Obj.Titular = Titular;
            Obj.Telefono = Telefono;

            return Obj.Editar(Obj);
        }

        public DataTable BuscarCliente(string textobuscar)
        {
            Console.WriteLine("textobuscar en capa negocio es : " + textobuscar);
            CD_Clientes Obj = new CD_Clientes();
            Obj.TextoBuscar = textobuscar;
            return Obj.BuscarCliente(Obj);
        }

        public DataSet listarTransacciones(int pDesde,string pFechaInicio, string pFechaFin)
        {
            DataSet tabla = new DataSet();
            tabla = objetoCD_caja.ListarTransacciones(pDesde, pFechaInicio, pFechaFin);
            return tabla;
        }

        public string dameEstadoCaja()
        {
            estadoCaja = objetoCD_caja.dameEstadoCaja();
            return estadoCaja;
        }
        public string abrirCaja(int IdUsuario,decimal montoInicial)
        {
            resp = objetoCD_caja.abrirCaja(IdUsuario,montoInicial);
            return resp;
        }
        public string cerrarCaja(int IdUsuario)
        {
            resp = objetoCD_caja.cerrarCaja(IdUsuario);
            return resp;
        }
    }
}

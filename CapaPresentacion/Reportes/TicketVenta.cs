
using CapaNegocio;
using System;

namespace CapaPresentacion.Reportes
{
    public class TicketVenta
    {
        CN_Configuraciones objetoCN = new CN_Configuraciones();

        private int _IdVenta;
        private string _Producto;
        private string _Empresa;
        private int _IdEmpleado;
        private string _Cantidad;
        private string _Direccion;
        private decimal _Precio;
        private decimal _PrecioTotal;
        private DateTime _Fecha;

        public int IdVenta { get => _IdVenta; set => _IdVenta = value; }
        public int IdEmpleado { get => _IdEmpleado; set => _IdEmpleado = value; }
        public string Cantidad { get => _Cantidad; set => _Cantidad = value; }
        public string Producto { get => _Producto; set => _Producto = value; }
        public string Empresa { get => _Empresa; set => _Empresa = value; }
        public string Direccion { get => _Direccion; set => _Direccion = value; }
        public decimal Precio { get => _Precio; set => _Precio = value; }
        public decimal PrecioTotal { get => _PrecioTotal; set => _PrecioTotal = value; }
        public DateTime Fecha { get => _Fecha; set => _Fecha = value; }

        public TicketVenta()
        {
            this.Fecha = DateTime.Now;
            this.Empresa = dameEmpresa();
            this.Direccion = dameDireccion();

        }

        private string dameEmpresa()
        {
            return objetoCN.dameEmpresa();
        }
        private string dameDireccion()
        {
            return objetoCN.dameDireccion();
        }



    }
}

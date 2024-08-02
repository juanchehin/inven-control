using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;

namespace CapaNegocio
{
    public class CN_Proveedores
    {
        private CD_Proveedores objetoCD = new CD_Proveedores();

        public static string InsertarProveedor(string Proveedor,string Nombres, string Apellidos, string CUIL, string Direccion, string Telefono,
                            string DNI,string Email)
        {
            // Console.WriteLine("En insertar , nombre es " + nombre);

            CD_Proveedores Obj = new CD_Proveedores();

            return Obj.InsertarProveedor(Proveedor,Nombres, Apellidos, CUIL, Direccion, Telefono,
                            DNI, Email);
        }

        // Muestra todos los proveedores dados de alta en la BD
        public DataTable MostrarProveedores()
        {

            DataTable tabla = new DataTable();
            tabla = objetoCD.Mostrar();
            return tabla;
        }
        // Devuelve solo un producto
        public DataTable MostrarProveedor(int IdProveedor)
        {

            DataTable tabla = new DataTable();
            tabla = objetoCD.MostrarProveedor(IdProveedor);
            return tabla;
        }
        public static string Eliminar(int IdProveedor)
        {
            CD_Proveedores Obj = new CD_Proveedores();
            Obj.IdProveedor = IdProveedor;
            return Obj.Eliminar(Obj);
        }


        public static string EditarProveedor(int IdProveedor, string Proveedor, string Nombres, string Apellidos, string CUIL, string Direccion, string Telefono,
                            string DNI, string Email)
        {
            CD_Proveedores Obj = new CD_Proveedores();

            return Obj.EditarProveedor(IdProveedor, Proveedor, Nombres, Apellidos, CUIL, Direccion, Telefono,
                            DNI, Email);
        }

        public DataTable BuscarProveedor(string textobuscar)
        {
            CD_Proveedores Obj = new CD_Proveedores();
            Obj.TextoBuscar = textobuscar;
            return Obj.BuscarProveedor(Obj);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Agregados
using CapaDatos;

namespace CapaNegocio
{
    public class CN_Empleados
    {
        private CD_Empleados objetoCD = new CD_Empleados();

        //Método Insertar que llama al método Insertar de la clase DArticulo
        //de la CapaDatos
        public static string InsertarUsuario(string Nombre, string Apellidos,string DNI,
                            string Direccion,string Telefono,string fechaNac,string Usuario,string Password,
                            string Email,string rol)
        {
            CD_Empleados Obj = new CD_Empleados();

            return Obj.InsertarUsuario(Nombre, Apellidos, DNI,
                            Direccion, Telefono, fechaNac, Usuario, Password,
                            Email, rol);
        }

        public DataTable MostrarEmp()
        {

            DataTable tabla = new DataTable();
            tabla = objetoCD.Mostrar();
            return tabla;
        }
        public static string Eliminar(int IdEmpleado)
        {
            CD_Empleados Obj = new CD_Empleados();
            Obj.IdEmpleado = IdEmpleado;
            return Obj.Eliminar(Obj);
        }

        public DataTable MostrarEmpleado(int IdEmpleado)
        {

            DataTable tabla = new DataTable();
            tabla = objetoCD.MostrarUsuario(IdEmpleado);
            return tabla;
        }

        public DataTable MostrarRoles()
        {

            DataTable tabla = new DataTable();
            tabla = objetoCD.MostrarRoles();
            return tabla;
        }

        public static string Editar(int IdEmpleado, string Nombre, string Apellidos, string DNI, string Direccion, string Telefono,string FechaNac)
        {
            CD_Empleados Obj = new CD_Empleados();
            Obj.IdEmpleado = IdEmpleado;

            Obj.Nombre = Nombre;
            Obj.Apellidos = Apellidos;
            Obj.DNI = DNI;
            Obj.Direccion = Direccion;
            Obj.Telefono = Telefono;
            Obj.FechaNac = FechaNac;


            return Obj.Editar(Obj);
        }

        public DataTable BuscarEmpleado(string textobuscar)
        {
            CD_Empleados Obj = new CD_Empleados();
            Obj.TextoBuscar = textobuscar;
            return Obj.BuscarEmpleado(Obj);
        }


    }
}

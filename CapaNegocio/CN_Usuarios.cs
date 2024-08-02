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
    // private CD_Usuarios objetoCD = new CD_Usuarios();
    public class CN_Usuarios
    {
        public static DataTable Login(string usuario, string password)
        {
            CD_Usuarios Obj = new CD_Usuarios();
            DataTable tabla = new DataTable();

            Obj.Usuario = usuario;
            Obj.Password = password;
            
            tabla = Obj.Login(Obj);
            return tabla;
        }

        public static string chequear_activacion()
        {
            CD_Usuarios Obj = new CD_Usuarios();

            return Obj.chequear_activacion();
        }

        public static string activar_producto(string pCodigo)
        {
            CD_Usuarios Obj = new CD_Usuarios();

            return Obj.activar_producto(pCodigo);
        }
    }
    
}

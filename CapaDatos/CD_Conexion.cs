using MySql.Data.MySqlClient;
using System;
using System.Windows;

namespace CapaDatos
{
    public class CD_Conexion
    {
        public static string cadenaConexion = "datasource =localhost;username = store;password = 'gC43pm>14^8a';database=store_soft";
        MySqlConnection Con = new MySqlConnection(cadenaConexion);

        public CD_Conexion()
        {
        }

        public MySqlConnection AbrirConexion()
        {
            try
            {
                Con.Open();
                return Con;
            }
            catch
            {
                return Con;
            }
        }

        public MySqlConnection CerrarConexion()
        {
            try
            {
                Con.Close();
                return Con;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return Con;
            }
        }

        public string dame_cadena()
        {
            return cadenaConexion;
        }
    }

}

using CapaPresentacion.Reportes;
using System;
using System.Windows.Forms;

namespace CapaPresentacion
{
    static class Program
    {
        // Bandera usada para cerrar la pantalla de presentacion
        public static bool OpenDetailFormOnClose { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            OpenDetailFormOnClose = false;
            Application.Run(new formLogin());   // Cambiar por new formLogin()

            if (OpenDetailFormOnClose)
            {
                Application.Run(new formLogin());
            }
        }
    }
}

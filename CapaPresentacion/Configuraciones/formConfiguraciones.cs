using CapaPresentacion.Configuraciones;
using CapaPresentacion.Configuraciones.balanzas;
using CapaPresentacion.Configuraciones.Empresa;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class formConfiguraciones : Form
    {
        public formConfiguraciones()
        {
            InitializeComponent();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            formBackup frm = new formBackup();
            frm.Show();
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            formImportacionBD frm = new formImportacionBD();
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            formEmpresa frm = new formEmpresa();
            frm.Show();
        }

        private void btnIP_Click(object sender, EventArgs e)
        {
            formIP frm = new formIP();
            frm.Show();
        }

        private void btnBackend_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("backend.exe");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ocurrio un problema, contactese con el administrador");
            }
        }

        private void btnBalanza_Click(object sender, EventArgs e)
        {
            formSeleccionarMetodo frm = new formSeleccionarMetodo();
            frm.Show();
        }

        private void btnLogs_Click(object sender, EventArgs e)
        {
            // Obtiene la ruta de acceso a la carpeta AppData del usuario actual
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            // Verifica si el directorio del archivo existe, si no, lo crea
            string directoryPath = Path.GetDirectoryName(appDataFolder);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            //string filePath = Path.Combine(appDataFolder, "store-soft", "logs_balanza_enviar_recibir.txt");

            // Obtén el directorio donde se encuentra el archivo
            string directory = Path.GetDirectoryName(directoryPath);

            // Abre el explorador de archivos en la ubicación del directorio
            Process.Start("explorer.exe", directory);
        }
    }
}

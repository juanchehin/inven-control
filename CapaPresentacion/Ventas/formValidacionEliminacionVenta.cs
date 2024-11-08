using CapaNegocio;
using System;
using System.IO;
using System.Windows.Forms;

namespace CapaPresentacion.Ventas
{
    public partial class formValidacionEliminacionVenta : Form
    {
        int id_venta_a_eliminar;

        public formValidacionEliminacionVenta(int p_id_venta)
        {
            InitializeComponent();
            this.id_venta_a_eliminar = p_id_venta;
            this.Shown += new System.EventHandler(this.Form1_Shown);
        }

        private void btnAceptarElimVenta_Click(object sender, EventArgs e)
        {
            if (this.txtContraDeleteVenta.Text == "sanjorge3")
            {
                CN_Ventas.Eliminar(this.id_venta_a_eliminar);
                MessageBox.Show("Se elimino de forma correcta la venta, recuerde refrescar el listado", "InvenControl", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Contraseña incorrecta", "InvenControl", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alta_log("Pass incorrecta - form Eliminacion venta");
            }
        }

        private void btnCancelarPassVenta_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void alta_log(string mensaje)
        {
            try
            {

                // Obtiene la ruta de acceso a la carpeta AppData del usuario actual
                string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                // Crea la ruta completa para tu archivo dentro de la carpeta AppData
                string filePath = Path.Combine(appDataFolder, "store-soft", "logs_venta.txt");

                // Verifica si el directorio del archivo existe, si no, lo crea
                string directoryPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }


                // Verifica si el archivo existe
                if (!File.Exists(filePath))
                {
                    // Si el archivo no existe, lo crea y escribe contenido
                    using (StreamWriter writer = File.CreateText(filePath))
                    {
                        string mensaje_creacion = "Este es un nuevo archivo creado.";

                        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // Obtiene la fecha y hora actual
                        writer.WriteLine($"[{timestamp}] - {mensaje_creacion}");
                    }

                    Console.WriteLine("Archivo creado exitosamente.");
                }

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // Obtiene la fecha y hora actual
                    writer.WriteLine($"[{timestamp}] - {mensaje}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error al crear el archivo logs");
            }

        }

        private void formValidacionEliminacionVenta_Load(object sender, EventArgs e)
        {
            txtContraDeleteVenta.Focus();
        }

        // Método que se ejecuta cuando el formulario ya está visible
        private void Form1_Shown(object sender, EventArgs e)
        {
            // Establecer el foco en el TextBox
            txtContraDeleteVenta.Focus();

            // Mover el puntero del ratón al TextBox después de que el formulario esté visible
            //Point textBoxPosition = txtContraDeleteVenta.PointToScreen(Point.Empty);
            //Cursor.Position = new Point(textBoxPosition.X + (txtContraDeleteVenta.Width / 2), textBoxPosition.Y + (txtContraDeleteVenta.Height / 2));
        }

        private void txtContraDeleteVenta_KeyDown(object sender, KeyEventArgs e)
        {
            // Verificar si se presionó la tecla Enter
            if (e.KeyCode == Keys.Enter)
            {
                // Simular la presión del botón
                btnAceptarElimVenta.PerformClick();

                // Evitar el beep típico de Enter
                e.SuppressKeyPress = true;
            }
        }
    }
}

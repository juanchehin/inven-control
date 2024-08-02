using LibUsbDotNet.LibUsb;
using LibUsbDotNet.Main;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;

using System;
using LibUsbDotNet;
using LibUsbDotNet.Main;

namespace CapaPresentacion.Configuraciones.balanzas
{
    public partial class formRecibir : Form
    {
        public formRecibir()
        {
            InitializeComponent();
            llenar_cb_metodos();
            llenar_cb_parity();
            llenar_cb_stop_bit();
            deshabilitar_botones();
        }

        private void llenar_cb_metodos()
        {
            cbMetodoConexion.Items.Add("Metodo 1");
            cbMetodoConexion.Items.Add("Metodo 2");
            cbMetodoConexion.Items.Add("Metodo 3 (LibUsbDotNet)");

            cbMetodoConexion.SelectedIndex = 0;

        }

        private void llenar_cb_parity()
        {
            cbParityBit.Items.Add("No");
            cbParityBit.Items.Add("Si");

            cbParityBit.SelectedIndex = 0;

        }

        private void llenar_cb_stop_bit()
        {
            cbStopBit.Items.Add("No");
            cbStopBit.Items.Add("Si");

            cbStopBit.SelectedIndex = 0;

        }
        private void btnConectar_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnConectar.Text == "Conectar")
                {
                    btnConectar.Text = "Desconectar";
                    // Obtener el valor seleccionado del ComboBox
                    string metodo_conexion = cbMetodoConexion.SelectedItem.ToString();

                    switch (metodo_conexion)
                    {
                        case "Metodo 1":
                            conectar_metodo_uno();
                            break;
                        case "Metodo 2":
                            conectar_metodo_dos();
                            break;
                        case "Metodo 3":
                            conectar_metodo_tres();
                            break;
                        default:
                            alta_log("Switch default");
                            MessageBox.Show("Debe seleccionar un metodo de conexion");
                            break;
                    }
                }
                else if (btnConectar.Text == "Desconectar")
                {
                    alta_log("clic Desconectar");
                    btnConectar.Text = "Conectar";

                    // Obtener el valor seleccionado del ComboBox
                    string metodo_conexion = cbMetodoConexion.SelectedItem.ToString();

                    switch (metodo_conexion)
                    {
                        case "Metodo 1":
                            desconectar_metodo_uno();
                            break;
                        case "Metodo 2":
                            desconectar_metodo_dos();
                            break;
                        case "Metodo 3":
                            desconectar_metodo_tres();
                            break;
                        default:
                            alta_log("Switch default");
                            MessageBox.Show("Debe seleccionar un metodo de conexion 2");
                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                alta_log("excepcion btnConectar_Click + " + ex.Message.ToString());
                MessageBox.Show(ex.Message.ToString());
            }
        }

        // ===========================
        // Metodo Uno
        // ===========================
        private void conectar_metodo_uno()
        {
            Parity parity_bit = Parity.None;
            StopBits stop_bit = StopBits.One;

            try
            {
                alta_log("clic Conectar metodo dos");


                if (cbParityBit.Text == "Si")
                {
                    parity_bit = Parity.Mark;
                }

                if (cbParityBit.Text == "2")
                {
                    stop_bit = StopBits.Two;
                }

                //spPuertos = new SerialPort(puertoSeleccionado, Int32.Parse(dupBaudRate.Text), parity_bit, Int32.Parse(dupDataBits.Text), StopBits.One);   // ver manual de la balanza para estos parametros


                SerialPort port = new SerialPort(
                    cbPuertos.Text,
                    int.Parse(dupBaudRate.Text),
                    parity_bit,
                    Int32.Parse(dupDataBits.Text),
                    stop_bit
               );

                port.Open();

                alta_log("Puerto " + cbPuertos.Text + " abierto , dupBaudRate : " + dupBaudRate.Text);

                alta_log("Configuracion del puerto ");

                // Muestra la configuración del puerto en la consola
                alta_log("Configuración del puerto serie:");
                alta_log($"Nombre del puerto: {port.PortName}");
                alta_log($"Velocidad de transmisión: {port.BaudRate} baudios");
                alta_log($"Bits de datos: {port.DataBits}");
                alta_log($"Paridad: {port.Parity}");
                alta_log($"Bits de parada: {port.StopBits}");

                int contador_limite = 10;
                port.ReadTimeout = 1000;

                while ((btnConectar.Text == "Desconectar") && (contador_limite > 0))
                {
                    alta_log("Pasa while - contador : " + contador_limite);

                    try
                    {
                        // Intentar leer una línea del puerto serie

                        string data = port.ReadLine();

                        alta_log("Data recibida - metodo 1 : " + data);
                        lblDatosRecibidos.Text = data;
                        // Manejar los datos recibidos
                    }
                    catch (TimeoutException)
                    {
                        // Manejar el tiempo de espera alcanzado
                        alta_log("Se alcanzó el tiempo de espera al leer del puerto serie.");
                        Console.WriteLine("Se alcanzó el tiempo de espera al leer del puerto serie.");
                    }
                    catch (Exception ex)
                    {
                        // Manejar otras excepciones
                        alta_log("Se produjo un error al leer del puerto serie: " + ex.Message);
                        Console.WriteLine("Se produjo un error al leer del puerto serie: " + ex.Message);
                    }

                    //string data = port.ReadLine();
                    
                    contador_limite -= 1;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Problema con conexion metodo dos : " + ex.Message.ToString(), "Problema InvenControl");
                alta_log(ex.Message.ToString());
                btnConectar.Text = "Conectar";
            }
        }

        private void desconectar_metodo_uno()
        {

            try
            {
                alta_log("clic desConectar metodo uno");
                btnConectar.Text = "Conectar";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                alta_log(ex.Message.ToString());
            }
        }
        // ===========================
        // Metodo Dos
        // ===========================
        private void conectar_metodo_dos()
        {
            Parity parity_bit = Parity.None;
            StopBits stop_bit = StopBits.One;

            try
            {
                alta_log("-----------------------------------------------");
                alta_log("clic Conectar metodo dos");


                if (cbParityBit.Text == "Si")
                {
                    parity_bit = Parity.Mark;
                }

                if (cbParityBit.Text == "2")
                {
                    stop_bit = StopBits.Two;
                }

                //spPuertos = new SerialPort(puertoSeleccionado, Int32.Parse(dupBaudRate.Text), parity_bit, Int32.Parse(dupDataBits.Text), StopBits.One);   // ver manual de la balanza para estos parametros


                SerialPort port = new SerialPort(
                    cbPuertos.Text,
                    int.Parse(dupBaudRate.Text),
                    parity_bit,
                    Int32.Parse(dupDataBits.Text),
                    stop_bit
               );


                try
                {
                    port.DataReceived += spPuertos_DataReceived;
                    port.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Selecciones otro puerto", "Puerto no disponible");
                    //cmbPuertos.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Problema con conexion metodo dos : " + ex.Message.ToString(), "Problema InvenControl");
                alta_log(ex.Message.ToString());
                btnConectar.Text = "Conectar";
            }
        }

        private void desconectar_metodo_dos()
        {

            try
            {
                alta_log("clic desConectar metodo dos");
                btnConectar.Text = "Conectar";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                alta_log(ex.Message.ToString());
            }
        }
        // ===========================
        // Metodo Tres
        // ===========================
        private void conectar_metodo_tres()
        {
            try
            {
                alta_log("-----------------------------------------------");
                alta_log("clic Conectar metodo tres");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Problema con conexion metodo dos : " + ex.Message.ToString(), "Problema InvenControl");
                alta_log(ex.Message.ToString());
                btnConectar.Text = "Conectar";
            }
        }

        private void desconectar_metodo_tres()
        {

            try
            {
                alta_log("clic desConectar metodo dos");
                btnConectar.Text = "Conectar";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                alta_log(ex.Message.ToString());
            }
        }

        private void alta_log(string mensaje)
        {
            try
            {

                // Obtiene la ruta de acceso a la carpeta AppData del usuario actual
                string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                // Crea la ruta completa para tu archivo dentro de la carpeta AppData
                string filePath = Path.Combine(appDataFolder, "store-soft", "logs_balanza_recibir.txt");

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

        private void btnBuscarPuertos_Click(object sender, EventArgs e)
        {
            alta_log("Clic boton BUSCAR PUERTOS");

            CargarPuertosDisponibles();
        }
        private void CargarPuertosDisponibles()
        {
            // Obtiene una lista de los nombres de los puertos COM disponibles
            string[] puertosDisponibles = SerialPort.GetPortNames();

            // Limpia los elementos existentes en el ComboBox
            cbPuertos.Items.Clear();

            // Agrega los nombres de los puertos disponibles al ComboBox
            foreach (string puerto in puertosDisponibles)
            {
                cbPuertos.Items.Add(puerto);
            }

            // Si se encontraron puertos disponibles, selecciona el primero en la lista
            if (cbPuertos.Items.Count > 0)
            {
                cbPuertos.SelectedIndex = 0;
                habilitar_botones();
            }
            else
            {
                deshabilitar_botones();
                // Si no se encontraron puertos, muestra un mensaje indicando que no hay puertos disponibles
                MessageBox.Show("No se encontraron puertos COM disponibles.", "Puertos COM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                alta_log("No se encontraron puertos COM disponibles.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            formInfoBaudRate frm = new formInfoBaudRate();
            frm.Show();
        }

        private void habilitar_botones()
        {
            btnConectar.Enabled = true;
        }

        private void deshabilitar_botones()
        {
            btnConectar.Enabled = false;
        }

        private void btn_ruta_logs_Click(object sender, EventArgs e)
        {
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string filePath = Path.Combine(appDataFolder, "store-soft", "logs_balanza_recibir.txt");

            // Obtén el directorio donde se encuentra el archivo
            string directory = Path.GetDirectoryName(filePath);

            // Abre el explorador de archivos en la ubicación del directorio
            Process.Start("explorer.exe", directory);
        }

        private void spPuertos_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string datorxdatorx = spPuertos.ReadExisting();
            alta_log("datorxdatorx  " + datorxdatorx);
            //txtRx.Text = datorx.Trim()
        }
    }
}

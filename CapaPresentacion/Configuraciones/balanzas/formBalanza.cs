using System;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace CapaPresentacion.Configuraciones.balanzas
{
    public partial class formBalanza : Form
    {
        private delegate void DelegadoAcceso(string accion);

        public formBalanza()
        {
            InitializeComponent();
            llenar_cb_metodos();
            llenar_cb_parity();
            llenar_cb_stop_bit();
        }

        private void acceso_form(string accion)
        {
            txtDatosRecibidos.Text = accion;
            lblDatosRecibidos.Text = accion;

        }

        private void llenar_cb_metodos()
        {
            cbMetodoConexion.Items.Add("Metodo 1");
            cbMetodoConexion.Items.Add("Metodo 2");

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

        private void acceso_interrupcion(string accion)
        {
            DelegadoAcceso var_delegado_acceso;
            var_delegado_acceso = new DelegadoAcceso(acceso_form);
            object[] arg = { accion };
            base.Invoke(var_delegado_acceso, arg);
        }

        void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (this.Enabled == true)
            {
                Thread.Sleep(500);
                string data = spPuertos.ReadExisting();
                this.BeginInvoke(new DelegadoAcceso(si_DataReceived), new object[] { data });
            }
        }

        private void si_DataReceived(string accion)
        {
            lblDatosRecibidos.Text = accion;
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
            }
            else
            {
                // Si no se encontraron puertos, muestra un mensaje indicando que no hay puertos disponibles
                MessageBox.Show("No se encontraron puertos COM disponibles.", "Puertos COM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                alta_log("No se encontraron puertos COM disponibles.");
            }
        }

        private void btnBuscarPuertos_Click(object sender, EventArgs e)
        {
            alta_log("Clic boton BUSCAR PUERTOS");

            CargarPuertosDisponibles();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            formInfoBaudRate frm = new formInfoBaudRate();
            frm.Show();
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
                        default:
                            alta_log("Switch default");
                            Console.WriteLine("Debe seleccionar un metodo de conexion");
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
                        default:
                            alta_log("Switch default");
                            Console.WriteLine("Debe seleccionar un metodo de conexion");
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

        private void conectar_metodo_uno()
        {
            try
            {
                alta_log("clic Conectar metodo uno");

                spPuertos.BaudRate = Int32.Parse(dupBaudRate.Text);
                spPuertos.DataBits = 8;
                spPuertos.Parity = Parity.None;
                spPuertos.StopBits = StopBits.One;
                spPuertos.Handshake = Handshake.None;
                spPuertos.PortName = cbPuertos.Text;

                spPuertos.Open();
                btnEnviarDatos.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Problema con conexion metodo uno : " + ex.Message.ToString(), "Problema InvenControl");
                alta_log(ex.Message.ToString());
                btnConectar.Text = "Conectar";
            }
        }

        private void desconectar_metodo_uno()
        {
            alta_log("clic desConectar metodo uno");
            try
            {
                spPuertos.Close();
                btnEnviarDatos.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                alta_log(ex.Message.ToString());
            }
        }

        private void conectar_metodo_dos()
        {
            try
            {
                alta_log("clic Conectar metodo dos");

                SerialPort port = new SerialPort(cbPuertos.Text, int.Parse(dupBaudRate.Text)); // Reemplaza "COM3" con el puerto correcto
                port.Open();

                alta_log("Puerto " + cbPuertos.Text + " abierto , dupBaudRate : " + dupBaudRate.Text);

                while (btnConectar.Text == "Conectar")
                {
                    string data = port.ReadLine();
                    alta_log("Data recibida - metodo 1 : " + data);
                    lblDatosRecibidos.Text = data;
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
            alta_log("clic desConectar metodo dos");
            //try
            //{

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString());
            //    alta_log(ex.Message.ToString());
            //}
        }

        private void btnEnviarDatos_Click(object sender, EventArgs e)
        {
            Parity parity_bit = Parity.None;
            StopBits stop_bit = StopBits.One;

            try
            {
                string puertoSeleccionado = cbPuertos.SelectedItem.ToString();

                if (cbParityBit.Text == "Si")
                {
                    parity_bit = Parity.Mark;
                }

                if (cbParityBit.Text == "2")
                {
                    stop_bit = StopBits.Two;
                }

                spPuertos = new SerialPort(puertoSeleccionado, Int32.Parse(dupBaudRate.Text), parity_bit, Int32.Parse(dupDataBits.Text), StopBits.One);   // ver manual de la balanza para estos parametros
                spPuertos.DiscardInBuffer();
                spPuertos.Handshake = Handshake.None;
                spPuertos.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
                spPuertos.ReadTimeout = 500;
                spPuertos.WriteTimeout = 500;
                spPuertos.Open();
                spPuertos.Write(txtEnviarDatos.Text);

                alta_log("Dato enviado : " + txtEnviarDatos.Text);
            }
            catch (Exception ex)
            {
                alta_log(ex.Message);
                MessageBox.Show(ex.Message, "Problema btnEnviarDatos_Click");
            }
        }

        private void dato_recibido_port(object sender, SerialDataReceivedEventArgs e)
        {
            acceso_interrupcion(spPuertos.ReadExisting());


            /*string Data_in = spPuertos.ReadExisting();
            MessageBox.Show(Data_in);
            txtDatosRecibidos.Text = Data_in;

            //
            lblDatosRecibidos.Text = Data_in;*/
        }

        private void alta_log(string mensaje)
        {
            try
            {

                // Obtiene la ruta de acceso a la carpeta AppData del usuario actual
                string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                // Crea la ruta completa para tu archivo dentro de la carpeta AppData
                string filePath = Path.Combine(appDataFolder, "store-soft", "logs_balanza.txt");

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

        private void btn_ruta_logs_Click(object sender, EventArgs e)
        {
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string filePath = Path.Combine(appDataFolder, "store-soft", "logs_balanza.txt");

            // Obtén el directorio donde se encuentra el archivo
            string directory = Path.GetDirectoryName(filePath);

            // Abre el explorador de archivos en la ubicación del directorio
            Process.Start("explorer.exe", directory);
        }

        private void btn_recibir_Click(object sender, EventArgs e)
        {

        }
    }
}

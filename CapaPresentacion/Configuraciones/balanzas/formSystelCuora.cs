using Microsoft.VisualBasic;
using System;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;

namespace CapaPresentacion.Configuraciones.balanzas
{
    public partial class formSystelCuora : Form
    {
        string Dato_Recibido;
        private delegate void DelegadoAcceso(string accion);

        public formSystelCuora()
        {
            InitializeComponent();
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
                //habilitar_botones();
            }
            else
            {
                //deshabilitar_botones();
                // Si no se encontraron puertos, muestra un mensaje indicando que no hay puertos disponibles
                MessageBox.Show("No se encontraron puertos COM disponibles.", "Puertos COM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                alta_log("No se encontraron puertos COM disponibles.");
            }
        }
        private void alta_log(string mensaje)
        {
            try
            {

                // Obtiene la ruta de acceso a la carpeta AppData del usuario actual
                string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                // Crea la ruta completa para tu archivo dentro de la carpeta AppData
                string filePath = Path.Combine(appDataFolder, "store-soft", "logs_balanza_systel.txt");

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

        private void btnAbrirPuerto_Click(object sender, EventArgs e)
        {
            // Este es el boton de Abrir Puerto
            if (cbPuertos.SelectedItem == "")
            {
                MessageBox.Show("Seleccione un puerto primero");
                return;
            }

            SerialPort1.PortName = cbPuertos.SelectedItem.ToString();   // Definimos el puerto COM que vamos a usar y lo asignamos al coponente SerialPort
            if ((SerialPort1.IsOpen))
            {
                MessageBox.Show("El puerto ya esta abierto");
                btnAbrirPuerto.Enabled = false;                     // Switcheamos los botones
                btnCerrarPuerto.Enabled = true;
                return;
            }
            SerialPort1.BaudRate = 115200;                   // Definimos la velocidad
            SerialPort1.DataBits = 8;                        // Y la cantidad de bits que maneja el puerto
            SerialPort1.Open();                              // Si estaba cerrado, lo abrimos
            btnAbrirPuerto.Enabled = false;                         // Switcheamos los botones
            btnCerrarPuerto.Enabled = true;
            btnEnviarDatos.Enabled = true;

        }

        private void btnEstado_Click(object sender, EventArgs e)
        {
            Text_Funcion.Text = "23";
            Text_Parametros.Text = "";
            MessageBox.Show("Ya se cargaron los valores correspondientes, ahora presione Enviar.");
        }

        private void btnPedirPeso_Click(object sender, EventArgs e)
        {
            Text_Funcion.Text = "1";
            Text_Parametros.Text = "";
            MessageBox.Show("Ya se cargaron los valores correspondientes, ahora presione Enviar.");
        }

        private void btnEnviarDatos_Click(object sender, EventArgs e)
        {
                // Función para enviar los datos por el puerto serie
                string IP;
                string ORDEN;
                string PARAMETROS = "";
                int CRC = 0;
                string TramaTX = "";
                int i;
                string Temp;

                // Pasamos los datos IP, Función y Parámetros a variables locales para trabajarlas
                IP = ((char)int.Parse(Text_IP.Text)).ToString(); // Convertimos el texto a carácter según su código ASCII
                ORDEN = ((char)int.Parse(Text_Funcion.Text)).ToString();
                PARAMETROS = Text_Parametros.Text;

                //.................... Calcula el CRC ......................
                Temp = Text_Parametros.Text;
                CRC = int.Parse(Text_IP.Text) ^ int.Parse(Text_Funcion.Text); // Sacamos el CRC entre la IP y la Función
                for (i = 0; i < PARAMETROS.Length; i++)                      // Recorre todo el parámetro, haciendo el CRC de cada carácter
                {
                    CRC ^= Convert.ToInt32(Ascii_a_Hexadecimal2(Temp[i]).ToString(), 16);
                }
                //...........................................................

                TramaTX = IP + ORDEN + PARAMETROS + ((char)CRC).ToString();   // Armamos la trama completa a enviar
                try
                {
                    SerialPort1.Write(TramaTX);                              // Lo enviamos por el puerto
                    Text_Recibir.Clear();                                   // Borramos todo el texto recibido para mostrar de cero el nuevo texto
                    TextBox_trama_enviada.Text = TramaTX;
                }
                catch (Exception ex)                                         // Manejo de errores
                {
                    MessageBox.Show(ex.Message);
                }
        }

        // Función Ascii_a_Hexadecimal (debe implementarse)
        private string Ascii_a_Hexadecimal2(char caracter)
        {
            return ((int)caracter).ToString("X2"); // Convertimos el carácter ASCII a su valor hexadecimal
        }

        private string Ascii_a_Hexadecimal(string Ascii)
        {
            // Funcion para convertir un Caracter Ascii a Hexadecimal
            byte[] byteArray;
            StringBuilder hexNumbers = new StringBuilder();
            byteArray = ASCIIEncoding.ASCII.GetBytes(Ascii);
            for (int i = 0; i <= byteArray.Length - 1; i++)
                hexNumbers.Append(byteArray[i].ToString("x"));
            return hexNumbers.ToString();
        }

        private void btnCerrarPuerto_Click(object sender, EventArgs e)
        {
            if (SerialPort1.IsOpen)
            {
                SerialPort1.Close();
                btnAbrirPuerto.Enabled = true;
                btnCerrarPuerto.Enabled = false;
                btnEnviarDatos.Enabled = false;
                return;
            }
            MessageBox.Show("El puerto ya esta Cerrado");
            btnAbrirPuerto.Enabled = true;
            btnCerrarPuerto.Enabled = false;
            btnEnviarDatos.Enabled = false;
        }

        private void cbPuertos_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnAbrirPuerto.Enabled = true;
        }

        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                //Dato_Recibido = SerialPort1.ReadExisting;
                acceso_interrupcion(SerialPort1.ReadExisting());

                if (SerialPort1.BytesToRead == 0)
                {
                    string Temp1;
                    Temp1 = Strings.Replace(Dato_Recibido, Constants.vbNullChar, " ");
                    Text_Recibir.Text = Temp1;
                }
                Text_Recibir.Text += Dato_Recibido;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void acceso_interrupcion(string accion)
        {
            DelegadoAcceso var_delegado_acceso;
            var_delegado_acceso = new DelegadoAcceso(acceso_form);
            object[] arg = { accion };
            base.Invoke(var_delegado_acceso, arg);
        }

        private void acceso_form(string accion)
        {
            Text_Recibir.Text = accion;
            //lblDatosRecibidos.Text = accion;

        }

    }
}

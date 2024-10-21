using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace CapaPresentacion.Ventas
{
    public partial class formFE : Form
    {
        private static readonly string baseDir = @"D:\dev\inven-control\CapaPresentacion\Resources\afip\xml"; // Cambiar por la ruta de tu base de directorios
        private static readonly string urlWsdl = "https://servicios1.afip.gov.ar/wsfe/service.asmx"; // Cambiar por la URL correcta
        private static readonly string filename = Path.Combine(baseDir, "Resources", "FEDummy.xml");

        public formFE()
        {
            InitializeComponent();

            // Crear una lista de ítems clave-valor
            List<ComboBoxItem> items_tipos_doc = new List<ComboBoxItem>
            {
                new ComboBoxItem("1", "CUIT"),
                new ComboBoxItem("2", "CUIL"),
                new ComboBoxItem("3", "CDI"),
                new ComboBoxItem("4", "LE"),
                new ComboBoxItem("5", "LC"),
                new ComboBoxItem("6", "DNI"),
                new ComboBoxItem("7", "Sin identificar"),
            };

            // Crear una lista de ítems clave-valor
            List<ComboBoxItem> items_tipos_fact_comp = new List<ComboBoxItem>
            {
                new ComboBoxItem("1", "Factura A"),
                new ComboBoxItem("2", "Factura B"),
                new ComboBoxItem("3", "Factura C"),
                new ComboBoxItem("4", "Ticket factura A"),
                new ComboBoxItem("5", "Nota debito A"),
                new ComboBoxItem("6", "Nota credito A"),
                new ComboBoxItem("7", "Consumidor final"),
            };

            // Llenar el ComboBox con la lista de ítems
            cbTipoDoc.DataSource = items_tipos_doc;
            cbTipoComp.DataSource = items_tipos_fact_comp;

        }

        public class ComboBoxItem
        {
            public string Key { get; set; }
            public string Value { get; set; }

            public ComboBoxItem(string key, string value)
            {
                Key = key;
                Value = value;
            }

            // Sobrescribir el método ToString para mostrar el Value en el ComboBox
            public override string ToString()
            {
                return Value;
            }
        }

        private void cbTipoDoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)cbTipoDoc.SelectedItem;
            string selectedKey = selectedItem.Key;  // Obtener la clave (Key)
            string selectedValue = selectedItem.Value;  // Obtener la descripción (Value)
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEnviarFE_Click(object sender, EventArgs e)
        {

        }

        private async Task txtTestServer_ClickAsync(object sender, EventArgs e)
        {
            // Define el nombre del archivo XML

            if (!File.Exists(filename))
            {
                Console.WriteLine("Failed to open FEDummy.xml.");
                alta_log("Failed to open FEDummy.xml - txtTestServer_Click()");

                //return false;
            }

            string xml = File.ReadAllText(filename);

            using (HttpClient client = new HttpClient())
            {
                // Deshabilitar la verificación de SSL (similar a CURLOPT_SSL_VERIFYPEER => false)
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender2, cert, chain, sslPolicyErrors) => true
                };

                client.DefaultRequestHeaders.Add("Content-Type", "application/xml");

                // Cuerpo de la solicitud
                HttpContent content = new StringContent(xml, System.Text.Encoding.UTF8, "application/xml");

                try
                {
                    // Crear y enviar la solicitud POST
                    HttpResponseMessage response = await client.PostAsync($"{urlWsdl}/FEDummy", content);
                    response.EnsureSuccessStatusCode(); // Verifica que la respuesta es exitosa

                    // Obtener el contenido de la respuesta
                    string resp = await response.Content.ReadAsStringAsync();

                    // Cargar el XML de la respuesta
                    XmlDocument xmlResponse = new XmlDocument();
                    xmlResponse.LoadXml(resp);

                    // Obtener valores del XML
                    string appserver = xmlResponse.SelectSingleNode("//appserver")?.InnerText;
                    string dbserver = xmlResponse.SelectSingleNode("//dbserver")?.InnerText;
                    string authserver = xmlResponse.SelectSingleNode("//authserver")?.InnerText;

                    // Verificar si todos los servidores están "OK"
                    if (appserver == "OK" && dbserver == "OK" && authserver == "OK")
                    {
                        alta_log("log 1");

                        //return true;
                    }
                    else
                    {
                        alta_log("log 2");
                        //return false;
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Request error: {ex.Message}");
                    alta_log($"Request error: {ex.Message}");

                    //return false;
                }
            }
        }

        private void alta_log(string mensaje)
        {
            try
            {

                // Obtiene la ruta de acceso a la carpeta AppData del usuario actual
                string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                // Crea la ruta completa para tu archivo dentro de la carpeta AppData
                string filePath = Path.Combine(appDataFolder, "store-soft", "logs_facturacion_elect.txt");

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
    }


}

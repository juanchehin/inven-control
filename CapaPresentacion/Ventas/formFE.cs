using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

using CapaNegocio;
using System.Security;

namespace CapaPresentacion.Ventas
{
    public partial class formFE : Form
    {
        //private static readonly string baseDir = AppDomain.CurrentDomain.BaseDirectory; // Cambiar por la ruta de tu base de directorios
        //D:\dev\inven-control\CapaPresentacion\Resources\afip\xml
        private static readonly string baseDir = @" D:\dev\inven-control\CapaPresentacion\Resources\afip\20296243230"; // Cambiar por la ruta de tu base de directorios
        private static readonly string urlWsdl = "https://servicios1.afip.gov.ar/wsfe/service.asmx"; // Cambiar por la URL correcta
        private static readonly string filename = System.IO.Path.Combine(baseDir, "FEDummy.xml");
        private static readonly HttpClient client = new HttpClient();
        LoginTicket login_ticket = new LoginTicket();
        private static readonly string URL_WSDL = "https://servicios1.afip.gov.ar/wsfe/service.asmx";   // produccion
        // private static readonly string URL_WSDL = "https://wsaahomo.afip.gov.ar/wsfe/service.asmx";  // homologacion
        public XmlDocument XmlLoginTicketResponse = null;

        private static readonly string CUIT = "20296243230";
        CN_Ventas objeto_ventas = new CN_Ventas();


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

        private void alta_log(string mensaje)
        {
            try
            {
                // Obtiene la ruta de acceso a la carpeta AppData del usuario actual
                string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                // Crea la ruta completa para tu archivo dentro de la carpeta AppData
                string filePath = System.IO.Path.Combine(appDataFolder, "store-soft", "logs_facturacion_elect.txt");

                // Verifica si el directorio del archivo existe, si no, lo crea
                string directoryPath = System.IO.Path.GetDirectoryName(filePath);
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

        private async Task get_last_comprobanteAsync(string p_token,string p_sign)
        {
            try
            {
                string xmlData = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                    <soap:Body>
                    <FERecuperaLastCMPRequest xmlns=""http://ar.gov.afip.dif.facturaelectronica/"">
                        <argAuth>
                            <Token>{p_token}</Token>
                            <Sign>{p_sign}</Sign>
                            <cuit>20296243230</cuit>
                        </argAuth>
                        <argTCMP>
                            <PtoVta>00016</PtoVta>
                            <TipoCbte>001</TipoCbte>
                        </argTCMP>
                    </FERecuperaLastCMPRequest>
                    </soap:Body>
                </soap:Envelope>";

                using (HttpClient client = new HttpClient())
                {
                    // Configurar la URL y los encabezados
                    client.BaseAddress = new Uri("https://servicios1.afip.gov.ar/");
                    client.DefaultRequestHeaders.Add("SOAPAction", "http://ar.gov.afip.dif.facturaelectronica/FEConsultaCAERequest");

                    // Crear el contenido HTTP con el XML
                    HttpContent content = new StringContent(xmlData, Encoding.UTF8, "text/xml");

                    // Enviar el POST
                    HttpResponseMessage response = await client.PostAsync("wsfe/service.asmx", content);

                    // Verificar la respuesta
                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Respuesta del servidor:");
                        Console.WriteLine(result);
                    }
                    else
                    {
                        MessageBox.Show("Error al enviar la solicitud. Código de estado: " + response.StatusCode);
                        alta_log("Error al enviar la solicitud. Código de estado: " + response.StatusCode);
                    }
                }

            }
            catch (Exception ex)
            {
                alta_log("Error get_last_comprobante - " + ex.Message);

                MessageBox.Show(ex.Message.ToString(), "Error get_last_comprobante");
            }

        }

        private async Task obtener_caeAsync(string p_token,string p_sign)
        {
            // Valores obtenidos de los campos de tu formulario
            string token = "valorToken"; // txtToken.Text
            string sign = "valorSign";   // txtSign.Text
            string cuit = CUIT;   // txtCuit.Text

            string tipoDoc = "valorTipoDoc"; // txtTipoDoc.Text
            string nroDoc = "valorNroDoc";   // txtNroDoc.Text

            try
            {

                /// XML que enviarás en la solicitud
                string xmlData = @"<?xml version=""1.0"" encoding=""utf-8""?>
                    <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                      <soap:Body>
                        <FEAutRequest xmlns=""http://ar.gov.afip.dif.facturaelectronica/"">
                          <argAuth>
                            <Token>{0}</Token>
                            <Sign>{1}</Sign>
                            <cuit>{2}</cuit>
                          </argAuth>
                          <Fer>
                            <Fecr>
                              <id>1</id>
                              <cantidadreg>1</cantidadreg>
                              <presta_serv>1</presta_serv>
                            </Fecr>
                            <Fedr>
                              <FEDetalleRequest>
                                <tipo_doc>80</tipo_doc>
                                <nro_doc>2034561258</nro_doc>
                                <tipo_cbte>int</tipo_cbte>
                                <punto_vta>int</punto_vta>
                                <cbt_desde>long</cbt_desde>
                                <cbt_hasta>long</cbt_hasta>
                                <imp_total>double</imp_total>
                                <imp_tot_conc>double</imp_tot_conc>
                                <imp_neto>double</imp_neto>
                                <impto_liq>double</impto_liq>
                                <impto_liq_rni>double</impto_liq_rni>
                                <imp_op_ex>double</imp_op_ex>
                                <fecha_cbte>string</fecha_cbte>
                                <fecha_serv_desde>string</fecha_serv_desde>
                                <fecha_serv_hasta>string</fecha_serv_hasta>
                                <fecha_venc_pago>string</fecha_venc_pago>
                              </FEDetalleRequest>
                            </Fedr>
                          </Fer>
                        </FEAutRequest>
                      </soap:Body>
                    </soap:Envelope>";

                using (HttpClient client = new HttpClient())
                {
                    // Configurar el contenido de la solicitud
                    HttpContent content = new StringContent(xmlData, Encoding.UTF8, "text/xml");
                    content.Headers.Add("SOAPAction", "http://ar.gov.afip.dif.facturaelectronica/FEAutRequest");

                    // Enviar solicitud POST
                    HttpResponseMessage response = await client.PostAsync(URL_WSDL, content);

                    // Verificar la respuesta
                    if (response.IsSuccessStatusCode)
                    {
                        string responseXml = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Respuesta del servidor:");
                        Console.WriteLine(responseXml);
                    }
                    else
                    {
                        Console.WriteLine($"Error en la solicitud: {response.StatusCode}");
                    }
                }

            }
            catch (Exception ex)
            {
                alta_log("Error obtener_cae - " + ex.Message);

                MessageBox.Show(ex.Message.ToString(), "Error obtener_cae");
            }

        }

        private void funcion_base()
        {
            try
            {


            }
            catch (Exception ex)
            {
                alta_log("Error get_last_comprobante - " + ex.Message);

                MessageBox.Show(ex.Message.ToString(), "Error get_last_comprobante");
            }

        }

        private void btnEnviarFE_Click(object sender, EventArgs e)
        {
            // chequear token AQUI
            string rutaCertificado = @"C:\afip\certificado.pfx";

            // cheqeuar expiration time
            if(this.check_expiration_time())
            {
               string ticket_response = login_ticket.ObtenerLoginTicketResponse("wsfe", "https://wsaa.afip.gov.ar/ws/services/LoginCms?WSDL",
               rutaCertificado,
               ConvertToSecureString("20351975"),
               null, null, null, true);

                XmlLoginTicketResponse = new XmlDocument();
                XmlLoginTicketResponse.LoadXml(ticket_response);

                if (ticket_response != null)
                {
                    this.get_last_comprobanteAsync(XmlLoginTicketResponse.SelectSingleNode("//token").InnerText, XmlLoginTicketResponse.SelectSingleNode("//sign").InnerText);

                    string UniqueId = XmlLoginTicketResponse.SelectSingleNode("//uniqueId").InnerText;
                    string GenerationTime = XmlLoginTicketResponse.SelectSingleNode("//generationTime").InnerText;
                    string ExpirationTime = XmlLoginTicketResponse.SelectSingleNode("//expirationTime").InnerText;
                    string Sign = XmlLoginTicketResponse.SelectSingleNode("//sign").InnerText;
                    string Token = XmlLoginTicketResponse.SelectSingleNode("//token").InnerText;

                    CN_Ventas.alta_credencial_afip(UniqueId, Token, Sign, ExpirationTime, GenerationTime);
                }
                else
                {
                    alta_log("Error ticket_response");

                    MessageBox.Show("Error ticket_response");
                }
            }
            else
            {
                DataSet data_set_result;
                data_set_result = objeto_ventas.dame_credencial_afip();

                var unique_id = "";
                var token = "";
                var sign = "";
                var expiration_time = "";

                if(data_set_result != null && data_set_result.Tables.Count > 0 && data_set_result.Tables[0].Rows.Count > 0)
                {
                    // Verificar si hay tablas en el DataSet
                    if (data_set_result.Tables.Count > 0)
                    {
                        DataTable dataTable = data_set_result.Tables[0]; // Obtener la primera tabla

                        // Recorrer las filas de la tabla
                        foreach (DataRow row in dataTable.Rows)
                        {
                            // Obtener los valores de las columnas, ajusta los nombres a los que tiene tu tabla
                            unique_id = row["unique_id"].ToString(); // Cambia "Id" por el nombre de la columna real
                            token = row["token"].ToString(); // Cambia "Token" por el nombre de la columna real
                            sign = row["sign"].ToString(); // Cambia "Sign" por el nombre de la columna real
                            expiration_time = row["expiration_time"].ToString(); // Cambia "Expiration" por el nombre de la columna real

                        }

                        this.get_last_comprobanteAsync(token, sign);

                    }
                    else
                    {
                        alta_log("No se encontraron resultados. dame_credencial_afip()");
                    }
                }
                else
                {
                    MessageBox.Show("Error data_set_result dame_credencial_afip ");

                    alta_log("No se encontraron resultados. dame_credencial_afip()");
                }

            }
            // 

        }

        private Boolean check_expiration_time()
        {
            try
            {
                if(CN_Ventas.check_expiration_time() == "Ok")
                {
                    return true;
                }
                else {
                    return false;
                }

            }
            catch (Exception ex)
            {
                alta_log("Error get_last_comprobante - " + ex.Message);

                MessageBox.Show(ex.Message.ToString(), "Error get_last_comprobante");
                return false;
            }

        }

        public static SecureString ConvertToSecureString(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException(nameof(input), "La cadena de entrada no puede ser nula o vacía.");

            SecureString secureString = new SecureString();

            foreach (char c in input)
            {
                secureString.AppendChar(c);
            }

            secureString.MakeReadOnly(); // Opcional: marca el SecureString como de solo lectura
            return secureString;
        }
    }


}

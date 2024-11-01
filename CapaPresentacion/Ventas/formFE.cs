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
using System.Security.Policy;

namespace CapaPresentacion.Ventas
{
    public partial class formFE : Form
    {
        //private static readonly string baseDir = AppDomain.CurrentDomain.BaseDirectory; // Cambiar por la ruta de tu base de directorios
        //D:\dev\inven-control\CapaPresentacion\Resources\afip\xml
        private static readonly string baseDir = @" D:\dev\inven-control\CapaPresentacion\Resources\afip\{cuit}"; // Cambiar por la ruta de tu base de directorios
        private static readonly string urlWsdl = "https://servicios1.afip.gov.ar/wsfe/service.asmx"; // Cambiar por la URL correcta
        private static readonly string filename = System.IO.Path.Combine(baseDir, "FEDummy.xml");
        private static readonly HttpClient client = new HttpClient();
        LoginTicket login_ticket = new LoginTicket();
        private static readonly string URL_WSDL = "https://servicios1.afip.gov.ar/wsfe/service.asmx";   // produccion
        // private static readonly string URL_WSDL = "https://wsaahomo.afip.gov.ar/wsfe/service.asmx";  // homologacion
        public XmlDocument XmlLoginTicketResponse = null;

        //private static readonly string CUIT = "{cuit}";
        CN_Ventas objeto_ventas = new CN_Ventas();
        DataSet datos_contribuyente;
        // info contributente
        string direccion_empresa;
        string cuit;
        string razon_social;
        string pto_venta;

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
            dame_info_contribuyente();
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

        // ==============================================
        // Retorna el ultimo comprobante autorizado para el tipo de comprobante / cuit / punto de venta ingresado / Tipo de Emisión
        // ==============================================
        private async Task get_last_comprobanteAsync(string p_token,string p_sign)
        {
            try
            {
                string xmlData = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ar=""http://ar.gov.afip.dif.FEV1/"">
                    <soapenv:Header/>
                    <soapenv:Body>
                        <ar:FECompUltimoAutorizado>
                            <ar:Auth>
                                <ar:Token>{p_token}</ar:Token>
                                <ar:Sign>{p_sign}</ar:Sign>
                                <ar:Cuit>{cuit}</ar:Cuit>
                            </ar:Auth>
                            <ar:PtoVta>00016</ar:PtoVta>
                            <ar:CbteTipo>{cbTipoComp.Text}</ar:CbteTipo>
                        </ar:FECompUltimoAutorizado>
                    </soapenv:Body>
                </soapenv:Envelope>";

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

        // ==============================================
        // Solicitud de Código de Autorización Electrónico (CAE)
        // ==============================================
        private async Task fe_cae_solicitarAsync(string p_token, string p_sign)
        {
            try
            {
                var url = "https://servicios1.afip.gov.ar/wsfev1/service.asmx";

                // Crear el XML usando interpolación de cadenas
                var xmlData = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                    <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" 
                                   xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" 
                                   xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                      <soap:Body>
                        <FECAESolicitar xmlns=""http://ar.gov.afip.dif.FEV1/"">
                          <Auth>
                            <Token>{p_token}</Token>
                            <Sign>{p_sign}</Sign>
                            <Cuit>{cuit}</Cuit>
                          </Auth>
                          <FeCAEReq>
                            <FeCabReq />
                            <FeDetReq>
                              <FECAEDetRequest />
                              <FECAEDetRequest />
                            </FeDetReq>
                          </FeCAEReq>
                        </FECAESolicitar>
                      </soap:Body>
                    </soap:Envelope>";

                using (var client = new HttpClient())
                {
                    // Configurar los encabezados de la solicitud
                    client.DefaultRequestHeaders.Add("SOAPAction", "http://ar.gov.afip.dif.FEV1/FECAESolicitar");

                    // Crear el contenido de la solicitud usando el XML
                    var content = new StringContent(xmlData, Encoding.UTF8, "text/xml");

                    // Enviar la solicitud y obtener la respuesta
                    HttpResponseMessage response = await client.PostAsync(url, content);

                    // Verificar si la respuesta es exitosa
                    if (response.IsSuccessStatusCode)
                    {
                        // Leer el contenido de la respuesta
                        string responseContent = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        alta_log("Error get_last_comprobante - " + response.StatusCode + " - " + await response.Content.ReadAsStringAsync());
                        MessageBox.Show("Error get_last_comprobante " + response.StatusCode);
                    }
                }

            }
            catch (Exception ex)
            {
                alta_log("Error get_last_comprobante - " + ex.Message);
                MessageBox.Show(ex.Message.ToString(), "Error get_last_comprobante");
            }

        }

        // ==============================================
        // Recupera el listado de puntos de venta registrados y su estado
        // FEParamGetPtosVenta 
        // ==============================================
        private async Task get_ptos_venta_Async(string p_token, string p_sign)
        {
            try
            {
                var url = "https://servicios1.afip.gov.ar/wsfev1/service.asmx";

                // Crear el XML usando interpolación de cadenas
                var xmlData = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                    <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" 
                                   xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" 
                                   xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                      <soap:Body>
                        <FEParamGetPtosVenta xmlns=""http://ar.gov.afip.dif.FEV1/"">
                          <Auth>
                            <Token>{p_token}</Token>
                            <Sign>{p_sign}</Sign>
                            <Cuit>{cuit}</Cuit>
                          </Auth>
                        </FEParamGetPtosVenta>
                      </soap:Body>
                    </soap:Envelope>";

                using (var client = new HttpClient())
                {
                    // Configurar los encabezados de la solicitud
                    client.DefaultRequestHeaders.Add("SOAPAction", "http://ar.gov.afip.dif.FEV1/FEParamGetPtosVenta");

                    // Crear el contenido de la solicitud usando el XML
                    var content = new StringContent(xmlData, Encoding.UTF8, "text/xml");

                    // Enviar la solicitud y obtener la respuesta
                    HttpResponseMessage response = await client.PostAsync(url, content);

                    // Verificar si la respuesta es exitosa
                    if (response.IsSuccessStatusCode)
                    {
                        // Leer el contenido de la respuesta
                        string responseContent = await response.Content.ReadAsStringAsync();

                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(responseContent);

                        XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
                        nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                        nsmgr.AddNamespace("fe", "http://ar.gov.afip.dif.FEV1/");

                        // Extraer y construir el mensaje
                        string ambiente = xmlDoc.SelectSingleNode("//fe:FEHeaderInfo/fe:ambiente", nsmgr)?.InnerText;
                        string fecha = xmlDoc.SelectSingleNode("//fe:FEHeaderInfo/fe:fecha", nsmgr)?.InnerText;
                        string id = xmlDoc.SelectSingleNode("//fe:FEHeaderInfo/fe:id", nsmgr)?.InnerText;
                        string nroPuntoVenta = xmlDoc.SelectSingleNode("//fe:PtoVenta/fe:Nro", nsmgr)?.InnerText;
                        string emisionTipo = xmlDoc.SelectSingleNode("//fe:PtoVenta/fe:EmisionTipo", nsmgr)?.InnerText;
                        string bloqueado = xmlDoc.SelectSingleNode("//fe:PtoVenta/fe:Bloqueado", nsmgr)?.InnerText;
                        string fchBaja = xmlDoc.SelectSingleNode("//fe:PtoVenta/fe:FchBaja", nsmgr)?.InnerText;

                        string mensaje = $"Ambiente: {ambiente}\n" +
                                         $"Fecha: {fecha}\n" +
                                         $"ID: {id}\n\n" +
                                         $"Punto de Venta:\n" +
                                         $"  Número: {nroPuntoVenta}\n" +
                                         $"  Tipo de Emisión: {emisionTipo}\n" +
                                         $"  Bloqueado: {bloqueado}\n" +
                                         $"  Fecha de Baja: {fchBaja}";

                        // Mostrar el mensaje
                        MessageBox.Show(mensaje, "Respuesta de AFIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        alta_log("Error get_last_comprobante - " + response.StatusCode + " - " + await response.Content.ReadAsStringAsync());
                        MessageBox.Show("Error get_last_comprobante " + response.StatusCode);
                    }
                }

            }
            catch (Exception ex)
            {
                alta_log("Error get_last_comprobante - " + ex.Message);
                MessageBox.Show(ex.Message.ToString(), "Error get_last_comprobante");
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
                    string UniqueId = XmlLoginTicketResponse.SelectSingleNode("//uniqueId").InnerText;
                    string GenerationTime = XmlLoginTicketResponse.SelectSingleNode("//generationTime").InnerText;
                    string ExpirationTime = XmlLoginTicketResponse.SelectSingleNode("//expirationTime").InnerText;
                    string Sign = XmlLoginTicketResponse.SelectSingleNode("//sign").InnerText;
                    string Token = XmlLoginTicketResponse.SelectSingleNode("//token").InnerText;

                    if(CN_Ventas.alta_credencial_afip(UniqueId, Token, Sign, ExpirationTime, GenerationTime) == "ok")
                    {
                        //this.get_last_comprobanteAsync(XmlLoginTicketResponse.SelectSingleNode("//token").InnerText, XmlLoginTicketResponse.SelectSingleNode("//sign").InnerText);
                        this.get_ptos_venta_Async(XmlLoginTicketResponse.SelectSingleNode("//token").InnerText, XmlLoginTicketResponse.SelectSingleNode("//sign").InnerText);
                    }
                    else
                    {
                        alta_log("Error CN_Ventas.alta_credencial_afip");
                        MessageBox.Show("Error ticket_response - Contactese con el administrador");
                        return;
                    }
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

                        //this.get_last_comprobanteAsync(token, sign);
                        this.get_ptos_venta_Async(token, sign);


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
                if(CN_Ventas.check_expiration_time() == "OK")
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

        private void dame_info_contribuyente()
        {
            try
            {
                datos_contribuyente = objeto_ventas.dame_info_contribuyente();

                if (datos_contribuyente.Tables[0].Rows.Count > 0)
                {
                    DataRow firstRow = datos_contribuyente.Tables[0].Rows[0];

                    object value = firstRow[0];
                    direccion_empresa = firstRow[1].ToString();
                    cuit = firstRow[2].ToString();
                    razon_social = firstRow[5].ToString();
                    pto_venta = firstRow[6].ToString();
                }
                else
                {
                    alta_log("Error dame_info_contribuyente");
                    MessageBox.Show("Error dame_info_contribuyente");
                }
            }
            catch (Exception ex)
            {
                alta_log("Error get_last_comprobante - " + ex.Message);

                MessageBox.Show(ex.Message.ToString(), "Error get_last_comprobante");
            }

        }

    }


}

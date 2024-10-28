using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
//
//using AfipWsfeClient;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Net;
//using AfipServiceReference;
using System.ServiceModel;
using System.Security.Cryptography.Xml;
using System.Security;
using ClienteLoginCms_CS;


namespace CapaPresentacion.Ventas
{
    public partial class formFE : Form
    {

        LoginTicket login_ticket = new LoginTicket();
        //private static readonly string baseDir = AppDomain.CurrentDomain.BaseDirectory; // Cambiar por la ruta de tu base de directorios
        //D:\dev\inven-control\CapaPresentacion\Resources\afip\xml
        private static readonly string baseDir = @" D:\dev\inven-control\CapaPresentacion\Resources\afip\20296243230"; // Cambiar por la ruta de tu base de directorios
        private static readonly string urlWsdl = "https://servicios1.afip.gov.ar/wsfe/service.asmx"; // Cambiar por la URL correcta
        private static readonly string filename = Path.Combine(baseDir, "FEDummy.xml");
        private static readonly HttpClient client = new HttpClient();

        private static readonly string URL_WSDL = "https://servicios1.afip.gov.ar/wsfe/service.asmx";   // produccion
                                                                                                        // private static readonly string URL_WSDL = "https://wsaahomo.afip.gov.ar/wsfe/service.asmx";  // homologacion

        public UInt32 UniqueId; // Entero de 32 bits sin signo que identifica el requerimiento
        public DateTime GenerationTime; // Momento en que fue generado el requerimiento
        public DateTime ExpirationTime; // Momento en el que expira la solicitud
        public string Service; // Identificacion del WSN para el cual se solicita el TA
        public string Sign; // Firma de seguridad recibida en la respuesta
        public string Token; // Token de seguridad recibido en la respuesta
        public XmlDocument XmlLoginTicketRequest = null;
        public XmlDocument XmlLoginTicketResponse = null;
        public string RutaDelCertificadoFirmante;
        public string XmlStrLoginTicketRequestTemplate = "<loginTicketRequest><header><uniqueId></uniqueId><generationTime></generationTime><expirationTime></expirationTime></header><service></service></loginTicketRequest>";
        private bool _verboseMode = true;
        private static UInt32 _globalUniqueID = 0; // OJO! NO ES THREAD-SAFE

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

        // Obtiene el token y el sign
        private async Task generar_ticket()
        {
            //var rutaCertificado = Path.Combine(baseDir,"produccion","keys", "certificado.pfx");

            string rutaCertificado = @"C:\afip\certificado.pfx";

            FirmarTRA(CrearTRA("wsfe"), rutaCertificado, "20351975");
        }

        //Crea El archivo es necesario para realizar la firma
        private string CrearTRA(string servicio)
        {
            var xmlDoc = new XDocument(
                new XElement("loginTicketRequest",
                    new XAttribute("version", "1.0"),
                    new XElement("header",
                        new XElement("uniqueId", Convert.ToInt32(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds)),
                        new XElement("generationTime", DateTime.UtcNow.AddMinutes(-10).ToString("yyyy-MM-ddTHH:mm:ssZ")),
                        new XElement("expirationTime", DateTime.UtcNow.AddMinutes(10).ToString("yyyy-MM-ddTHH:mm:ssZ"))
                    ),
                    new XElement("service", servicio)
                )
            );

            return xmlDoc.ToString();
        }

        // Esta funcion realiza la firma PKCS#7 usando como entrada el archivo TRA.xml, el certificado y la clave privada
        private byte[] FirmarTRA(string traXML, string rutaCertificado, string password)
        {
            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(traXML);

                // Cargar el certificado digital
                var certificado = new X509Certificate2(rutaCertificado, password);

                // Crear una instancia de SignedXml y asociarla al documento
                var signedXml = new SignedXml(xmlDoc) { SigningKey = certificado.PrivateKey };

                // Agregar una referencia al XML a firmar
                var reference = new Reference
                {
                    Uri = ""
                };
                //reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
                signedXml.AddReference(reference);

                // Agregar información clave
                //var keyInfo = new KeyInfo();
                //keyInfo.AddClause(new KeyInfoX509Data(certificado));
                //signedXml.KeyInfo = keyInfo;

                // Firmar el documento
                signedXml.ComputeSignature();

                // Obtener el XML firmado
                var xmlDigitalSignature = signedXml.GetXml();
                xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));

                return Encoding.UTF8.GetBytes(xmlDoc.OuterXml);
            }
            catch (Exception ex)
            {
                alta_log("Error FirmarTRA - " + ex.Message);

                MessageBox.Show(ex.Message.ToString(), "Error FirmarTRA");
                return Encoding.UTF8.GetBytes("");
            }
        }



        private (string token, string sign) ObtenerTokenYSign(string xmlFirmado)
        {
            // Configuración del endpoint WSAA
            string wsaaUrl = "https://servicios1.afip.gov.ar/wsfe/service.asmx"; // Cambia a producción si es necesario

            // Crear solicitud HTTP POST
            var request = WebRequest.Create(wsaaUrl);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            // Cargar el XML firmado
            string soapRequest = $"<loginCmsRequest><loginCms>{Convert.ToBase64String(Encoding.UTF8.GetBytes(xmlFirmado))}</loginCms></loginCmsRequest>";
            byte[] soapBytes = Encoding.UTF8.GetBytes(soapRequest);
            request.ContentLength = soapBytes.Length;

            using (var dataStream = request.GetRequestStream())
            {
                dataStream.Write(soapBytes, 0, soapBytes.Length);
            }

            // Obtener respuesta
            using (var response = request.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                string responseXml = reader.ReadToEnd();

                // Procesar el XML de respuesta para obtener token y sign
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(responseXml);

                string token = xmlDoc.SelectSingleNode("//token")?.InnerText;
                string sign = xmlDoc.SelectSingleNode("//sign")?.InnerText;

                return (token, sign);
            }
        }


        private async void txtTestServer_ClickAsync(object sender, EventArgs e)
        {
            if(await test_serverAsync())
            {
                MessageBox.Show("Server Ok", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Server Error", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void get_last_comprobante(string p_pto_venta, string p_tipo_comprobante)
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

        private void solicitar_car()
        {
            try
            {
                // Configuración del binding (enlace) y endpoint del servicio
                BasicHttpBinding binding = new BasicHttpBinding();
                binding.Security.Mode = BasicHttpSecurityMode.Transport;  // Para HTTPS
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;

                // Endpoint del servicio AFIP WSFE (URL del WSDL)
                EndpointAddress endpointAddress = new EndpointAddress(URL_WSDL);

                //// Crear el cliente del servicio utilizando binding y endpoint
                //ServiceSoapClient wsfeClient = new ServiceSoapClient(binding, endpointAddress);


                //// Establecer las credenciales del servicio (Token y Sign obtenidos desde WSAA)
                //var auth = new FEAuthRequest
                //{
                //    Token = "TU_TOKEN",  // Token obtenido de WSAA
                //    Sign = "TU_SIGN",    // Sign obtenido de WSAA
                //    Cuit = 12345678901   // CUIT del contribuyente
                //};

                // Parámetros para obtener el último comprobante autorizado
                int puntoDeVenta = 1;  // El número de punto de venta que deseas consultar
                int tipoComprobante = 1;  // Tipo de comprobante (Factura A = 1, Factura B = 6, etc.)

                // Realizar la consulta
                //var resultado = wsfeClient.FECompUltimoAutorizado(auth, puntoDeVenta, tipoComprobante);

                //if (resultado != null)
                //{
                //    Console.WriteLine($"Último comprobante autorizado: {resultado.CbteNro}");
                //}
                //else
                //{
                //    Console.WriteLine("No se pudo obtener el último comprobante autorizado.");
                //}
            }
            catch (Exception ex)
            {
                alta_log("Error solicitar_car - " + ex.Message);

                MessageBox.Show(ex.Message.ToString(), "Error get_last_comprobante");
            }

        }

        private void check_token()
        {
            try
            {
                string ta = Path.Combine(baseDir, "produccion", "xml", "TA.xml");
                bool generateToken;

                // ¿Genero el token?
                if (!File.Exists(ta))
                {
                    generateToken = true;
                }
                else
                {
                    var TA = XDocument.Load(ta);
                    DateTime expirationTime = DateTime.Parse(TA.Root.Element("header").Element("expirationTime").Value);
                    DateTime actualTime = DateTime.UtcNow; // Usar UtcNow para mantener la consistencia con el tiempo UTC
                    generateToken = actualTime >= expirationTime;
                }

                if (generateToken)
                {
                    //// Renovamos el token
                    //var wsaaClient = new Wsaa(this.serviceName, this.modo, this.cuit, this.logXmls);
                    //wsaaClient.GenerateToken();

                    //// Recargamos con el nuevo token
                    //var TA = XDocument.Load(ta);
                }

                //this.token = TA.Root.Element("credentials").Element("token").Value;
                //this.sign = TA.Root.Element("credentials").Element("sign").Value;

            }
            catch (Exception ex)
            {
                alta_log("Error check_token - " + ex.Message);

                MessageBox.Show(ex.Message.ToString(), "Error check_token");
            }

        }

        private async Task dame_token_signAsync()
        {
            try
            {
                // Ruta al archivo CMS firmado previamente con OpenSSL
                string cmsFilePath = @"ruta_al_archivo/LoginTicketRequest.cms";

                // Cargar el contenido del archivo CMS
                byte[] cmsData = File.ReadAllBytes(cmsFilePath);
                string cmsBase64 = Convert.ToBase64String(cmsData);

                // Crear la solicitud SOAP para WSAA
                string soapRequest = $@"
                <soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:wsaa='http://wsaa.view.sua.dvadac.desein.afip.gov'>
                    <soapenv:Header/>
                    <soapenv:Body>
                        <wsaa:loginCms>
                            <wsaa:in0>{cmsBase64}</wsaa:in0>
                        </wsaa:loginCms>
                    </soapenv:Body>
                </soapenv:Envelope>";

                // URL del WSAA (Producción o Homologación)
                string wsaaUrl = "https://servicios1.afip.gov.ar/wsfe/service.asmx";  // Cambiar a la URL de producción si es necesario

                // Hacer la solicitud HTTP POST
                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(soapRequest, Encoding.UTF8, "text/xml");
                    HttpResponseMessage response = await client.PostAsync(wsaaUrl, content);

                    // Leer la respuesta
                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Respuesta del WSAA:");
                    Console.WriteLine(responseContent);

                    // Procesar la respuesta para obtener Token y Sign
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(responseContent);

                    // Extraer el Token y Sign del XML de respuesta
                    string token = xmlDoc.SelectSingleNode("//token").InnerText;
                    string sign = xmlDoc.SelectSingleNode("//sign").InnerText;

                    Console.WriteLine("Token: " + token);
                    Console.WriteLine("Sign: " + sign);
                }

            }
            catch (Exception ex)
            {
                alta_log("Error dame_token_sign - " + ex.Message);
                MessageBox.Show(ex.Message.ToString(), "Error dame_token_sign");
            }
        }
        private async Task<bool> test_serverAsync()
        {
            // Define la ruta al archivo XML
            //string filePath = @"ruta\a\tu\archivo\FEDummy.xml"; // Cambia esta línea a la ruta correcta

            // Cargar el contenido del archivo XML
            string soapEnvelope;
            try
            {
                soapEnvelope = File.ReadAllText(filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al leer el archivo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Configura el contenido de la solicitud
            HttpContent content = new StringContent(soapEnvelope, Encoding.UTF8, "text/xml");
            content.Headers.Add("SOAPAction", "\"http://ar.gov.afip.dif.facturaelectronica/FEDummy\"");


            try
            {
                // Envía la solicitud POST
                HttpResponseMessage response = await client.PostAsync(urlWsdl, content);

                // Verifica si la respuesta fue exitosa
                if (response.IsSuccessStatusCode)
                {
                    // Lee el contenido de la respuesta
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Cargar la respuesta en un objeto XElement para análisis
                    XElement xmlResponse = XElement.Parse(responseBody);

                    // Extraer los valores de appserver, dbserver y authserver
                    var appserver = xmlResponse.Descendants()
                        .FirstOrDefault(x => x.Name.LocalName == "appserver")?.Value;
                    var dbserver = xmlResponse.Descendants()
                        .FirstOrDefault(x => x.Name.LocalName == "dbserver")?.Value;
                    var authserver = xmlResponse.Descendants()
                        .FirstOrDefault(x => x.Name.LocalName == "authserver")?.Value;

                    // Crear el mensaje para mostrar en el MessageBox
                    string message = $"App Server: {appserver}\nDB Server: {dbserver}\nAuth Server: {authserver}";

                    // Mostrar el mensaje en un MessageBox
                    //MessageBox.Show(message, "Respuesta Server AFIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alta_log("message response server AFIP : " + message);

                    if(appserver == "Ok" && dbserver == "Ok" && authserver == "Ok")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    string errorResponse = await response.Content.ReadAsStringAsync();
                    //MessageBox.Show(errorResponse);
                    return false;
                }
            }
            catch (Exception ex)
            {
                alta_log($"Exception: {ex.Message}");
                return false;
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

            login_ticket.ObtenerLoginTicketResponse("wsfe", "https://wsaa.afip.gov.ar/ws/services/LoginCms", rutaCertificado, 
                ConvertToSecureString("20351975"),
                null, null, null, true);
            //generar_ticket();
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

﻿using AfipWsfeClient;
using CapaNegocio;
//using CapaPresentacion.WSFEv1_afip;
//using CapaPresentacion.wsfe_afip;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security;
//using AfipWsfeClient;
//using AfipServiceReference;
using System.ServiceModel; // La referencia agregada al WSFEv1
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
//using AfipServiceReference; // Ajusta esto según el nombre de tu referencia
//using System.Collections.Generic;
using AfipWsfeClient;

namespace CapaPresentacion.Ventas
{
    public partial class formFE : Form
    {
        //Conexión servicio web de la AFIP
        //Service ServiceConect { get; set; }
        //FEAuthRequest AuthAutorizar { get; set; }

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
        DateTime fecha_actual = DateTime.Now;
        string cbTipoCompSeleccionado = "0";


        public formFE()
        {
            InitializeComponent();

            try
            {
                //ServiceConect = new Service();
                //AuthAutorizar = new FEAuthRequest();

                //CargarAuthRequest(cuit, sign, token);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            // Crear una lista de ítems clave-valor
            // Tipos documento
            // https://drive.google.com/file/d/1-E02cH-uJRVNkelBTHN5vlj8zWI7wgRc/view
            List<ComboBoxItem> items_tipos_doc = new List<ComboBoxItem>
            {
                new ComboBoxItem("80", "CUIT"),
                new ComboBoxItem("86", "CUIL"),
                new ComboBoxItem("87", "CDI"),
                new ComboBoxItem("89", "LE"),
                new ComboBoxItem("90", "LC"),
                new ComboBoxItem("96", "DNI"),
                new ComboBoxItem("99", "Sin identificar"),
            };

            // Crear una lista de ítems clave-valor
            List<ComboBoxItem> items_tipos_fact_comp = new List<ComboBoxItem>
            {
                new ComboBoxItem("001", "Factura A"),
                new ComboBoxItem("006", "Factura B"),
                new ComboBoxItem("011", "Factura C"),
                new ComboBoxItem("081", "Ticke factura A"),
                new ComboBoxItem("002", "Nota debito A"),
                new ComboBoxItem("003", "Nota credito A"),
                //new ComboBoxItem("7", "Consumidor final"),
            };

            // Llenar el ComboBox con la lista de ítems
            cbTipoDoc.DataSource = items_tipos_doc;
            cbTipoDoc.DisplayMember = "Value";
            cbTipoDoc.ValueMember = "Key";

            cbTipoComp.DataSource = items_tipos_fact_comp;
            cbTipoComp.DisplayMember = "Value";
            cbTipoComp.ValueMember = "Key";

            pto_venta = "16";

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
        // Retorna el ultimo comprobante autorizado para el
        // tipo de comprobante / cuit / punto de venta ingresado / Tipo de Emisión
        // ==============================================
        private async Task get_last_comprobanteAsync(string p_token, string p_sign)
        {
            try
            {
                string cbTipoCompSeleccionado = ((ComboBoxItem)cbTipoComp.SelectedItem).Key;
                var url = "https://servicios1.afip.gov.ar/wsfev1/service.asmx";

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
                            <ar:CbteTipo>{cbTipoCompSeleccionado}</ar:CbteTipo>
                        </ar:FECompUltimoAutorizado>
                    </soapenv:Body>
                </soapenv:Envelope>";

                using (var client = new HttpClient())
                {
                    // Configurar los encabezados de la solicitud
                    client.DefaultRequestHeaders.Add("SOAPAction", "http://ar.gov.afip.dif.FEV1/FECompUltimoAutorizado");

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
            if (this.check_expiration_time())
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

                    if (CN_Ventas.alta_credencial_afip(UniqueId, Token, Sign, ExpirationTime, GenerationTime) == "ok")
                    {
                        //this.get_last_comprobanteAsync(XmlLoginTicketResponse.SelectSingleNode("//token").InnerText, XmlLoginTicketResponse.SelectSingleNode("//sign").InnerText);
                        this.solicitar_caeAsync(XmlLoginTicketResponse.SelectSingleNode("//token").InnerText, XmlLoginTicketResponse.SelectSingleNode("//sign").InnerText);
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

                if (data_set_result != null && data_set_result.Tables.Count > 0 && data_set_result.Tables[0].Rows.Count > 0)
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
                        this.solicitar_caeAsync(token, sign);


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
                if (CN_Ventas.check_expiration_time() == "OK")
                {
                    return true;
                }
                else
                {
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

        // ==============================================
        // Recibe la información de un comprobante o lote de comprobantes.
        // FECAESolicitar
        // ==============================================
        private async Task solicitar_caeAsync2(string p_token, string p_sign)
        {
            try
            {
                string cbTipoCompSeleccionado = ((ComboBoxItem)cbTipoComp.SelectedItem).Key;
                string cbTipoDocSeleccionado = ((ComboBoxItem)cbTipoDoc.SelectedItem).Key;

                // Construcción del XML
                XmlDocument xmlDoc = new XmlDocument();

                XmlElement envelope = xmlDoc.CreateElement("soapenv", "Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
                xmlDoc.AppendChild(envelope);

                XmlElement header = xmlDoc.CreateElement("soapenv", "Header", "http://schemas.xmlsoap.org/soap/envelope/");
                envelope.AppendChild(header);

                XmlElement body = xmlDoc.CreateElement("soapenv", "Body", "http://schemas.xmlsoap.org/soap/envelope/");
                envelope.AppendChild(body);

                XmlElement solicitar = xmlDoc.CreateElement("ar", "FECAESolicitar", "http://ar.gov.afip.dif.FEV1/");
                body.AppendChild(solicitar);

                // Auth
                XmlElement auth = xmlDoc.CreateElement("ar", "Auth", "http://ar.gov.afip.dif.FEV1/");
                solicitar.AppendChild(auth);

                XmlElement xmlToken = xmlDoc.CreateElement("ar", "Token", "http://ar.gov.afip.dif.FEV1/");
                xmlToken.InnerText = p_token;
                auth.AppendChild(xmlToken);

                XmlElement xmlSign = xmlDoc.CreateElement("ar", "Sign", "http://ar.gov.afip.dif.FEV1/");
                xmlSign.InnerText = p_sign;
                auth.AppendChild(xmlSign);

                XmlElement xmlCuit = xmlDoc.CreateElement("ar", "Cuit", "http://ar.gov.afip.dif.FEV1/");
                xmlCuit.InnerText = cuit.ToString();
                auth.AppendChild(xmlCuit);

                // FeCAEReq
                XmlElement feCAEReq = xmlDoc.CreateElement("ar", "FeCAEReq", "http://ar.gov.afip.dif.FEV1/");
                solicitar.AppendChild(feCAEReq);

                // FeCabReq
                XmlElement feCabReq = xmlDoc.CreateElement("ar", "FeCabReq", "http://ar.gov.afip.dif.FEV1/");
                feCAEReq.AppendChild(feCabReq);

                XmlElement xmlCantReg = xmlDoc.CreateElement("ar", "CantReg", "http://ar.gov.afip.dif.FEV1/");
                xmlCantReg.InnerText = "1";
                feCabReq.AppendChild(xmlCantReg);

                XmlElement xmlPtoVta = xmlDoc.CreateElement("ar", "PtoVta", "http://ar.gov.afip.dif.FEV1/");
                xmlPtoVta.InnerText = pto_venta.ToString();
                feCabReq.AppendChild(xmlPtoVta);

                XmlElement xmlCbteTipo = xmlDoc.CreateElement("ar", "CbteTipo", "http://ar.gov.afip.dif.FEV1/");
                xmlCbteTipo.InnerText = cbTipoCompSeleccionado;
                feCabReq.AppendChild(xmlCbteTipo);

                // FeDetReq
                XmlElement feDetReq = xmlDoc.CreateElement("ar", "FeDetReq", "http://ar.gov.afip.dif.FEV1/");
                feCAEReq.AppendChild(feDetReq);

                // FECAEDetRequest
                XmlElement feCAEDetRequest = xmlDoc.CreateElement("ar", "FECAEDetRequest", "http://ar.gov.afip.dif.FEV1/");
                feDetReq.AppendChild(feCAEDetRequest);

                // https://www.afip.gob.ar/ws/WSFEV1/documentos/manual-desarrollador-COMPG-v3-4-2.pdf
                // Detalles del comprobante

                // El campo  'Importe Total' ImpTotal, debe ser igual  a la  suma de ImpTotConc + ImpNeto + ImpOpEx + ImpTrib + ImpIVA.
                // https://www.afip.gob.ar/ws/WSFEV1/documentos/manual-desarrollador-COMPG-v3-4-2.pdf
                var v_imp_total = 0 + Convert.ToInt32(txtImporteTotal.Text) + 0 + Convert.ToInt32(txtImporteTotal.Text) + 0;
                double baseImponible = Convert.ToInt32(txtImporteTotal.Text) / (1 + (Convert.ToDouble(txtIva.Text) / 100));

                AppendElement(feCAEDetRequest, "ar", "Concepto", "1");
                AppendElement(feCAEDetRequest, "ar", "DocTipo", cbTipoDocSeleccionado);
                AppendElement(feCAEDetRequest, "ar", "DocNro", txtDocComp.Text);
                AppendElement(feCAEDetRequest, "ar", "CbteDesde", "1");
                AppendElement(feCAEDetRequest, "ar", "CbteHasta", "1");
                AppendElement(feCAEDetRequest, "ar", "CbteFch", fecha_actual.ToString("yyyyMMdd"));
                AppendElement(feCAEDetRequest, "ar", "ImpTotal", v_imp_total.ToString());     // El campo  'Importe Total' ImpTotal,
                                                                                              // debe ser igual  a la  suma de ImpTotConc +
                                                                                              // ImpNeto + ImpOpEx + ImpTrib + ImpIVA.
                AppendElement(feCAEDetRequest, "ar", "ImpTotConc", "0");    // cero comprobante C
                AppendElement(feCAEDetRequest, "ar", "ImpNeto", txtImporteTotal.Text);
                AppendElement(feCAEDetRequest, "ar", "ImpOpEx", "0");
                AppendElement(feCAEDetRequest, "ar", "ImpTrib", txtImporteTotal.Text);
                AppendElement(feCAEDetRequest, "ar", "ImpIVA", txtImporteTotal.Text);
                AppendElement(feCAEDetRequest, "ar", "FchServDesde", "");
                AppendElement(feCAEDetRequest, "ar", "FchServHasta", "");
                AppendElement(feCAEDetRequest, "ar", "FchVtoPago", "");
                AppendElement(feCAEDetRequest, "ar", "MonId", "1");
                AppendElement(feCAEDetRequest, "ar", "MonCotiz", "1");

                // CbtesAsoc
                XmlElement cbtesAsoc = xmlDoc.CreateElement("ar", "CbtesAsoc", "http://ar.gov.afip.dif.FEV1/");
                feCAEDetRequest.AppendChild(cbtesAsoc);

                XmlElement cbteAsoc = xmlDoc.CreateElement("ar", "CbteAsoc", "http://ar.gov.afip.dif.FEV1/");
                cbtesAsoc.AppendChild(cbteAsoc);

                AppendElement(cbteAsoc, "ar", "Tipo", cbTipoCompSeleccionado);
                AppendElement(cbteAsoc, "ar", "PtoVta", pto_venta);
                AppendElement(cbteAsoc, "ar", "Nro", "1");
                AppendElement(cbteAsoc, "ar", "Cuit", cuit);
                AppendElement(cbteAsoc, "ar", "CbteFch", fecha_actual.ToString("yyyyMMdd"));

                // Tributos
                XmlElement tributos = xmlDoc.CreateElement("ar", "Tributos", "http://ar.gov.afip.dif.FEV1/");
                feCAEDetRequest.AppendChild(tributos);

                XmlElement tributo = xmlDoc.CreateElement("ar", "Tributo", "http://ar.gov.afip.dif.FEV1/");
                tributos.AppendChild(tributo);

                AppendElement(tributo, "ar", "Id", "1");
                AppendElement(tributo, "ar", "Desc", "0");
                AppendElement(tributo, "ar", "BaseImp", baseImponible.ToString());
                AppendElement(tributo, "ar", "Alic", "1");
                AppendElement(tributo, "ar", "Importe", txtImporteTotal.Text);

                // IVA
                XmlElement iva = xmlDoc.CreateElement("ar", "Iva", "http://ar.gov.afip.dif.FEV1/");
                feCAEDetRequest.AppendChild(iva);

                XmlElement alicIva = xmlDoc.CreateElement("ar", "AlicIva", "http://ar.gov.afip.dif.FEV1/");
                iva.AppendChild(alicIva);

                AppendElement(alicIva, "ar", "Id", "0005");
                AppendElement(alicIva, "ar", "BaseImp", "0");
                AppendElement(alicIva, "ar", "Importe", "1");

                // Opcionales
                XmlElement opcionales = xmlDoc.CreateElement("ar", "Opcionales", "http://ar.gov.afip.dif.FEV1/");
                feCAEDetRequest.AppendChild(opcionales);

                XmlElement opcional = xmlDoc.CreateElement("ar", "Opcional", "http://ar.gov.afip.dif.FEV1/");
                opcionales.AppendChild(opcional);

                AppendElement(opcional, "ar", "Id", "1");
                AppendElement(opcional, "ar", "Valor", "1");

                // Comprador
                XmlElement comprador = xmlDoc.CreateElement("ar", "Comprador", "http://ar.gov.afip.dif.FEV1/");
                feCAEDetRequest.AppendChild(comprador);

                AppendElement(comprador, "ar", "DocTipo", cbTipoDocSeleccionado);
                AppendElement(comprador, "ar", "DocNro", txtDocComp.Text);
                AppendElement(comprador, "ar", "Porcentaje", "1");

                // Filtro
                XmlElement filtro = xmlDoc.CreateElement("ar", "Filtro", "http://ar.gov.afip.dif.FEV1/");
                feCAEDetRequest.AppendChild(filtro);

                AppendElement(filtro, "ar", "FchDesde", fecha_actual.ToString("yyyyMMdd"));
                AppendElement(filtro, "ar", "FchHasta", fecha_actual.ToString("yyyyMMdd"));

                // Actividad
                XmlElement actividad = xmlDoc.CreateElement("ar", "Actividad", "http://ar.gov.afip.dif.FEV1/");
                feCAEDetRequest.AppendChild(actividad);

                AppendElement(actividad, "ar", "Id", "1");

                // Convertir el XML a string
                string xmlString = xmlDoc.OuterXml;

                // Mostrar el XML en un MessageBox
                //MessageBox.Show(xmlString, "XML Solicitud Comprobante", MessageBoxButtons.OK, MessageBoxIcon.Information);
                var url = "https://servicios1.afip.gov.ar/wsfev1/service.asmx?WSDL";

                // Crear un objeto HttpClient
                using (var client = new HttpClient())
                {
                    // Configurar los encabezados de la solicitud
                    client.DefaultRequestHeaders.Add("SOAPAction", "http://ar.gov.afip.dif.FEV1/FECAESolicitar");

                    // Crear el contenido de la solicitud usando el XML
                    var content = new StringContent(xmlString, Encoding.UTF8, "text/xml");

                    // Enviar la solicitud y obtener la respuesta
                    HttpResponseMessage response = await client.PostAsync(url, content);

                    // Verificar si la respuesta es exitosa
                    if (response.IsSuccessStatusCode)
                    {
                        // Leer el contenido de la respuesta -XML
                        string responseContent = await response.Content.ReadAsStringAsync();
                        string code;
                        string msg;

                        XmlDocument xmlDocResponse = new XmlDocument();
                        xmlDocResponse.LoadXml(responseContent);

                        // Definir el espacio de nombres
                        XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDocResponse.NameTable);
                        nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                        nsmgr.AddNamespace("ar", "http://ar.gov.afip.dif.FEV1/");

                        // Seleccionar los nodos de observación
                        XmlNodeList observations = xmlDocResponse.SelectNodes("//ar:Observaciones/ar:Obs", nsmgr);

                        code = "";
                        // Construir el mensaje para mostrar
                        string message = "Observaciones encontradas:\n\n";
                        foreach (XmlNode observation in observations)
                        {
                            code = observation.SelectSingleNode("ar:Code", nsmgr)?.InnerText ?? "No disponible";
                            msg = observation.SelectSingleNode("ar:Msg", nsmgr)?.InnerText ?? "No disponible";
                            message += $"Code: {code}\nMsg: {msg}\n\n";
                        }

                        if (code != "")
                        {
                            // Mostrar el mensaje en un MessageBox
                            MessageBox.Show(message, "Observaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            alta_log("solicitar_caeAsync - responseContent  - " + message);

                        }

                        Console.WriteLine("Respuesta de AFIP: ");
                        Console.WriteLine(responseContent);
                    }
                    else
                    {
                        alta_log("get_last_comprobante - " + response.StatusCode);
                        MessageBox.Show("get_last_comprobante - " + response.StatusCode);

                    }
                }

            }
            catch (Exception ex)
            {
                alta_log("Error get_last_comprobante - " + ex.Message);
                MessageBox.Show(ex.Message.ToString(), "Error get_last_comprobante");
            }

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


        private void AppendElement(XmlElement parent, string prefix, string name, string value)
        {
            XmlElement element = parent.OwnerDocument.CreateElement(prefix, name, parent.NamespaceURI);
            element.InnerText = value;
            parent.AppendChild(element);
        }

        private void cbTipoComp_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbTipoCompSeleccionado = ((ComboBoxItem)cbTipoComp.SelectedItem).Value;
        }

        private void txtImporteTotal_KeyDown(object sender, KeyEventArgs e)
        {
            // Verifica si la tecla presionada es Enter
            if (e.KeyCode == Keys.Enter)
            {
                btnEnviarFE.PerformClick();

                // Evita el sonido de "ding" cuando se presiona Enter
                e.Handled = true;
                e.SuppressKeyPress = true;

            }
        }

        private void txtTestServer_Click(object sender, EventArgs e)
        {
            test_serverAsync();
        }
        // ==============================================
        // Testeo de servers AFIP
        // ==============================================
        private async Task test_serverAsync()
        {
            // Define la URL del servicio web
            string url = "https://servicios1.afip.gov.ar/wsfe/service.asmx";

            // Crea el contenido XML para la solicitud SOAP
            string soapEnvelope = @"<?xml version=""1.0"" encoding=""utf-8""?>
            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" 
                           xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" 
                           xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
              <soap:Body>
                <FEDummy xmlns=""http://ar.gov.afip.dif.facturaelectronica/"" />
              </soap:Body>
            </soap:Envelope>";

            // Configura el contenido de la solicitud
            HttpContent content = new StringContent(soapEnvelope, Encoding.UTF8, "text/xml");
            content.Headers.Add("SOAPAction", "\"http://ar.gov.afip.dif.facturaelectronica/FEDummy\"");

            try
            {
                // Envía la solicitud POST
                HttpResponseMessage response = await client.PostAsync(url, content);

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
                    MessageBox.Show(message, "Respuesta Server AFIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alta_log("message response server AFIP : " + message);

                }
                else
                {
                    string errorResponse = await response.Content.ReadAsStringAsync();
                    MessageBox.Show(errorResponse);
                }
            }
            catch (Exception ex)
            {
                alta_log($"Exception: {ex.Message}");
            }
        }
        // ==============================================
        // 
        // ==============================================
        private async Task FECAESolicitarAsync(string p_token, string p_sign)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://servicios1.afip.gov.ar/wsfev1/service.asmx?op=FECAESolicitar");

                // https://wswhomo.afip.gov.ar/wsfev1/service.asmx?op=FECAESolicitar

                string xmlTemplate = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/""
                                    xmlns:ar=""http://ar.gov.afip.dif.FEV1/"">
                                    <soapenv:Header/>
                                    <soapenv:Body>
                                        <ar:FECAESolicitar>
                                            <ar:Auth>
                                                <ar:Token>?</ar:Token>
                                                <ar:Sign>?</ar:Sign>
                                                <ar:Cuit>20296243230</ar:Cuit>
                                            </ar:Auth>
                                            <ar:FeCAEReq>
                                                <ar:FeCabReq>
                                                    <ar:CantReg>1</ar:CantReg>
                                                    <ar:PtoVta>16</ar:PtoVta>
                                                    <ar:CbteTipo>049</ar:CbteTipo>
                                                </ar:FeCabReq>
                                                <ar:FeDetReq>
                                                    <ar:FECAEDetRequest>
                                                        <ar:Concepto>1</ar:Concepto>
                                                        <ar:DocTipo>80</ar:DocTipo>
                                                        <ar:DocNro>20296243230</ar:DocNro>
                                                        <ar:CbteDesde>1010</ar:CbteDesde>
                                                        <ar:CbteHasta>1010</ar:CbteHasta>
                                                        <ar:CbteFch>20241106</ar:CbteFch>
                                                        <ar:ImpTotal>10</ar:ImpTotal>
                                                        <ar:ImpTotConc>10</ar:ImpTotConc>
                                                        <ar:ImpNeto>10</ar:ImpNeto>
                                                        <ar:ImpOpEx>0</ar:ImpOpEx>
                                                        <ar:ImpTrib>0</ar:ImpTrib>
                                                        <ar:ImpIVA>0</ar:ImpIVA>
                                                        <ar:MonId>PES</ar:MonId>
                                                        <ar:MonCotiz>1</ar:MonCotiz>
                                                        <ar:Compradores>
                                                            <ar:Comprador>
                                                                <ar:DocTipo>80</ar:DocTipo>
                                                                <ar:DocNro>20233237540</ar:DocNro>
                                                                <ar:Porcentaje>0</ar:Porcentaje>
                                                            </ar:Comprador>
                                                        </ar:Compradores>
                                                    </ar:FECAEDetRequest>
                                                </ar:FeDetReq>
                                            </ar:FeCAEReq>
                                        </ar:FECAESolicitar>
                                    </soapenv:Body>
                                </soapenv:Envelope>";

                // Cargar el XML en un XDocument
                XDocument xmlDoc = XDocument.Parse(xmlTemplate);

                // Definir el espacio de nombres
                XNamespace arNamespace = "http://ar.gov.afip.dif.FEV1/";
                XNamespace soapenvNamespace = "http://schemas.xmlsoap.org/soap/envelope/";

                // Reemplazar los valores en el XML
                //xmlDoc.Descendants("ar:Token").First().Value = p_token;
                //xmlDoc.Descendants("ar:Sign").First().Value = p_sign;
                //xmlDoc.Descendants("ar:CbteFch").First().Value = DateTime.Now.ToString("yyyyMMdd"); // Fecha actual en formato YYYYMMDD

                // Reemplazar los valores en el XML utilizando el espacio de nombres
                xmlDoc.Descendants(arNamespace + "Token").First().Value = p_token;
                xmlDoc.Descendants(arNamespace + "Sign").First().Value = p_sign;
                xmlDoc.Descendants(arNamespace + "CbteFch").First().Value = DateTime.Now.ToString("yyyyMMdd"); // Fecha actual en formato YYYYMMDD


                // Convertir el XML modificado a cadena
                string modifiedXml = xmlDoc.ToString();


                var content = new StringContent(modifiedXml);

                request.Content = content;

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(await response.Content.ReadAsStringAsync());

                // Verificar si la respuesta es exitosa
                if (response.IsSuccessStatusCode)
                {
                    // Leer el contenido de la respuesta -XML
                    string responseContent = await response.Content.ReadAsStringAsync();
                    string code;
                    string msg;

                    XmlDocument xmlDocResponse = new XmlDocument();
                    xmlDocResponse.LoadXml(responseContent);

                    // Definir el espacio de nombres
                    XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDocResponse.NameTable);
                    nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                    nsmgr.AddNamespace("ar", "http://ar.gov.afip.dif.FEV1/");

                    // Seleccionar los nodos de observación
                    XmlNodeList observations = xmlDocResponse.SelectNodes("//ar:Observaciones/ar:Obs", nsmgr);

                    code = "";
                    // Construir el mensaje para mostrar
                    string message = "Observaciones encontradas:\n\n";
                    foreach (XmlNode observation in observations)
                    {
                        code = observation.SelectSingleNode("ar:Code", nsmgr)?.InnerText ?? "No disponible";
                        msg = observation.SelectSingleNode("ar:Msg", nsmgr)?.InnerText ?? "No disponible";
                        message += $"Code: {code}\nMsg: {msg}\n\n";
                    }

                    if (code != "")
                    {
                        // Mostrar el mensaje en un MessageBox
                        MessageBox.Show(message, "Observaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        alta_log("solicitar_caeAsync - responseContent  - " + message);

                    }

                    Console.WriteLine("Respuesta de AFIP: ");
                    Console.WriteLine(responseContent);
                }
                else
                {
                    alta_log("get_last_comprobante - " + response.StatusCode);
                    MessageBox.Show("get_last_comprobante - " + response.StatusCode);

                }


            }
            catch (Exception ex)
            {
                alta_log("Error get_last_comprobante - " + ex.Message);

                MessageBox.Show(ex.Message.ToString(), "Error get_last_comprobante");
            }

        }

        private async Task solicitar_caeAsync(string p_token, string p_sign)
        {
            try
            {
                long cuit_long = long.Parse(cuit);
                string cbTipoCompSeleccionado = ((ComboBoxItem)cbTipoComp.SelectedItem).Key;
                string cbTipoDocSeleccionado = ((ComboBoxItem)cbTipoDoc.SelectedItem).Key;

                DateTime fecha_con_un_mes_mas = fecha_actual.AddMonths(1);
                string fecha_con_un_mes_mas_formateada = fecha_con_un_mes_mas.ToString("yyyyMMdd");


                var wsfeClient = new WsfeClient
                {
                    IsProdEnvironment = true,
                    Cuit = cuit_long,
                    Sign = p_sign,
                    Token = p_token
                };

                // Get next WSFE Comp. Number
                var response = await wsfeClient.FECompUltimoAutorizadoAsync(Convert.ToInt32(txtPtoVenta.Text), Convert.ToInt32(cbTipoCompSeleccionado));
                var compNumber = response.Body.FECompUltimoAutorizadoResult.CbteNro + 4;


                //Build WSFE FECAERequest            
                var feCaeReq = new AfipServiceReference.FECAERequest
                {
                    FeCabReq = new AfipServiceReference.FECAECabRequest
                    {
                        CantReg = 1,
                        CbteTipo = 6,
                        PtoVta = 16
                    },
                    FeDetReq = new List<AfipServiceReference.FECAEDetRequest>
                    {
                        new AfipServiceReference.FECAEDetRequest
                        {
                            CbteDesde = compNumber,
                            CbteHasta = compNumber,
                            CbteFch = fecha_actual.ToString("yyyyMMdd"),
                            Concepto = 2,
                            DocNro = 20233237540,   // nro doc comprador (hugo)
                            DocTipo = Convert.ToInt32(cbTipoDocSeleccionado),
                            FchVtoPago = fecha_con_un_mes_mas_formateada,
                            ImpNeto = 10,
                            ImpTotal = 10,
                            FchServDesde = fecha_actual.ToString("yyyyMMdd"),
                            FchServHasta = fecha_con_un_mes_mas_formateada,
                            MonCotiz = 1,
                            MonId = "PES",
                            Iva = new List<AfipServiceReference.AlicIva>
                            {
                                new AfipServiceReference.AlicIva
                                {
                                    BaseImp = 10,
                                    Id = 3,
                                    Importe = 0
                                }
                            }
                        }
                    }
                };

                //Call WSFE FECAESolicitar
                var compResult = await wsfeClient.FECAESolicitarAsync(feCaeReq);

                // Accede al resultado, generalmente llamado 'Body' o similar
                var responseBody = compResult.Body;

                // Ahora, puedes acceder a los detalles específicos de la respuesta
                if (responseBody.FECAESolicitarResult != null)
                {
                    var result = responseBody.FECAESolicitarResult;

                    // Accede al estado de la respuesta
                    var estado = result.FeCabResp.Resultado; // "A" (aprobado), "R" (rechazado)

                    if (estado == "R")
                    {
                        alta_log("Factura rechazada ");
                        MessageBox.Show("Factura rechazada ");
                    }
                    else
                    {
                        // Código de CAE (si fue aprobado)
                        var cae = result.FeDetResp[0].CAE;

                        //// Fecha de vencimiento del CAE (si aplica)
                        //var caeFechaVencimiento = result.FeDetResp[0].CAEFchVto;
                    }


                    if (result.Errors.Any())
                    {
                        foreach (var error in result.Errors)
                        {
                            alta_log("Error solicitar_caeAsync " + error.Code + " - Descripción : " + error.Msg);

                            MessageBox.Show("Error solicitar_caeAsync " + error.Code + " - Descripción : " + error.Msg);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No hubo errores en la respuesta.");
                    }
                }
                else
                {
                    Console.WriteLine("La respuesta no contiene resultados válidos.");
                }

            }
            catch (Exception ex)
            {
                alta_log("Error solicitar_caeAsync - " + ex.Message);

                MessageBox.Show(ex.Message.ToString(), "Error solicitar_caeAsync");
            }

        }

    }


}

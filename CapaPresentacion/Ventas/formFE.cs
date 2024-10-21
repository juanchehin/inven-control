using DocumentFormat.OpenXml.Wordprocessing;
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
using System.Xml.Linq;

namespace CapaPresentacion.Ventas
{
    public partial class formFE : Form
    {
        //private static readonly string baseDir = AppDomain.CurrentDomain.BaseDirectory; // Cambiar por la ruta de tu base de directorios
        //D:\dev\inven-control\CapaPresentacion\Resources\afip\xml
        private static readonly string baseDir = @" D:\dev\inven-control\CapaPresentacion\Resources\afip\xml"; // Cambiar por la ruta de tu base de directorios
        private static readonly string urlWsdl = "https://servicios1.afip.gov.ar/wsfe/service.asmx"; // Cambiar por la URL correcta
        private static readonly string filename = Path.Combine(baseDir, "FEDummy.xml");
        private static readonly HttpClient client = new HttpClient();

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
            // Reemplaza estos TextBox con los nombres reales que estás usando en tu formulario
            string token = txtToken.Text;
            string sign = txtSign.Text;
            long cuit = long.Parse("20296243230");
            long id = long.Parse(txtId.Text);
            int cantidadReg = 1;
            int prestaServ = int.Parse(txtPrestaServ.Text);

            // Datos del primer detalle
            int tipoDoc1 = int.Parse(cbTipoDoc.Text);
            long nroDoc1 = long.Parse(txtDocComp.Text);
            int tipoCbte1 = int.Parse(cbTipoComp.Text);
            int puntoVta1 = int.Parse(txtPtoVenta.Text);
            long cbtDesde1 = long.Parse("1");
            long cbtHasta1 = long.Parse("1");
            double impTotal1 = double.Parse(txtImporteTotal.Text);
            double impTotConc1 = double.Parse(txtImporteTotal.Text);
            double impNeto1 = double.Parse(txtImporteTotal.Text);
            double imptoLiq1 = double.Parse(txtImptoLiq1.Text);
            double imptoLiqRni1 = double.Parse(txtImptoLiqRni1.Text);
            double impOpEx1 = double.Parse(txtImpOpEx1.Text);
            string fechaCbte1 = txtFechaCbte1.Text;
            string fechaServDesde1 = txtFechaServDesde1.Text;
            string fechaServHasta1 = txtFechaServHasta1.Text;
            string fechaVencPago1 = txtFechaVencPago1.Text;

            // Datos del segundo detalle
            int tipoDoc2 = int.Parse(txtTipoDoc2.Text);
            long nroDoc2 = long.Parse(txtNroDoc2.Text);
            int tipoCbte2 = int.Parse(txtTipoCbte2.Text);
            int puntoVta2 = int.Parse(txtPuntoVta2.Text);
            long cbtDesde2 = long.Parse(txtCbtDesde2.Text);
            long cbtHasta2 = long.Parse(txtCbtHasta2.Text);
            double impTotal2 = double.Parse(txtImpTotal2.Text);
            double impTotConc2 = double.Parse(txtImpTotConc2.Text);
            double impNeto2 = double.Parse(txtImpNeto2.Text);
            double imptoLiq2 = double.Parse(txtImptoLiq2.Text);
            double imptoLiqRni2 = double.Parse(txtImptoLiqRni2.Text);
            double impOpEx2 = double.Parse(txtImpOpEx2.Text);
            string fechaCbte2 = txtFechaCbte2.Text;
            string fechaServDesde2 = txtFechaServDesde2.Text;
            string fechaServHasta2 = txtFechaServHasta2.Text;
            string fechaVencPago2 = txtFechaVencPago2.Text;

            // Construir el XML
            StringBuilder xml = new StringBuilder();
            xml.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            xml.AppendLine("<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">");
            xml.AppendLine("  <soap:Body>");
            xml.AppendLine("    <FEAutRequest xmlns=\"http://ar.gov.afip.dif.facturaelectronica/\">");
            xml.AppendLine("      <argAuth>");
            xml.AppendLine($"        <Token>{token}</Token>");
            xml.AppendLine($"        <Sign>{sign}</Sign>");
            xml.AppendLine($"        <cuit>{cuit}</cuit>");
            xml.AppendLine("      </argAuth>");
            xml.AppendLine("      <Fer>");
            xml.AppendLine("        <Fecr>");
            xml.AppendLine($"          <id>{id}</id>");
            xml.AppendLine($"          <cantidadreg>{cantidadReg}</cantidadreg>");
            xml.AppendLine($"          <presta_serv>{prestaServ}</presta_serv>");
            xml.AppendLine("        </Fecr>");
            xml.AppendLine("        <Fedr>");

            // Primer detalle
            xml.AppendLine("          <FEDetalleRequest>");
            xml.AppendLine($"            <tipo_doc>{tipoDoc1}</tipo_doc>");
            xml.AppendLine($"            <nro_doc>{nroDoc1}</nro_doc>");
            xml.AppendLine($"            <tipo_cbte>{tipoCbte1}</tipo_cbte>");
            xml.AppendLine($"            <punto_vta>{puntoVta1}</punto_vta>");
            xml.AppendLine($"            <cbt_desde>{cbtDesde1}</cbt_desde>");
            xml.AppendLine($"            <cbt_hasta>{cbtHasta1}</cbt_hasta>");
            xml.AppendLine($"            <imp_total>{impTotal1}</imp_total>");
            xml.AppendLine($"            <imp_tot_conc>{impTotConc1}</imp_tot_conc>");
            xml.AppendLine($"            <imp_neto>{impNeto1}</imp_neto>");
            xml.AppendLine($"            <impto_liq>{imptoLiq1}</impto_liq>");
            xml.AppendLine($"            <impto_liq_rni>{imptoLiqRni1}</impto_liq_rni>");
            xml.AppendLine($"            <imp_op_ex>{impOpEx1}</imp_op_ex>");
            xml.AppendLine($"            <fecha_cbte>{fechaCbte1}</fecha_cbte>");
            xml.AppendLine($"            <fecha_serv_desde>{fechaServDesde1}</fecha_serv_desde>");
            xml.AppendLine($"            <fecha_serv_hasta>{fechaServHasta1}</fecha_serv_hasta>");
            xml.AppendLine($"            <fecha_venc_pago>{fechaVencPago1}</fecha_venc_pago>");
            xml.AppendLine("          </FEDetalleRequest>");

            // Segundo detalle
            xml.AppendLine("          <FEDetalleRequest>");
            xml.AppendLine($"            <tipo_doc>{tipoDoc2}</tipo_doc>");
            xml.AppendLine($"            <nro_doc>{nroDoc2}</nro_doc>");
            xml.AppendLine($"            <tipo_cbte>{tipoCbte2}</tipo_cbte>");
            xml.AppendLine($"            <punto_vta>{puntoVta2}</punto_vta>");
            xml.AppendLine($"            <cbt_desde>{cbtDesde2}</cbt_desde>");
            xml.AppendLine($"            <cbt_hasta>{cbtHasta2}</cbt_hasta>");
            xml.AppendLine($"            <imp_total>{impTotal2}</imp_total>");
            xml.AppendLine($"            <imp_tot_conc>{impTotConc2}</imp_tot_conc>");
            xml.AppendLine($"            <imp_neto>{impNeto2}</imp_neto>");
            xml.AppendLine($"            <impto_liq>{imptoLiq2}</impto_liq>");
            xml.AppendLine($"            <impto_liq_rni>{imptoLiqRni2}</impto_liq_rni>");
            xml.AppendLine($"            <imp_op_ex>{impOpEx2}</imp_op_ex>");
            xml.AppendLine($"            <fecha_cbte>{fechaCbte2}</fecha_cbte>");
            xml.AppendLine($"            <fecha_serv_desde>{fechaServDesde2}</fecha_serv_desde>");
            xml.AppendLine($"            <fecha_serv_hasta>{fechaServHasta2}</fecha_serv_hasta>");
            xml.AppendLine($"            <fecha_venc_pago>{fechaVencPago2}</fecha_venc_pago>");
            xml.AppendLine("          </FEDetalleRequest>");

            xml.AppendLine("        </Fedr>");
            xml.AppendLine("      </Fer>");
            xml.AppendLine("    </FEAutRequest>");
            xml.AppendLine("  </soap:Body>");
            xml.AppendLine("</soap:Envelope>");

            // Enviar el XML
            await EnviarXml(xml.ToString());
        }

        private async Task EnviarXml(string xml)
        {
            using (var httpClient = new HttpClient())
            {
                var requestUri = "https://servicios1.afip.gov.ar/wsfe/service.asmx"; // Cambia la URL si es necesario

                var request = new HttpRequestMessage(HttpMethod.Post, requestUri)
                {
                    Content = new StringContent(xml, Encoding.UTF8, "text/xml")
                };

                request.Headers.Add("SOAPAction", "http://ar.gov.afip.dif.facturaelectronica/FEAutRequest");

                try
                {
                    var response = await httpClient.SendAsync(request);

                    // Verificar si la respuesta es exitosa
                    response.EnsureSuccessStatusCode();

                    string responseContent = await response.Content.ReadAsStringAsync();
                    MessageBox.Show(responseContent, "Respuesta del Servidor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error al enviar XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void txtTestServer_ClickAsync(object sender, EventArgs e)
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
                return; // Salir del método si no se puede leer el archivo
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

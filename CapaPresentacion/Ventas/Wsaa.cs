using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Xml.Linq;

namespace CapaPresentacion.Ventas
{
    internal class Wsaa
    {
        private object serviceName;
        private object modo;
        private object cuit;
        private object logXmls;

        //************* CONSTANTES *****************************
        private const int MODO_HOMOLOGACION = 0;
        private const int MODO_PRODUCCION = 1;
        private const string WSDL_HOMOLOGACION = "/wsdl/homologacion/wsaa.wsdl"; // WSDL del web service WSAA
        private const string URL_HOMOLOGACION = "https://wsaahomo.afip.gov.ar/ws/services/LoginCms";
        private const string CERT_HOMOLOGACION = "/key/homologacion/certificado.pem"; // Certificado X.509 otorgado por AFIP
        private const string PRIVATEKEY_HOMOLOGACION = "/key/homologacion/privada"; // Clave privada de la PC
        private const string WSDL_PRODUCCION = "/wsdl/produccion/wsaa.wsdl";
        private const string URL_PRODUCCION = "https://wsaa.afip.gov.ar/ws/services/LoginCms";
        private const string CERT_PRODUCCION = "/key/produccion/certificado.pem";
        private const string PRIVATEKEY_PRODUCCION = "/key/produccion/privada";

        //************* VARIABLES *****************************
        private string baseDir = Directory.GetCurrentDirectory(); // Asigna el directorio actual
        private string service = "";
        //private int modo = 0;
        //private bool logXmls = true;
        //private long cuit = 0; // Cambiado a long para manejar valores más grandes
        private string wsdl = "";
        private string url = "";
        private string cert = "";
        private string privateKey = "";


        public Wsaa(object serviceName, object modo, object cuit, object logXmls)
        {
            this.serviceName = serviceName;
            this.modo = modo;
            this.cuit = cuit;
            this.logXmls = logXmls;
        }

        public void GenerateToken()
        {
            ValidateFileExists(this.cert);
            ValidateFileExists(this.privateKey);
            ValidateFileExists(this.wsdl);

            // string filenameTa = Path.Combine(Directory.GetCurrentDirectory(), "xml", "TA.xml");
            string filenameTa = Path.Combine(this.baseDir, "xml", "TA.xml");

            CreateTRA();
            string cms = SignTRA();
            string loginResult = CallWSAA(cms);
            File.WriteAllText(filenameTa, loginResult);
        }

        /// <summary>
        /// Verifies the existence of a file and throws an exception if it does not exist.
        /// </summary>
        /// <param name="filePath">The path of the file to check.</param>
        /// <exception cref="Exception">Thrown when the file does not exist.</exception>
        private void ValidateFileExists(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new Exception($"No pudo abrirse el archivo {filePath}");
            }
        }

        /// <summary>
        /// Creates the file ./:CUIT/:WebService/token/TRA.xml
        /// The file is necessary for signing.
        /// </summary>
        /// <author>NeoComplexx Group S.A.</author>
        private void CreateTRA()
        {
            try
            {
                string filenameTra = Path.Combine(baseDir, "xml", "TRA.xml");

                var TRA = new XDocument(
                    new XElement("loginTicketRequest", new XAttribute("version", "1.0"),
                        new XElement("header",
                            new XElement("uniqueId", DateTimeOffset.UtcNow.ToUnixTimeSeconds()),
                            new XElement("generationTime", DateTime.UtcNow.AddMinutes(-1).ToString("o")),
                            new XElement("expirationTime", DateTime.UtcNow.AddMinutes(1).ToString("o"))
                        ),
                        new XElement("service", service)
                    )
                );

                TRA.Save(filenameTra);
            }
            catch (Exception exc)
            {
                throw new Exception($"Error al crear TRA.xml: {exc.StackTrace}");
            }
        }

        /// <summary>
        /// This function performs PKCS#7 signing using the TRA.xml file, the certificate, and the private key as input.
        /// It generates an intermediate file ./:CUIT/:WebService/TRA.tmp and finally retrieves from the header only what is needed for WSAA.
        /// </summary>
        /// <author>NeoComplexx Group S.A.</author>
        private string SignTRA()
        {
            string inputFileName = Path.Combine(this.baseDir, "xml", "TRA.xml");
            string outputFileName = Path.Combine(this.baseDir, "tmp", "TRA.tmp");
            string filenameTraCert = Path.Combine(this.baseDir, "keys", "cert");
            string filenameTraKey = Path.Combine(this.baseDir, "keys", "key");

            // Create a process to execute OpenSSL commands
            var psi = new ProcessStartInfo
            {
                FileName = "openssl",
                Arguments = $"smime -sign -in \"{inputFileName}\" -out \"{outputFileName}\" -signer \"{filenameTraCert}\" -inkey \"{filenameTraKey}\" -outform PEM -nodetach",
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (var process = Process.Start(psi))
            {
                process.WaitForExit();
                if (process.ExitCode != 0)
                {
                    string error = process.StandardError.ReadToEnd();
                    alta_log("problem fact elect - ERROR al generar la firma PKCS#7 - wsaa.php");
                    throw new Exception("ERROR al generar la firma PKCS#7: " + error);
                }
            }

            // Load the TRA.tmp
            string cms = "";
            using (var reader = new StreamReader(outputFileName))
            {
                string line;
                int i = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    if (i++ >= 4)
                    {
                        cms += line;
                    }
                }
            }

            // Delete the temporary file
            File.Delete(outputFileName);

            return cms;
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
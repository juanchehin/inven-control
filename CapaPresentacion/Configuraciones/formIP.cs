using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace CapaPresentacion.Configuraciones
{
    public partial class formIP : Form
    {
        public formIP()
        {
            InitializeComponent();
            obtenerIp();
        }

        private void obtenerIp()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    this.lblIP.Text = ip.ToString();
                }
            }
        }

        private void btnAceptar_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}

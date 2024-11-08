using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using Zen.Barcode;

namespace CapaPresentacion.Productos
{
    public partial class formBarcode : Form
    {
        private string p_barcode;
        public formBarcode(string g_barcode)
        {
            InitializeComponent();
            this.p_barcode = g_barcode;
            generar_barcode();
        }
        private void generar_barcode()
        {
            Code128BarcodeDraw barcode = BarcodeDrawFactory.Code128WithChecksum;
            pbBarcode.Image = barcode.Draw(this.p_barcode, 350, 4);
            lbl_barcode.Text = this.p_barcode;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            PrintDocument pDoc = new PrintDocument();
            pDoc.PrintPage += PrintPicture;
            pd.Document = pDoc;
            if (pd.ShowDialog() == DialogResult.OK)
            {
                pDoc.Print();
            }
        }


        private void PrintPicture(object sender, PrintPageEventArgs e)
        {
            Bitmap bmp = new Bitmap(pbBarcode.Width, pbBarcode.Height);
            pbBarcode.DrawToBitmap(bmp, new Rectangle(0, 0, pbBarcode.Width, pbBarcode.Height)); e.Graphics.DrawImage(bmp, 0, 0);
            bmp.Dispose();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

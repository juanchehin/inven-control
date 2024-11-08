// Agregados
using CapaNegocio;
using CapaPresentacion.Productos;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace CapaPresentacion
{
    public partial class formProductos : Form
    {
        CN_Productos objetoCN_productos = new CN_Productos();

        private int IdProducto;
        private string g_barcode;
        int desde = 0;
        int totalProductos = 0;
        DataSet dsProductos = new DataSet();
        private DataGridView dataListadoTodosProductos;


        public formProductos()
        {
            InitializeComponent();
            ListarProductos(0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListarProductos(0);
        }
        public void ListarProductos(int pDesde)
        {
            dsProductos = objetoCN_productos.ListarProductos(pDesde);
            dataListadoProductos.DataSource = dsProductos.Tables[0];
            totalProductos = Convert.ToInt32(dsProductos.Tables[1].Rows[0][0]);
            dataListadoProductos.Columns["IdProducto"].Visible = false;

            lblTotalProductos.Text = "Total de productos : " + totalProductos.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.BuscarProducto();
        }

        private void MensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje, "InvenControl", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }


        //Mostrar Mensaje de Error
        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "InvenControl", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Realmente Desea Eliminar el producto", "InvenControl", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (Opcion == DialogResult.OK)
                {
                    CN_Productos.Eliminar(this.IdProducto);
                    this.ListarProductos(0);
                    this.MensajeOk("Se elimino de forma correcta el producto");
                }

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnNuevoProducto_Click(object sender, EventArgs e)
        {
            formNuevoEditarProducto frm = new formNuevoEditarProducto(this.IdProducto, true);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void botonEditarListado_Click(object sender, EventArgs e)
        {
            formNuevoEditarProducto frm = new formNuevoEditarProducto(this.IdProducto, false);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void dataListadoProductos_SelectionChanged(object sender, EventArgs e)
        {

            if (dataListadoProductos.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataListadoProductos.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataListadoProductos.Rows[selectedrowindex];
                this.IdProducto = Convert.ToInt32(selectedRow.Cells["IdProducto"].Value);
                this.g_barcode = selectedRow.Cells["Codigo"].Value.ToString();
            }
        }

        private void BuscarProducto()
        {
            this.dataListadoProductos.DataSource = objetoCN_productos.BuscarProducto(this.txtBuscar.Text);
            lblTotalProductos.Text = "Total de Registros: " + Convert.ToString(dataListadoProductos.Rows.Count);
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(this, new EventArgs());
            }
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            ListarProductos(0);
        }

        private void btnActualizarPrecios_Click(object sender, EventArgs e)
        {
            formActualizarPrecios frm = new formActualizarPrecios();
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void btnCategorias_Click(object sender, EventArgs e)
        {
            formCategorias frm = new formCategorias();
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if ((desde + 20) >= Convert.ToInt32(totalProductos))
            {
                return;
            }

            if (desde < 0)
            {
                return;
            }

            this.desde += 20;
            this.ListarProductos(this.desde);
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (desde <= 0)
            {
                return;
            }

            this.desde -= 20;
            this.ListarProductos(this.desde);
        }

        private void btnRefrescar_Click_1(object sender, EventArgs e)
        {
            ListarProductos(0);
            txtBuscar.Text = "";
        }

        private void dataListadoProductos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)

        {
            // Verifica si la celda actual es de la columna "stock" o "stock_alerta"
            if (e.ColumnIndex == dataListadoProductos.Columns["Stock"].Index || e.ColumnIndex == dataListadoProductos.Columns["stock_alerta"].Index)
            {
                // Obtén el valor de "stock" y "stock_alerta" de la fila actual
                int stock = Convert.ToInt32(dataListadoProductos.Rows[e.RowIndex].Cells["stock"].Value);
                int stockAlerta = Convert.ToInt32(dataListadoProductos.Rows[e.RowIndex].Cells["stock_alerta"].Value);

                // Si la condición stock <= stock_alerta se cumple, cambia el color de fondo de la fila a rojo
                if (stock <= stockAlerta)
                {
                    dataListadoProductos.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                    dataListadoProductos.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White; // Cambia el color del texto para hacerlo más legible
                }
                else
                {
                    // Si no se cumple la condición, restaura los colores predeterminados
                    dataListadoProductos.Rows[e.RowIndex].DefaultCellStyle.BackColor = dataListadoProductos.DefaultCellStyle.BackColor;
                    dataListadoProductos.Rows[e.RowIndex].DefaultCellStyle.ForeColor = dataListadoProductos.DefaultCellStyle.ForeColor;
                }
            }

        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Crear un nuevo paquete de Excel
            using (ExcelPackage package = new ExcelPackage())
            {
                // Crear una hoja de trabajo en el paquete
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Productos");

                // Estilo para las celdas de encabezado
                var headerStyle = worksheet.Cells["A1:F1"].Style;
                headerStyle.Font.Size = 12;
                headerStyle.Font.Bold = true;
                headerStyle.Font.Color.SetColor(Color.Black);
                headerStyle.Fill.PatternType = ExcelFillStyle.Solid;
                headerStyle.Fill.BackgroundColor.SetColor(Color.LightGray);

                // Establecer los encabezados
                worksheet.Cells[1, 1].Value = "Item/Codigo";
                worksheet.Cells[1, 2].Value = "Producto";
                worksheet.Cells[1, 3].Value = "Medida";
                worksheet.Cells[1, 4].Value = "Existencia inicial";
                worksheet.Cells[1, 5].Value = "Stock minimo";
                worksheet.Cells[1, 6].Value = "Stock fisico";

                DataSet dataListadoTodosProductos = objetoCN_productos.ListarTodosProductos();

                if (dataListadoTodosProductos.Tables.Count > 0)
                {
                    int i = 2;
                    // Iterar sobre las filas de la primera tabla del DataSet
                    foreach (DataRow row in dataListadoTodosProductos.Tables[0].Rows)
                    {

                        // Obtener el valor del código
                        string codigo = row["Codigo"].ToString();

                        // Verificar si el código está vacío
                        if (string.IsNullOrWhiteSpace(codigo))
                        {
                            // Si está vacío, colocar "-" o dejar la celda vacía
                            worksheet.Cells[i, 1].Value = "-"; // O puedes usar string.Empty para dejar la celda vacía
                        }
                        else
                        {

                            worksheet.Cells[i, 1].Value = row["Codigo"].ToString();

                            //// Generar la imagen del código de barras
                            //var barcodeWriter = new BarcodeWriter
                            //{
                            //    Format = BarcodeFormat.CODE_128,
                            //    Options = new ZXing.Common.EncodingOptions
                            //    {
                            //        Height = 50,
                            //        Width = 200
                            //    }
                            //};

                            //using (Bitmap barcodeBitmap = barcodeWriter.Write(row["Codigo"].ToString()))
                            //using (MemoryStream memoryStream = new MemoryStream())
                            //{
                            //    barcodeBitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                            //    memoryStream.Position = 0;

                            //    // Insertar la imagen en la celda correspondiente
                            //    var picture = worksheet.Drawings.AddPicture($"barcode_{i}", memoryStream);
                            //    picture.SetPosition(i - 1, 0, 3, 0); // i-1 porque EPPlus utiliza 0-based index, columna 4
                            //    picture.SetSize(100, 50); // Ajustar el tamaño según sea necesario

                            //    worksheet.Row(i).Height = 50 * 0.75;
                            //}

                        }

                        worksheet.Cells[i, 2].Value = row["Producto"].ToString();
                        worksheet.Cells[i, 3].Value = row["unidad"].ToString();
                        worksheet.Cells[i, 4].Value = row["stock_inicial"].ToString();
                        worksheet.Cells[i, 5].Value = row["stock_alerta"].ToString();
                        worksheet.Cells[i, 6].Value = row["Stock"].ToString();

                        i++;
                    }
                }

                // Ajustar las columnas al contenido
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Mostrar el SaveFileDialog para que el usuario elija la ruta y el nombre del archivo
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Files|*.xlsx";
                saveFileDialog.Title = "Guardar archivo excel";

                string fechaActual = DateTime.Now.ToString("dd-MM-yyyy");
                saveFileDialog.FileName = $"productos-{fechaActual}.xlsx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileInfo fileInfo = new FileInfo(saveFileDialog.FileName);
                    package.SaveAs(fileInfo);
                    MessageBox.Show("Archivo guardado exitosamente en: " + fileInfo.FullName);
                }

            }
        }

        private void btnBarcode_Click(object sender, EventArgs e)
        {
            formBarcode frm = new formBarcode(this.g_barcode);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }
    }
}

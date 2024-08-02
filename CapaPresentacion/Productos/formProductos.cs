using System;
using System.Data;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
// Agregados
using CapaNegocio;
using CapaPresentacion.Productos;
using SpreadsheetLight;
using MessageBox = System.Windows.Forms.MessageBox;

namespace CapaPresentacion
{
    public partial class formProductos : Form
    {
        CN_Productos objetoCN_productos = new CN_Productos();

        private int  IdProducto;
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
            formNuevoEditarProducto frm = new formNuevoEditarProducto(this.IdProducto,true);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void botonEditarListado_Click(object sender, EventArgs e)
        {
            formNuevoEditarProducto frm = new formNuevoEditarProducto(this.IdProducto,false);
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
            SLDocument sl = new SLDocument();
            SLStyle style = new SLStyle();
            style.Font.FontSize = 12;
            style.Font.Bold = true;

            sl.SetCellValue(1, 1, "Codigo");
            sl.SetCellValue(1, 2, "Producto");
            sl.SetCellValue(1, 3, "PrecioVenta");

            sl.SetCellStyle(1, 1, style);
            sl.SetCellStyle(1, 2, style);
            sl.SetCellStyle(1, 3, style);

            DataSet dataListadoTodosProductos = objetoCN_productos.ListarTodosProductos();

            if (dataListadoTodosProductos.Tables.Count > 0)
            {
                int i = 2;
                // Iterar sobre las filas de la primera tabla del DataSet
                foreach (DataRow row in dataListadoTodosProductos.Tables[0].Rows)
                {
                    sl.SetCellValue(i, 1, row["Codigo"].ToString());
                    sl.SetCellValue(i, 2, row["Producto"].ToString());
                    sl.SetCellValue(i, 3, row["PrecioVenta"].ToString());

                    i++;
                }
            }
            else
            {
                System.Windows.MessageBox.Show("El DataSet no contiene tablas - Productos - Tabla vacia");
                return;
            }

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = "Guardar archivo";
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.DefaultExt = "xlsx";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    sl.SaveAs(saveFileDialog1.FileName);
                    System.Windows.MessageBox.Show("Archivo exportado con éxito");
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            }
        }
    }
}

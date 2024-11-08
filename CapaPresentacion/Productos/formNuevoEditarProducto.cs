using CapaNegocio;
using ExcelDataReader;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class formNuevoEditarProducto : Form
    {
        CN_Productos objetoCN = new CN_Productos();
        DataTable respuesta;
        bool bandera;
        bool IsNuevo = false;
        bool IsEditar = false;
        DataTable categorias;
        DataTable unidades;

        private string categoriaActual;
        private int IdCategoria;

        private int IdProducto;
        private string Producto;
        private string Codigo;

        private string PrecioCompra;
        private string PrecioVenta;
        private string PrecioOferta;

        private string Stock;
        private string StockAlerta;
        private string Descripcion;
        private string Categoria;
        private string Unidad;

        private string NroRack;
        private string Nivel;

        public formNuevoEditarProducto(int parametro, bool IsNuevoEditar)
        {
            InitializeComponent();
            this.IdProducto = parametro;
            this.bandera = IsNuevoEditar;

        }

        private void formNuevoEditarProducto_Load(object sender, EventArgs e)
        {
            panelCargando.Hide();
            panelNuevoEditarCategoria.Visible = false;
            panelNuevaUnidad.Visible = false;

            this.ActiveControl = txtNombre;
            this.CargarCategoriasComboBox();
            this.cargar_unidades_cb();

            if (this.bandera)
            {
                lblEditarNuevo.Text = "Nuevo";
                this.IsNuevo = true;
                this.IsEditar = false;
            }
            else
            {
                lblEditarNuevo.Text = "Editar";
                this.btnImportarExcel.Enabled = false;
                this.IsNuevo = false;
                this.IsEditar = true;
                this.MostrarProducto(this.IdProducto);
            }
        }

        // Carga los valores en los campos de texto del formulario para que se modifiquen los que se desean
        private void MostrarProducto(int IdProducto)
        {
            respuesta = objetoCN.MostrarProducto(IdProducto);

            foreach (DataRow row in respuesta.Rows)
            {
                IdProducto = Convert.ToInt32(row["IdProducto"]);
                Producto = Convert.ToString(row["Producto"]);
                Codigo = Convert.ToString(row["Codigo"]);
                PrecioCompra = Convert.ToString(row["PrecioCompra"]);
                PrecioVenta = Convert.ToString(row["PrecioVenta"]);
                PrecioOferta = Convert.ToString(row["precio_oferta"]);
                Stock = Convert.ToString(row["Stock"]);
                StockAlerta = Convert.ToString(row["stock_alerta"]);
                Descripcion = Convert.ToString(row["Descripcion"]);
                Categoria = Convert.ToString(row["Categoria"]);
                Unidad = Convert.ToString(row["unidad"]);

                NroRack = Convert.ToString(row["nro_rack"]);
                Nivel = Convert.ToString(row["nivel"]);

                txtNombre.Text = Producto;
                txtCodigo.Text = Codigo;
                txtStock.Text = Stock;
                txtStockAlerta.Text = StockAlerta;
                txtPrecioCompra.Text = PrecioCompra;
                txtPrecioVenta.Text = PrecioVenta;
                txtPrecioOferta.Text = PrecioOferta;
                txtDescripcion.Text = Descripcion;
                txt_nro_rack.Text = NroRack;
                txt_nivel.Text = Nivel;
                cbCategorias.Text = Categoria;
                cbUnidad.Text = Unidad;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                DialogResult Opcion;

                if (decimal.TryParse(txtPrecioVenta.Text, out decimal precio_venta) && decimal.TryParse(txtPrecioCompra.Text, out decimal precio_compra))
                {
                    if (precio_venta < precio_compra)
                    {
                        Opcion = MessageBox.Show("El precio de venta es menor al precio de compra, ¿Desea continuar?", "InvenControl", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                        if (Opcion != DialogResult.OK)
                        {
                            return;
                        }
                    }
                }

                if (this.txtPrecioOferta.Text == string.Empty)
                {
                    this.txtPrecioOferta.Text = "0";
                }

                if (this.txtNombre.Text == string.Empty || this.txtStock.Text == string.Empty || this.txtPrecioCompra.Text == string.Empty || this.txtPrecioVenta.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos");
                }
                else
                {
                    if (this.txtNombre.Text == string.Empty || this.txtStock.Text == string.Empty || this.txtPrecioCompra.Text == string.Empty || this.txtPrecioVenta.Text == string.Empty)
                    {
                        MensajeError("Falta ingresar algunos datos");
                    }
                    if (this.IsNuevo)
                    {
                        rpta = CN_Productos.Insertar(this.txtNombre.Text.Trim(), this.txtCodigo.Text.Trim(), this.txtPrecioCompra.Text.Trim(),
                            this.txtPrecioVenta.Text.Trim(), this.txtPrecioOferta.Text.Trim(), this.txtDescripcion.Text.Trim(),
                            this.txtStock.Text.Trim(), this.txtStockAlerta.Text.Trim(), this.cbCategorias.Text, this.cbUnidad.Text
                            , this.txt_nro_rack.Text, this.txt_nivel.Text);
                    }
                    else
                    {
                        rpta = CN_Productos.Editar(this.IdProducto, this.txtNombre.Text.Trim(), this.txtCodigo.Text.Trim(),
                            this.txtPrecioCompra.Text.Trim(), this.txtPrecioVenta.Text.Trim(), this.txtPrecioOferta.Text.Trim(), this.txtDescripcion.Text.Trim()
                            , this.txtStock.Text.Trim(), this.txtStockAlerta.Text.Trim(), this.cbCategorias.Text, this.cbUnidad.Text
                        , this.txt_nro_rack.Text, this.txt_nivel.Text);
                    }

                    if (rpta.Equals("OK"))
                    {
                        if (this.IsNuevo)
                        {
                            this.MensajeOk("Se Insertó de forma correcta el registro");
                        }
                        else
                        {
                            this.MensajeOk("Se Actualizó de forma correcta el registro");
                        }
                    }
                    else
                    {
                        this.MensajeError(rpta);
                    }
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }

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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPrecioCompra_KeyPress(object sender, KeyPressEventArgs e)
        {

            Char chr = e.KeyChar;

            if (!Char.IsDigit(chr) && chr != 8 && chr != 8 && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
                MessageBox.Show("Debe ingresar valores numericos ");
            }
        }

        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8 && chr != 8 && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
                MessageBox.Show("Debe ingresar valores numericos o comas o puntos");
            }
        }

        private void txtPrecioVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;

            if (!Char.IsDigit(chr) && chr != 8 && chr != 8 && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
                MessageBox.Show("Debe ingresar valores numericos ");
            }
        }

        private void formNuevoEditarProducto_FormClosed(object sender, FormClosedEventArgs e)
        {
            Console.WriteLine("Se debe mostrar cuando se cierre el formulario de editar");
            formProductos formP = new formProductos();
            formP.ListarProductos(0);
        }

        private void txtPrecioCompra_MouseHover(object sender, EventArgs e)
        {
            this.ttPrecioCompra.SetToolTip(txtPrecioCompra, "Precio al que se esta comprando actualmente el producto");
        }

        private void txtPrecioVenta_MouseHover(object sender, EventArgs e)
        {
            this.ttPrecioVenta.SetToolTip(txtPrecioCompra, "Precio al que se esta vendiendo actualmente el producto");
        }

        public void CargarCategoriasComboBox()
        {
            categorias = objetoCN.DameCategorias();
            cbCategorias.DataSource = categorias;
            cbCategorias.DisplayMember = "Categoria";
            this.categoriaActual = cbCategorias.ValueMember.ToString();
        }

        public void cargar_unidades_cb()
        {
            unidades = objetoCN.listar_unidades();
            cbUnidad.DataSource = unidades;
            cbUnidad.DisplayMember = "Unidad";
            this.categoriaActual = cbUnidad.ValueMember.ToString();
        }

        private void btnImportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog oOpenFileDialog = new OpenFileDialog();
                oOpenFileDialog.Filter = "Excel Worbook|*.xlsx";

                if (oOpenFileDialog.ShowDialog() == DialogResult.OK)
                {

                    int i = 1;
                    int j = 0;
                    int regCargados = 0;
                    string rpta;

                    FileStream fsSource = new FileStream(oOpenFileDialog.FileName, FileMode.Open, FileAccess.Read);

                    //ExcelReaderFactory.CreateBinaryReader = formato XLS
                    //ExcelReaderFactory.CreateOpenXmlReader = formato XLSX
                    //ExcelReaderFactory.CreateReader = XLS o XLSX
                    IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(fsSource);

                    DataSet result = excelReader.AsDataSet();
                    // excelReader.IsFirstRowAsColumnNames = true;
                    DataTable dt = result.Tables[0];

                    panelCargando.Show();

                    while (i < excelReader.RowCount)
                    {
                        string producto = dt.Rows[i][j].ToString();
                        j = j + 1;
                        string categoria = dt.Rows[i][j].ToString();

                        if (categoria == "" || categoria == null)
                        {
                            categoria = "Sin categoria";
                        }
                        else
                        {
                            string existe = CN_Productos.DameCategoria(categoria);

                            if (existe != "OK")
                            {
                                CN_Productos.AltaCategoria(categoria);
                            }
                        }

                        j = j + 1;
                        string codigo = dt.Rows[i][j].ToString();
                        j = j + 1;
                        string stock = dt.Rows[i][j].ToString();
                        j = j + 1;
                        string preciocompra = dt.Rows[i][j].ToString();
                        j = j + 1;
                        string precioventa = dt.Rows[i][j].ToString();
                        j = j + 1;
                        string descripcion = dt.Rows[i][j].ToString();
                        j = j + 1;
                        string nro_rack = dt.Rows[i][j].ToString();
                        j = j + 1;
                        string nivel = dt.Rows[i][j].ToString();
                        j = j + 1;

                        rpta = CN_Productos.Insertar(producto.Trim(), codigo.Trim(), preciocompra.Trim(), precioventa.Trim(), "0",
                            descripcion.Trim(), stock.Trim(), "0", categoria.Trim(), "-", nro_rack.Trim(), nivel.Trim());

                        if (rpta == "Ok" || rpta == "OK")
                        {
                            regCargados++;
                        }
                        i = i + 1;
                        j = 0;
                    }

                    panelCargando.Dispose();
                    MensajeOk("Se cargaron " + regCargados + " registros en la Base de datos");
                }
            }
            catch (Exception ex)
            {
                MensajeError(ex.Message);
            }

        }

        private void btnAgregarCategoria_Click(object sender, EventArgs e)
        {
            panelNuevoEditarCategoria.Visible = true;
        }

        private void btnGuardarCategoria_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (this.txtCategoria.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos");
                }
                else
                {
                    rpta = CN_Productos.AltaCategoria(this.txtCategoria.Text.Trim());
                    if (rpta.Equals("OK"))
                    {
                        this.MensajeOk("Se Insertó de forma correcta el registro");
                        this.CargarCategoriasComboBox();
                        this.txtCategoria.Clear();
                    }
                    else
                    {
                        this.MensajeError(rpta);
                    }
                    panelNuevoEditarCategoria.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnCancelarCategoria_Click(object sender, EventArgs e)
        {
            panelNuevoEditarCategoria.Visible = false;
        }

        private void btn_generar_codigo_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            const int longitudCodigo = 13;

            // Generar una cadena de 13 dígitos aleatorios
            string codigoProducto = "";
            for (int i = 0; i < longitudCodigo; i++)
            {
                codigoProducto += random.Next(0, 10).ToString();
            }
            this.txtCodigo.Text = codigoProducto;
        }

        private void btnAltaUnidad_Click(object sender, EventArgs e)
        {
            panelNuevaUnidad.Visible = true;
        }

        private void cancelarUnidad_Click(object sender, EventArgs e)
        {
            panelNuevaUnidad.Visible = false;

        }

        private void guardarUnidad_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if ((this.txtNombreUnidad.Text == string.Empty) || (this.txtNombreCortoUnidad.Text == string.Empty))
                {
                    MensajeError("Falta ingresar algunos datos");
                }
                else
                {
                    rpta = CN_Productos.alta_unidad(this.txtNombreUnidad.Text.Trim(), this.txtNombreCortoUnidad.Text);
                    if (rpta.Equals("OK"))
                    {
                        this.MensajeOk("Se Insertó de forma correcta el registro");
                        this.cargar_unidades_cb();

                        this.txtNombreUnidad.Clear();
                        this.txtNombreCortoUnidad.Clear();
                    }
                    else
                    {
                        this.MensajeError(rpta);
                    }
                    panelNuevoEditarCategoria.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void txtStockAlerta_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8 && chr != 8 && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
                MessageBox.Show("Debe ingresar valores numericos o comas o puntos");
            }
        }
    }
}

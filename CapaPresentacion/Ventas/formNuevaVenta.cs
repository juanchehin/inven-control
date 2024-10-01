using System;
using System.Data;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using CapaNegocio;
using CapaPresentacion.Reportes;

namespace CapaPresentacion.Ventas
{
    public partial class formNuevaVenta : Form
    {
        CN_Ventas objetoCN = new CN_Ventas();
        CN_Empleados objetoCN_empleado = new CN_Empleados();

        CN_Productos objetoCN_productos = new CN_Productos();
        CN_Clientes objetoCN_clientes = new CN_Clientes();

        DataTable respuesta;
        DataTable tiposPagos;
        DataTable productos_coincidencias;
        DataTable productos_venta = new DataTable();

        private AutoCompleteStringCollection autoCompleteProductos = new AutoCompleteStringCollection();

        static ManualResetEvent evento = new ManualResetEvent(false);

        bool bandera;
        bool IsNuevo = false;
        bool IsEditar = false;
        bool bandera_lectura_balanza = false;

        private int IdVenta;
        private int IdUsuario;  // IdEmpleado
        private int IdCliente = 2;  // Cliente generico si no se especifica otro
        public string Apellidos = "Publico ";
        public string Nombres = "en general";
        public string Usuario;
        private string tipoPago;
        private int pDesde = 0;
        string unidad;
        string codigo_pendiente_producto;
        string unidad_pendiente_producto;
        decimal stock_pendiente_producto;
        double peso_pendiente = 0;

        string precio_venta_producto_seleccionado = "0";
        string precio_oferta_producto_seleccionado = "0";

        private int IdProducto;

        private DateTime Fecha;

        private string Cantidad;

        private decimal precioTotal = 0;

        DataTable dtRespuesta = new DataTable();

        string Dato_Recibido;
        private delegate void DelegadoAcceso(string accion);

        public formNuevaVenta(int IdUsuario, string usuario)
        {
            InitializeComponent();

            this.KeyPreview = true; // Esto es importante para capturar eventos de teclado en el formulario
            this.KeyDown += formNuevaVenta_KeyDown;

            dgvAutocompleteProducto.Visible = false;

            panelClientes.Visible = false;
            panelVuelto.Visible = false;
            panelProductos.Visible = false;

            this.IdUsuario = IdUsuario;
            this.Usuario = usuario;
            // Creo las columnas de el listado de ventas

            this.dataListadoProductos.Columns.Add("IdProducto", "IdProducto");
            this.dataListadoProductos.Columns.Add("Codigo", "Codigo");
            this.dataListadoProductos.Columns.Add("Producto", "Producto");
            this.dataListadoProductos.Columns.Add("Cantidad", "Cantidad");
            this.dataListadoProductos.Columns.Add("Unidad", "Unidad");
            this.dataListadoProductos.Columns.Add("PrecioUnitario", "Precio unitario");
            this.dataListadoProductos.Columns.Add("Peso", "Peso");

            this.dataListadoProductos.Columns["IdProducto"].Visible = false;   // Oculto "IdProducto"

            this.lblUsuario.Text = usuario;
            this.IdUsuario = IdUsuario;


            this.lblCliente.Text = this.Apellidos + this.Nombres;
            this.llenarCBTiposPago();

            try
            {
                if (SerialPort1.IsOpen)
                {
                    MessageBox.Show("El puerto ya esta abierto");
                    return;
                }
                else
                {
                    SerialPort1.Open();                              // Si estaba cerrado, lo abrimos
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"El puerto está ocupado por otro proceso.");
            }
            catch (IOException)
            {
                Console.WriteLine($"El puerto no está disponible.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al abrir el puerto: {ex.Message}");
            }

        }

        private void llenarCBTiposPago()
        {
            tiposPagos = objetoCN.DameTiposPago();

            cbTiposPago.DataSource = tiposPagos;

            cbTiposPago.DisplayMember = "TipoPago";
        }


        private void btnRegistrar_Click(object sender, EventArgs e)
        {

            try
            {
                if (!productos_venta.Columns.Contains("IdProducto"))
                {
                    productos_venta.Columns.Add("IdProducto", typeof(System.Int32));
                }
                if (!productos_venta.Columns.Contains("Cantidad"))
                {
                    productos_venta.Columns.Add("Cantidad", typeof(System.Decimal));
                }

                if (!productos_venta.Columns.Contains("Peso"))
                {
                    productos_venta.Columns.Add("Peso", typeof(System.Decimal));
                }

                if (!productos_venta.Columns.Contains("precio"))
                {
                    productos_venta.Columns.Add("precio", typeof(System.Decimal));
                }

                foreach (DataGridViewRow rowGrid in this.dataListadoProductos.Rows)
                {
                    DataRow row = productos_venta.NewRow();

                    row["IdProducto"] = Convert.ToDouble(rowGrid.Cells[0].Value);
                    row["Cantidad"] = rowGrid.Cells[3].Value;
                    string pesoConComas = rowGrid.Cells[6].Value.ToString().Replace('.', ',');
                    row["Peso"] = pesoConComas;
                    row["precio"] = Convert.ToDouble(rowGrid.Cells[5].Value);

                    productos_venta.Rows.Add(row);
                }
                string rpta = "";

                if (this.dataListadoProductos.CurrentRow == null)
                {
                    MensajeError("Productos inexistentes");
                    return;
                }

                panelVuelto.Visible = true;
                this.lblImporte.Text = this.precioTotal.ToString();
                txtEntrega.Focus();

            }
            catch (Exception ex)
            {
                alta_log("Problema btnRegistrar_Click - " + ex.Message);

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;

            if (e.KeyChar == '.')
            {
                // Reemplazar el punto '.' por una coma ','
                e.KeyChar = ',';
            }

            if (!Char.IsDigit(chr) && chr != 8 && chr != 8 && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
                MessageBox.Show("Debe ingresar valores numericos ");
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            panelProductos.Visible = true;
            this.dataListadoProductosPanel.DataSource = objetoCN_productos.ListarProductos(this.pDesde).Tables[0];
            txtBuscarProductoPanel.Focus();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvAutocompleteProducto.SelectedRows.Count > 0)
            {
                int selectedrowindex = dgvAutocompleteProducto.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dgvAutocompleteProducto.Rows[selectedrowindex];

                this.IdProducto = Convert.ToInt32(selectedRow.Cells["IdProducto"].Value);
                this.lblNombreProd.Text = selectedRow.Cells["Producto"].Value.ToString();
                // this.lblPrecioUnitario.Text = selectedRow.Cells["PrecioVenta"].Value.ToString();
                txtPrecioUnitario.Text = selectedRow.Cells["PrecioVenta"].Value.ToString();
                codigo_pendiente_producto = selectedRow.Cells["Codigo"].Value.ToString();
                unidad_pendiente_producto = selectedRow.Cells["unidad"].Value.ToString();

                stock_pendiente_producto = Convert.ToDecimal(selectedRow.Cells["Stock"].Value);
                // this.txtBuscarProductoForm.Text = selectedRow.Cells["Codigo"].Value.ToString();
                // this.unidad = selectedRow.Cells["unidad"].Value.ToString();

                // this.txtBuscarProductoForm.Text = "";
                this.txtCantidad.Text = "1";
                this.dgvAutocompleteProducto.Visible = false;

                this.precio_venta_producto_seleccionado = selectedRow.Cells["PrecioVenta"].Value.ToString();
                this.precio_oferta_producto_seleccionado = selectedRow.Cells["precio_oferta"].Value.ToString();
            }

            if (e.KeyCode == Keys.Down)
            {
                int currentRowIndex = dgvAutocompleteProducto.CurrentCell.RowIndex;

                if (currentRowIndex < dgvAutocompleteProducto.Rows.Count - 1)
                {
                    dgvAutocompleteProducto.Rows[currentRowIndex + 1].Selected = true;
                    dgvAutocompleteProducto.CurrentCell = dgvAutocompleteProducto.Rows[currentRowIndex + 1].Cells[0];
                }
            }

        }

        private void btnSeleccionarCliente_Click(object sender, EventArgs e)
        {
            panelClientes.Visible = !panelClientes.Visible;
            MostrarClientes();
        }

        private void btnBuscarProd_Click(object sender, EventArgs e)
        {
            this.BuscarProductoPorCodigo();
        }
        // Devuelve los datos de un solo producto. Si no devuelve un mensaje de 'no encontrado'
        private void BuscarProductoPorCodigo()
        {
            try
            {
                // leo el PLU del codigo de barra leido por la pistola
                string codigo_plu = this.txtBuscarProductoForm.Text.Substring(2, 4);

                this.dtRespuesta = objetoCN_productos.BuscarProductoPorCodigo(codigo_plu);

                if (this.dtRespuesta.Rows[0][0].ToString() == "Producto con codigo inexistente")
                {
                    this.MensajeError("Producto con codigo inexistente");
                    return;
                }

                // Sacar de 'respuesta' los datos para completar en el form
                this.IdProducto = Convert.ToInt32(this.dtRespuesta.Rows[0][0]);
                this.lblNombreProd.Text = this.dtRespuesta.Rows[0][1].ToString();
                codigo_pendiente_producto = this.dtRespuesta.Rows[0][2].ToString();
                txtPrecioUnitario.Text = this.dtRespuesta.Rows[0][3].ToString();
                stock_pendiente_producto = Convert.ToDecimal(this.dtRespuesta.Rows[0][4]);
            }
            catch (Exception ex)
            {
                MensajeError("Error al buscar o leer codigo del producto - " + ex.Message);
                alta_log("Error al buscar o leer codigo del producto - " + ex.Message);
            }

        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (this.lblNombreProd.Text == "" || this.txtPrecioUnitario.Text == "")
            {
                MensajeError("Debe cargar un producto");
                return;
            }

            if (this.stock_pendiente_producto <= 0)
            {
                MensajeError("El producto no cuenta con stock suficiente");
                return;
            }

            decimal dec = decimal.Parse(this.txtPrecioUnitario.Text);
            bool bandera = false;

            if(dec <= 0)
            {
                MensajeError("Precio invalido");
                return;
            }

            if (dataListadoProductos.Rows.Count == 0)
            {
                // Si no cargo la cantidad, cargo por defecto 1
                if (String.IsNullOrEmpty(this.txtCantidad.Text))
                {
                    this.dataListadoProductos.Rows.Insert(this.dataListadoProductos.RowCount, this.IdProducto,
                        codigo_pendiente_producto, this.lblNombreProd.Text, 1, unidad_pendiente_producto, this.txtPrecioUnitario.Text, peso_pendiente);
                    this.precioTotal += dec;
                }
                else
                {
                    decimal cant = decimal.Parse(this.txtCantidad.Text);
                    this.dataListadoProductos.Rows.Insert(this.dataListadoProductos.RowCount, this.IdProducto, codigo_pendiente_producto,
                        this.lblNombreProd.Text, this.txtCantidad.Text, unidad_pendiente_producto, this.txtPrecioUnitario.Text, this.peso_pendiente);
                    this.precioTotal += dec * cant;
                }
            }
            else
            {
                bandera = false;
                // Chequeo si ya existe el producto en el listado para poder aumentar la cantidad
                foreach (DataGridViewRow row in dataListadoProductos.Rows)
                {
                    if (Convert.ToInt32(row.Cells[0].Value) == this.IdProducto)
                    {
                        bandera = true;
                        // Si no cargo la cantidad, cargo por defecto 1
                        if (String.IsNullOrEmpty(this.txtCantidad.Text))
                        {
                            row.Cells["Cantidad"].Value = 1 + Convert.ToInt32(row.Cells[3].Value);
                            this.precioTotal += dec;
                        }
                        else
                        {
                            decimal cant = decimal.Parse(this.txtCantidad.Text);
                            row.Cells["Cantidad"].Value = 1 + decimal.Parse(row.Cells[3].Value.ToString());
                            this.precioTotal += dec * cant;
                        }
                        break;
                    }
                }
                if (bandera == false)
                {
                    // Si no cargo la cantidad, cargo por defecto 1
                    if (String.IsNullOrEmpty(this.txtCantidad.Text))
                    {
                        this.dataListadoProductos.Rows.Insert(this.dataListadoProductos.RowCount, this.IdProducto, codigo_pendiente_producto,
                            this.lblNombreProd.Text, 1, unidad_pendiente_producto, this.txtPrecioUnitario.Text, this.peso_pendiente);
                        this.precioTotal += dec;
                    }
                    else
                    {
                        decimal cant = decimal.Parse(this.txtCantidad.Text);
                        this.dataListadoProductos.Rows.Insert(this.dataListadoProductos.RowCount, this.IdProducto, codigo_pendiente_producto,
                            this.lblNombreProd.Text, this.txtCantidad.Text, unidad_pendiente_producto, this.txtPrecioUnitario.Text, this.peso_pendiente);
                        this.precioTotal += dec * cant;
                    }
                }
            }

            // redondeo a 2 digitos despues de la coma
            this.precioTotal = Math.Round(this.precioTotal, 2);

            this.lblSubTotal.Text = this.precioTotal.ToString();
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (this.dataListadoProductos.CurrentRow == null)
            {
                MensajeError("Productos inexistentes");
                return;
            }
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Realmente Desea Eliminar el producto", "InvenControl", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (Opcion == DialogResult.OK)
                {
                    foreach (DataGridViewRow item in this.dataListadoProductos.SelectedRows)
                    {
                        decimal precio = decimal.Parse(this.dataListadoProductos.Rows[item.Index].Cells["PrecioUnitario"].Value.ToString());
                        decimal cantidad = decimal.Parse(this.dataListadoProductos.Rows[item.Index].Cells["Cantidad"].Value.ToString());

                        this.dataListadoProductos.Rows.RemoveAt(item.Index);

                        this.precioTotal = this.precioTotal - (precio * cantidad);
                        this.lblSubTotal.Text = precioTotal.ToString();
                    }

                    this.MensajeOk("Se elimino de forma correcta el producto");
                }

            }
            catch (Exception ex)
            {
                alta_log("Problema btnQuitar_Click - " + ex.Message);
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataListadoClientes.SelectedCells.Count > 0)
                {
                    int selectedrowindex = dataListadoClientes.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataListadoClientes.Rows[selectedrowindex];

                    this.IdCliente = Convert.ToInt32(selectedRow.Cells["IdPersona"].Value);
                    this.Apellidos = selectedRow.Cells["Apellidos"].Value.ToString();
                    this.Nombres = selectedRow.Cells["Nombres"].Value.ToString();
                }

                lblCliente.Text = this.Apellidos + " " + this.Nombres;

                panelClientes.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        public void MostrarClientes()
        {
            dataListadoClientes.DataSource = objetoCN_clientes.MostrarClientes();
            dataListadoClientes.Columns[0].Visible = false;
        }

        private void txtEntrega_TextChanged(object sender, EventArgs e)
        {
            if (this.txtEntrega.Text != "")
            {
                decimal result = (this.precioTotal - Convert.ToDecimal(this.txtEntrega.Text)) * -1;
                this.lblVuelto.Text = result.ToString();
            }
            else
            {
                this.lblVuelto.Text = this.precioTotal.ToString();
            }
        }

        private void btnImprimirTicket_Click(object sender, EventArgs e)
        {
            formTicket frm = new formTicket(this.IdUsuario, this.IdCliente, this.dataListadoProductos);
            frm.MdiParent = this.MdiParent;
            frm.Show();

            this.Close();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            panelVuelto.Visible = false;
            //this.Close();
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            this.BuscarProducto();
        }

        private void BuscarProducto()
        {
            // string codigo_plu = this.txtBuscarProductoForm.Text.Substring(2, 4);

            this.dataListadoProductosPanel.DataSource = objetoCN_productos.BuscarProducto(this.txtBuscarProductoPanel.Text);
        }

        private void btnNuevoProducto_Click(object sender, EventArgs e)
        {
            formNuevoEditarProducto frm = new formNuevoEditarProducto(0, true);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void btnSeleccionarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataListadoProductosPanel.SelectedCells.Count > 0)
                {
                    int selectedrowindex = dataListadoProductosPanel.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataListadoProductosPanel.Rows[selectedrowindex];

                    this.IdProducto = Convert.ToInt32(selectedRow.Cells["IdProducto"].Value);
                    this.lblNombreProd.Text = selectedRow.Cells["Producto"].Value.ToString();
                    // this.lblPrecioUnitario.Text = selectedRow.Cells["PrecioVenta"].Value.ToString();
                    txtPrecioUnitario.Text = selectedRow.Cells["PrecioVenta"].Value.ToString();
                    this.txtBuscarProductoForm.Text = selectedRow.Cells["Codigo"].Value.ToString();
                    codigo_pendiente_producto = selectedRow.Cells["Codigo"].Value.ToString();

                    this.unidad = selectedRow.Cells["unidad"].Value.ToString();
                    stock_pendiente_producto = Convert.ToDecimal(selectedRow.Cells["Stock"].Value);


                }
            }
            catch (Exception ex)
            {
                alta_log("Problema btnSeleccionarProducto_Click - " + ex.Message);

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
            panelProductos.Visible = false;
        }

        private void txtBuscarProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.BuscarProducto();
            }
        }

        private void btnCancelarPanelProductos_Click(object sender, EventArgs e)
        {
            panelProductos.Visible = false;
        }

        private void btn_confirmar_venta_Click(object sender, EventArgs e)
        {
            string rpta = CN_Ventas.AltaVenta(this.IdUsuario, this.IdCliente, cbTiposPago.Text, productos_venta, Convert.ToDecimal(this.precioTotal.ToString()));

            if (rpta.Equals("Ok"))
            {
                MessageBox.Show("Venta cargada con exito", "Mensaje", MessageBoxButtons.OKCancel);

                //DialogResult resultado = MessageBox.Show("Venta cargada con exito \n\n ¿Desea imprimir el ticket?", "Confirmación", MessageBoxButtons.OKCancel);

                //// Verificar la respuesta del usuario
                //if (resultado == DialogResult.OK)
                //{
                //    formTicket frm = new formTicket(this.IdUsuario, this.IdCliente, this.dataListadoProductos);
                //    frm.MdiParent = this.MdiParent;
                //    frm.Show();
                //}
            }
            else
            {
                alta_log("Problema btn_confirmar_venta_Click - " + rpta);

                this.MensajeError(rpta);
            }

            this.Close();
        }

        private void btnCerrarPanelClientes_Click(object sender, EventArgs e)
        {
            panelClientes.Visible = false;
        }

        private void formNuevaVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            // panel vuelto
            if ((e.KeyChar == (char)Keys.Enter) && (this.panelVuelto.Visible == true))
            {
                this.btn_confirmar_venta.PerformClick();
                e.Handled = true;
            }

            if ((e.KeyChar == (char)Keys.Escape) && (this.panelVuelto.Visible == true))
            {
                this.btnCerrarPanelVuelto.PerformClick();
                e.Handled = true;
            }

            // panel productos
            if ((e.KeyChar == (char)Keys.Enter) && (this.panelProductos.Visible == true))
            {
                this.btnSeleccionarProducto.PerformClick();
                e.Handled = true;
            }

            if ((e.KeyChar == (char)Keys.Escape) && (this.panelProductos.Visible == true))
            {
                this.btnCancelarPanelProductos.PerformClick();
                e.Handled = true;
            }

            // panel clientes
            if ((e.KeyChar == (char)Keys.Enter) && (this.panelClientes.Visible == true))
            {
                this.btnSeleccionarCliente.PerformClick();
                e.Handled = true;
            }

            if ((e.KeyChar == (char)Keys.Escape) && (this.panelClientes.Visible == true))
            {
                this.btnCerrarPanelClientes.PerformClick();
                e.Handled = true;
            }
        }

        private void formNuevaVenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {
                // Simular un clic en el botón
                btnRegistrar.PerformClick();

                // Indicar que el evento ya ha sido manejado
                e.Handled = true;
            }

            if (e.KeyCode == Keys.F2)
            {
                // Simular un clic en el botón
                btnAgregarProducto.PerformClick();

                // Indicar que el evento ya ha sido manejado
                e.Handled = true;
            }

            if (e.KeyCode == Keys.F3)
            {
                // Simular un clic en el botón
                btnBuscar.PerformClick();

                // Indicar que el evento ya ha sido manejado
                e.Handled = true;
            }

            if (e.KeyCode == Keys.F4)
            {
                // Simular un clic en el botón
                btnSeleccionarCliente.PerformClick();

                // Indicar que el evento ya ha sido manejado
                e.Handled = true;
            }


            if (e.KeyCode == Keys.F12)
            {
                // Simular un clic en el botón
                btnCancelar.PerformClick();

                // Indicar que el evento ya ha sido manejado
                e.Handled = true;
            }
        }

        private void formNuevaVenta_Load(object sender, EventArgs e)
        {
            txtBuscarProductoForm.Select();
        }

        private void txtBuscarProductoForm_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvAutocompleteProducto_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && dgvAutocompleteProducto.SelectedRows.Count > 0 && dgvAutocompleteProducto.Visible == true)
                {
                    if (dgvAutocompleteProducto.SelectedCells.Count > 0)
                    {
                        int selectedrowindex = dgvAutocompleteProducto.SelectedCells[0].RowIndex;
                        DataGridViewRow selectedRow = dgvAutocompleteProducto.Rows[selectedrowindex];

                        this.IdProducto = Convert.ToInt32(selectedRow.Cells["IdProducto"].Value);
                        this.lblNombreProd.Text = selectedRow.Cells["Producto"].Value.ToString();
                        txtPrecioUnitario.Text = selectedRow.Cells["PrecioVenta"].Value.ToString();
                        // this.lblPrecioUnitario.Text = selectedRow.Cells["PrecioVenta"].Value.ToString();
                        //this.txtBuscarProductoForm.Text = selectedRow.Cells["Codigo"].Value.ToString();
                        //this.unidad = selectedRow.Cells["unidad"].Value.ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                alta_log("Problema dgvAutocompleteProducto_KeyDown - " + ex.Message);
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
            dgvAutocompleteProducto.Visible = false;
        }

        private void txtBuscarProductoForm_Leave(object sender, EventArgs e)
        {
            this.dgvAutocompleteProducto.Visible = false;
        }

        private void btnLeerPeso_Click(object sender, EventArgs e)
        {
            leer_peso_balanza();
        }

        private void SerialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                //Dato_Recibido = SerialPort1.ReadExisting;
                acceso_interrupcion(SerialPort1.ReadExisting());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                alta_log("Problema SerialPort1_DataReceived - " + ex.Message);
            }
        }

        private void acceso_interrupcion(string accion)
        {
            try
            {
                DelegadoAcceso var_delegado_acceso;
                var_delegado_acceso = new DelegadoAcceso(acceso_form);
                object[] arg = { accion };
                base.Invoke(var_delegado_acceso, arg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
                alta_log("Problema acceso_interrupcion - " + ex.Message);
            }
        }

        private void acceso_form(string accion)
        {
            try
            {
                int longitud = System.Text.Encoding.Unicode.GetByteCount(accion);

                if (longitud > 8)
                {
                    string partePeso = accion.Substring(2, 6);
                    double pesoKg = double.Parse(partePeso) / 1000.0;

                    txtPesoBalanza.Text = pesoKg.ToString();
                    peso_pendiente = pesoKg;

                    //this.bandera_lectura_balanza = true;

                    if (dataListadoProductos.RowCount > 0)
                    {
                        int ultimaFilaIndex = dataListadoProductos.Rows.Count - 1;

                        // Acceder a la celda en la última fila y columna "Peso"
                        DataGridViewCell celdaPeso = dataListadoProductos.Rows[ultimaFilaIndex].Cells["Peso"];

                        // Modificar el valor de la celda
                        celdaPeso.Value = pesoKg;
                    }


                    // calculo
                    double calculo_precio = (pesoKg * Convert.ToDouble(txtPrecioUnitario.Text));

                    txtPrecioUnitario.Text = calculo_precio.ToString();
                }
                else
                {
                    //MensajeError("Error al leer peso, intente nuevamente");

                    // elimino el ultimo elemento
                    //if (dataListadoProductos.Rows.Count > 0)
                    //{
                    //    int ultimaFilaIndex = dataListadoProductos.Rows.Count - 1;
                    //    dataListadoProductos.Rows.RemoveAt(ultimaFilaIndex);
                    //}
                    alta_log("Problema con cadena en acceso_form - accion : " + accion);
                }

            }
            catch (Exception ex)
            {
                //MensajeError("Error al leer peso, intente nuevamente");
                // MessageBox.Show(ex.Message + ex.StackTrace);
                alta_log("Problemas acceso_form - " + ex.Message);
                alta_log("accion : " + accion);
            }
        }

        private void txtEntrega_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números y puntos
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true; // Bloquear la entrada
            }

            // Permitir solo un punto decimal
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true; // Bloquear la entrada
            }
        }

        private void leer_peso_balanza()
        {
            try
            {
                if (!SerialPort1.IsOpen)
                {
                    // MessageBox.Show("Problemas con lectura de balanza. Contactese con el administrador");
                    alta_log("Problemas con lectura de balanza - leer_peso_balanza()");
                    return;
                }

                txtPesoBalanza.Enabled = true;

                string CRC = "0";
                string TramaTX = "";
                Int16 i;
                string Temp;

                CRC = "21";         // funcion para pedir peso

                TramaTX = Convert.ToChar(20).ToString() + Convert.ToChar(1).ToString() + Convert.ToChar(21).ToString();
                SerialPort1.Write(TramaTX);
            }
            catch (Exception ex)
            {
                alta_log("Problemas con lectura de balanza 2 - leer_peso_balanza()" + ex.Message);
                //MessageBox.Show("Problemas con lectura de balanza. Contactese con el administrador - " + ex.Message);
            }
        }

        private void alta_log(string mensaje)
        {
            try
            {

                // Obtiene la ruta de acceso a la carpeta AppData del usuario actual
                string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                // Crea la ruta completa para tu archivo dentro de la carpeta AppData
                string filePath = Path.Combine(appDataFolder, "store-soft", "logs_form_venta.txt");

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

        private void txtBuscarProductoForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Return) // Verificar si se presionó la tecla Enter
                {
                    // Obtener el código de barras completo del TextBox
                    string codigoBarras = txtBuscarProductoForm.Text;

                    // Procesar el código de barras (ejemplo: imprimirlo en la consola)
                    // Console.WriteLine("Código de barras completo: " + codigoBarras);

                    procesar_producto(codigoBarras);

                    // Limpiar el TextBox para la próxima lectura
                    txtBuscarProductoForm.Clear();
                }

            }
            catch (Exception ex)
            {
                alta_log("Problema txtBuscarProductoForm_KeyPress - " + ex.Message);
                // MessageBox.Show(ex.Message);
            }
        }


        private void procesar_producto(string codigo_barra)
        {
            try
            {
                if (!string.IsNullOrEmpty(codigo_barra))
                {
                    dgvAutocompleteProducto.Visible = true;

                    if (codigo_barra.Length > 12)
                    {
                        string digitos = codigo_barra.Substring(0, 3);  // PLU

                        //
                        string importe_ticket = codigo_barra.Substring(4, 11);


                        string importe_ticket_formateado = (int.Parse(importe_ticket) / 100.0).ToString("N2");

                        txtPrecioUnitario.Text = importe_ticket_formateado;

                        leer_peso_balanza();

                        this.dgvAutocompleteProducto.DataSource = objetoCN_productos.BuscarProducto(digitos);

                        int selectedrowindex = dgvAutocompleteProducto.SelectedCells[0].RowIndex;
                        DataGridViewRow selectedRow = dgvAutocompleteProducto.Rows[selectedrowindex];

                        this.IdProducto = Convert.ToInt32(selectedRow.Cells["IdProducto"].Value);
                        this.lblNombreProd.Text = selectedRow.Cells["Producto"].Value.ToString();
                        //txtPrecioUnitario.Text = selectedRow.Cells["PrecioVenta"].Value.ToString();
                        stock_pendiente_producto = Convert.ToDecimal(selectedRow.Cells["Stock"].Value);
                        codigo_pendiente_producto = selectedRow.Cells["Codigo"].Value.ToString();


                        btnAgregarProducto.PerformClick();
                        dgvAutocompleteProducto.Visible = false;
                        

                    }
                    else
                    {
                        this.dgvAutocompleteProducto.DataSource = objetoCN_productos.BuscarProducto(this.txtBuscarProductoForm.Text);
                    }

                }
                else
                {
                    dgvAutocompleteProducto.Visible = false;
                    // this.txtBuscarProductoForm.Text = "";

                }

            }
            catch (Exception ex)
            {
                alta_log("Problemas con lectura de balanza 2 - leer_peso_balanza()" + ex.Message);
                //MessageBox.Show("Problemas con lectura de balanza. Contactese con el administrador - " + ex.Message);
            }
        }

        private void txtDescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter presionado es un número o una tecla de control (como el backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("Entrada invalida");
                e.Handled = true; // Cancelar la entrada si no es un número
                return;
            }

            // *** Calcular el descuento ***
            // Validar que los valores ingresados en ambos TextBox son numéricos
            if (decimal.TryParse(lblSubTotal.Text, out decimal precioOriginal) &&
                decimal.TryParse(txtDescuento.Text, out decimal porcentajeDescuento))
            {
                // Verificar que el porcentaje de descuento esté entre 0 y 100
                if (porcentajeDescuento >= 0 && porcentajeDescuento <= 100)
                {
                    // Calcular el descuento
                    decimal descuento = precioOriginal * (porcentajeDescuento / 100);
                    decimal precioFinal = precioOriginal - descuento;

                    // Mostrar el resultado en un TextBox o Label
                    lblTotal.Text = precioFinal.ToString("F2"); // Formato con 2 decimales
                }
                else
                {
                    MessageBox.Show("El porcentaje de descuento debe estar entre 0 y 100.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese valores válidos para el precio y el porcentaje de descuento.");
            }

            

        }

        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtDescuento.Text, out int value))
            {
                // Si el valor está fuera del rango 0-99, lo corriges
                if (value < 0 || value > 99)
                {
                    MessageBox.Show("Solo se permiten valores entre 0 y 99.");
                    txtDescuento.Text = "0"; // Limpiar el texto si el valor no es válido
                }
            }
            else if (!string.IsNullOrEmpty(txtDescuento.Text)) // Verificar si no es vacío
            {
                MessageBox.Show("Solo se permiten valores numéricos.");
                txtDescuento.Text = "0"; // Limpiar el texto si no es numérico
            }


            // *** poner valor ingresado en el lblDescuento ***
            lblDescuento.Text = txtDescuento.Text;

        }

        private void txtDescuento_Leave(object sender, EventArgs e)
        {

            // ** agregar enn la fila en el listado de ventas **
            this.dataListadoProductos.Rows.Insert(this.dataListadoProductos.RowCount, 1,
                        1, "Descuento", 1, 1, 0, 0);
        }
    }
}

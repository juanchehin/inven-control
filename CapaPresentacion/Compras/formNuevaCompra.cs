using CapaNegocio;
using System;
using System.Data;
using System.Windows.Forms;

namespace CapaPresentacion.Compras
{
    public partial class formNuevaCompra : Form
    {
        CN_Ventas objetoCN = new CN_Ventas();
        CN_Empleados objetoCN_empleado = new CN_Empleados();
        CN_Productos objetoCN_productos = new CN_Productos();
        CN_Proveedores objetoCN_proveedores = new CN_Proveedores();
        CN_Clientes objetoCN_clientes = new CN_Clientes();

        DataTable respuesta;
        DataTable tiposPagos;

        private int IdCompra;
        private int IdUsuario;  // IdEmpleado
        private int IdProveedor = 4;  // Proveedor generico si no se especifica otro
        public string Proveedor = "Proveedor";
        public string Usuario;
        private string tipoPago;

        private int IdProducto;

        private DateTime Fecha;

        private string Cantidad;
        private int pDesde = 0;

        private decimal precioTotal = 0;

        DataTable dtRespuesta = new DataTable();
        public formNuevaCompra(int IdUsuario, string usuario)
        {
            InitializeComponent();
            panelProductos.Visible = false;
            panelProveedores.Visible = false;

            this.IdUsuario = IdUsuario;
            this.Usuario = usuario;
            // Creo las columnas de el listado de ventas

            this.dataListadoProductos.Columns.Add("IdProducto", "IdProducto");
            this.dataListadoProductos.Columns.Add("Codigo", "Codigo");
            this.dataListadoProductos.Columns.Add("Producto", "Producto");
            this.dataListadoProductos.Columns.Add("Cantidad", "Cantidad");
            this.dataListadoProductos.Columns.Add("PrecioUnitario", "Precio unitario");

            this.dataListadoProductos.Columns["IdProducto"].Visible = false;   // Oculto "IdProducto"

            this.lblUsuario.Text = usuario;
            this.IdUsuario = IdUsuario;

            this.lblProveedor.Text = this.Proveedor;
            this.llenarCBTiposPago();
        }

        
        
        private void llenarCBTiposPago()
        {
            tiposPagos = objetoCN.DameTiposPago();

            cbTiposPago.DataSource = tiposPagos;

            cbTiposPago.DisplayMember = "TipoPago";
            //cbTiposPago.ValueMember = "IdTipoPago";

            //this.tipoPago = cbTiposPago.SelectedItem.DataView;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            DataTable productos = new DataTable();
            productos.Columns.Add("IdProducto", typeof(System.Int32));
            productos.Columns.Add("Cantidad", typeof(System.Int32));


            foreach (DataGridViewRow rowGrid in this.dataListadoProductos.Rows)
            {
                DataRow row = productos.NewRow();
                row["IdProducto"] = Convert.ToDouble(rowGrid.Cells[0].Value);
                row["Cantidad"] = rowGrid.Cells[3].Value;

                productos.Rows.Add(row);
            }

            try
            {
                string rpta = "";
                if (this.dataListadoProductos.CurrentRow == null)
                {
                    MensajeError("Productos inexistentes");
                    return;
                }

                //rpta = CN_Ventas.AltaCompra(this.IdUsuario, this.IdProveedor, cbTiposPago.Text, productos, Convert.ToDecimal(this.precioTotal.ToString()));

                if (rpta.Equals("Ok"))
                {
                    //panelVuelto.Visible = true;
                    //this.lblImporte.Text = this.precioTotal.ToString();

                }
                else
                {
                    this.MensajeError(rpta);
                }
                //this.Close();
            }
            catch (Exception ex)
            {
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

            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Debe ingresar valores numericos ");
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.dataListadoProductos.DataSource = objetoCN_empleado.BuscarEmpleado(this.txtBuscar.Text);
        }


        // Devuelve los datos de un solo producto. Si no devuelve un mensaje de 'no encontrado'
        private void BuscarProductoPorCodigo()
        {
            this.dtRespuesta = objetoCN_productos.BuscarProductoPorCodigo(this.txtBuscar.Text);

            if (this.dtRespuesta.Rows[0][0].ToString() == "Producto con codigo inexistente")
            {
                this.MensajeError("Producto con codigo inexistente");
                return;
            }

            // Sacar de 'respuesta' los datos para completar en el form
            this.IdProducto = Convert.ToInt32(this.dtRespuesta.Rows[0][0]);
            this.lblNombreProd.Text = this.dtRespuesta.Rows[0][1].ToString();
            this.lblPrecioUnitario.Text = this.dtRespuesta.Rows[0][3].ToString();


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
                        int cantidad = int.Parse(this.dataListadoProductos.Rows[item.Index].Cells["Cantidad"].Value.ToString());

                        this.dataListadoProductos.Rows.RemoveAt(item.Index);

                        this.precioTotal = this.precioTotal - (precio * cantidad);
                        this.lblTotal.Text = precioTotal.ToString();
                    }

                    this.MensajeOk("Se elimino de forma correcta el producto");
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

        public void MostrarProveedores()
        {
            dataListadoProveedores.DataSource = objetoCN_clientes.MostrarClientes();
            dataListadoProveedores.Columns[0].Visible = false;
        }

        private void txtEntrega_TextChanged(object sender, EventArgs e)
        {
            //if (this.txtEntrega.Text != "")
            //{
            //    decimal result = (this.precioTotal - Convert.ToDecimal(this.txtEntrega.Text)) * -1;
            //    this.lblVuelto.Text = result.ToString();
            //}
            //else
            //{
            //    this.lblVuelto.Text = this.precioTotal.ToString();
            //}
        }

        private void btnImprimirTicket_Click(object sender, EventArgs e)
        {
            //formTicket frm = new formTicket(this.IdUsuario, this.IdCliente, this.dataListadoProductos);
            //frm.MdiParent = this.MdiParent;
            //frm.Show();

            this.Close();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            //panelVuelto.Visible = false;
            this.Close();
        }

        private void btnSeleccionarProveedor_Click(object sender, EventArgs e)
        {
            panelProveedores.Visible = !panelProveedores.Visible;
            CargarProveedores();
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
                    this.lblPrecioUnitario.Text = selectedRow.Cells["PrecioVenta"].Value.ToString();
                    this.txtBuscar.Text = selectedRow.Cells["Codigo"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
            panelProductos.Visible = false;
        }

        private void btnSeleccionarProveedorPanel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataListadoProveedores.SelectedCells.Count > 0)
                {
                    int selectedrowindex = dataListadoProveedores.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataListadoProveedores.Rows[selectedrowindex];

                    this.IdProveedor = Convert.ToInt32(selectedRow.Cells["IdProveedor"].Value);
                    this.Proveedor = selectedRow.Cells["Proveedor"].Value.ToString();
                }

                lblProveedor.Text = this.Proveedor;

                panelProductos.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
            panelProveedores.Visible = false;
        }

        private void btnBuscarProd_Click_1(object sender, EventArgs e)
        {
            this.BuscarProductoPorCodigo();
        }

        private void btnAgregarProducto_Click_1(object sender, EventArgs e)
        {
            if (this.lblNombreProd.Text == "" || this.lblPrecioUnitario.Text == "")
            {
                MensajeError("Debe cargar un producto");
                return;
            }

            decimal dec = decimal.Parse(this.lblPrecioUnitario.Text);
            bool bandera = false;

            if (dataListadoProductos.Rows.Count == 0)
            {
                // Si no cargo la cantidad, cargo por defecto 1
                if (String.IsNullOrEmpty(this.txtCantidad.Text))
                {
                    this.dataListadoProductos.Rows.Insert(this.dataListadoProductos.RowCount, this.IdProducto, this.txtBuscar.Text, this.lblNombreProd.Text, 1, this.lblPrecioUnitario.Text);
                    this.precioTotal += dec;
                }
                else
                {
                    decimal cant = decimal.Parse(this.txtCantidad.Text);
                    this.dataListadoProductos.Rows.Insert(this.dataListadoProductos.RowCount, this.IdProducto, this.txtBuscar.Text, this.lblNombreProd.Text, this.txtCantidad.Text, this.lblPrecioUnitario.Text);
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
                            row.Cells["Cantidad"].Value = 1 + Convert.ToInt32(row.Cells[3].Value);
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
                        this.dataListadoProductos.Rows.Insert(this.dataListadoProductos.RowCount, this.IdProducto, this.txtBuscar.Text, this.lblNombreProd.Text, 1, this.lblPrecioUnitario.Text);
                        this.precioTotal += dec;
                    }
                    else
                    {
                        decimal cant = decimal.Parse(this.txtCantidad.Text);
                        this.dataListadoProductos.Rows.Insert(this.dataListadoProductos.RowCount, this.IdProducto, this.txtBuscar.Text, this.lblNombreProd.Text, this.txtCantidad.Text, this.lblPrecioUnitario.Text);
                        this.precioTotal += dec * cant;
                    }
                }
            }

            this.lblTotal.Text = this.precioTotal.ToString();
        }

        private void btnCancelarPanelProductos_Click(object sender, EventArgs e)
        {
            panelProductos.Visible = false;
        }

        private void btnCancelarPanelProveedor_Click(object sender, EventArgs e)
        {
            panelProveedores.Visible = false;
        }

        private void btnBusquedaAvanzada_Click(object sender, EventArgs e)
        {
            panelProductos.Visible = true;
            CargarProductos();
        }

        private void CargarProductos()
        {
            //panelProductos.Visible = true;
            dataListadoProductosPanel.DataSource = objetoCN_productos.ListarProductos(this.pDesde).Tables[0];
        }
        private void CargarProveedores()
        {
            dataListadoProveedores.DataSource = objetoCN_proveedores.MostrarProveedores();
        }

        private void btnRegistrar_Click_1(object sender, EventArgs e)
        {
            DataTable productos = new DataTable();
            productos.Columns.Add("IdProducto", typeof(System.Int32));
            productos.Columns.Add("Cantidad", typeof(System.Decimal));


            foreach (DataGridViewRow rowGrid in this.dataListadoProductos.Rows)
            {
                DataRow row = productos.NewRow();
                row["IdProducto"] = Convert.ToDouble(rowGrid.Cells[0].Value);
                row["Cantidad"] = rowGrid.Cells[3].Value;

                productos.Rows.Add(row);
            }

            try
            {
                string rpta = "";
                if (this.dataListadoProductos.CurrentRow == null)
                {
                    MensajeError("Productos inexistentes");
                    return;
                }

                rpta = CN_Compras.AltaCompra(this.IdUsuario, this.IdProveedor, cbTiposPago.Text, productos, Convert.ToDecimal(this.precioTotal.ToString()));

                if (rpta.Equals("Ok"))
                {
                    this.MensajeOk("Compra realizada");
                    this.Close();

                }
                else
                {
                    this.MensajeError(rpta);
                }
                //this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            this.dataListadoProductosPanel.DataSource = objetoCN_productos.BuscarProducto(this.txtBuscarProductoPanel.Text);
        }

        private void btnBuscarProveedor_Click(object sender, EventArgs e)
        {
            this.dataListadoProveedores.DataSource = objetoCN_proveedores.BuscarProveedor(this.txtBuscarProveedor.Text);
        }

        private void txtBuscar_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.BuscarProductoPorCodigo();
            }
        }

        private void txtBuscarProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.BuscarProducto();
            }
        }

        private void BuscarProducto()
        {
            this.dataListadoProductosPanel.DataSource = objetoCN_productos.BuscarProducto(this.txtBuscarProductoPanel.Text);
        }

        private void btnNuevoProducto_Click(object sender, EventArgs e)
        {
            formNuevoEditarProducto frm = new formNuevoEditarProducto(0, true);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void btnAgregarProveedorPanel_Click(object sender, EventArgs e)
        {
            formNuevoEditarProveedor frm = new formNuevoEditarProveedor(0, true);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnQuitar_Click_1(object sender, EventArgs e)
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
                        int cantidad = int.Parse(this.dataListadoProductos.Rows[item.Index].Cells["Cantidad"].Value.ToString());

                        this.dataListadoProductos.Rows.RemoveAt(item.Index);

                        this.precioTotal = this.precioTotal - (precio * cantidad);
                        this.lblTotal.Text = precioTotal.ToString();
                    }

                    this.MensajeOk("Se elimino de forma correcta el producto");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;

            if (e.KeyChar == '.')
            {
                // Reemplazar el punto '.' por una coma ','
                e.KeyChar = ',';
            }

            if (!Char.IsDigit(chr) && chr != 8 && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
                MessageBox.Show("Debe ingresar valores numericos,comas o puntos");
            }
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;

            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Debe ingresar valores numericos ");
            }
        }
    }
}

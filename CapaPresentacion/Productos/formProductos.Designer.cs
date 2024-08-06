namespace CapaPresentacion
{
    partial class formProductos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formProductos));
            this.label1 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.botonEditarListado = new System.Windows.Forms.Button();
            this.btnNuevoProducto = new System.Windows.Forms.Button();
            this.dataListadoProductos = new System.Windows.Forms.DataGridView();
            this.lblTotalProductos = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ttCategorias = new System.Windows.Forms.ToolTip(this.components);
            this.ttActPrecios = new System.Windows.Forms.ToolTip(this.components);
            this.ttRefrescar = new System.Windows.Forms.ToolTip(this.components);
            this.btnSiguiente = new System.Windows.Forms.Button();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.btnBarcode = new System.Windows.Forms.Button();
            this.btnExportarExcel = new System.Windows.Forms.Button();
            this.btnCategorias = new System.Windows.Forms.Button();
            this.btnActualizarPrecios = new System.Windows.Forms.Button();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataListadoProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 54.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(354, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(397, 85);
            this.label1.TabIndex = 3;
            this.label1.Text = "Productos";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(232, 143);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 1;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(34, 143);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(181, 20);
            this.txtBuscar.TabIndex = 2;
            this.txtBuscar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuscar_KeyDown);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(587, 143);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 23);
            this.btnEliminar.TabIndex = 3;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // botonEditarListado
            // 
            this.botonEditarListado.Location = new System.Drawing.Point(314, 143);
            this.botonEditarListado.Name = "botonEditarListado";
            this.botonEditarListado.Size = new System.Drawing.Size(75, 23);
            this.botonEditarListado.TabIndex = 5;
            this.botonEditarListado.Text = "Editar";
            this.botonEditarListado.UseVisualStyleBackColor = true;
            this.botonEditarListado.Click += new System.EventHandler(this.botonEditarListado_Click);
            // 
            // btnNuevoProducto
            // 
            this.btnNuevoProducto.Location = new System.Drawing.Point(395, 143);
            this.btnNuevoProducto.Name = "btnNuevoProducto";
            this.btnNuevoProducto.Size = new System.Drawing.Size(186, 23);
            this.btnNuevoProducto.TabIndex = 6;
            this.btnNuevoProducto.Text = "Nuevo producto";
            this.btnNuevoProducto.UseVisualStyleBackColor = true;
            this.btnNuevoProducto.Click += new System.EventHandler(this.btnNuevoProducto_Click);
            // 
            // dataListadoProductos
            // 
            this.dataListadoProductos.AllowUserToAddRows = false;
            this.dataListadoProductos.AllowUserToDeleteRows = false;
            this.dataListadoProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataListadoProductos.Location = new System.Drawing.Point(37, 184);
            this.dataListadoProductos.MultiSelect = false;
            this.dataListadoProductos.Name = "dataListadoProductos";
            this.dataListadoProductos.ReadOnly = true;
            this.dataListadoProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataListadoProductos.Size = new System.Drawing.Size(992, 332);
            this.dataListadoProductos.TabIndex = 0;
            this.dataListadoProductos.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataListadoProductos_CellFormatting);
            this.dataListadoProductos.SelectionChanged += new System.EventHandler(this.dataListadoProductos_SelectionChanged);
            // 
            // lblTotalProductos
            // 
            this.lblTotalProductos.AutoSize = true;
            this.lblTotalProductos.Location = new System.Drawing.Point(909, 168);
            this.lblTotalProductos.Name = "lblTotalProductos";
            this.lblTotalProductos.Size = new System.Drawing.Size(35, 13);
            this.lblTotalProductos.TabIndex = 7;
            this.lblTotalProductos.Text = "label2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Busqueda por codigo o producto";
            // 
            // ttCategorias
            // 
            this.ttCategorias.Tag = "Categorias";
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.Location = new System.Drawing.Point(951, 522);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(75, 23);
            this.btnSiguiente.TabIndex = 18;
            this.btnSiguiente.Text = "Siguiente >>";
            this.btnSiguiente.UseVisualStyleBackColor = true;
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click);
            // 
            // btnAnterior
            // 
            this.btnAnterior.Location = new System.Drawing.Point(869, 522);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(75, 23);
            this.btnAnterior.TabIndex = 17;
            this.btnAnterior.Text = "<< Anterior";
            this.btnAnterior.UseVisualStyleBackColor = true;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            // 
            // btnBarcode
            // 
            this.btnBarcode.Image = ((System.Drawing.Image)(resources.GetObject("btnBarcode.Image")));
            this.btnBarcode.Location = new System.Drawing.Point(825, 121);
            this.btnBarcode.Name = "btnBarcode";
            this.btnBarcode.Size = new System.Drawing.Size(46, 42);
            this.btnBarcode.TabIndex = 23;
            this.ttCategorias.SetToolTip(this.btnBarcode, "Categorias");
            this.btnBarcode.UseVisualStyleBackColor = true;
            this.btnBarcode.Click += new System.EventHandler(this.btnBarcode_Click);
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.Image")));
            this.btnExportarExcel.Location = new System.Drawing.Point(980, 121);
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(46, 42);
            this.btnExportarExcel.TabIndex = 22;
            this.ttActPrecios.SetToolTip(this.btnExportarExcel, "Actualizar precios");
            this.btnExportarExcel.UseVisualStyleBackColor = true;
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // btnCategorias
            // 
            this.btnCategorias.Image = ((System.Drawing.Image)(resources.GetObject("btnCategorias.Image")));
            this.btnCategorias.Location = new System.Drawing.Point(877, 121);
            this.btnCategorias.Name = "btnCategorias";
            this.btnCategorias.Size = new System.Drawing.Size(46, 42);
            this.btnCategorias.TabIndex = 16;
            this.ttCategorias.SetToolTip(this.btnCategorias, "Categorias");
            this.btnCategorias.UseVisualStyleBackColor = true;
            this.btnCategorias.Click += new System.EventHandler(this.btnCategorias_Click);
            // 
            // btnActualizarPrecios
            // 
            this.btnActualizarPrecios.Image = ((System.Drawing.Image)(resources.GetObject("btnActualizarPrecios.Image")));
            this.btnActualizarPrecios.Location = new System.Drawing.Point(929, 121);
            this.btnActualizarPrecios.Name = "btnActualizarPrecios";
            this.btnActualizarPrecios.Size = new System.Drawing.Size(46, 42);
            this.btnActualizarPrecios.TabIndex = 15;
            this.ttActPrecios.SetToolTip(this.btnActualizarPrecios, "Actualizar precios");
            this.btnActualizarPrecios.UseVisualStyleBackColor = true;
            this.btnActualizarPrecios.Click += new System.EventHandler(this.btnActualizarPrecios_Click);
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.Image = global::CapaPresentacion.Properties.Resources.refresh;
            this.btnRefrescar.Location = new System.Drawing.Point(769, 127);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(47, 31);
            this.btnRefrescar.TabIndex = 19;
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click_1);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::CapaPresentacion.Properties.Resources.box1;
            this.pictureBox2.Location = new System.Drawing.Point(956, 9);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(70, 69);
            this.pictureBox2.TabIndex = 20;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 70);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            // 
            // formProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 588);
            this.Controls.Add(this.btnBarcode);
            this.Controls.Add(this.btnExportarExcel);
            this.Controls.Add(this.btnSiguiente);
            this.Controls.Add(this.btnAnterior);
            this.Controls.Add(this.btnCategorias);
            this.Controls.Add(this.btnActualizarPrecios);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnRefrescar);
            this.Controls.Add(this.lblTotalProductos);
            this.Controls.Add(this.btnNuevoProducto);
            this.Controls.Add(this.dataListadoProductos);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.botonEditarListado);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnBuscar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formProductos";
            this.Text = "                                                                                 " +
    "                                                          ..:: Productos ::..";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataListadoProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button botonEditarListado;
        private System.Windows.Forms.Button btnNuevoProducto;
        private System.Windows.Forms.DataGridView dataListadoProductos;
        private System.Windows.Forms.Label lblTotalProductos;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnActualizarPrecios;
        private System.Windows.Forms.Button btnCategorias;
        private System.Windows.Forms.ToolTip ttCategorias;
        private System.Windows.Forms.ToolTip ttActPrecios;
        private System.Windows.Forms.ToolTip ttRefrescar;
        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.Button btnExportarExcel;
        private System.Windows.Forms.Button btnBarcode;
    }
}


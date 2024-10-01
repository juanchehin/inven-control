namespace CapaPresentacion.Ventas
{
    partial class formVentas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formVentas));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_detalle_venta = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtFechaFin = new System.Windows.Forms.DateTimePicker();
            this.dtFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.dataListadoVentas = new System.Windows.Forms.DataGridView();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.btnSiguiente = new System.Windows.Forms.Button();
            this.btnNuevaVenta = new System.Windows.Forms.Button();
            this.lblTotalVentas = new System.Windows.Forms.Label();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.cbTiposPago = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_transferencia = new System.Windows.Forms.Label();
            this.lbl_mp = new System.Windows.Forms.Label();
            this.lbl_tarjeta = new System.Windows.Forms.Label();
            this.lbl_efectivo = new System.Windows.Forms.Label();
            this.btnEliminar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataListadoVentas)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CapaPresentacion.Properties.Resources.acquisition;
            this.pictureBox1.Location = new System.Drawing.Point(12, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 35;
            this.pictureBox1.TabStop = false;
            // 
            // btn_detalle_venta
            // 
            this.btn_detalle_venta.Location = new System.Drawing.Point(601, 174);
            this.btn_detalle_venta.Name = "btn_detalle_venta";
            this.btn_detalle_venta.Size = new System.Drawing.Size(75, 23);
            this.btn_detalle_venta.TabIndex = 72;
            this.btn_detalle_venta.Text = "Detalle";
            this.btn_detalle_venta.UseVisualStyleBackColor = true;
            this.btn_detalle_venta.Click += new System.EventHandler(this.btn_detalle_venta_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(244, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 65;
            this.label2.Text = "Fecha fin :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 64;
            this.label1.Text = "Fecha inicio :";
            // 
            // dtFechaFin
            // 
            this.dtFechaFin.Location = new System.Drawing.Point(244, 91);
            this.dtFechaFin.Name = "dtFechaFin";
            this.dtFechaFin.Size = new System.Drawing.Size(200, 20);
            this.dtFechaFin.TabIndex = 63;
            this.dtFechaFin.ValueChanged += new System.EventHandler(this.dtFechaFin_ValueChanged);
            // 
            // dtFechaInicio
            // 
            this.dtFechaInicio.Location = new System.Drawing.Point(13, 91);
            this.dtFechaInicio.Name = "dtFechaInicio";
            this.dtFechaInicio.Size = new System.Drawing.Size(200, 20);
            this.dtFechaInicio.TabIndex = 62;
            this.dtFechaInicio.ValueChanged += new System.EventHandler(this.dtFechaInicio_ValueChanged);
            // 
            // dataListadoVentas
            // 
            this.dataListadoVentas.AllowUserToAddRows = false;
            this.dataListadoVentas.AllowUserToDeleteRows = false;
            this.dataListadoVentas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataListadoVentas.Location = new System.Drawing.Point(12, 211);
            this.dataListadoVentas.MultiSelect = false;
            this.dataListadoVentas.Name = "dataListadoVentas";
            this.dataListadoVentas.ReadOnly = true;
            this.dataListadoVentas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataListadoVentas.Size = new System.Drawing.Size(798, 324);
            this.dataListadoVentas.TabIndex = 61;
            this.dataListadoVentas.SelectionChanged += new System.EventHandler(this.dataListadoCaja_SelectionChanged);
            // 
            // btnAnterior
            // 
            this.btnAnterior.Location = new System.Drawing.Point(637, 558);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(75, 23);
            this.btnAnterior.TabIndex = 73;
            this.btnAnterior.Text = "<< Anterior";
            this.btnAnterior.UseVisualStyleBackColor = true;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.Location = new System.Drawing.Point(735, 558);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(75, 23);
            this.btnSiguiente.TabIndex = 74;
            this.btnSiguiente.Text = "Siguiente >>";
            this.btnSiguiente.UseVisualStyleBackColor = true;
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click);
            // 
            // btnNuevaVenta
            // 
            this.btnNuevaVenta.Location = new System.Drawing.Point(443, 174);
            this.btnNuevaVenta.Name = "btnNuevaVenta";
            this.btnNuevaVenta.Size = new System.Drawing.Size(152, 23);
            this.btnNuevaVenta.TabIndex = 75;
            this.btnNuevaVenta.Text = "Nueva venta (F2)";
            this.btnNuevaVenta.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnNuevaVenta.UseVisualStyleBackColor = true;
            this.btnNuevaVenta.Click += new System.EventHandler(this.btnNuevaVenta_Click_1);
            // 
            // lblTotalVentas
            // 
            this.lblTotalVentas.AutoSize = true;
            this.lblTotalVentas.Location = new System.Drawing.Point(13, 195);
            this.lblTotalVentas.Name = "lblTotalVentas";
            this.lblTotalVentas.Size = new System.Drawing.Size(35, 13);
            this.lblTotalVentas.TabIndex = 76;
            this.lblTotalVentas.Text = "label3";
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.Image = global::CapaPresentacion.Properties.Resources.refresh;
            this.btnRefrescar.Location = new System.Drawing.Point(763, 167);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(47, 31);
            this.btnRefrescar.TabIndex = 77;
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
            // 
            // cbTiposPago
            // 
            this.cbTiposPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTiposPago.FormattingEnabled = true;
            this.cbTiposPago.Location = new System.Drawing.Point(12, 117);
            this.cbTiposPago.Name = "cbTiposPago";
            this.cbTiposPago.Size = new System.Drawing.Size(121, 21);
            this.cbTiposPago.TabIndex = 78;
            this.cbTiposPago.SelectedIndexChanged += new System.EventHandler(this.cbTiposPago_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Consolas", 35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(322, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(206, 55);
            this.label3.TabIndex = 93;
            this.label3.Text = "Egresos";
            // 
            // lbl_transferencia
            // 
            this.lbl_transferencia.AutoSize = true;
            this.lbl_transferencia.Location = new System.Drawing.Point(202, 170);
            this.lbl_transferencia.Name = "lbl_transferencia";
            this.lbl_transferencia.Size = new System.Drawing.Size(84, 13);
            this.lbl_transferencia.TabIndex = 94;
            this.lbl_transferencia.Text = "lbl_transferencia";
            // 
            // lbl_mp
            // 
            this.lbl_mp.AutoSize = true;
            this.lbl_mp.Location = new System.Drawing.Point(202, 157);
            this.lbl_mp.Name = "lbl_mp";
            this.lbl_mp.Size = new System.Drawing.Size(37, 13);
            this.lbl_mp.TabIndex = 95;
            this.lbl_mp.Text = "lbl_mp";
            // 
            // lbl_tarjeta
            // 
            this.lbl_tarjeta.AutoSize = true;
            this.lbl_tarjeta.Location = new System.Drawing.Point(13, 170);
            this.lbl_tarjeta.Name = "lbl_tarjeta";
            this.lbl_tarjeta.Size = new System.Drawing.Size(52, 13);
            this.lbl_tarjeta.TabIndex = 96;
            this.lbl_tarjeta.Text = "lbl_tarjeta";
            // 
            // lbl_efectivo
            // 
            this.lbl_efectivo.AutoSize = true;
            this.lbl_efectivo.Location = new System.Drawing.Point(13, 157);
            this.lbl_efectivo.Name = "lbl_efectivo";
            this.lbl_efectivo.Size = new System.Drawing.Size(61, 13);
            this.lbl_efectivo.TabIndex = 97;
            this.lbl_efectivo.Text = "lbl_efectivo";
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(682, 174);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 23);
            this.btnEliminar.TabIndex = 98;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // formVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 590);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.lbl_efectivo);
            this.Controls.Add(this.lbl_tarjeta);
            this.Controls.Add(this.lbl_mp);
            this.Controls.Add(this.lbl_transferencia);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbTiposPago);
            this.Controls.Add(this.btnRefrescar);
            this.Controls.Add(this.lblTotalVentas);
            this.Controls.Add(this.btnNuevaVenta);
            this.Controls.Add(this.btnSiguiente);
            this.Controls.Add(this.btnAnterior);
            this.Controls.Add(this.btn_detalle_venta);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtFechaFin);
            this.Controls.Add(this.dtFechaInicio);
            this.Controls.Add(this.dataListadoVentas);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formVentas";
            this.Text = "Ventas";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.formVentas_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataListadoVentas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_detalle_venta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtFechaFin;
        private System.Windows.Forms.DateTimePicker dtFechaInicio;
        private System.Windows.Forms.DataGridView dataListadoVentas;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.Button btnNuevaVenta;
        private System.Windows.Forms.Label lblTotalVentas;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.ComboBox cbTiposPago;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_transferencia;
        private System.Windows.Forms.Label lbl_mp;
        private System.Windows.Forms.Label lbl_tarjeta;
        private System.Windows.Forms.Label lbl_efectivo;
        private System.Windows.Forms.Button btnEliminar;
    }
}
namespace CapaPresentacion.Caja
{
    partial class formDetalleVenta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formDetalleVenta));
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_nro_transaccion = new System.Windows.Forms.Label();
            this.dataListadoDetalleVenta = new System.Windows.Forms.DataGridView();
            this.lblTotalLineasVenta = new System.Windows.Forms.Label();
            this.lbl_tipo_pago = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataListadoDetalleVenta)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Consolas", 30F);
            this.label6.Location = new System.Drawing.Point(163, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(306, 47);
            this.label6.TabIndex = 10;
            this.label6.Text = "Detalle venta";
            // 
            // lbl_nro_transaccion
            // 
            this.lbl_nro_transaccion.AutoSize = true;
            this.lbl_nro_transaccion.Font = new System.Drawing.Font("Consolas", 30F);
            this.lbl_nro_transaccion.Location = new System.Drawing.Point(475, 29);
            this.lbl_nro_transaccion.Name = "lbl_nro_transaccion";
            this.lbl_nro_transaccion.Size = new System.Drawing.Size(86, 47);
            this.lbl_nro_transaccion.TabIndex = 11;
            this.lbl_nro_transaccion.Text = "# -";
            // 
            // dataListadoDetalleVenta
            // 
            this.dataListadoDetalleVenta.AllowUserToAddRows = false;
            this.dataListadoDetalleVenta.AllowUserToDeleteRows = false;
            this.dataListadoDetalleVenta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataListadoDetalleVenta.Location = new System.Drawing.Point(41, 98);
            this.dataListadoDetalleVenta.MultiSelect = false;
            this.dataListadoDetalleVenta.Name = "dataListadoDetalleVenta";
            this.dataListadoDetalleVenta.ReadOnly = true;
            this.dataListadoDetalleVenta.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataListadoDetalleVenta.Size = new System.Drawing.Size(703, 315);
            this.dataListadoDetalleVenta.TabIndex = 12;
            // 
            // lblTotalLineasVenta
            // 
            this.lblTotalLineasVenta.AutoSize = true;
            this.lblTotalLineasVenta.Location = new System.Drawing.Point(623, 82);
            this.lblTotalLineasVenta.Name = "lblTotalLineasVenta";
            this.lblTotalLineasVenta.Size = new System.Drawing.Size(35, 13);
            this.lblTotalLineasVenta.TabIndex = 13;
            this.lblTotalLineasVenta.Text = "label1";
            // 
            // lbl_tipo_pago
            // 
            this.lbl_tipo_pago.AutoSize = true;
            this.lbl_tipo_pago.Location = new System.Drawing.Point(41, 79);
            this.lbl_tipo_pago.Name = "lbl_tipo_pago";
            this.lbl_tipo_pago.Size = new System.Drawing.Size(35, 13);
            this.lbl_tipo_pago.TabIndex = 14;
            this.lbl_tipo_pago.Text = "label1";
            // 
            // formDetalleVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbl_tipo_pago);
            this.Controls.Add(this.lblTotalLineasVenta);
            this.Controls.Add(this.dataListadoDetalleVenta);
            this.Controls.Add(this.lbl_nro_transaccion);
            this.Controls.Add(this.label6);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formDetalleVenta";
            this.Text = "Detalle venta";
            this.Load += new System.EventHandler(this.formDetalleVenta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataListadoDetalleVenta)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_nro_transaccion;
        private System.Windows.Forms.DataGridView dataListadoDetalleVenta;
        private System.Windows.Forms.Label lblTotalLineasVenta;
        private System.Windows.Forms.Label lbl_tipo_pago;
    }
}
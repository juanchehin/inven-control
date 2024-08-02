namespace CapaPresentacion.Compras
{
    partial class formDetalleCompra
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formDetalleCompra));
            this.lblTotalLineasCompra = new System.Windows.Forms.Label();
            this.dataListadoDetalleCompra = new System.Windows.Forms.DataGridView();
            this.lbl_nro_transaccion = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataListadoDetalleCompra)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTotalLineasCompra
            // 
            this.lblTotalLineasCompra.AutoSize = true;
            this.lblTotalLineasCompra.Location = new System.Drawing.Point(46, 86);
            this.lblTotalLineasCompra.Name = "lblTotalLineasCompra";
            this.lblTotalLineasCompra.Size = new System.Drawing.Size(35, 13);
            this.lblTotalLineasCompra.TabIndex = 18;
            this.lblTotalLineasCompra.Text = "label1";
            // 
            // dataListadoDetalleCompra
            // 
            this.dataListadoDetalleCompra.AllowUserToAddRows = false;
            this.dataListadoDetalleCompra.AllowUserToDeleteRows = false;
            this.dataListadoDetalleCompra.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataListadoDetalleCompra.Location = new System.Drawing.Point(49, 102);
            this.dataListadoDetalleCompra.MultiSelect = false;
            this.dataListadoDetalleCompra.Name = "dataListadoDetalleCompra";
            this.dataListadoDetalleCompra.ReadOnly = true;
            this.dataListadoDetalleCompra.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataListadoDetalleCompra.Size = new System.Drawing.Size(703, 315);
            this.dataListadoDetalleCompra.TabIndex = 17;
            // 
            // lbl_nro_transaccion
            // 
            this.lbl_nro_transaccion.AutoSize = true;
            this.lbl_nro_transaccion.Font = new System.Drawing.Font("Consolas", 30F);
            this.lbl_nro_transaccion.Location = new System.Drawing.Point(505, 33);
            this.lbl_nro_transaccion.Name = "lbl_nro_transaccion";
            this.lbl_nro_transaccion.Size = new System.Drawing.Size(86, 47);
            this.lbl_nro_transaccion.TabIndex = 16;
            this.lbl_nro_transaccion.Text = "# -";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Consolas", 30F);
            this.label6.Location = new System.Drawing.Point(171, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(328, 47);
            this.label6.TabIndex = 15;
            this.label6.Text = "Detalle compra";
            // 
            // formDetalleCompra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblTotalLineasCompra);
            this.Controls.Add(this.dataListadoDetalleCompra);
            this.Controls.Add(this.lbl_nro_transaccion);
            this.Controls.Add(this.label6);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formDetalleCompra";
            this.Text = "Detalle Compra";
            this.Load += new System.EventHandler(this.formDetalleCompra_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataListadoDetalleCompra)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTotalLineasCompra;
        private System.Windows.Forms.DataGridView dataListadoDetalleCompra;
        private System.Windows.Forms.Label lbl_nro_transaccion;
        private System.Windows.Forms.Label label6;
    }
}
namespace CapaPresentacion.Ventas
{
    partial class formValidacionEliminacionVenta
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
            this.btnAceptarElimVenta = new System.Windows.Forms.Button();
            this.btnCancelarPassVenta = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtContraDeleteVenta = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnAceptarElimVenta
            // 
            this.btnAceptarElimVenta.Location = new System.Drawing.Point(74, 159);
            this.btnAceptarElimVenta.Name = "btnAceptarElimVenta";
            this.btnAceptarElimVenta.Size = new System.Drawing.Size(75, 23);
            this.btnAceptarElimVenta.TabIndex = 0;
            this.btnAceptarElimVenta.Text = "Aceptar";
            this.btnAceptarElimVenta.UseVisualStyleBackColor = true;
            this.btnAceptarElimVenta.Click += new System.EventHandler(this.btnAceptarElimVenta_Click);
            // 
            // btnCancelarPassVenta
            // 
            this.btnCancelarPassVenta.Location = new System.Drawing.Point(253, 159);
            this.btnCancelarPassVenta.Name = "btnCancelarPassVenta";
            this.btnCancelarPassVenta.Size = new System.Drawing.Size(75, 23);
            this.btnCancelarPassVenta.TabIndex = 1;
            this.btnCancelarPassVenta.Text = "Cancelar";
            this.btnCancelarPassVenta.UseVisualStyleBackColor = true;
            this.btnCancelarPassVenta.Click += new System.EventHandler(this.btnCancelarPassVenta_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Ingrese la contraseña:";
            // 
            // txtContraDeleteVenta
            // 
            this.txtContraDeleteVenta.Location = new System.Drawing.Point(176, 69);
            this.txtContraDeleteVenta.Name = "txtContraDeleteVenta";
            this.txtContraDeleteVenta.Size = new System.Drawing.Size(152, 20);
            this.txtContraDeleteVenta.TabIndex = 1;
            this.txtContraDeleteVenta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtContraDeleteVenta_KeyDown);
            // 
            // formValidacionEliminacionVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 203);
            this.Controls.Add(this.txtContraDeleteVenta);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancelarPassVenta);
            this.Controls.Add(this.btnAceptarElimVenta);
            this.Name = "formValidacionEliminacionVenta";
            this.Text = "Eliminacion venta";
            this.Load += new System.EventHandler(this.formValidacionEliminacionVenta_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAceptarElimVenta;
        private System.Windows.Forms.Button btnCancelarPassVenta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtContraDeleteVenta;
    }
}
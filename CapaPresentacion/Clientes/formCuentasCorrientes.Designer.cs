namespace CapaPresentacion.Clientes
{
    partial class formCuentasCorrientes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formCuentasCorrientes));
            this.btnAnterior = new System.Windows.Forms.Button();
            this.btnSiguiente = new System.Windows.Forms.Button();
            this.lblTotalMovimientosCC = new System.Windows.Forms.Label();
            this.btnNuevoDeposito = new System.Windows.Forms.Button();
            this.dataListadoCC = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblApellidNombre = new System.Windows.Forms.Label();
            this.lblSaldo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataListadoCC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAnterior
            // 
            this.btnAnterior.Location = new System.Drawing.Point(578, 490);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(75, 23);
            this.btnAnterior.TabIndex = 33;
            this.btnAnterior.Text = "<< Anterior";
            this.btnAnterior.UseVisualStyleBackColor = true;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.Location = new System.Drawing.Point(659, 490);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(75, 23);
            this.btnSiguiente.TabIndex = 32;
            this.btnSiguiente.Text = "Siguiente >>";
            this.btnSiguiente.UseVisualStyleBackColor = true;
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click);
            // 
            // lblTotalMovimientosCC
            // 
            this.lblTotalMovimientosCC.AutoSize = true;
            this.lblTotalMovimientosCC.Location = new System.Drawing.Point(49, 152);
            this.lblTotalMovimientosCC.Name = "lblTotalMovimientosCC";
            this.lblTotalMovimientosCC.Size = new System.Drawing.Size(35, 13);
            this.lblTotalMovimientosCC.TabIndex = 29;
            this.lblTotalMovimientosCC.Text = "label2";
            // 
            // btnNuevoDeposito
            // 
            this.btnNuevoDeposito.Location = new System.Drawing.Point(564, 135);
            this.btnNuevoDeposito.Name = "btnNuevoDeposito";
            this.btnNuevoDeposito.Size = new System.Drawing.Size(124, 23);
            this.btnNuevoDeposito.TabIndex = 28;
            this.btnNuevoDeposito.Text = "Nuevo Deposito";
            this.btnNuevoDeposito.UseVisualStyleBackColor = true;
            this.btnNuevoDeposito.Click += new System.EventHandler(this.btnNuevoDeposito_Click);
            // 
            // dataListadoCC
            // 
            this.dataListadoCC.AllowUserToAddRows = false;
            this.dataListadoCC.AllowUserToDeleteRows = false;
            this.dataListadoCC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataListadoCC.Location = new System.Drawing.Point(31, 168);
            this.dataListadoCC.MultiSelect = false;
            this.dataListadoCC.Name = "dataListadoCC";
            this.dataListadoCC.ReadOnly = true;
            this.dataListadoCC.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataListadoCC.Size = new System.Drawing.Size(703, 315);
            this.dataListadoCC.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(176, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(416, 47);
            this.label1.TabIndex = 22;
            this.label1.Text = "Movimientos Cuenta";
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.Image = ((System.Drawing.Image)(resources.GetObject("btnRefrescar.Image")));
            this.btnRefrescar.Location = new System.Drawing.Point(694, 130);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(40, 32);
            this.btnRefrescar.TabIndex = 21;
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(12, 22);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(72, 66);
            this.pictureBox2.TabIndex = 31;
            this.pictureBox2.TabStop = false;
            // 
            // lblApellidNombre
            // 
            this.lblApellidNombre.AutoSize = true;
            this.lblApellidNombre.Font = new System.Drawing.Font("Consolas", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApellidNombre.Location = new System.Drawing.Point(143, 77);
            this.lblApellidNombre.Name = "lblApellidNombre";
            this.lblApellidNombre.Size = new System.Drawing.Size(329, 32);
            this.lblApellidNombre.TabIndex = 34;
            this.lblApellidNombre.Text = "Apellido Nombre - DNI";
            // 
            // lblSaldo
            // 
            this.lblSaldo.AutoSize = true;
            this.lblSaldo.Location = new System.Drawing.Point(49, 130);
            this.lblSaldo.Name = "lblSaldo";
            this.lblSaldo.Size = new System.Drawing.Size(32, 13);
            this.lblSaldo.TabIndex = 35;
            this.lblSaldo.Text = "saldo";
            // 
            // formCuentasCorrientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 528);
            this.Controls.Add(this.lblSaldo);
            this.Controls.Add(this.lblApellidNombre);
            this.Controls.Add(this.btnAnterior);
            this.Controls.Add(this.btnSiguiente);
            this.Controls.Add(this.btnRefrescar);
            this.Controls.Add(this.lblTotalMovimientosCC);
            this.Controls.Add(this.btnNuevoDeposito);
            this.Controls.Add(this.dataListadoCC);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formCuentasCorrientes";
            this.Text = "Cuentas Corrientes";
            ((System.ComponentModel.ISupportInitialize)(this.dataListadoCC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.Label lblTotalMovimientosCC;
        private System.Windows.Forms.Button btnNuevoDeposito;
        private System.Windows.Forms.DataGridView dataListadoCC;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblApellidNombre;
        private System.Windows.Forms.Label lblSaldo;
    }
}
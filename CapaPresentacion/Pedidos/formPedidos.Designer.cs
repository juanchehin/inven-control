namespace CapaPresentacion.Pedidos
{
    partial class formPedidos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formPedidos));
            this.lblPedidosPendientes = new System.Windows.Forms.Label();
            this.btnNuevoPedido = new System.Windows.Forms.Button();
            this.btnSiguiente = new System.Windows.Forms.Button();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtFechaFin = new System.Windows.Forms.DateTimePicker();
            this.dtFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.dataListadoPedidos = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.btnConfirmarPedido = new System.Windows.Forms.Button();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblPedidosConfirmados = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbEstadoPedido = new System.Windows.Forms.ComboBox();
            this.lblNroPedidosPendientes = new System.Windows.Forms.Label();
            this.lblNroPedidosConfirmado = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataListadoPedidos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPedidosPendientes
            // 
            this.lblPedidosPendientes.AutoSize = true;
            this.lblPedidosPendientes.Location = new System.Drawing.Point(13, 131);
            this.lblPedidosPendientes.Name = "lblPedidosPendientes";
            this.lblPedidosPendientes.Size = new System.Drawing.Size(103, 13);
            this.lblPedidosPendientes.TabIndex = 100;
            this.lblPedidosPendientes.Text = "Pedidos pendientes:";
            // 
            // btnNuevoPedido
            // 
            this.btnNuevoPedido.Location = new System.Drawing.Point(649, 145);
            this.btnNuevoPedido.Name = "btnNuevoPedido";
            this.btnNuevoPedido.Size = new System.Drawing.Size(108, 23);
            this.btnNuevoPedido.TabIndex = 99;
            this.btnNuevoPedido.Text = "Nuevo pedido";
            this.btnNuevoPedido.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnNuevoPedido.UseVisualStyleBackColor = true;
            this.btnNuevoPedido.Click += new System.EventHandler(this.btnNuevoPedido_Click);
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.Location = new System.Drawing.Point(735, 504);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(75, 23);
            this.btnSiguiente.TabIndex = 98;
            this.btnSiguiente.Text = "Siguiente >>";
            this.btnSiguiente.UseVisualStyleBackColor = true;
            // 
            // btnAnterior
            // 
            this.btnAnterior.Location = new System.Drawing.Point(637, 504);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(75, 23);
            this.btnAnterior.TabIndex = 97;
            this.btnAnterior.Text = "<< Anterior";
            this.btnAnterior.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(244, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 96;
            this.label2.Text = "Fecha fin :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 95;
            this.label1.Text = "Fecha inicio :";
            // 
            // dtFechaFin
            // 
            this.dtFechaFin.Location = new System.Drawing.Point(244, 98);
            this.dtFechaFin.Name = "dtFechaFin";
            this.dtFechaFin.Size = new System.Drawing.Size(200, 20);
            this.dtFechaFin.TabIndex = 94;
            this.dtFechaFin.ValueChanged += new System.EventHandler(this.dtFechaFin_ValueChanged);
            // 
            // dtFechaInicio
            // 
            this.dtFechaInicio.Location = new System.Drawing.Point(13, 98);
            this.dtFechaInicio.Name = "dtFechaInicio";
            this.dtFechaInicio.Size = new System.Drawing.Size(200, 20);
            this.dtFechaInicio.TabIndex = 93;
            this.dtFechaInicio.ValueChanged += new System.EventHandler(this.dtFechaInicio_ValueChanged);
            // 
            // dataListadoPedidos
            // 
            this.dataListadoPedidos.AllowUserToAddRows = false;
            this.dataListadoPedidos.AllowUserToDeleteRows = false;
            this.dataListadoPedidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataListadoPedidos.Location = new System.Drawing.Point(12, 180);
            this.dataListadoPedidos.MultiSelect = false;
            this.dataListadoPedidos.Name = "dataListadoPedidos";
            this.dataListadoPedidos.ReadOnly = true;
            this.dataListadoPedidos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataListadoPedidos.Size = new System.Drawing.Size(798, 301);
            this.dataListadoPedidos.TabIndex = 92;
            this.dataListadoPedidos.SelectionChanged += new System.EventHandler(this.dataListadoPedidos_SelectionChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Consolas", 35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(315, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(206, 55);
            this.label3.TabIndex = 102;
            this.label3.Text = "Pedidos";
            // 
            // btnConfirmarPedido
            // 
            this.btnConfirmarPedido.Location = new System.Drawing.Point(535, 145);
            this.btnConfirmarPedido.Name = "btnConfirmarPedido";
            this.btnConfirmarPedido.Size = new System.Drawing.Size(108, 23);
            this.btnConfirmarPedido.TabIndex = 103;
            this.btnConfirmarPedido.Text = "Confirmar pedido";
            this.btnConfirmarPedido.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnConfirmarPedido.UseVisualStyleBackColor = true;
            this.btnConfirmarPedido.Click += new System.EventHandler(this.btnConfirmarPedido_Click);
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.Image = global::CapaPresentacion.Properties.Resources.refresh;
            this.btnRefrescar.Location = new System.Drawing.Point(763, 137);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(47, 31);
            this.btnRefrescar.TabIndex = 101;
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 91;
            this.pictureBox1.TabStop = false;
            // 
            // lblPedidosConfirmados
            // 
            this.lblPedidosConfirmados.AutoSize = true;
            this.lblPedidosConfirmados.Location = new System.Drawing.Point(12, 155);
            this.lblPedidosConfirmados.Name = "lblPedidosConfirmados";
            this.lblPedidosConfirmados.Size = new System.Drawing.Size(108, 13);
            this.lblPedidosConfirmados.TabIndex = 104;
            this.lblPedidosConfirmados.Text = "Pedidos confirmados:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(471, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 106;
            this.label5.Text = "Estado:";
            // 
            // cbEstadoPedido
            // 
            this.cbEstadoPedido.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEstadoPedido.FormattingEnabled = true;
            this.cbEstadoPedido.Location = new System.Drawing.Point(474, 97);
            this.cbEstadoPedido.Name = "cbEstadoPedido";
            this.cbEstadoPedido.Size = new System.Drawing.Size(121, 21);
            this.cbEstadoPedido.TabIndex = 105;
            this.cbEstadoPedido.SelectedIndexChanged += new System.EventHandler(this.cbEstadoPedido_SelectedIndexChanged);
            // 
            // lblNroPedidosPendientes
            // 
            this.lblNroPedidosPendientes.AutoSize = true;
            this.lblNroPedidosPendientes.Location = new System.Drawing.Point(127, 131);
            this.lblNroPedidosPendientes.Name = "lblNroPedidosPendientes";
            this.lblNroPedidosPendientes.Size = new System.Drawing.Size(13, 13);
            this.lblNroPedidosPendientes.TabIndex = 107;
            this.lblNroPedidosPendientes.Text = "0";
            // 
            // lblNroPedidosConfirmado
            // 
            this.lblNroPedidosConfirmado.AutoSize = true;
            this.lblNroPedidosConfirmado.Location = new System.Drawing.Point(127, 155);
            this.lblNroPedidosConfirmado.Name = "lblNroPedidosConfirmado";
            this.lblNroPedidosConfirmado.Size = new System.Drawing.Size(13, 13);
            this.lblNroPedidosConfirmado.TabIndex = 108;
            this.lblNroPedidosConfirmado.Text = "0";
            // 
            // formPedidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 536);
            this.Controls.Add(this.lblNroPedidosConfirmado);
            this.Controls.Add(this.lblNroPedidosPendientes);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbEstadoPedido);
            this.Controls.Add(this.lblPedidosConfirmados);
            this.Controls.Add(this.btnConfirmarPedido);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnRefrescar);
            this.Controls.Add(this.lblPedidosPendientes);
            this.Controls.Add(this.btnNuevoPedido);
            this.Controls.Add(this.btnSiguiente);
            this.Controls.Add(this.btnAnterior);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtFechaFin);
            this.Controls.Add(this.dtFechaInicio);
            this.Controls.Add(this.dataListadoPedidos);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formPedidos";
            this.Text = "Pedidos";
            ((System.ComponentModel.ISupportInitialize)(this.dataListadoPedidos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.Label lblPedidosPendientes;
        private System.Windows.Forms.Button btnNuevoPedido;
        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtFechaFin;
        private System.Windows.Forms.DateTimePicker dtFechaInicio;
        private System.Windows.Forms.DataGridView dataListadoPedidos;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnConfirmarPedido;
        private System.Windows.Forms.Label lblPedidosConfirmados;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbEstadoPedido;
        private System.Windows.Forms.Label lblNroPedidosPendientes;
        private System.Windows.Forms.Label lblNroPedidosConfirmado;
    }
}
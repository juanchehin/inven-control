namespace CapaPresentacion.Configuraciones.balanzas
{
    partial class formRecibir
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formRecibir));
            this.btn_ruta_logs = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.btnBuscarPuertos = new System.Windows.Forms.Button();
            this.cbStopBit = new System.Windows.Forms.ComboBox();
            this.dupDataBits = new System.Windows.Forms.DomainUpDown();
            this.cbParityBit = new System.Windows.Forms.ComboBox();
            this.dupBaudRate = new System.Windows.Forms.DomainUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbPuertos = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbMetodoConexion = new System.Windows.Forms.ComboBox();
            this.btnConectar = new System.Windows.Forms.Button();
            this.lblDatosRecibidos = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.spPuertos = new System.IO.Ports.SerialPort(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_ruta_logs
            // 
            this.btn_ruta_logs.Image = ((System.Drawing.Image)(resources.GetObject("btn_ruta_logs.Image")));
            this.btn_ruta_logs.Location = new System.Drawing.Point(12, 329);
            this.btn_ruta_logs.Name = "btn_ruta_logs";
            this.btn_ruta_logs.Size = new System.Drawing.Size(43, 44);
            this.btn_ruta_logs.TabIndex = 69;
            this.btn_ruta_logs.UseVisualStyleBackColor = true;
            this.btn_ruta_logs.Click += new System.EventHandler(this.btn_ruta_logs_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.btnBuscarPuertos);
            this.groupBox1.Controls.Add(this.cbStopBit);
            this.groupBox1.Controls.Add(this.dupDataBits);
            this.groupBox1.Controls.Add(this.cbParityBit);
            this.groupBox1.Controls.Add(this.dupBaudRate);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbPuertos);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(110, 86);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(533, 159);
            this.groupBox1.TabIndex = 73;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuracion balanza";
            // 
            // button3
            // 
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.Location = new System.Drawing.Point(204, 77);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(32, 22);
            this.button3.TabIndex = 18;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnBuscarPuertos
            // 
            this.btnBuscarPuertos.Location = new System.Drawing.Point(204, 31);
            this.btnBuscarPuertos.Name = "btnBuscarPuertos";
            this.btnBuscarPuertos.Size = new System.Drawing.Size(75, 23);
            this.btnBuscarPuertos.TabIndex = 11;
            this.btnBuscarPuertos.Text = "Buscar";
            this.btnBuscarPuertos.UseVisualStyleBackColor = true;
            this.btnBuscarPuertos.Click += new System.EventHandler(this.btnBuscarPuertos_Click);
            // 
            // cbStopBit
            // 
            this.cbStopBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStopBit.FormattingEnabled = true;
            this.cbStopBit.Location = new System.Drawing.Point(77, 121);
            this.cbStopBit.Name = "cbStopBit";
            this.cbStopBit.Size = new System.Drawing.Size(121, 21);
            this.cbStopBit.TabIndex = 10;
            // 
            // dupDataBits
            // 
            this.dupDataBits.Location = new System.Drawing.Point(408, 77);
            this.dupDataBits.Name = "dupDataBits";
            this.dupDataBits.Size = new System.Drawing.Size(120, 20);
            this.dupDataBits.TabIndex = 9;
            this.dupDataBits.Text = "8";
            // 
            // cbParityBit
            // 
            this.cbParityBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbParityBit.FormattingEnabled = true;
            this.cbParityBit.Location = new System.Drawing.Point(406, 33);
            this.cbParityBit.Name = "cbParityBit";
            this.cbParityBit.Size = new System.Drawing.Size(121, 21);
            this.cbParityBit.TabIndex = 8;
            // 
            // dupBaudRate
            // 
            this.dupBaudRate.Location = new System.Drawing.Point(76, 79);
            this.dupBaudRate.Name = "dupBaudRate";
            this.dupBaudRate.Size = new System.Drawing.Size(120, 20);
            this.dupBaudRate.TabIndex = 7;
            this.dupBaudRate.Text = "9600";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Stop bits:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Baud rate:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(350, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Parity bit:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(350, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Data bits:";
            // 
            // cbPuertos
            // 
            this.cbPuertos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPuertos.FormattingEnabled = true;
            this.cbPuertos.Location = new System.Drawing.Point(77, 33);
            this.cbPuertos.Name = "cbPuertos";
            this.cbPuertos.Size = new System.Drawing.Size(121, 21);
            this.cbPuertos.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Puerto: ";
            // 
            // cbMetodoConexion
            // 
            this.cbMetodoConexion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMetodoConexion.FormattingEnabled = true;
            this.cbMetodoConexion.Location = new System.Drawing.Point(436, 251);
            this.cbMetodoConexion.Name = "cbMetodoConexion";
            this.cbMetodoConexion.Size = new System.Drawing.Size(121, 21);
            this.cbMetodoConexion.TabIndex = 21;
            // 
            // btnConectar
            // 
            this.btnConectar.Location = new System.Drawing.Point(563, 251);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(75, 23);
            this.btnConectar.TabIndex = 19;
            this.btnConectar.Text = "Conectar";
            this.btnConectar.UseVisualStyleBackColor = true;
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // lblDatosRecibidos
            // 
            this.lblDatosRecibidos.AutoSize = true;
            this.lblDatosRecibidos.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatosRecibidos.Location = new System.Drawing.Point(421, 320);
            this.lblDatosRecibidos.Name = "lblDatosRecibidos";
            this.lblDatosRecibidos.Size = new System.Drawing.Size(29, 31);
            this.lblDatosRecibidos.TabIndex = 72;
            this.lblDatosRecibidos.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(216, 320);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 31);
            this.label1.TabIndex = 71;
            this.label1.Text = "Medicion:";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(19, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(73, 63);
            this.pictureBox2.TabIndex = 70;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(19, 86);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(73, 63);
            this.pictureBox1.TabIndex = 74;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(226, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(284, 47);
            this.label2.TabIndex = 75;
            this.label2.Text = "Recibir data";
            // 
            // spPuertos
            // 
            this.spPuertos.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.spPuertos_DataReceived);
            // 
            // formRecibir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 385);
            this.Controls.Add(this.cbMetodoConexion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnConectar);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btn_ruta_logs);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblDatosRecibidos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formRecibir";
            this.Text = "Recibir";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_ruta_logs;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbMetodoConexion;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnBuscarPuertos;
        private System.Windows.Forms.ComboBox cbStopBit;
        private System.Windows.Forms.DomainUpDown dupDataBits;
        private System.Windows.Forms.ComboBox cbParityBit;
        private System.Windows.Forms.DomainUpDown dupBaudRate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbPuertos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblDatosRecibidos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.IO.Ports.SerialPort spPuertos;
    }
}
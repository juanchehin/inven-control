namespace CapaPresentacion.Configuraciones.balanzas
{
    partial class formSystelCuora
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formSystelCuora));
            this.Label6 = new System.Windows.Forms.Label();
            this.TextBox_trama_enviada = new System.Windows.Forms.TextBox();
            this.btnApagarBalanza = new System.Windows.Forms.Button();
            this.Button9 = new System.Windows.Forms.Button();
            this.Button8 = new System.Windows.Forms.Button();
            this.Button7 = new System.Windows.Forms.Button();
            this.Button6 = new System.Windows.Forms.Button();
            this.btnEstado = new System.Windows.Forms.Button();
            this.btnPedirPeso = new System.Windows.Forms.Button();
            this.Text_Parametros = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Text_IP = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Text_Recibir = new System.Windows.Forms.TextBox();
            this.Text_Funcion = new System.Windows.Forms.TextBox();
            this.btnEnviarDatos = new System.Windows.Forms.Button();
            this.btnCerrarPuerto = new System.Windows.Forms.Button();
            this.btnAbrirPuerto = new System.Windows.Forms.Button();
            this.cbPuertos = new System.Windows.Forms.ComboBox();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnBuscarPuertos = new System.Windows.Forms.Button();
            this.SerialPort1 = new System.IO.Ports.SerialPort(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(5, 231);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(78, 13);
            this.Label6.TabIndex = 48;
            this.Label6.Text = "Trama enviada";
            // 
            // TextBox_trama_enviada
            // 
            this.TextBox_trama_enviada.Location = new System.Drawing.Point(84, 231);
            this.TextBox_trama_enviada.Margin = new System.Windows.Forms.Padding(2);
            this.TextBox_trama_enviada.Multiline = true;
            this.TextBox_trama_enviada.Name = "TextBox_trama_enviada";
            this.TextBox_trama_enviada.Size = new System.Drawing.Size(352, 99);
            this.TextBox_trama_enviada.TabIndex = 47;
            // 
            // btnApagarBalanza
            // 
            this.btnApagarBalanza.Location = new System.Drawing.Point(481, 196);
            this.btnApagarBalanza.Name = "btnApagarBalanza";
            this.btnApagarBalanza.Size = new System.Drawing.Size(135, 25);
            this.btnApagarBalanza.TabIndex = 46;
            this.btnApagarBalanza.Text = "Apagar Balanza";
            this.btnApagarBalanza.UseVisualStyleBackColor = true;
            // 
            // Button9
            // 
            this.Button9.Enabled = false;
            this.Button9.Location = new System.Drawing.Point(481, 227);
            this.Button9.Name = "Button9";
            this.Button9.Size = new System.Drawing.Size(135, 25);
            this.Button9.TabIndex = 45;
            this.Button9.Text = "Borrar PLU de la Balanza";
            this.Button9.UseVisualStyleBackColor = true;
            // 
            // Button8
            // 
            this.Button8.Enabled = false;
            this.Button8.Location = new System.Drawing.Point(481, 258);
            this.Button8.Name = "Button8";
            this.Button8.Size = new System.Drawing.Size(135, 25);
            this.Button8.TabIndex = 44;
            this.Button8.Text = "Enviar PLU a la Balanza";
            this.Button8.UseVisualStyleBackColor = true;
            // 
            // Button7
            // 
            this.Button7.Enabled = false;
            this.Button7.Location = new System.Drawing.Point(481, 288);
            this.Button7.Name = "Button7";
            this.Button7.Size = new System.Drawing.Size(135, 25);
            this.Button7.TabIndex = 43;
            this.Button7.Text = "Pedir PLU a la Balanza";
            this.Button7.UseVisualStyleBackColor = true;
            // 
            // Button6
            // 
            this.Button6.Enabled = false;
            this.Button6.Location = new System.Drawing.Point(481, 319);
            this.Button6.Name = "Button6";
            this.Button6.Size = new System.Drawing.Size(135, 25);
            this.Button6.TabIndex = 42;
            this.Button6.Text = "Marquesina";
            this.Button6.UseVisualStyleBackColor = true;
            // 
            // btnEstado
            // 
            this.btnEstado.Location = new System.Drawing.Point(481, 352);
            this.btnEstado.Name = "btnEstado";
            this.btnEstado.Size = new System.Drawing.Size(135, 25);
            this.btnEstado.TabIndex = 41;
            this.btnEstado.Text = "Estado (Ping)";
            this.btnEstado.UseVisualStyleBackColor = true;
            this.btnEstado.Click += new System.EventHandler(this.btnEstado_Click);
            // 
            // btnPedirPeso
            // 
            this.btnPedirPeso.Location = new System.Drawing.Point(481, 383);
            this.btnPedirPeso.Name = "btnPedirPeso";
            this.btnPedirPeso.Size = new System.Drawing.Size(136, 26);
            this.btnPedirPeso.TabIndex = 40;
            this.btnPedirPeso.Text = "Pedir Peso";
            this.btnPedirPeso.UseVisualStyleBackColor = true;
            this.btnPedirPeso.Click += new System.EventHandler(this.btnPedirPeso_Click);
            // 
            // Text_Parametros
            // 
            this.Text_Parametros.Location = new System.Drawing.Point(84, 52);
            this.Text_Parametros.Multiline = true;
            this.Text_Parametros.Name = "Text_Parametros";
            this.Text_Parametros.Size = new System.Drawing.Size(352, 54);
            this.Text_Parametros.TabIndex = 29;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(17, 52);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(60, 13);
            this.Label5.TabIndex = 38;
            this.Label5.Text = "Parametros";
            // 
            // Text_IP
            // 
            this.Text_IP.Location = new System.Drawing.Point(155, 358);
            this.Text_IP.Name = "Text_IP";
            this.Text_IP.Size = new System.Drawing.Size(137, 20);
            this.Text_IP.TabIndex = 37;
            this.Text_IP.Text = "20";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(17, 115);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(54, 13);
            this.Label4.TabIndex = 36;
            this.Label4.Text = "Recibidos";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(17, 22);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(45, 13);
            this.Label3.TabIndex = 35;
            this.Label3.Text = "Funcion";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(15, 343);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(62, 13);
            this.Label2.TabIndex = 34;
            this.Label2.Text = "Puerto Com";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(153, 342);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(73, 13);
            this.Label1.TabIndex = 33;
            this.Label1.Text = "Nº IP Balanza";
            // 
            // Text_Recibir
            // 
            this.Text_Recibir.Location = new System.Drawing.Point(85, 112);
            this.Text_Recibir.Multiline = true;
            this.Text_Recibir.Name = "Text_Recibir";
            this.Text_Recibir.Size = new System.Drawing.Size(351, 113);
            this.Text_Recibir.TabIndex = 32;
            // 
            // Text_Funcion
            // 
            this.Text_Funcion.Location = new System.Drawing.Point(84, 22);
            this.Text_Funcion.Name = "Text_Funcion";
            this.Text_Funcion.Size = new System.Drawing.Size(352, 20);
            this.Text_Funcion.TabIndex = 27;
            // 
            // btnEnviarDatos
            // 
            this.btnEnviarDatos.Enabled = false;
            this.btnEnviarDatos.Location = new System.Drawing.Point(479, 115);
            this.btnEnviarDatos.Name = "btnEnviarDatos";
            this.btnEnviarDatos.Size = new System.Drawing.Size(137, 26);
            this.btnEnviarDatos.TabIndex = 31;
            this.btnEnviarDatos.Text = "Enviar Datos";
            this.btnEnviarDatos.UseVisualStyleBackColor = true;
            this.btnEnviarDatos.Click += new System.EventHandler(this.btnEnviarDatos_Click);
            // 
            // btnCerrarPuerto
            // 
            this.btnCerrarPuerto.Enabled = false;
            this.btnCerrarPuerto.Location = new System.Drawing.Point(298, 415);
            this.btnCerrarPuerto.Name = "btnCerrarPuerto";
            this.btnCerrarPuerto.Size = new System.Drawing.Size(138, 26);
            this.btnCerrarPuerto.TabIndex = 28;
            this.btnCerrarPuerto.Text = "Cerrar Puerto";
            this.btnCerrarPuerto.UseVisualStyleBackColor = true;
            this.btnCerrarPuerto.Click += new System.EventHandler(this.btnCerrarPuerto_Click);
            // 
            // btnAbrirPuerto
            // 
            this.btnAbrirPuerto.Enabled = false;
            this.btnAbrirPuerto.Location = new System.Drawing.Point(299, 384);
            this.btnAbrirPuerto.Name = "btnAbrirPuerto";
            this.btnAbrirPuerto.Size = new System.Drawing.Size(137, 26);
            this.btnAbrirPuerto.TabIndex = 30;
            this.btnAbrirPuerto.Text = "Abrir Puerto";
            this.btnAbrirPuerto.UseVisualStyleBackColor = true;
            this.btnAbrirPuerto.Click += new System.EventHandler(this.btnAbrirPuerto_Click);
            // 
            // cbPuertos
            // 
            this.cbPuertos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPuertos.FormattingEnabled = true;
            this.cbPuertos.Location = new System.Drawing.Point(11, 359);
            this.cbPuertos.Name = "cbPuertos";
            this.cbPuertos.Size = new System.Drawing.Size(138, 21);
            this.cbPuertos.TabIndex = 26;
            this.cbPuertos.SelectedIndexChanged += new System.EventHandler(this.cbPuertos_SelectedIndexChanged);
            // 
            // PictureBox1
            // 
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(481, 19);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(137, 46);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox1.TabIndex = 39;
            this.PictureBox1.TabStop = false;
            // 
            // btnBuscarPuertos
            // 
            this.btnBuscarPuertos.Location = new System.Drawing.Point(299, 352);
            this.btnBuscarPuertos.Name = "btnBuscarPuertos";
            this.btnBuscarPuertos.Size = new System.Drawing.Size(137, 26);
            this.btnBuscarPuertos.TabIndex = 49;
            this.btnBuscarPuertos.Text = "Buscar puertos";
            this.btnBuscarPuertos.UseVisualStyleBackColor = true;
            this.btnBuscarPuertos.Click += new System.EventHandler(this.btnBuscarPuertos_Click);
            // 
            // SerialPort1
            // 
            this.SerialPort1.BaudRate = 115200;
            this.SerialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.SerialPort1_DataReceived);
            // 
            // formSystelCuora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 448);
            this.Controls.Add(this.btnBuscarPuertos);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.TextBox_trama_enviada);
            this.Controls.Add(this.btnApagarBalanza);
            this.Controls.Add(this.Button9);
            this.Controls.Add(this.Button8);
            this.Controls.Add(this.Button7);
            this.Controls.Add(this.Button6);
            this.Controls.Add(this.btnEstado);
            this.Controls.Add(this.btnPedirPeso);
            this.Controls.Add(this.PictureBox1);
            this.Controls.Add(this.Text_Parametros);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Text_IP);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Text_Recibir);
            this.Controls.Add(this.Text_Funcion);
            this.Controls.Add(this.btnEnviarDatos);
            this.Controls.Add(this.btnCerrarPuerto);
            this.Controls.Add(this.btnAbrirPuerto);
            this.Controls.Add(this.cbPuertos);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formSystelCuora";
            this.Text = "Systel Cuora";
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.TextBox TextBox_trama_enviada;
        internal System.Windows.Forms.Button btnApagarBalanza;
        internal System.Windows.Forms.Button Button9;
        internal System.Windows.Forms.Button Button8;
        internal System.Windows.Forms.Button Button7;
        internal System.Windows.Forms.Button Button6;
        internal System.Windows.Forms.Button btnEstado;
        internal System.Windows.Forms.Button btnPedirPeso;
        internal System.Windows.Forms.PictureBox PictureBox1;
        internal System.Windows.Forms.TextBox Text_Parametros;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.TextBox Text_IP;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox Text_Recibir;
        internal System.Windows.Forms.TextBox Text_Funcion;
        internal System.Windows.Forms.Button btnEnviarDatos;
        internal System.Windows.Forms.Button btnCerrarPuerto;
        internal System.Windows.Forms.Button btnAbrirPuerto;
        internal System.Windows.Forms.ComboBox cbPuertos;
        private System.Windows.Forms.Button btnBuscarPuertos;
        internal System.IO.Ports.SerialPort SerialPort1;
    }
}
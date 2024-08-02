namespace CapaPresentacion
{
    partial class formActualizarPrecios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formActualizarPrecios));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblLineal = new System.Windows.Forms.Label();
            this.tbLineal = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbPorcentual = new System.Windows.Forms.RadioButton();
            this.lblPorcentual = new System.Windows.Forms.Label();
            this.tbPorcentual = new System.Windows.Forms.TextBox();
            this.rbLineal = new System.Windows.Forms.RadioButton();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbDesdeArticulo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbHastaArticulo = new System.Windows.Forms.TextBox();
            this.cbActualizarTodos = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CapaPresentacion.Properties.Resources.box1;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 71);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // lblLineal
            // 
            this.lblLineal.AutoSize = true;
            this.lblLineal.Location = new System.Drawing.Point(40, 65);
            this.lblLineal.Name = "lblLineal";
            this.lblLineal.Size = new System.Drawing.Size(100, 13);
            this.lblLineal.TabIndex = 3;
            this.lblLineal.Text = "Actualizacion lineal:";
            this.lblLineal.Click += new System.EventHandler(this.lblLineal_Click);
            this.lblLineal.DoubleClick += new System.EventHandler(this.lblLineal_DoubleClick);
            // 
            // tbLineal
            // 
            this.tbLineal.Location = new System.Drawing.Point(189, 62);
            this.tbLineal.Name = "tbLineal";
            this.tbLineal.Size = new System.Drawing.Size(100, 20);
            this.tbLineal.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbPorcentual);
            this.groupBox1.Controls.Add(this.lblPorcentual);
            this.groupBox1.Controls.Add(this.tbPorcentual);
            this.groupBox1.Controls.Add(this.rbLineal);
            this.groupBox1.Controls.Add(this.lblLineal);
            this.groupBox1.Controls.Add(this.tbLineal);
            this.groupBox1.Location = new System.Drawing.Point(15, 153);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(353, 106);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipo de actualizacion";
            // 
            // rbPorcentual
            // 
            this.rbPorcentual.AutoSize = true;
            this.rbPorcentual.Location = new System.Drawing.Point(20, 26);
            this.rbPorcentual.Name = "rbPorcentual";
            this.rbPorcentual.Size = new System.Drawing.Size(14, 13);
            this.rbPorcentual.TabIndex = 9;
            this.rbPorcentual.TabStop = true;
            this.rbPorcentual.UseVisualStyleBackColor = true;
            this.rbPorcentual.Click += new System.EventHandler(this.rbPorcentual_Click);
            // 
            // lblPorcentual
            // 
            this.lblPorcentual.AutoSize = true;
            this.lblPorcentual.Location = new System.Drawing.Point(40, 28);
            this.lblPorcentual.Name = "lblPorcentual";
            this.lblPorcentual.Size = new System.Drawing.Size(143, 13);
            this.lblPorcentual.TabIndex = 8;
            this.lblPorcentual.Text = "Actualizacion porcentual (%):";
            this.lblPorcentual.Click += new System.EventHandler(this.lblPorcentual_Click);
            this.lblPorcentual.DoubleClick += new System.EventHandler(this.lblPorcentual_DoubleClick);
            // 
            // tbPorcentual
            // 
            this.tbPorcentual.Location = new System.Drawing.Point(189, 25);
            this.tbPorcentual.Name = "tbPorcentual";
            this.tbPorcentual.Size = new System.Drawing.Size(100, 20);
            this.tbPorcentual.TabIndex = 10;
            // 
            // rbLineal
            // 
            this.rbLineal.AutoSize = true;
            this.rbLineal.Location = new System.Drawing.Point(20, 65);
            this.rbLineal.Name = "rbLineal";
            this.rbLineal.Size = new System.Drawing.Size(14, 13);
            this.rbLineal.TabIndex = 4;
            this.rbLineal.TabStop = true;
            this.rbLineal.UseVisualStyleBackColor = true;
            this.rbLineal.Click += new System.EventHandler(this.rbLineal_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(403, 268);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 8;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(484, 268);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 9;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(154, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Desde articulo : ";
            // 
            // tbDesdeArticulo
            // 
            this.tbDesdeArticulo.Location = new System.Drawing.Point(244, 93);
            this.tbDesdeArticulo.Name = "tbDesdeArticulo";
            this.tbDesdeArticulo.Size = new System.Drawing.Size(100, 20);
            this.tbDesdeArticulo.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(357, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Hasta articulo : ";
            // 
            // tbHastaArticulo
            // 
            this.tbHastaArticulo.Location = new System.Drawing.Point(444, 92);
            this.tbHastaArticulo.Name = "tbHastaArticulo";
            this.tbHastaArticulo.Size = new System.Drawing.Size(100, 20);
            this.tbHastaArticulo.TabIndex = 3;
            // 
            // cbActualizarTodos
            // 
            this.cbActualizarTodos.AutoSize = true;
            this.cbActualizarTodos.Location = new System.Drawing.Point(12, 95);
            this.cbActualizarTodos.Name = "cbActualizarTodos";
            this.cbActualizarTodos.Size = new System.Drawing.Size(101, 17);
            this.cbActualizarTodos.TabIndex = 1;
            this.cbActualizarTodos.Text = "Actualizar todos";
            this.cbActualizarTodos.UseVisualStyleBackColor = true;
            this.cbActualizarTodos.CheckedChanged += new System.EventHandler(this.cbActualizarTodos_CheckedChanged);
            // 
            // formActualizarPrecios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 315);
            this.Controls.Add(this.cbActualizarTodos);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbHastaArticulo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbDesdeArticulo);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formActualizarPrecios";
            this.Text = "Actualizacion de precios";
            this.Load += new System.EventHandler(this.formActualizarPrecios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblLineal;
        private System.Windows.Forms.TextBox tbLineal;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.RadioButton rbLineal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbDesdeArticulo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbHastaArticulo;
        private System.Windows.Forms.CheckBox cbActualizarTodos;
        private System.Windows.Forms.RadioButton rbPorcentual;
        private System.Windows.Forms.Label lblPorcentual;
        private System.Windows.Forms.TextBox tbPorcentual;
    }
}
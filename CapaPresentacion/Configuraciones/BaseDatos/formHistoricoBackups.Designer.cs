namespace CapaPresentacion.Configuraciones.BaseDatos
{
    partial class formHistoricoBackups
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
            this.dataListadoBackups = new System.Windows.Forms.DataGridView();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnNuevoBackup = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataListadoBackups)).BeginInit();
            this.SuspendLayout();
            // 
            // dataListadoBackups
            // 
            this.dataListadoBackups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataListadoBackups.Location = new System.Drawing.Point(12, 120);
            this.dataListadoBackups.Name = "dataListadoBackups";
            this.dataListadoBackups.Size = new System.Drawing.Size(776, 296);
            this.dataListadoBackups.TabIndex = 0;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(142, 85);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Seleccione una fecha : ";
            // 
            // btnNuevoBackup
            // 
            this.btnNuevoBackup.Location = new System.Drawing.Point(674, 81);
            this.btnNuevoBackup.Name = "btnNuevoBackup";
            this.btnNuevoBackup.Size = new System.Drawing.Size(114, 23);
            this.btnNuevoBackup.TabIndex = 3;
            this.btnNuevoBackup.Text = "Nuevo Backup";
            this.btnNuevoBackup.UseVisualStyleBackColor = true;
            this.btnNuevoBackup.Click += new System.EventHandler(this.btnNuevoBackup_Click);
            // 
            // formHistoricoBackups
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnNuevoBackup);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.dataListadoBackups);
            this.Name = "formHistoricoBackups";
            this.Text = "Historico de backups";
            ((System.ComponentModel.ISupportInitialize)(this.dataListadoBackups)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataListadoBackups;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnNuevoBackup;
    }
}
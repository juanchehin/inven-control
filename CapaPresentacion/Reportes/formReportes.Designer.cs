namespace CapaPresentacion.Reportes
{
    partial class formReportes
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formReportes));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.top_ingresos = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_ingresos_mes = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbl_egresos_mes = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.top_egresos = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ct_torta_racks = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnPrinter = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.top_ingresos)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.top_egresos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ct_torta_racks)).BeginInit();
            this.SuspendLayout();
            // 
            // top_ingresos
            // 
            chartArea4.Name = "ChartArea1";
            this.top_ingresos.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.top_ingresos.Legends.Add(legend4);
            this.top_ingresos.Location = new System.Drawing.Point(36, 252);
            this.top_ingresos.Name = "top_ingresos";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.top_ingresos.Series.Add(series4);
            this.top_ingresos.Size = new System.Drawing.Size(433, 283);
            this.top_ingresos.TabIndex = 0;
            this.top_ingresos.Text = "chart1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Consolas", 35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(337, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(466, 55);
            this.label3.TabIndex = 95;
            this.label3.Text = "Reportes/Informes";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_ingresos_mes);
            this.groupBox1.Location = new System.Drawing.Point(36, 78);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(238, 114);
            this.groupBox1.TabIndex = 96;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ingresos Mes actual";
            // 
            // lbl_ingresos_mes
            // 
            this.lbl_ingresos_mes.AutoSize = true;
            this.lbl_ingresos_mes.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ingresos_mes.Location = new System.Drawing.Point(6, 61);
            this.lbl_ingresos_mes.Name = "lbl_ingresos_mes";
            this.lbl_ingresos_mes.Size = new System.Drawing.Size(115, 24);
            this.lbl_ingresos_mes.TabIndex = 2;
            this.lbl_ingresos_mes.Text = "0 Unidades";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbl_egresos_mes);
            this.groupBox2.Location = new System.Drawing.Point(310, 78);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(220, 114);
            this.groupBox2.TabIndex = 97;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Egresos Mes actual";
            // 
            // lbl_egresos_mes
            // 
            this.lbl_egresos_mes.AutoSize = true;
            this.lbl_egresos_mes.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_egresos_mes.Location = new System.Drawing.Point(6, 61);
            this.lbl_egresos_mes.Name = "lbl_egresos_mes";
            this.lbl_egresos_mes.Size = new System.Drawing.Size(115, 24);
            this.lbl_egresos_mes.TabIndex = 1;
            this.lbl_egresos_mes.Text = "0 Unidades";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 98;
            this.pictureBox1.TabStop = false;
            // 
            // top_egresos
            // 
            chartArea5.Name = "ChartArea1";
            this.top_egresos.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.top_egresos.Legends.Add(legend5);
            this.top_egresos.Location = new System.Drawing.Point(495, 252);
            this.top_egresos.Name = "top_egresos";
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            this.top_egresos.Series.Add(series5);
            this.top_egresos.Size = new System.Drawing.Size(433, 283);
            this.top_egresos.TabIndex = 99;
            this.top_egresos.Text = "chart2";
            // 
            // ct_torta_racks
            // 
            chartArea6.Name = "ChartArea1";
            this.ct_torta_racks.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.ct_torta_racks.Legends.Add(legend6);
            this.ct_torta_racks.Location = new System.Drawing.Point(649, 78);
            this.ct_torta_racks.Name = "ct_torta_racks";
            this.ct_torta_racks.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SemiTransparent;
            series6.ChartArea = "ChartArea1";
            series6.Legend = "Legend1";
            series6.Name = "Series1";
            this.ct_torta_racks.Series.Add(series6);
            this.ct_torta_racks.Size = new System.Drawing.Size(267, 153);
            this.ct_torta_racks.TabIndex = 100;
            this.ct_torta_racks.Text = "Racks utilizados";
            // 
            // btnPrinter
            // 
            this.btnPrinter.Image = ((System.Drawing.Image)(resources.GetObject("btnPrinter.Image")));
            this.btnPrinter.Location = new System.Drawing.Point(995, 9);
            this.btnPrinter.Name = "btnPrinter";
            this.btnPrinter.Size = new System.Drawing.Size(90, 51);
            this.btnPrinter.TabIndex = 101;
            this.btnPrinter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrinter.UseVisualStyleBackColor = true;
            this.btnPrinter.Click += new System.EventHandler(this.btnPrinter_Click_1);
            // 
            // formReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1097, 615);
            this.Controls.Add(this.btnPrinter);
            this.Controls.Add(this.ct_torta_racks);
            this.Controls.Add(this.top_egresos);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.top_ingresos);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formReportes";
            this.Text = "Reportes";
            ((System.ComponentModel.ISupportInitialize)(this.top_ingresos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.top_egresos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ct_torta_racks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart top_ingresos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbl_ingresos_mes;
        private System.Windows.Forms.Label lbl_egresos_mes;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataVisualization.Charting.Chart top_egresos;
        private System.Windows.Forms.DataVisualization.Charting.Chart ct_torta_racks;
        private System.Windows.Forms.Button btnPrinter;
    }
}
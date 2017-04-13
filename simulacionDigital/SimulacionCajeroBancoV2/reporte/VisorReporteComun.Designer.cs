namespace SimulacionCajeroBancoV2.reporte
{
    partial class VisorReporteComun
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
            this.Reporte = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // Reporte
            // 
            this.Reporte.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Reporte.Location = new System.Drawing.Point(0, 0);
            this.Reporte.Name = "Reporte";
            this.Reporte.Size = new System.Drawing.Size(928, 528);
            this.Reporte.TabIndex = 0;
            this.Reporte.Load += new System.EventHandler(this.Reporte_Load);
            // 
            // VisorReporteComun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 528);
            this.Controls.Add(this.Reporte);
            this.Name = "VisorReporteComun";
            this.Text = "VisorReporteComun";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.VisorReporteComun_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer Reporte;
    }
}
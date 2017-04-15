namespace SimulacionCajeroBancoV2.views
{
    partial class visor_problemas
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.faseColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tiempoColum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tiempo2Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProblemaColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Respuestacolumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToResizeColumns = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.faseColumn,
            this.tiempoColum,
            this.Column1,
            this.tiempo2Column,
            this.ProblemaColumn,
            this.Respuestacolumn});
            this.dataGridView2.Location = new System.Drawing.Point(12, 12);
            this.dataGridView2.MultiSelect = false;
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(1118, 515);
            this.dataGridView2.TabIndex = 14;
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.DarkBlue;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn1.FillWeight = 30F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Cliente";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.DarkBlue;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn2.FillWeight = 60F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Operacion";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // faseColumn
            // 
            this.faseColumn.FillWeight = 70F;
            this.faseColumn.HeaderText = "Fase";
            this.faseColumn.Name = "faseColumn";
            this.faseColumn.ReadOnly = true;
            // 
            // tiempoColum
            // 
            this.tiempoColum.FillWeight = 46F;
            this.tiempoColum.HeaderText = "T. anterior";
            this.tiempoColum.Name = "tiempoColum";
            this.tiempoColum.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.FillWeight = 40F;
            this.Column1.HeaderText = "T. problema";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // tiempo2Column
            // 
            this.tiempo2Column.FillWeight = 46F;
            this.tiempo2Column.HeaderText = "T. Despues";
            this.tiempo2Column.Name = "tiempo2Column";
            this.tiempo2Column.ReadOnly = true;
            // 
            // ProblemaColumn
            // 
            this.ProblemaColumn.FillWeight = 70F;
            this.ProblemaColumn.HeaderText = "Problema";
            this.ProblemaColumn.Name = "ProblemaColumn";
            this.ProblemaColumn.ReadOnly = true;
            // 
            // Respuestacolumn
            // 
            this.Respuestacolumn.FillWeight = 150F;
            this.Respuestacolumn.HeaderText = "Respuesta";
            this.Respuestacolumn.Name = "Respuestacolumn";
            this.Respuestacolumn.ReadOnly = true;
            // 
            // visor_problemas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1142, 539);
            this.Controls.Add(this.dataGridView2);
            this.Name = "visor_problemas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "visor_problemas";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn faseColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tiempoColum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn tiempo2Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProblemaColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Respuestacolumn;
    }
}
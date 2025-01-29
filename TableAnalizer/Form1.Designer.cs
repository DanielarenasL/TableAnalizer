namespace TableAnalizer
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.SelectDocument = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Limpiar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.chartPanel1 = new System.Windows.Forms.Panel();
            this.chartPanel2 = new System.Windows.Forms.Panel();
            this.chartPanel3 = new System.Windows.Forms.Panel();
            this.chartPanel4 = new System.Windows.Forms.Panel();
            this.chartPanel7 = new System.Windows.Forms.Panel();
            this.chartPanel8 = new System.Windows.Forms.Panel();
            this.chartPanel9 = new System.Windows.Forms.Panel();
            this.chartPanel5 = new System.Windows.Forms.Panel();
            this.chartPanel6 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // SelectDocument
            // 
            this.SelectDocument.AutoSize = true;
            this.SelectDocument.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.SelectDocument.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SelectDocument.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectDocument.Location = new System.Drawing.Point(464, 627);
            this.SelectDocument.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SelectDocument.Name = "SelectDocument";
            this.SelectDocument.Size = new System.Drawing.Size(337, 159);
            this.SelectDocument.TabIndex = 0;
            this.SelectDocument.Text = "Seleccionar el documento";
            this.SelectDocument.UseMnemonic = false;
            this.SelectDocument.UseVisualStyleBackColor = false;
            this.SelectDocument.Click += new System.EventHandler(this.SelectDocument_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(-3, -8);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(318, 202);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(-3, -8);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(318, 202);
            this.dataGridView2.TabIndex = 1;
            this.dataGridView2.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(36, 599);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 39);
            this.label1.TabIndex = 5;
            this.label1.Text = "Total";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.SystemColors.Control;
            this.checkBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(43, 522);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(348, 35);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Mostrar datos importantes";
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(39, 560);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 20);
            this.label3.TabIndex = 8;
            // 
            // Limpiar
            // 
            this.Limpiar.AutoSize = true;
            this.Limpiar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Limpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Limpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Limpiar.Location = new System.Drawing.Point(827, 627);
            this.Limpiar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Limpiar.Name = "Limpiar";
            this.Limpiar.Size = new System.Drawing.Size(337, 159);
            this.Limpiar.TabIndex = 9;
            this.Limpiar.Text = "Seleccionar otro documento";
            this.Limpiar.UseMnemonic = false;
            this.Limpiar.UseVisualStyleBackColor = false;
            this.Limpiar.Click += new System.EventHandler(this.Limpiar_Click);
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(875, 533);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(247, 88);
            this.button1.TabIndex = 10;
            this.button1.Text = "Seleccionar el documento";
            this.button1.UseMnemonic = false;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chartPanel1
            // 
            this.chartPanel1.Location = new System.Drawing.Point(22, 238);
            this.chartPanel1.Name = "chartPanel1";
            this.chartPanel1.Size = new System.Drawing.Size(313, 232);
            this.chartPanel1.TabIndex = 11;
            // 
            // chartPanel2
            // 
            this.chartPanel2.Location = new System.Drawing.Point(581, 12);
            this.chartPanel2.Name = "chartPanel2";
            this.chartPanel2.Size = new System.Drawing.Size(200, 100);
            this.chartPanel2.TabIndex = 12;
            // 
            // chartPanel3
            // 
            this.chartPanel3.Location = new System.Drawing.Point(827, 12);
            this.chartPanel3.Name = "chartPanel3";
            this.chartPanel3.Size = new System.Drawing.Size(200, 100);
            this.chartPanel3.TabIndex = 12;
            // 
            // chartPanel4
            // 
            this.chartPanel4.Location = new System.Drawing.Point(341, 144);
            this.chartPanel4.Name = "chartPanel4";
            this.chartPanel4.Size = new System.Drawing.Size(200, 100);
            this.chartPanel4.TabIndex = 12;
            // 
            // chartPanel7
            // 
            this.chartPanel7.Location = new System.Drawing.Point(341, 279);
            this.chartPanel7.Name = "chartPanel7";
            this.chartPanel7.Size = new System.Drawing.Size(200, 100);
            this.chartPanel7.TabIndex = 15;
            // 
            // chartPanel8
            // 
            this.chartPanel8.Location = new System.Drawing.Point(581, 279);
            this.chartPanel8.Name = "chartPanel8";
            this.chartPanel8.Size = new System.Drawing.Size(200, 100);
            this.chartPanel8.TabIndex = 16;
            // 
            // chartPanel9
            // 
            this.chartPanel9.Location = new System.Drawing.Point(827, 279);
            this.chartPanel9.Name = "chartPanel9";
            this.chartPanel9.Size = new System.Drawing.Size(200, 100);
            this.chartPanel9.TabIndex = 12;
            // 
            // chartPanel5
            // 
            this.chartPanel5.Location = new System.Drawing.Point(581, 144);
            this.chartPanel5.Name = "chartPanel5";
            this.chartPanel5.Size = new System.Drawing.Size(200, 100);
            this.chartPanel5.TabIndex = 17;
            // 
            // chartPanel6
            // 
            this.chartPanel6.Location = new System.Drawing.Point(827, 144);
            this.chartPanel6.Name = "chartPanel6";
            this.chartPanel6.Size = new System.Drawing.Size(200, 100);
            this.chartPanel6.TabIndex = 17;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 875);
            this.Controls.Add(this.chartPanel6);
            this.Controls.Add(this.chartPanel5);
            this.Controls.Add(this.chartPanel9);
            this.Controls.Add(this.chartPanel8);
            this.Controls.Add(this.chartPanel7);
            this.Controls.Add(this.chartPanel4);
            this.Controls.Add(this.chartPanel3);
            this.Controls.Add(this.chartPanel2);
            this.Controls.Add(this.chartPanel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Limpiar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.SelectDocument);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SelectDocument;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Limpiar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel chartPanel1;
        private System.Windows.Forms.Panel chartPanel2;
        private System.Windows.Forms.Panel chartPanel3;
        private System.Windows.Forms.Panel chartPanel4;
        private System.Windows.Forms.Panel chartPanel7;
        private System.Windows.Forms.Panel chartPanel8;
        private System.Windows.Forms.Panel chartPanel9;
        private System.Windows.Forms.Panel chartPanel5;
        private System.Windows.Forms.Panel chartPanel6;
    }
}


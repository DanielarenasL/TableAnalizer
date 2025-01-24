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
            this.SuspendLayout();
            // 
            // SelectDocument
            // 
            this.SelectDocument.AutoSize = true;
            this.SelectDocument.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.SelectDocument.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SelectDocument.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectDocument.Location = new System.Drawing.Point(163, 107);
            this.SelectDocument.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SelectDocument.Name = "SelectDocument";
            this.SelectDocument.Size = new System.Drawing.Size(386, 202);
            this.SelectDocument.TabIndex = 0;
            this.SelectDocument.Text = "Seleccione el documento";
            this.SelectDocument.UseMnemonic = false;
            this.SelectDocument.UseVisualStyleBackColor = false;
            this.SelectDocument.Click += new System.EventHandler(this.SelectDocument_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 419);
            this.Controls.Add(this.SelectDocument);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SelectDocument;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}


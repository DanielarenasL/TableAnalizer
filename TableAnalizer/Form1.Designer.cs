namespace TableAnalizer
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            this.SelectDocument = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Limpiar = new System.Windows.Forms.Button();
            this.chartPanel1 = new System.Windows.Forms.Panel();
            this.chartPanel2 = new System.Windows.Forms.Panel();
            this.chartPanel3 = new System.Windows.Forms.Panel();
            this.chartPanel4 = new System.Windows.Forms.Panel();
            this.chartPanel7 = new System.Windows.Forms.Panel();
            this.chartPanel8 = new System.Windows.Forms.Panel();
            this.chartPanel5 = new System.Windows.Forms.Panel();
            this.chartPanel6 = new System.Windows.Forms.Panel();
            this.openDataGridViewButton = new System.Windows.Forms.Button();
            this.chartPanel9 = new System.Windows.Forms.Panel();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Next = new System.Windows.Forms.Button();
            this.Back = new System.Windows.Forms.Button();
            this.chartPanel10 = new System.Windows.Forms.Panel();
            this.chartPanel11 = new System.Windows.Forms.Panel();
            this.chartPanel12 = new System.Windows.Forms.Panel();
            this.chartPanel13 = new System.Windows.Forms.Panel();
            this.chartPanel14 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // SelectDocument
            // 
            this.SelectDocument.AutoSize = true;
            this.SelectDocument.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.SelectDocument.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SelectDocument.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectDocument.Location = new System.Drawing.Point(840, 537);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(833, 455);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 39);
            this.label1.TabIndex = 5;
            this.label1.Text = "Total";
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
            this.Limpiar.Location = new System.Drawing.Point(840, 717);
            this.Limpiar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Limpiar.Name = "Limpiar";
            this.Limpiar.Size = new System.Drawing.Size(337, 159);
            this.Limpiar.TabIndex = 9;
            this.Limpiar.Text = "Seleccionar otro documento";
            this.Limpiar.UseMnemonic = false;
            this.Limpiar.UseVisualStyleBackColor = false;
            this.Limpiar.Click += new System.EventHandler(this.Limpiar_Click);
            // 
            // chartPanel1
            // 
            this.chartPanel1.Location = new System.Drawing.Point(380, 55);
            this.chartPanel1.Name = "chartPanel1";
            this.chartPanel1.Size = new System.Drawing.Size(117, 52);
            this.chartPanel1.TabIndex = 11;
            // 
            // chartPanel2
            // 
            this.chartPanel2.Location = new System.Drawing.Point(529, 55);
            this.chartPanel2.Name = "chartPanel2";
            this.chartPanel2.Size = new System.Drawing.Size(163, 65);
            this.chartPanel2.TabIndex = 12;
            // 
            // chartPanel3
            // 
            this.chartPanel3.Location = new System.Drawing.Point(631, 248);
            this.chartPanel3.Name = "chartPanel3";
            this.chartPanel3.Size = new System.Drawing.Size(121, 89);
            this.chartPanel3.TabIndex = 12;
            // 
            // chartPanel4
            // 
            this.chartPanel4.Location = new System.Drawing.Point(12, 225);
            this.chartPanel4.Name = "chartPanel4";
            this.chartPanel4.Size = new System.Drawing.Size(128, 101);
            this.chartPanel4.TabIndex = 12;
            // 
            // chartPanel7
            // 
            this.chartPanel7.Location = new System.Drawing.Point(190, 225);
            this.chartPanel7.Name = "chartPanel7";
            this.chartPanel7.Size = new System.Drawing.Size(142, 73);
            this.chartPanel7.TabIndex = 15;
            // 
            // chartPanel8
            // 
            this.chartPanel8.Location = new System.Drawing.Point(401, 165);
            this.chartPanel8.Name = "chartPanel8";
            this.chartPanel8.Size = new System.Drawing.Size(210, 84);
            this.chartPanel8.TabIndex = 16;
            // 
            // chartPanel5
            // 
            this.chartPanel5.Location = new System.Drawing.Point(499, 465);
            this.chartPanel5.Name = "chartPanel5";
            this.chartPanel5.Size = new System.Drawing.Size(159, 135);
            this.chartPanel5.TabIndex = 17;
            // 
            // chartPanel6
            // 
            this.chartPanel6.Location = new System.Drawing.Point(833, 55);
            this.chartPanel6.Name = "chartPanel6";
            this.chartPanel6.Size = new System.Drawing.Size(53, 104);
            this.chartPanel6.TabIndex = 17;
            // 
            // openDataGridViewButton
            // 
            this.openDataGridViewButton.Location = new System.Drawing.Point(944, 888);
            this.openDataGridViewButton.Name = "openDataGridViewButton";
            this.openDataGridViewButton.Size = new System.Drawing.Size(117, 65);
            this.openDataGridViewButton.TabIndex = 19;
            this.openDataGridViewButton.Text = "button2";
            this.openDataGridViewButton.UseVisualStyleBackColor = true;
            this.openDataGridViewButton.Click += new System.EventHandler(this.openDataGridViewButton_Click);
            // 
            // chartPanel9
            // 
            this.chartPanel9.Location = new System.Drawing.Point(348, 409);
            this.chartPanel9.Name = "chartPanel9";
            this.chartPanel9.Size = new System.Drawing.Size(48, 113);
            this.chartPanel9.TabIndex = 12;
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
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(-3, -8);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(318, 202);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Next
            // 
            this.Next.Location = new System.Drawing.Point(545, 886);
            this.Next.Name = "Next";
            this.Next.Size = new System.Drawing.Size(168, 67);
            this.Next.TabIndex = 20;
            this.Next.Text = "Siguiente";
            this.Next.UseVisualStyleBackColor = true;
            this.Next.Click += new System.EventHandler(this.Next_Click);
            // 
            // Back
            // 
            this.Back.Location = new System.Drawing.Point(309, 886);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(168, 67);
            this.Back.TabIndex = 21;
            this.Back.Text = "Anterior";
            this.Back.UseVisualStyleBackColor = true;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // chartPanel10
            // 
            this.chartPanel10.Location = new System.Drawing.Point(900, 315);
            this.chartPanel10.Name = "chartPanel10";
            this.chartPanel10.Size = new System.Drawing.Size(200, 100);
            this.chartPanel10.TabIndex = 18;
            // 
            // chartPanel11
            // 
            this.chartPanel11.Location = new System.Drawing.Point(601, 776);
            this.chartPanel11.Name = "chartPanel11";
            this.chartPanel11.Size = new System.Drawing.Size(200, 100);
            this.chartPanel11.TabIndex = 18;
            // 
            // chartPanel12
            // 
            this.chartPanel12.Location = new System.Drawing.Point(68, 697);
            this.chartPanel12.Name = "chartPanel12";
            this.chartPanel12.Size = new System.Drawing.Size(200, 100);
            this.chartPanel12.TabIndex = 18;
            // 
            // chartPanel13
            // 
            this.chartPanel13.Location = new System.Drawing.Point(1167, 422);
            this.chartPanel13.Name = "chartPanel13";
            this.chartPanel13.Size = new System.Drawing.Size(200, 100);
            this.chartPanel13.TabIndex = 18;
            // 
            // chartPanel14
            // 
            this.chartPanel14.Location = new System.Drawing.Point(112, 970);
            this.chartPanel14.Name = "chartPanel14";
            this.chartPanel14.Size = new System.Drawing.Size(134, 100);
            this.chartPanel14.TabIndex = 18;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 974);
            this.Controls.Add(this.chartPanel11);
            this.Controls.Add(this.chartPanel10);
            this.Controls.Add(this.chartPanel9);
            this.Controls.Add(this.chartPanel8);
            this.Controls.Add(this.chartPanel1);
            this.Controls.Add(this.chartPanel6);
            this.Controls.Add(this.chartPanel12);
            this.Controls.Add(this.chartPanel13);
            this.Controls.Add(this.chartPanel14);
            this.Controls.Add(this.Back);
            this.Controls.Add(this.Next);
            this.Controls.Add(this.chartPanel3);
            this.Controls.Add(this.chartPanel2);
            this.Controls.Add(this.openDataGridViewButton);
            this.Controls.Add(this.chartPanel5);
            this.Controls.Add(this.chartPanel7);
            this.Controls.Add(this.chartPanel4);
            this.Controls.Add(this.Limpiar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.SelectDocument);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SelectDocument;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Limpiar;
        private System.Windows.Forms.Panel chartPanel1;
        private System.Windows.Forms.Panel chartPanel2;
        private System.Windows.Forms.Panel chartPanel3;
        private System.Windows.Forms.Panel chartPanel4;
        private System.Windows.Forms.Panel chartPanel7;
        private System.Windows.Forms.Panel chartPanel8;
        private System.Windows.Forms.Panel chartPanel5;
        private System.Windows.Forms.Panel chartPanel6;
        private System.Windows.Forms.Button openDataGridViewButton;
        private System.Windows.Forms.Panel chartPanel9;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button Next;
        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.Panel chartPanel13;
        private System.Windows.Forms.Panel chartPanel12;
        private System.Windows.Forms.Panel chartPanel11;
        private System.Windows.Forms.Panel chartPanel10;
        private System.Windows.Forms.Panel chartPanel14;
    }
}
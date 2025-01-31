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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.SelectDocument = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
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
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.chartPanel15 = new System.Windows.Forms.Panel();
            this.chartPanel16 = new System.Windows.Forms.Panel();
            this.chartPanel17 = new System.Windows.Forms.Panel();
            this.FromDate = new System.Windows.Forms.DateTimePicker();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.ToDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // SelectDocument
            // 
            this.SelectDocument.AutoSize = true;
            this.SelectDocument.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.SelectDocument.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SelectDocument.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectDocument.Location = new System.Drawing.Point(417, 354);
            this.SelectDocument.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SelectDocument.Name = "SelectDocument";
            this.SelectDocument.Size = new System.Drawing.Size(490, 276);
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
            // Limpiar
            // 
            this.Limpiar.AutoSize = true;
            this.Limpiar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Limpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Limpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Limpiar.Location = new System.Drawing.Point(870, 697);
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
            this.chartPanel1.Location = new System.Drawing.Point(330, 12);
            this.chartPanel1.Name = "chartPanel1";
            this.chartPanel1.Size = new System.Drawing.Size(117, 52);
            this.chartPanel1.TabIndex = 11;
            // 
            // chartPanel2
            // 
            this.chartPanel2.Location = new System.Drawing.Point(12, 267);
            this.chartPanel2.Name = "chartPanel2";
            this.chartPanel2.Size = new System.Drawing.Size(79, 67);
            this.chartPanel2.TabIndex = 12;
            // 
            // chartPanel3
            // 
            this.chartPanel3.Location = new System.Drawing.Point(483, 248);
            this.chartPanel3.Name = "chartPanel3";
            this.chartPanel3.Size = new System.Drawing.Size(56, 34);
            this.chartPanel3.TabIndex = 12;
            // 
            // chartPanel4
            // 
            this.chartPanel4.Location = new System.Drawing.Point(91, 200);
            this.chartPanel4.Name = "chartPanel4";
            this.chartPanel4.Size = new System.Drawing.Size(72, 61);
            this.chartPanel4.TabIndex = 12;
            // 
            // chartPanel7
            // 
            this.chartPanel7.Location = new System.Drawing.Point(330, 101);
            this.chartPanel7.Name = "chartPanel7";
            this.chartPanel7.Size = new System.Drawing.Size(142, 73);
            this.chartPanel7.TabIndex = 15;
            // 
            // chartPanel8
            // 
            this.chartPanel8.Location = new System.Drawing.Point(530, 172);
            this.chartPanel8.Name = "chartPanel8";
            this.chartPanel8.Size = new System.Drawing.Size(101, 43);
            this.chartPanel8.TabIndex = 16;
            // 
            // chartPanel5
            // 
            this.chartPanel5.Location = new System.Drawing.Point(483, 21);
            this.chartPanel5.Name = "chartPanel5";
            this.chartPanel5.Size = new System.Drawing.Size(110, 59);
            this.chartPanel5.TabIndex = 17;
            // 
            // chartPanel6
            // 
            this.chartPanel6.Location = new System.Drawing.Point(653, 12);
            this.chartPanel6.Name = "chartPanel6";
            this.chartPanel6.Size = new System.Drawing.Size(99, 22);
            this.chartPanel6.TabIndex = 17;
            // 
            // openDataGridViewButton
            // 
            this.openDataGridViewButton.BackColor = System.Drawing.Color.Plum;
            this.openDataGridViewButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openDataGridViewButton.Location = new System.Drawing.Point(870, 519);
            this.openDataGridViewButton.Name = "openDataGridViewButton";
            this.openDataGridViewButton.Size = new System.Drawing.Size(337, 159);
            this.openDataGridViewButton.TabIndex = 19;
            this.openDataGridViewButton.Text = "Ver tablas";
            this.openDataGridViewButton.UseVisualStyleBackColor = false;
            this.openDataGridViewButton.Click += new System.EventHandler(this.openDataGridViewButton_Click);
            // 
            // chartPanel9
            // 
            this.chartPanel9.Location = new System.Drawing.Point(617, 101);
            this.chartPanel9.Name = "chartPanel9";
            this.chartPanel9.Size = new System.Drawing.Size(25, 45);
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
            this.Next.BackColor = System.Drawing.Color.LightSkyBlue;
            this.Next.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Next.Location = new System.Drawing.Point(1073, 429);
            this.Next.Name = "Next";
            this.Next.Size = new System.Drawing.Size(180, 67);
            this.Next.TabIndex = 20;
            this.Next.Text = "Siguiente";
            this.Next.UseVisualStyleBackColor = false;
            this.Next.Click += new System.EventHandler(this.Next_Click);
            // 
            // Back
            // 
            this.Back.BackColor = System.Drawing.Color.LightSkyBlue;
            this.Back.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Back.Location = new System.Drawing.Point(853, 429);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(185, 67);
            this.Back.TabIndex = 21;
            this.Back.Text = "Anterior";
            this.Back.UseVisualStyleBackColor = false;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // chartPanel10
            // 
            this.chartPanel10.Location = new System.Drawing.Point(295, 227);
            this.chartPanel10.Name = "chartPanel10";
            this.chartPanel10.Size = new System.Drawing.Size(116, 55);
            this.chartPanel10.TabIndex = 18;
            // 
            // chartPanel11
            // 
            this.chartPanel11.Location = new System.Drawing.Point(62, 365);
            this.chartPanel11.Name = "chartPanel11";
            this.chartPanel11.Size = new System.Drawing.Size(92, 39);
            this.chartPanel11.TabIndex = 18;
            // 
            // chartPanel12
            // 
            this.chartPanel12.Location = new System.Drawing.Point(152, 309);
            this.chartPanel12.Name = "chartPanel12";
            this.chartPanel12.Size = new System.Drawing.Size(64, 50);
            this.chartPanel12.TabIndex = 18;
            // 
            // chartPanel13
            // 
            this.chartPanel13.Location = new System.Drawing.Point(247, 429);
            this.chartPanel13.Name = "chartPanel13";
            this.chartPanel13.Size = new System.Drawing.Size(68, 55);
            this.chartPanel13.TabIndex = 18;
            // 
            // chartPanel14
            // 
            this.chartPanel14.Location = new System.Drawing.Point(62, 448);
            this.chartPanel14.Name = "chartPanel14";
            this.chartPanel14.Size = new System.Drawing.Size(134, 100);
            this.chartPanel14.TabIndex = 18;
            // 
            // dataGridView3
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView3.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView3.DefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridView3.Location = new System.Drawing.Point(853, 35);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.Size = new System.Drawing.Size(400, 159);
            this.dataGridView3.TabIndex = 22;
            // 
            // chartPanel15
            // 
            this.chartPanel15.Location = new System.Drawing.Point(220, 227);
            this.chartPanel15.Name = "chartPanel15";
            this.chartPanel15.Size = new System.Drawing.Size(26, 30);
            this.chartPanel15.TabIndex = 13;
            // 
            // chartPanel16
            // 
            this.chartPanel16.Location = new System.Drawing.Point(251, 297);
            this.chartPanel16.Name = "chartPanel16";
            this.chartPanel16.Size = new System.Drawing.Size(64, 37);
            this.chartPanel16.TabIndex = 14;
            // 
            // chartPanel17
            // 
            this.chartPanel17.Location = new System.Drawing.Point(364, 316);
            this.chartPanel17.Name = "chartPanel17";
            this.chartPanel17.Size = new System.Drawing.Size(83, 18);
            this.chartPanel17.TabIndex = 15;
            // 
            // FromDate
            // 
            this.FromDate.Location = new System.Drawing.Point(948, 241);
            this.FromDate.Name = "FromDate";
            this.FromDate.Size = new System.Drawing.Size(200, 20);
            this.FromDate.TabIndex = 23;
            // 
            // ToDate
            // 
            this.ToDate.Location = new System.Drawing.Point(948, 339);
            this.ToDate.Name = "ToDate";
            this.ToDate.Size = new System.Drawing.Size(200, 20);
            this.ToDate.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(994, 200);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 39);
            this.label1.TabIndex = 25;
            this.label1.Text = "Desde";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(994, 284);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 39);
            this.label2.TabIndex = 26;
            this.label2.Text = "Hasta";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 1005);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ToDate);
            this.Controls.Add(this.FromDate);
            this.Controls.Add(this.chartPanel17);
            this.Controls.Add(this.chartPanel16);
            this.Controls.Add(this.chartPanel15);
            this.Controls.Add(this.dataGridView3);
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SelectDocument;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
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
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.Panel chartPanel15;
        private System.Windows.Forms.Panel chartPanel16;
        private System.Windows.Forms.Panel chartPanel17;
        private System.Windows.Forms.DateTimePicker FromDate;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DateTimePicker ToDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
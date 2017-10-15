namespace TokenExtraction1
{
    partial class Form_Phase4
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnProcessUnigram = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblAverageResult = new System.Windows.Forms.Label();
            this.lblAverage = new System.Windows.Forms.Label();
            this.btnProcessTrigram = new System.Windows.Forms.Button();
            this.btnProcessBigram = new System.Windows.Forms.Button();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnProcessUnigram);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.btnProcessTrigram);
            this.panel1.Controls.Add(this.btnProcessBigram);
            this.panel1.Controls.Add(this.btnOpenFile);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(771, 68);
            this.panel1.TabIndex = 0;
            // 
            // btnProcessUnigram
            // 
            this.btnProcessUnigram.Location = new System.Drawing.Point(359, 23);
            this.btnProcessUnigram.Name = "btnProcessUnigram";
            this.btnProcessUnigram.Size = new System.Drawing.Size(119, 23);
            this.btnProcessUnigram.TabIndex = 8;
            this.btnProcessUnigram.Text = "Pocess With Unigram";
            this.btnProcessUnigram.UseVisualStyleBackColor = true;
            this.btnProcessUnigram.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.lblAverageResult);
            this.panel3.Controls.Add(this.lblAverage);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(629, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(142, 68);
            this.panel3.TabIndex = 7;
            // 
            // lblAverageResult
            // 
            this.lblAverageResult.AutoSize = true;
            this.lblAverageResult.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblAverageResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAverageResult.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblAverageResult.Location = new System.Drawing.Point(11, 27);
            this.lblAverageResult.Name = "lblAverageResult";
            this.lblAverageResult.Size = new System.Drawing.Size(2, 15);
            this.lblAverageResult.TabIndex = 1;
            // 
            // lblAverage
            // 
            this.lblAverage.AutoSize = true;
            this.lblAverage.Location = new System.Drawing.Point(16, 4);
            this.lblAverage.Name = "lblAverage";
            this.lblAverage.Size = new System.Drawing.Size(50, 13);
            this.lblAverage.TabIndex = 0;
            this.lblAverage.Text = "Average:";
            // 
            // btnProcessTrigram
            // 
            this.btnProcessTrigram.Location = new System.Drawing.Point(223, 23);
            this.btnProcessTrigram.Name = "btnProcessTrigram";
            this.btnProcessTrigram.Size = new System.Drawing.Size(117, 23);
            this.btnProcessTrigram.TabIndex = 2;
            this.btnProcessTrigram.Text = "Process With Trigram";
            this.btnProcessTrigram.UseVisualStyleBackColor = true;
            this.btnProcessTrigram.Click += new System.EventHandler(this.btnProcessTrigram_Click);
            // 
            // btnProcessBigram
            // 
            this.btnProcessBigram.Location = new System.Drawing.Point(103, 23);
            this.btnProcessBigram.Name = "btnProcessBigram";
            this.btnProcessBigram.Size = new System.Drawing.Size(114, 23);
            this.btnProcessBigram.TabIndex = 1;
            this.btnProcessBigram.Text = "Process With Bigram";
            this.btnProcessBigram.UseVisualStyleBackColor = true;
            this.btnProcessBigram.Click += new System.EventHandler(this.btnProcessBigram_Click);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(22, 23);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(75, 23);
            this.btnOpenFile.TabIndex = 0;
            this.btnOpenFile.Text = "Open File";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 68);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(771, 426);
            this.panel2.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(771, 426);
            this.dataGridView1.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(34, 22);
            this.button1.TabIndex = 9;
            this.button1.Text = "OF";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Form_Phase4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 494);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form_Phase4";
            this.Text = "Form_Phase4";
            this.Load += new System.EventHandler(this.Form_Phase4_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnProcessTrigram;
        private System.Windows.Forms.Button btnProcessBigram;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblAverageResult;
        private System.Windows.Forms.Label lblAverage;
        private System.Windows.Forms.Button btnProcessUnigram;
        private System.Windows.Forms.Button button1;
    }
}
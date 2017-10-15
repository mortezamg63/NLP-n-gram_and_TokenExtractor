namespace TokenExtraction1
{
    partial class ShowData
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
            this.pnlUnigram = new System.Windows.Forms.Panel();
            this.pnlNgram = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnSentences = new System.Windows.Forms.Button();
            this.btnEndMarks = new System.Windows.Forms.Button();
            this.btnSigns = new System.Windows.Forms.Button();
            this.btnDigits = new System.Windows.Forms.Button();
            this.btnWords = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblAverageResult = new System.Windows.Forms.Label();
            this.lblAverage = new System.Windows.Forms.Label();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.btnEditCorpus = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.pnlUnigram.SuspendLayout();
            this.pnlNgram.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlUnigram
            // 
            this.pnlUnigram.Controls.Add(this.pnlNgram);
            this.pnlUnigram.Controls.Add(this.btnSentences);
            this.pnlUnigram.Controls.Add(this.btnEndMarks);
            this.pnlUnigram.Controls.Add(this.btnSigns);
            this.pnlUnigram.Controls.Add(this.btnDigits);
            this.pnlUnigram.Controls.Add(this.btnWords);
            this.pnlUnigram.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlUnigram.Location = new System.Drawing.Point(0, 0);
            this.pnlUnigram.Name = "pnlUnigram";
            this.pnlUnigram.Size = new System.Drawing.Size(685, 64);
            this.pnlUnigram.TabIndex = 0;
            // 
            // pnlNgram
            // 
            this.pnlNgram.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.pnlNgram.Controls.Add(this.button4);
            this.pnlNgram.Controls.Add(this.button3);
            this.pnlNgram.Controls.Add(this.button1);
            this.pnlNgram.Controls.Add(this.button2);
            this.pnlNgram.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlNgram.Location = new System.Drawing.Point(449, 0);
            this.pnlNgram.Name = "pnlNgram";
            this.pnlNgram.Size = new System.Drawing.Size(236, 64);
            this.pnlNgram.TabIndex = 5;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(123, 34);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(104, 23);
            this.button4.TabIndex = 8;
            this.button4.Text = "TriGram_Corpus";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(123, 8);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(104, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "TriGram_Sentence";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "BiGram_Sentence";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 34);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "BiGram_Corpus";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnSentences
            // 
            this.btnSentences.Location = new System.Drawing.Point(336, 21);
            this.btnSentences.Name = "btnSentences";
            this.btnSentences.Size = new System.Drawing.Size(75, 23);
            this.btnSentences.TabIndex = 4;
            this.btnSentences.Text = "Sentences";
            this.btnSentences.UseVisualStyleBackColor = true;
            this.btnSentences.Click += new System.EventHandler(this.btnSentences_Click);
            // 
            // btnEndMarks
            // 
            this.btnEndMarks.Location = new System.Drawing.Point(255, 21);
            this.btnEndMarks.Name = "btnEndMarks";
            this.btnEndMarks.Size = new System.Drawing.Size(75, 23);
            this.btnEndMarks.TabIndex = 3;
            this.btnEndMarks.Text = "EndMarks";
            this.btnEndMarks.UseVisualStyleBackColor = true;
            this.btnEndMarks.Click += new System.EventHandler(this.btnEndMarks_Click);
            // 
            // btnSigns
            // 
            this.btnSigns.Location = new System.Drawing.Point(174, 21);
            this.btnSigns.Name = "btnSigns";
            this.btnSigns.Size = new System.Drawing.Size(75, 23);
            this.btnSigns.TabIndex = 2;
            this.btnSigns.Text = "Signs";
            this.btnSigns.UseVisualStyleBackColor = true;
            this.btnSigns.Click += new System.EventHandler(this.btnSigns_Click);
            // 
            // btnDigits
            // 
            this.btnDigits.Location = new System.Drawing.Point(93, 21);
            this.btnDigits.Name = "btnDigits";
            this.btnDigits.Size = new System.Drawing.Size(75, 23);
            this.btnDigits.TabIndex = 1;
            this.btnDigits.Text = "Digits";
            this.btnDigits.UseVisualStyleBackColor = true;
            this.btnDigits.Click += new System.EventHandler(this.btnDigits_Click);
            // 
            // btnWords
            // 
            this.btnWords.Location = new System.Drawing.Point(12, 21);
            this.btnWords.Name = "btnWords";
            this.btnWords.Size = new System.Drawing.Size(75, 23);
            this.btnWords.TabIndex = 0;
            this.btnWords.Text = "Words";
            this.btnWords.UseVisualStyleBackColor = true;
            this.btnWords.Click += new System.EventHandler(this.btnWords_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 64);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.button9);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.button8);
            this.splitContainer1.Panel2.Controls.Add(this.button7);
            this.splitContainer1.Panel2.Controls.Add(this.button6);
            this.splitContainer1.Panel2.Controls.Add(this.button5);
            this.splitContainer1.Panel2.Controls.Add(this.btnEditCorpus);
            this.splitContainer1.Size = new System.Drawing.Size(685, 370);
            this.splitContainer1.SplitterDistance = 308;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(685, 308);
            this.panel2.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(685, 308);
            this.dataGridView1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblAverageResult);
            this.panel1.Controls.Add(this.lblAverage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(543, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(142, 58);
            this.panel1.TabIndex = 6;
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
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(291, 3);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 52);
            this.button8.TabIndex = 5;
            this.button8.Text = "Open Dialog Phase4";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(372, 30);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(89, 23);
            this.button7.TabIndex = 4;
            this.button7.Text = "Correct Trigram";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(372, 3);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(86, 23);
            this.button6.TabIndex = 3;
            this.button6.Text = "Correct Bigram";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(147, 18);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(138, 23);
            this.button5.TabIndex = 2;
            this.button5.Text = "CorpusEditor_Trigram";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // btnEditCorpus
            // 
            this.btnEditCorpus.Location = new System.Drawing.Point(3, 18);
            this.btnEditCorpus.Name = "btnEditCorpus";
            this.btnEditCorpus.Size = new System.Drawing.Size(138, 23);
            this.btnEditCorpus.TabIndex = 1;
            this.btnEditCorpus.Text = "CorpusEditor_Bigram";
            this.btnEditCorpus.UseVisualStyleBackColor = true;
            this.btnEditCorpus.Click += new System.EventHandler(this.btnEditCorpus_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(461, 2);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(76, 51);
            this.button9.TabIndex = 7;
            this.button9.Text = "AutoChange";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // ShowData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 434);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.pnlUnigram);
            this.Name = "ShowData";
            this.Text = "ShowData";
            this.Load += new System.EventHandler(this.ShowData_Load);
            this.pnlUnigram.ResumeLayout(false);
            this.pnlNgram.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlUnigram;
        private System.Windows.Forms.Button btnSentences;
        private System.Windows.Forms.Button btnEndMarks;
        private System.Windows.Forms.Button btnSigns;
        private System.Windows.Forms.Button btnDigits;
        private System.Windows.Forms.Button btnWords;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel pnlNgram;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnEditCorpus;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblAverageResult;
        private System.Windows.Forms.Label lblAverage;
        private System.Windows.Forms.Button button9;
    }
}
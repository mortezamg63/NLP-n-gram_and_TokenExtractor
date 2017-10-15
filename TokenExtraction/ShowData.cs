using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;

namespace TokenExtraction1
{
    public partial class ShowData : Form
    {

        string CorpusEdited;
        public ShowData()
        {
            InitializeComponent();            
        }

       
        private void btnSigns_Click(object sender, EventArgs e)
        {
            lblAverageResult.Text = string.Empty;
            showInformationToDataGridView(Regex_Patterns.Instance.GetSignList);
        }

        private void btnWords_Click(object sender, EventArgs e)
        {
            lblAverageResult.Text = string.Empty;
            showInformationToDataGridView(Regex_Patterns.Instance.GetWordList);
           

        }

        private void btnDigits_Click(object sender, EventArgs e)
        {
            lblAverageResult.Text = string.Empty;
            showInformationToDataGridView(Regex_Patterns.Instance.GetDigitList);
        }

        private void btnEndMarks_Click(object sender, EventArgs e)
        {
            lblAverageResult.Text = string.Empty;
            showInformationToDataGridView(Regex_Patterns.Instance.GetEndMarkList);
        }

        private void btnSentences_Click(object sender, EventArgs e)
        {
            lblAverageResult.Text = string.Empty;
            //---------------------
            DataSet ds = show_Sentences();            
            if (ds.Tables.Count > 0)
            {
                DataView DV = ds.Tables[0].DefaultView;
                DV.Sort = "Key ASC";
                dataGridView1.DataSource = DV;                
            }
            else
                MessageBox.Show("No Data Exist");             
        }

        private DataSet show_Sentences()
        {

            DataSet mydataset = new DataSet();
            ArrayList sents = Regex_Patterns.Instance.GetSentenceList;
            IEnumerator en = sents.GetEnumerator();
            if (sents.Count > 0)
            {
                DataTable table1 = new DataTable();
                table1.Columns.Add("Key", typeof(string));
                //table1.Columns.Add("Value", typeof(string));

                IEnumerator enumerator = sents.GetEnumerator();

                DataRow row;
                while (enumerator.MoveNext())
                {
                    row = table1.NewRow();
                    row["Key"] = (string)enumerator.Current;
                    // row["Value"] = enumerator.Value.ToString();

                    table1.Rows.Add(row);
                }
                mydataset.Tables.Add(table1);
            }
            return mydataset;
        }

        #region توابعی که خودم تعریف کردم        

        private void showInformationToDataGridView(Hashtable hash)
        {
            DataSet ds = BindDataGridView(hash);
            if (ds.Tables.Count>0)
            {
                DataView DV = ds.Tables[0].DefaultView;
                DV.Sort = "Key ASC";
                dataGridView1.DataSource = DV;
            }
            else
                MessageBox.Show("No Data Exist");
        }

        private DataSet BindDataGridView(Hashtable hash)
        {
            double Avg = 0;

            DataSet mydataset = new DataSet();
            if (hash.Count > 0)
            {
                DataTable table1 = new DataTable();
                table1.Columns.Add("Key", typeof(string));
                table1.Columns.Add("Value", typeof(int));
                table1.Columns.Add("Probability", typeof(string));                
                IDictionaryEnumerator enumerator = hash.GetEnumerator();
                int Total_count_Tokens = Regex_Patterns.Instance.GetTotalTokens_count();
                double prob;
                while (enumerator.MoveNext())
                {
                    DataRow row;
                    row = table1.NewRow();
                    row["Key"] = (string)enumerator.Key;
                    row["Value"] = enumerator.Value;
                    prob = (double)int.Parse(enumerator.Value.ToString()) / Total_count_Tokens;
                    row["Probability"] = prob;
                    Avg += prob;
                    //row = table1.NewRow();
                    //row["Key"] = (string)enumerator.Key;
                    //row["Value"] = enumerator.Value.ToString();

                    table1.Rows.Add(row);
                }
                mydataset.Tables.Add(table1);
                lblAverageResult.Text = ((double)Avg / (mydataset.Tables[0].Rows.Count)).ToString();

            }
            
            return mydataset;
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {            
            BindDataToDataGridView(false, false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BindDataToDataGridView(false, true);
        }

        private void BindDataToDataGridView(bool isTriGram,bool isCorpus)
        {
            string sentences = string.Empty;

            DataSet ds = new DataSet();
            DataTable mytable = new DataTable();
            mytable.Columns.Add("Token", typeof(string));
            mytable.Columns.Add("PrevToken", typeof(string));
            if (isTriGram)
                mytable.Columns.Add("PrevPrevToken", typeof(string));
            mytable.Columns.Add("Probility", typeof(double));
            mytable.Columns.Add("Count", typeof(int));            
            DataRow r;

            double Avg = 0;

            if (isCorpus)
            {
                foreach (string sent in Regex_Patterns.Instance.GetSentenceList)
                    sentences += sent;
                //sentences += Regex_Patterns.Instance.Tape;
                if (isTriGram)
                {
                    List<StructData_TriGram> l = new GRAMCLASS().TriGram(sentences);

                    foreach (StructData_TriGram SD in l)
                    {
                        r = mytable.NewRow();
                        r[0] = SD.CurrentToken;
                        r[1] = SD.PrevToken;
                        r[2] = SD.PrevPrevToken;
                        r[3] = SD.Probability;
                        r[4] = SD.Count;
                        mytable.Rows.Add(r);
                        Avg += SD.Probability;
                    }
                }
                else
                {
                    List<StructData_BiGram> l = new GRAMCLASS().BiGram(sentences);

                    foreach (StructData_BiGram SD in l)
                    {
                        r = mytable.NewRow();
                        r[0] = SD.CurrentToken;
                        r[1] = SD.PrevToken;
                        r[2] = SD.Probability;
                        r[3] = SD.Count;
                        mytable.Rows.Add(r);
                        Avg += SD.Probability;
                    }
                }
            }
            else
            {
                foreach (string sentence in Regex_Patterns.Instance.GetSentenceList)
                {
                    sentences = sentence;

                    if (isTriGram)
                    {
                        List<StructData_TriGram> l = new GRAMCLASS().TriGram(sentences);

                        foreach (StructData_TriGram SD in l)
                        {
                            r = mytable.NewRow();
                            r[0] = SD.CurrentToken;
                            r[1] = SD.PrevToken;
                            r[2] = SD.PrevPrevToken;
                            r[3] = SD.Probability;
                            r[4] = SD.Count;
                            mytable.Rows.Add(r);
                            Avg += SD.Probability;
                        }
                    }
                    else
                    {
                        List<StructData_BiGram> l = new GRAMCLASS().BiGram(sentences);

                        foreach (StructData_BiGram SD in l)
                        {
                            r = mytable.NewRow();
                            r[0] = SD.CurrentToken;
                            r[1] = SD.PrevToken;
                            r[2] = SD.Probability;
                            r[3] = SD.Count;
                            mytable.Rows.Add(r);
                            Avg += SD.Probability;
                        }
                    }
                }
            }
            
            ds.Tables.Add(mytable);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
            lblAverageResult.Text = ((double)Avg / (dataGridView1.Rows.Count - 1)).ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BindDataToDataGridView(true, false);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            BindDataToDataGridView(true, true);
        }

        private void ShowData_Load(object sender, EventArgs e)
        {
           // pnlEdit.Visible = false;
            btnEditCorpus.Text = "CorpusEditor_Bigram";
            
        }

        private void btnEditCorpus_Click(object sender, EventArgs e)
        {            
            Form f;
            DataSet ds;
           // Show_FromPhase4(out f, out ds);
            
            //----------------------------------------------------------------------------------------
            //Show_Phase4_TO_GridView(f, ds,false);
            new AutoChange().Show();
            
        }

        private void Show_Phase4_TO_GridView(Form f, DataSet ds,bool isTrigram)
        {
            DataTable table1 = new DataTable();
            table1.Columns.Add("Setntence", typeof(string));
            table1.Columns.Add("Probability", typeof(double));
            DataRow r;
            DataGridView dg2 = ((DataGridView)f.Controls[f.Controls.IndexOfKey("grid_sentences")]);            
            
            GRAMCLASS gl = new GRAMCLASS();
            List<StructData_BiGram> bigram = new List<StructData_BiGram>();//= gl.BiGram(Regex_Patterns.Instance.Tape);
            List<StructData_TriGram> trigram = new List<StructData_TriGram>();
            string sentences = string.Empty;
            foreach (string sentence in Regex_Patterns.Instance.GetSentenceList)
                sentences += sentence;
            if (isTrigram)
                trigram = gl.TriGram(sentences.ToLower());
            else
                bigram = gl.BiGram(sentences.ToLower());

            //bool isFirst = true;
            double Avg = 0;
            foreach (DataGridViewRow row in dg2.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    string sentence = string.Empty;
                   
                        sentence = row.Cells[0].Value.ToString();

                    ArrayList new_SeperatedSentence = gl.Seperator_Phase4(sentence, isTrigram);// BiGram

                    r = table1.NewRow();
                    r[0] = row.Cells[0].Value.ToString();
                    double currentProbability;
                    if (isTrigram)
                        currentProbability = gl.Verify_Sentence(new_SeperatedSentence, trigram, isTrigram);
                    else
                        currentProbability = gl.Verify_Sentence(new_SeperatedSentence, bigram, isTrigram);
                    
                    r[1] = currentProbability;
                    Avg += currentProbability;

                    table1.Rows.Add(r);
                }
            }
            ds.Tables.Add(table1);
            DataView dv = new DataView();
            dv = ds.Tables[1].AsDataView();
            dv.Sort = "Probability ASC";
            dataGridView1.DataSource = dv;
            lblAverageResult.Text = ((double)Avg / (dataGridView1.Rows.Count-1)).ToString();
        }

        private void Show_FromPhase4(out Form f, out DataSet ds)
        {
            f = new Form();
            //--- create datagridview into new form
            DataGridView gridview = new DataGridView();
            gridview.Name = "grid_sentences";
            gridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ds = show_Sentences();
            //DataView dv = ds.Tables[0].AsDataView();
            gridview.DataSource = ds.Tables[0].AsDataView();
            gridview.Dock = DockStyle.Fill;
            //----- add datagridview to form
            f.Controls.Add(gridview);
            f.Width = 500; f.Height = 500;
            f.Text = "Corpus Editor";

            f.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {            
            Form f;
            DataSet ds;
            Show_FromPhase4(out f, out ds);
            //----------------------------------            
            Show_Phase4_TO_GridView(f, ds, true);            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string sentences = string.Empty;

            foreach (string stnc in Regex_Patterns.Instance.GetSentenceList)
                sentences += stnc;
            DataSet ds = new DataSet();
            DataTable mytable = new DataTable();
            mytable.Columns.Add("Token", typeof(string));
            mytable.Columns.Add("PrevToken", typeof(string));            
            mytable.Columns.Add("Probility", typeof(double));
            mytable.Columns.Add("Count", typeof(int));
            DataRow r;

            double SumProbability = 0;            

            List<StructData_BiGram> l = new GRAMCLASS().BiGram2(sentences,false);

            foreach (StructData_BiGram SD in l)
            {
                r = mytable.NewRow();
                r[0] = SD.CurrentToken;
                r[1] = SD.PrevToken;
                r[2] = SD.Probability;
                SumProbability += SD.Probability;
                r[3] = SD.Count;
                mytable.Rows.Add(r);
            }
            ds.Tables.Add(mytable);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
            lblAverageResult.Text = ((double)SumProbability / l.Count).ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();

            string sentences = string.Empty;

            foreach (string stnc in Regex_Patterns.Instance.GetSentenceList)
                sentences += stnc;

            DataSet ds = new DataSet();
            DataTable mytable = new DataTable();
            mytable.Columns.Add("Token", typeof(string));
            mytable.Columns.Add("PrevToken", typeof(string));
            mytable.Columns.Add("PrevPrevToken", typeof(string));
            mytable.Columns.Add("Probility", typeof(double));
            mytable.Columns.Add("Count", typeof(int));
            DataRow r;

            double SumProbability = 0;
            List<StructData_TriGram> l = new GRAMCLASS().TriGram2(sentences,false);

            foreach (StructData_TriGram SD in l)
            {
                r = mytable.NewRow();
                r[0] = SD.CurrentToken;
                r[1] = SD.PrevToken;
                r[2] = SD.PrevPrevToken;
                r[3] = SD.Probability;
                SumProbability += SD.Probability;
                r[4] = SD.Count;
                mytable.Rows.Add(r);
            }
            ds.Tables.Add(mytable);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
            lblAverageResult.Text = ((double)SumProbability / l.Count).ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            new Form_Phase4().ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            new AutoChange().Show();
        }
    }
}

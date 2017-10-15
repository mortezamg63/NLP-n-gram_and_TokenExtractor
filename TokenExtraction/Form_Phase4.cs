using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using HtmlAgilityPack;

namespace TokenExtraction1
{
    public partial class Form_Phase4 : Form
    {
        public Form_Phase4()
        {
            InitializeComponent();
        }
        ArrayList sentencesOfCorpus;
        GRAMCLASS gramclass=new GRAMCLASS();


        void Fill_MemoryClass()
        {
            if (MemoryClass.Instance.Get_OldCorpus.Count < 1)
            {
                ArrayList al = new ArrayList();
                foreach (string sent in Regex_Patterns.Instance.GetSentenceList)
                    al.Add(sent);
                MemoryClass.Instance.Fill_oldCorpus(al);
            }
            if (MemoryClass.Instance.Get_SignList.Count < 1)
            {
                Hashtable hashsign = new Hashtable();
                foreach (object obj in Regex_Patterns.Instance.GetSignList.Keys)
                {
                    hashsign.Add(obj, Regex_Patterns.Instance.GetSignList[obj.ToString()]);
                }
                MemoryClass.Instance.Fill_SignList(hashsign);
            }

            if (MemoryClass.Instance.Get_EndMarkList.Count < 1)
            {
                Hashtable hashee = new Hashtable();
                foreach (object obj in Regex_Patterns.Instance.GetEndMarkList.Keys)
                {
                    hashee.Add(obj, Regex_Patterns.Instance.GetEndMarkList[obj.ToString()]);
                }
                MemoryClass.Instance.Fill_EndMarkList(hashee);
            }

            if (MemoryClass.Instance.Get_DigitList.Count < 1)
            {
                Hashtable hashdigit = new Hashtable();
                foreach (object obj in Regex_Patterns.Instance.GetDigitList.Keys)
                {
                    hashdigit.Add(obj, Regex_Patterns.Instance.GetDigitList[obj.ToString()]);
                }
                MemoryClass.Instance.Fill_DigitList(hashdigit);
            }

            if (MemoryClass.Instance.Get_WordList.Count < 1)
            {
                Hashtable hashword = new Hashtable();
                foreach (object obj in Regex_Patterns.Instance.GetWordList.Keys)
                {
                    hashword.Add(obj, Regex_Patterns.Instance.GetWordList[obj.ToString()]);
                }
                MemoryClass.Instance.Fill_wordList(hashword);
            }
        }
        private void btnOpenFile_Click(object sender, EventArgs e)
        {                        
            GRAMCLASS gc = new GRAMCLASS();            
            string corpus = string.Empty;

            Fill_MemoryClass();

            openFileDialog1.Filter = "HtmFile (*.htm)|*.htm|Htmlfile (*.html)|*.html|MhtFile(*.mht)|*.mht";
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                StreamReader file = new StreamReader(openFileDialog1.FileName);
                corpus= file.ReadToEnd();
                Form1 f1 = new Form1();
                corpus = f1.RemoveTags(corpus);
                file.Close();                
            }
            //------------ Start Process ---------            

            Regex_Patterns.Instance.ClearDigitList();
            Regex_Patterns.Instance.ClearEndMarkList();
            Regex_Patterns.Instance.ClearSignList();
            Regex_Patterns.Instance.ClearSentenceList();
            Regex_Patterns.Instance.ClearWordList();
            Regex_Patterns.Instance.ClearCurrentSentence();
            Regex_Patterns.Instance.Tape = corpus;
            Regex_Patterns.Instance.HeadTape = -1;
            
            while (Regex_Patterns.Instance.HeadTape < Regex_Patterns.Instance.Tape.Length - 1)
                new S0().Compute();

            DataTable mytable = new DataTable();
            mytable.Columns.Add("sentence", typeof(string));
            DataRow row;
            
            sentencesOfCorpus = Regex_Patterns.Instance.GetSentenceList;

            foreach (string sent in Regex_Patterns.Instance.GetSentenceList)
            {
                row = mytable.NewRow();
                row[0] = sent;
                mytable.Rows.Add(row);
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(mytable);
            dataGridView1.DataSource = ds.Tables[0].AsDataView();
            
            btnProcessTrigram.Enabled = true;
            btnProcessBigram.Enabled = true;
            btnProcessUnigram.Enabled = true;
        }

        private void btnProcessBigram_Click(object sender, EventArgs e)
        {
            string sentences = string.Empty;
            int Max_Index = 0;            
            foreach (string sentence in Regex_Patterns.Instance.GetSentenceList)
            {
                Max_Index = (sentence.Length > Max_Index) ? sentence.Length : Max_Index;
                sentences += sentence;
            }
            if (MemoryClass.Instance.IsEmptyBigramList())
                gramclass.BiGram2(MemoryClass.Instance.Get_Sentences_From_OldCorpus(),true);

            GRAMCLASS gc = new GRAMCLASS();
           
            string currentSentence = string.Empty;
            
            dataGridView1.Columns.Clear();
            DataTable mytable = new DataTable();
            mytable.Columns.Add("Sentence", typeof(string));
            mytable.Columns.Add("Probability", typeof(double));

            DataRow row;
            double SumProbability = 0;

            foreach (string sentence in Regex_Patterns.Instance.GetSentenceList)
            {                
                currentSentence = sentence;
                ArrayList SS = gc.Seperator_Phase4(currentSentence, false);
                double PDF = 1;
                StructData_BiGram dataBigram = new StructData_BiGram();
                for (int i = 1; i < SS.Count; i++)
                {
                    dataBigram.CurrentToken = (string)SS[i].ToString().ToLower();
                    dataBigram.PrevToken = (string)SS[i - 1].ToString().ToLower();
                    if (MemoryClass.Instance.GetListBigram.Exists(s => s.CurrentToken == dataBigram.CurrentToken && s.PrevToken == dataBigram.PrevToken))
                    {
                        StructData_BiGram bigrm = MemoryClass.Instance.GetListBigram[MemoryClass.Instance.GetListBigram.FindIndex(s => s.CurrentToken == dataBigram.CurrentToken && s.PrevToken == dataBigram.PrevToken)];
                        int root = (bigrm.Probability!=1)?bigrm.Probability.ToString().Remove(0,2).Length:1;
                        PDF *= Math.Pow(bigrm.Probability, (double)1 / SS.Count);
                    }
                    else
                        PDF=0;
                }

                row = mytable.NewRow();
                row[0] = currentSentence;
                row[1] = PDF;//*((double)currentSentence.Length/Max_Index);
                SumProbability += PDF;// *((double)currentSentence.Length / Max_Index);
                mytable.Rows.Add(row);
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(mytable);
            dataGridView1.DataSource = ds.Tables[0].AsDataView();
            lblAverageResult.Text = ((double)SumProbability / Regex_Patterns.Instance.GetSentenceList.Count).ToString();
        }

        private void btnProcessTrigram_Click(object sender, EventArgs e)
        {
            string sentences = string.Empty;
            //int Max_Index = 0;
            foreach (string sentence in Regex_Patterns.Instance.GetSentenceList)
            {
              //  Max_Index = (sentence.Length > Max_Index) ? sentence.Length : Max_Index;
                sentences += sentence;
            }
            if(MemoryClass.Instance.isEmptyTrigramList())
                gramclass.TriGram2(MemoryClass.Instance.Get_Sentences_From_OldCorpus(), true);
            
            GRAMCLASS gc = new GRAMCLASS();

            string currentSentence = string.Empty;

            dataGridView1.Columns.Clear();
            DataTable mytable = new DataTable();
            mytable.Columns.Add("Sentence", typeof(string));
            mytable.Columns.Add("Probability", typeof(double));

            DataRow row;

            double SumProbability = 0;

            foreach (string sentence in Regex_Patterns.Instance.GetSentenceList)
            {                
                currentSentence = sentence;
                ArrayList SS = gc.Seperator_Phase4(currentSentence, false);
                double PDF = 1;
                StructData_TriGram dataTrigram = new StructData_TriGram();
                for (int i = 2; i < SS.Count; i++)
                {
                    dataTrigram.CurrentToken = ((string)SS[i]).ToLower();
                    dataTrigram.PrevToken = ((string)SS[i - 1]).ToLower();
                    dataTrigram.PrevPrevToken = ((string)SS[i - 2]).ToLower();
                    if (MemoryClass.Instance.GetListTrigram.Exists(s => s.CurrentToken == dataTrigram.CurrentToken && s.PrevToken == dataTrigram.PrevToken && s.PrevPrevToken==dataTrigram.PrevPrevToken))
                    {
                        StructData_TriGram trigrm = MemoryClass.Instance.GetListTrigram[gramclass.ListTrigram.FindIndex(s => s.CurrentToken == dataTrigram.CurrentToken &&
                            s.PrevToken == dataTrigram.PrevToken && s.PrevPrevToken==dataTrigram.PrevPrevToken)];

                        int root=(trigrm.Probability!=1)?trigrm.Probability.ToString().Remove(0,2).Length:1;
                        PDF *= Math.Pow(trigrm.Probability,(double)1/SS.Count);
                    }
                    else
                        PDF = 0;
                }

                row = mytable.NewRow();
                row[0] = currentSentence;
                row[1] = (PDF);// *((double)currentSentence.Length / Max_Index);
                SumProbability += (PDF);// *((double)currentSentence.Length / Max_Index);
                mytable.Rows.Add(row);
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(mytable);
            dataGridView1.DataSource = ds.Tables[0].AsDataView();
            lblAverageResult.Text = ((double)SumProbability / Regex_Patterns.Instance.GetSentenceList.Count).ToString();
        }

        private void Form_Phase4_Load(object sender, EventArgs e)
        {
            btnProcessBigram.Enabled = false;
            btnProcessTrigram.Enabled = false;
            btnProcessUnigram.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            table.Columns.Add("sentence", typeof(string));
            table.Columns.Add("Probability",typeof(double));
            DataRow row;
            
            double SumProbability = 0;
            int CountOfTotalTokens = MemoryClass.Instance.Get_CountOfTotalTokens();
            foreach (string sentence in Regex_Patterns.Instance.GetSentenceList)
            {
                ArrayList al = gramclass.Seperator_Phase4(sentence, false);
                double PDF = 1;

                foreach (string token in al)
                {
                    int Count = MemoryClass.Instance.Get_CountOfSpecialToken_Unigram(token);
                    if (Count != -1)
                        PDF *= (double)MemoryClass.Instance.Get_CountOfSpecialToken_Unigram(token) / CountOfTotalTokens;
                    else
                    {
                        PDF = 0;
                        break;
                    }
                }
                row=table.NewRow();
                row[0]=sentence;
                row[1]=PDF;
                SumProbability += (PDF);
                table.Rows.Add(row);
            }
            DataSet ds=new DataSet();
            ds.Tables.Add(table);
            dataGridView1.DataSource = ds.Tables[0].AsDataView();
            lblAverageResult.Text = ((double)SumProbability / Regex_Patterns.Instance.GetSentenceList.Count).ToString();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            GRAMCLASS gc = new GRAMCLASS();
            string corpus = string.Empty;

            Fill_MemoryClass();

            openFileDialog1.Filter = "HtmFile (*.htm)|*.htm|Htmlfile (*.html)|*.html|MhtFile(*.mht)|*.mht";
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                StreamReader file = new StreamReader(openFileDialog1.FileName);
                corpus = file.ReadToEnd();
                Form1 f1 = new Form1();
                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                document.Load(new MemoryStream(File.ReadAllBytes(openFileDialog1.FileName)), UnicodeEncoding.UTF8);
                corpus = ExtractViewableTextCleaned(document.DocumentNode).Replace("&gt;", "").Replace("&nbsp;", "").Replace("&quot;", "");
                //corpus = f1.RemoveTags(corpus);
                file.Close();
            }
            //------------ Start Process ---------            

            Regex_Patterns.Instance.ClearDigitList();
            Regex_Patterns.Instance.ClearEndMarkList();
            Regex_Patterns.Instance.ClearSignList();
            Regex_Patterns.Instance.ClearSentenceList();
            Regex_Patterns.Instance.ClearWordList();
            Regex_Patterns.Instance.ClearCurrentSentence();
            Regex_Patterns.Instance.Tape = corpus;
            Regex_Patterns.Instance.HeadTape = -1;

            while (Regex_Patterns.Instance.HeadTape < Regex_Patterns.Instance.Tape.Length - 1)
                new S0().Compute();

            DataTable mytable = new DataTable();
            mytable.Columns.Add("sentence", typeof(string));
            DataRow row;

            sentencesOfCorpus = Regex_Patterns.Instance.GetSentenceList;

            foreach (string sent in Regex_Patterns.Instance.GetSentenceList)
            {
                row = mytable.NewRow();
                row[0] = sent;
                mytable.Rows.Add(row);
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(mytable);
            dataGridView1.DataSource = ds.Tables[0].AsDataView();

            btnProcessTrigram.Enabled = true;
            btnProcessBigram.Enabled = true;
            btnProcessUnigram.Enabled = true;
        }

        #region methods for remove tags with DLL
        
        private static Regex _removeRepeatedWhitespaceRegex = new Regex(@"(\s|\n|\r){2,}", RegexOptions.Singleline | RegexOptions.IgnoreCase);

        public static string ExtractViewableTextCleaned(HtmlNode node)
        {
            string textWithLotsOfWhiteSpaces = ExtractViewableText(node);
            return _removeRepeatedWhitespaceRegex.Replace(textWithLotsOfWhiteSpaces, " ");
        }

        public static string ExtractViewableText(HtmlNode node)
        {
            StringBuilder sb = new StringBuilder();
            ExtractViewableTextHelper(sb, node);
            return sb.ToString();
        }
        private static void ExtractViewableTextHelper(StringBuilder sb, HtmlNode node)
        {
            if (node.Name != "script" && node.Name != "style")
            {
                if (node.NodeType == HtmlNodeType.Text)
                {
                    AppendNodeText(sb, node);
                }

                foreach (HtmlNode child in node.ChildNodes)
                {
                    ExtractViewableTextHelper(sb, child);
                }
            }
        }

        private static void AppendNodeText(StringBuilder sb, HtmlNode node)
        {
            string text = ((HtmlTextNode)node).Text;
            if (string.IsNullOrWhiteSpace(text) == false)
            {
                sb.Append(text);

                // If the last char isn't a white-space, add a white space
                // otherwise words will be added ontop of each other when they're only separated by
                // tags
                if (text.EndsWith("\t") || text.EndsWith("\n") || text.EndsWith(" ") || text.EndsWith("\r"))
                {
                    // We're good!
                }
                else
                {
                    sb.Append(" ");
                }
            }
        }
        #endregion
    }
}

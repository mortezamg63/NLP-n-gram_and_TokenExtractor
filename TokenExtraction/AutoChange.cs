using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace TokenExtraction1
{
    public partial class AutoChange : Form
    {
        //---- variables---        
        int[] history_index;
        int index_history_index = 0;
        int numberChanges = 0;
        //-----------------
        public AutoChange()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {            
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrEmpty(textBox1.Text))
            {
                for (int index_operation = 1; index_operation <= int.Parse(textBox1.Text); index_operation++)
                {
                    Random rnd = new Random();
                    int index_function = (index_operation % 4) == 0 ? 4 : index_operation % 4;
                    switch (index_function)
                    {
                        case 1:
                            Switch_tokens_Corpus();
                            break;
                        case 2:
                            Delete_corpus();
                            break;
                        case 3:
                            Insert_Corpus();
                            break;
                        case 4:
                            incorrectSpell_Corpus();
                            break;
                    }
                }
            }
            else
                MessageBox.Show("please enter \" تعداد تغییرات\"");

            //---------------- buttons enable -------------------
            button4.Enabled = true;
            button3.Enabled = true;
            button2.Enabled = true;
        }

        private void Switch_tokens_Corpus()
        {            
                GRAMCLASS gc = new GRAMCLASS();
                Random rnd_row_index = new Random();
                Random rnd_token_index = new Random();
                //---------- first select row and token randomly --------
                ArrayList al;
                int token_index_1;
                int row_index_1;
                string cur_token = Select_Token_From_GridView(out al, out token_index_1, out row_index_1);

                //---------- second select row and token randomly --------
                int token_index_2;
                int row_index_2;
                ArrayList al_new;
                string new_token = Select_Token_From_GridView(out al_new, out token_index_2, out row_index_2);

                //------ switch tokens --------
                al.RemoveAt(token_index_1); al.Insert(token_index_1, new_token);
                al_new.Remove(token_index_2); al_new.Insert(token_index_2, cur_token);
                //----- fill sentence from al list --------
                string current_sentence = dataGridView1.Rows[row_index_1].Cells[0].Value.ToString();
                string new_sentence1 = string.Empty;
                string new_sentence2 = string.Empty;
                for (int al_i = 0; al_i < al.Count; al_i++)
                {
                    new_sentence1 += " " + al[al_i].ToString();                 
                }

                for (int al_new_i = 0; al_new_i < al_new.Count; al_new_i++)
                {
                    new_sentence2 += " " + al_new[al_new_i].ToString();
                }

                dataGridView1.Rows[row_index_1].Cells[0].Value = new_sentence1;
                dataGridView1.Rows[row_index_2].Cells[0].Value = new_sentence2;
                dataGridView1.Rows[row_index_1].DefaultCellStyle.BackColor = Color.Red;
                dataGridView1.Rows[row_index_2].DefaultCellStyle.BackColor = Color.Red;            
        }

        private string Select_Token_From_GridView(out ArrayList al,out int token_index,out int row_index)
        {
            GRAMCLASS gc = new GRAMCLASS();
            //------- create row_index randomly --------------------------------------
            row_index = Create_Random_index(dataGridView1.Rows.Count - 1, false);
            //------ select row randomly and seperate sentence within row -------
            al = new ArrayList();
            al.AddRange(gc.Seperator_ChangeCorpus(dataGridView1.Rows[row_index].Cells[0].Value.ToString()));
            //------ select a token randomly from previous line --------
            token_index = Create_Random_index(al.Count - 1, true);
            string cur_token = al[token_index].ToString();
            
            if (Regex_Patterns.Instance.GetMarkType(cur_token) != TypeMark.Word && Regex_Patterns.Instance.GetMarkType(cur_token) != TypeMark.Digit)
                Select_Token_From_GridView(out al, out token_index, out row_index);

            return cur_token;
        }

        private int Create_Random_index(int maxIndex,bool isToken)
        {
            Random rnd_row_index = new Random();
             int row_index=0;
            if (!isToken)
            {                
                row_index = rnd_row_index.Next(maxIndex);
                while (isExist_index(row_index))
                    row_index = rnd_row_index.Next(maxIndex);

                history_index[index_history_index <= (history_index.Length - 1) ? index_history_index++ : --index_history_index] = row_index;
            }
            else
            {
                row_index = rnd_row_index.Next(maxIndex);
            }
            return row_index;
        }
        /// <summary>
        /// if index is exist then 
        ///     return true 
        /// else 
        ///     return false
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private bool isExist_index(int index)
        {
            bool isExist = true;

            int search_index=0;

            while (search_index <= (index_history_index-1))
            {
                if (history_index[search_index] == index)
                    break;
                search_index++;
            }
            isExist = search_index > (index_history_index-1) ? false : true;
            return isExist;
        }

        private void Fill_MemoryClass()
        {
            MemoryClass.Instance.Fill_DigitList(Regex_Patterns.Instance.GetDigitList);
            MemoryClass.Instance.Fill_EndMarkList(Regex_Patterns.Instance.GetEndMarkList);
            MemoryClass.Instance.Fill_oldCorpus(Regex_Patterns.Instance.GetSentenceList);
            MemoryClass.Instance.Fill_SignList(Regex_Patterns.Instance.GetSignList);
            MemoryClass.Instance.Fill_wordList(Regex_Patterns.Instance.GetWordList);            
        }
        private void AutoChange_Load(object sender, EventArgs e)
        {
            Fill_MemoryClass();

            GRAMCLASS gc = new GRAMCLASS();            

            DataTable table1 = new DataTable();
            table1.Columns.Add("جمله", typeof(string));
            DataRow row;
            foreach (string sentence in Regex_Patterns.Instance.GetSentenceList)
            {
                //------ add to table ------
                row = table1.NewRow();
                row[0] = sentence;
                table1.Rows.Add(row);                
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(table1);
            dataGridView1.DataSource = ds.Tables[0].AsDataView();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //------ show colors for change events in datagridview 
            lblDelete.BackColor = Color.SeaGreen;
            lblInsert.BackColor = Color.DarkKhaki;
            lblSwitch.BackColor = Color.Red;
            lblIncorrectSpell.BackColor = Color.Silver;

            //----------------Disable buttons -----------------
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;              
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GRAMCLASS gramclass = new GRAMCLASS();            
            if (MemoryClass.Instance.IsEmptyBigramList())
                gramclass.BiGram2(MemoryClass.Instance.Get_Sentences_From_OldCorpus()==""?Regex_Patterns.Instance.Get_Corpus():MemoryClass.Instance.Get_Sentences_From_OldCorpus(),true);

            GRAMCLASS gc = new GRAMCLASS();

            string currentSentence = string.Empty;

            //dataGridView1.Columns.Clear();
            DataTable mytable = new DataTable();
            mytable.Columns.Add("Sentence", typeof(string));
            mytable.Columns.Add("Probability", typeof(double));

            DataRow row;
            double SumProbability = 0;
            for (int index_gridview_row = 0; index_gridview_row < dataGridView1.Rows.Count-1; index_gridview_row++)
            {
                //foreach (string sentence in )
                
                    currentSentence = dataGridView1[0,index_gridview_row].Value.ToString();
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
                            
                            PDF *= Math.Pow(bigrm.Probability, (double)1 / SS.Count);
                        }
                        else
                            PDF = 0;
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

        private void Delete_corpus()
        {            
                ArrayList al;
                int token_index_1;
                int row_index_1;
                string cur_token = Select_Token_From_GridView(out al, out token_index_1, out row_index_1);
                al.RemoveAt(token_index_1);

                //------------------------------
                string current_sentence = dataGridView1.Rows[row_index_1].Cells[0].Value.ToString();
                string new_sentence = string.Empty;
                for (int al_i = 0; al_i < al.Count; al_i++)
                {
                    new_sentence += " " + al[al_i].ToString();
                }
                //MessageBox.Show(current_sentence + "\r\n\r\n\r\n" + new_sentence);
                dataGridView1.Rows[row_index_1].Cells[0].Value = new_sentence;
                dataGridView1.Rows[row_index_1].DefaultCellStyle.BackColor = Color.SeaGreen;            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            history_index = new int[int.Parse(textBox1.Text) * 2];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void Insert_Corpus()
        {            
                //---------------- get first token from a row randomly -----------
                ArrayList al;
                int token_index_1;
                int row_index_1;
                string cur_token = Select_Token_From_GridView(out al, out token_index_1, out row_index_1);

                //---------------- get second token from a row randomly -----------
                ArrayList al_new;
                int token_index_2;
                int row_index_2;
                string new_token = Select_Token_From_GridView(out al_new, out token_index_2, out row_index_2);

                //--------------- Replace new_token by cur_token -------------------------
                al.RemoveAt(token_index_1); al.Insert(token_index_1, new_token);

                //-------------- show new sentence in the datagridview ---------------------
                string sentence = string.Empty;

                for (int al_i = 0; al_i < al.Count; al_i++)
                {
                    sentence += " " + al[al_i].ToString();
                }
                //MessageBox.Show(dataGridView1.Rows[row_index_1].Cells[0].Value + " \r\n\r\n\r\n" + sentence);
                dataGridView1.Rows[row_index_1].Cells[0].Value = sentence;
                dataGridView1.Rows[row_index_1].DefaultCellStyle.BackColor = Color.DarkKhaki;            
        }


        char[] characters ={'q','w','e','r','t','y','u','i','o','p','a','s','d','f','g','h','j','k','l','z','x','c','v','b','n','m',
                              'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M','ض','ص',
                              'ث','ق','ف','غ','ع','ه','خ','ح','ج','چ','پ','ش','س','ی','ب','ل','ا','آ','ت','ن','م','ک','گ','ظ','ط','ز','ر',
                              'ذ','د','ئ'};
        private void button4_Click(object sender, EventArgs e)
        {            
        }

        private void incorrectSpell_Corpus()
        {
                ArrayList al;
                int token_index_1;
                int row_index_1;
                string cur_token = Select_Token_From_GridView(out al, out token_index_1, out row_index_1);
                //---------- change token -------------------

                int chars_index = Create_Random_index(characters.Length - 1, true);
                int inner_token_index = Create_Random_index(cur_token.Length - 1, true);

                char[] changed_token = cur_token.ToCharArray();
                changed_token[inner_token_index] = characters[chars_index];
                string new_token = string.Empty;
                foreach (char ch in changed_token)
                    new_token += ch.ToString();
                al[token_index_1] = new_token;
                //-------------- show new sentence in the datagridview ---------------------
                string sentence = string.Empty;

                for (int al_i = 0; al_i < al.Count; al_i++)
                {
                    sentence += " " + al[al_i].ToString();
                }
                //MessageBox.Show(dataGridView1.Rows[row_index_1].Cells[0].Value + " \r\n\r\n\r\n" + sentence);
                dataGridView1.Rows[row_index_1].Cells[0].Value = sentence;
                dataGridView1.Rows[row_index_1].DefaultCellStyle.BackColor = Color.Silver;           
        }

        private bool add_numberchanges()
        {
            bool _continue=true;
            numberChanges++;
            if (numberChanges > int.Parse(textBox1.Text))
            {
                MessageBox.Show("ظرفیت تغییرات پر شده است.");
                _continue = false;
            }
            return _continue;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            GRAMCLASS gc = new GRAMCLASS();
            if (MemoryClass.Instance.isEmptyTrigramList())
                gc.TriGram2(MemoryClass.Instance.Get_Sentences_From_OldCorpus(), true);
            

            string currentSentence = string.Empty;
            
            DataTable mytable = new DataTable();
            mytable.Columns.Add("Sentence", typeof(string));
            mytable.Columns.Add("Probability", typeof(double));

            DataRow row;

            double SumProbability = 0;

            for (int rowIndex = 0; rowIndex < (dataGridView1.Rows.Count - 1); rowIndex++)
            {
                currentSentence = dataGridView1[0, rowIndex].Value.ToString();
                ArrayList SS = gc.Seperator_Phase4(currentSentence, false);
                double PDF = 1;
                StructData_TriGram dataTrigram = new StructData_TriGram();
                for (int i = 2; i < SS.Count; i++)
                {
                    dataTrigram.CurrentToken = ((string)SS[i]).ToLower();
                    dataTrigram.PrevToken = ((string)SS[i - 1]).ToLower();
                    dataTrigram.PrevPrevToken = ((string)SS[i - 2]).ToLower();
                    if (MemoryClass.Instance.GetListTrigram.Exists(s => s.CurrentToken == dataTrigram.CurrentToken && s.PrevToken == dataTrigram.PrevToken && s.PrevPrevToken == dataTrigram.PrevPrevToken))
                    {
                        StructData_TriGram trigrm = MemoryClass.Instance.GetListTrigram[gc.ListTrigram.FindIndex(s => s.CurrentToken == dataTrigram.CurrentToken &&
                            s.PrevToken == dataTrigram.PrevToken && s.PrevPrevToken == dataTrigram.PrevPrevToken)];

                        int root = (trigrm.Probability != 1) ? trigrm.Probability.ToString().Remove(0, 2).Length : 1;
                        PDF *= Math.Pow(trigrm.Probability, (double)1 / SS.Count);
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

        private void button4_Click_1(object sender, EventArgs e)
        {
            GRAMCLASS gramclass = new GRAMCLASS();

            DataTable table = new DataTable();
            table.Columns.Add("sentence", typeof(string));
            table.Columns.Add("Probability", typeof(double));
            DataRow row;

            double SumProbability = 0;
            int CountOfTotalTokens = MemoryClass.Instance.Get_CountOfTotalTokens();

            string sentence = string.Empty;

            //foreach (string sentence in Regex_Patterns.Instance.GetSentenceList)
            for(int index_row=0; index_row < dataGridView1.Rows.Count-1; index_row++)
            {
                sentence = dataGridView1.Rows[index_row].Cells[0].Value.ToString();

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
                row = table.NewRow();
                row[0] = sentence;
                row[1] = PDF;
                SumProbability += (PDF);
                table.Rows.Add(row);
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(table);
            dataGridView1.DataSource = ds.Tables[0].AsDataView();
            lblAverageResult.Text = ((double)SumProbability / Regex_Patterns.Instance.GetSentenceList.Count).ToString();
        }
    }

}

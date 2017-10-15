using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace WindowsFormsApplication1
{
    struct StructData
    {
        public string CurrentToken;
        public string PrevToken;
        public int Count;
    }
    class mydata
    {
        public List<StructData> BiGram(string sentence)
        {
            List<StructData> list = new List<StructData>();                        
            ArrayList l = Seperator(sentence);
            for (int i = 1; i < l.Count; i++)
            {
                string curtoken = l[i].ToString();
                string prevtoken = l[i - 1].ToString();
                if (list.Exists(s => s.CurrentToken == curtoken && s.PrevToken==prevtoken))
                {
                    StructData SD = list[list.FindIndex(s => s.CurrentToken == curtoken && s.PrevToken==prevtoken)];
                    SD.Count += 1;
                    list[list.FindIndex(s => s.CurrentToken == curtoken && s.PrevToken == prevtoken)] = SD;
                }
                else
                {
                    StructData SD = new StructData();
                    SD.CurrentToken = curtoken;
                    SD.PrevToken = prevtoken;
                    SD.Count = 1;
                    list.Add(SD);
                }
            }
            return list;
        }

        public ArrayList Seperator(string sentence)
        {
            ArrayList al = new ArrayList();
            string token = string.Empty;

            al.Add("↨");
            foreach (char ch in sentence.ToCharArray())
            {
                switch (ch)
                {
                    case ' ':
                        if (!string.IsNullOrWhiteSpace(token.ToString()))
                            al.Add(token);
                        token = string.Empty;
                        break;
                    case '.':
                        if (!string.IsNullOrWhiteSpace(token))
                            al.Add(token);
                        al.Add(ch.ToString());
                        token = string.Empty;
                        break;
                    default:
                        if(!string.IsNullOrWhiteSpace(ch.ToString()))
                            token+=ch.ToString();
                        break;
                }
            }
            return al;
        }

        #region original design

        Hashtable Form_BiGram(string sentence)
        {
            Hashtable hash = new Hashtable();
            List<string> l = Form_Seperation(sentence);
            for (int i = 1; i < l.Count; i++)
            {
                string probability = ".[" + l[i] + " ▓ " + l[i - 1] + "].";
                if (hash.Contains(probability))
                    hash[probability] = int.Parse(hash[probability.ToLower()].ToString()) + 1;
                else
                    hash.Add(probability, 1);
            }
            return hash;
        }

        private List<string> Form_Seperation(string sentence)
        {
            List<string> sentence_seperated = new List<string>();
            string token = string.Empty;

            sentence_seperated.Add("♀");

            foreach (char ch in sentence.ToCharArray())
            {
                if (ch == ' ' && token != string.Empty)
                {
                    sentence_seperated.Add(token);
                    token = string.Empty;
                }
                else if (ch == '.')
                {
                    if (token != string.Empty)
                        sentence_seperated.Add(token);
                    sentence_seperated.Add(ch.ToString());
                    token = string.Empty;
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(ch.ToString()))
                        token += ch.ToString();
                }
            }

            return sentence_seperated;
        }

        #endregion
    }
}

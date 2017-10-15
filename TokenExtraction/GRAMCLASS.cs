using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace TokenExtraction1
{
    public struct StructData_BiGram
    {
        public string CurrentToken;
        public string PrevToken;
        public int Count;
        public double Probability;
    }

    public struct StructData_TriGram
    {
        public string CurrentToken;
        public string PrevToken;
        public string PrevPrevToken;
        public int Count;
        public double Probability;
    }
    class GRAMCLASS
    {
        public List<StructData_BiGram> ListBigram = null;

        public List<StructData_TriGram> ListTrigram = null;

        #region Don't Correct Ngram
        public List<StructData_BiGram> BiGram(string sentence)
        {
            List<StructData_BiGram> list = new List<StructData_BiGram>();                        
            ArrayList l = Seperator(sentence,false);
            for (int i = 1; i < l.Count; i++)
            {
                string curtoken = l[i].ToString().ToLower();
                string prevtoken = l[i - 1].ToString().ToLower();
                if (list.Exists(s => s.CurrentToken == curtoken && s.PrevToken==prevtoken))
                {
                    StructData_BiGram SD = list[list.FindIndex(s => s.CurrentToken == curtoken && s.PrevToken==prevtoken)];
                    SD.Count += 1;
                    SD.Probability = (double)SD.Count / Regex_Patterns.Instance.GetTotalTokens_count();
                    list[list.FindIndex(s => s.CurrentToken == curtoken && s.PrevToken == prevtoken)] = SD;
                }
                else
                {
                    StructData_BiGram SD = new StructData_BiGram();
                    SD.CurrentToken = curtoken;
                    SD.PrevToken = prevtoken;
                    SD.Count = 1;
                    SD.Probability = (double)1 / Regex_Patterns.Instance.GetTotalTokens_count();
                    list.Add(SD);
                }
            }
            return list;
        }

        public List<StructData_TriGram> TriGram(string sentence)
        {
            List<StructData_TriGram> list = new List<StructData_TriGram>();
            ArrayList l = Seperator(sentence, true);
            for (int i = 2; i < l.Count; i++)
            {
                string curtoken = l[i].ToString();
                string prevtoken = l[i - 1].ToString().ToLower();
                string prevprevtoken = l[i - 2].ToString().ToLower();

                if (list.Exists(s => s.CurrentToken == curtoken && s.PrevToken == prevtoken && s.PrevPrevToken == prevprevtoken))
                {
                    StructData_TriGram SD = list[list.FindIndex(s => s.CurrentToken == curtoken && s.PrevToken == prevtoken && s.PrevPrevToken == prevprevtoken)];
                    SD.Count += 1;
                    SD.Probability = (double)SD.Count / Regex_Patterns.Instance.GetTotalTokens_count();
                    list[list.FindIndex(s => s.CurrentToken == curtoken && s.PrevToken == prevtoken && s.PrevPrevToken == prevprevtoken)] = SD;
                }
                else
                {
                    StructData_TriGram SD = new StructData_TriGram();
                    SD.CurrentToken = curtoken;
                    SD.PrevToken = prevtoken;
                    SD.PrevPrevToken = prevprevtoken;
                    SD.Count = 1;
                    SD.Probability = (double)1 / Regex_Patterns.Instance.GetTotalTokens_count();
                    list.Add(SD);
                }
            }
            return list;
        }
        #endregion

        #region Ngram Corrected
        public List<StructData_BiGram> BiGram2(string sentence,bool isPhase4)
        {
            List<StructData_BiGram> list = new List<StructData_BiGram>();
            ArrayList l = Seperator(sentence, false);
            for (int i = 1; i < l.Count; i++)
            {
                string curtoken = l[i].ToString().ToLower();
                string prevtoken = l[i - 1].ToString().ToLower();
                if (list.Exists(s => s.CurrentToken == curtoken && s.PrevToken == prevtoken))
                {
                    StructData_BiGram SD = list[list.FindIndex(s => s.CurrentToken == curtoken && s.PrevToken == prevtoken)];
                    SD.Count += 1;
                    if (isPhase4)
                        SD.Probability = (double)SD.Count / MemoryClass.Instance.Get_CountOfSpecialToken(prevtoken);
                    else
                        SD.Probability = (double)SD.Count / Regex_Patterns.Instance.Get_CountOfSpecialToken(prevtoken);

                    //SD.Probability = (double)SD.Count / Regex_Patterns.Instance.Get_CountOfSpecialToken(prevtoken);

                    list[list.FindIndex(s => s.CurrentToken == curtoken && s.PrevToken == prevtoken)] = SD;// Add to Count for exist item
                }
                else
                {
                    StructData_BiGram SD = new StructData_BiGram();
                    SD.CurrentToken = curtoken;
                    SD.PrevToken = prevtoken;
                    SD.Count = 1;
                    
                    if (isPhase4)
                        SD.Probability = (double)1 / MemoryClass.Instance.Get_CountOfSpecialToken(prevtoken);
                    else
                        SD.Probability = (double)1 / Regex_Patterns.Instance.Get_CountOfSpecialToken(prevtoken);

                    list.Add(SD); //Add new item to list
                }
            }
            ListBigram = list;
            MemoryClass.Instance.FillBigramList(list);
            return list;
        }

        public List<StructData_TriGram> TriGram2(string sentence,bool isPhase4)
        {
            List<StructData_TriGram> list = new List<StructData_TriGram>();
            if (MemoryClass.Instance.GetListBigram == null)
                MemoryClass.Instance.FillBigramList(BiGram2(sentence,false));
           
            ArrayList l = Seperator(sentence, true);
            for (int i = 2; i < l.Count; i++)
            {
                string curtoken = l[i].ToString().ToLower();
                string prevtoken = l[i - 1].ToString().ToLower();
                string prevprevtoken = l[i - 2].ToString();

                if (list.Exists(s => s.CurrentToken == curtoken && s.PrevToken == prevtoken && s.PrevPrevToken == prevprevtoken))
                {                    
                    StructData_TriGram SD = list[list.FindIndex(s => s.CurrentToken == curtoken && s.PrevToken == prevtoken && s.PrevPrevToken == prevprevtoken)];
                    
                    SD.Count += 1;
                    if (isPhase4)
                        SD.Probability = (double)SD.Count / MemoryClass.Instance.Get_CountOfSpecialToken(prevtoken);
                    else
                        SD.Probability = (double)SD.Count / Regex_Patterns.Instance.Get_CountOfSpecialToken(prevtoken);

                    /*if (MemoryClass.Instance.GetListBigram.Exists(s => s.CurrentToken == SD.PrevToken && s.PrevToken == SD.PrevPrevToken))
                    {
                        SD.Probability = (double)SD.Count / MemoryClass.Instance.GetListBigram[MemoryClass.Instance.GetListBigram.FindIndex(s => s.CurrentToken == SD.PrevToken && s.PrevToken == SD.PrevPrevToken)].Count;
                    }*/
                    list[list.FindIndex(s => s.CurrentToken == curtoken && s.PrevToken == prevtoken && s.PrevPrevToken == prevprevtoken)] = SD;
                }
                else
                {
                    StructData_TriGram SD = new StructData_TriGram();
                    SD.CurrentToken = curtoken;
                    SD.PrevToken = prevtoken;
                    SD.PrevPrevToken = prevprevtoken;
                    SD.Count = 1;
                    if (isPhase4)
                        SD.Probability = (double)SD.Count / MemoryClass.Instance.Get_CountOfSpecialToken(prevtoken);
                    else
                        SD.Probability = (double)SD.Count / Regex_Patterns.Instance.Get_CountOfSpecialToken(prevtoken);
                    /*if (MemoryClass.Instance.GetListBigram.Exists(s => s.CurrentToken == SD.PrevToken && s.PrevToken == SD.PrevPrevToken))
                        SD.Probability = (double)SD.Count / MemoryClass.Instance.GetListBigram[MemoryClass.Instance.GetListBigram.FindIndex(s => s.CurrentToken == SD.PrevToken && s.PrevToken == SD.PrevPrevToken)].Count;*/

                    list.Add(SD);
                }
            }
            ListTrigram = list;
            MemoryClass.Instance.FillTrigramList(list);
            return list;
        }
        #endregion
       

        public double Verify_Sentence(ArrayList sentence,object nGram, bool isTrigram)
        {
            double probability = 1;
            //-------------------------------------------
            List<StructData_BiGram> bigramList=new List<StructData_BiGram>();
            List<StructData_TriGram> trigramList=new List<StructData_TriGram>();
            if (isTrigram)
                trigramList = (List<StructData_TriGram>)nGram;
            else
                bigramList = (List<StructData_BiGram>)nGram;
            //---------------------------------------------

            if (isTrigram)
            {
                for (int i = 2; i < sentence.Count; i++)
                {

                    StructData_TriGram sdt = new StructData_TriGram();
                    sdt.CurrentToken = sentence[i].ToString().ToLower();
                    sdt.PrevToken = sentence[i - 1].ToString().ToLower();
                    sdt.PrevPrevToken = sentence[i - 2].ToString().ToLower();
                    if (trigramList.Exists(s => s.CurrentToken == sdt.CurrentToken && s.PrevToken == sdt.PrevToken && s.PrevPrevToken==sdt.PrevPrevToken))
                    {
                        StructData_TriGram b = trigramList.Find(s => s.CurrentToken == sdt.CurrentToken && s.PrevToken == sdt.PrevToken && s.PrevPrevToken == sdt.PrevPrevToken);
                        
                        double prob = (double)b.Probability;
                        probability =Math.Log10( (double)probability + (double)(-Math.Log10(prob)));
                    }
                    else
                        return 0;
                }
            }
            else
            {
                for (int i = 1; i < sentence.Count; i++)
                {
                    StructData_BiGram sdb = new StructData_BiGram();
                    sdb.CurrentToken = sentence[i].ToString().ToLower();
                    sdb.PrevToken = sentence[i - 1].ToString().ToLower();
                    if (bigramList.Exists(s=>s.CurrentToken==sdb.CurrentToken && s.PrevToken==sdb.PrevToken))
                    {
                        StructData_BiGram b = bigramList.Find(s => s.CurrentToken == sdb.CurrentToken && s.PrevToken == sdb.PrevToken);
                        double prob = (double)b.Probability;
                        probability =Math.Log10((double)probability+ (double)(-Math.Log10(prob)));
                    }
                    else
                        return 0;
                }
            }
            return (double)probability;
        }

        public ArrayList Seperator(string sentence,bool isTriGram)
        {
            ArrayList al = new ArrayList();
            string token = string.Empty;
            if (isTriGram)
                al.Add("↨");

            al.Add("↨");
            foreach (char ch in sentence.ToCharArray())
            {
                switch (Regex_Patterns.Instance.GetMarkType(ch.ToString()))
                {
                    case TypeMark.Sign:
                        if (!string.IsNullOrWhiteSpace(token.ToString()))
                            al.Add(token);
                        token = string.Empty;
                        break;
                    case TypeMark.EndMark:
                        if (!string.IsNullOrWhiteSpace(token))
                            al.Add(token);
                        al.Add(ch.ToString());
                        token = string.Empty;
                        break;
                    default:
                        if (!string.IsNullOrWhiteSpace(ch.ToString()))
                            token += ch.ToString();
                        break;
                }
            }
            return al;
        }
           
        public ArrayList Seperator_Phase4(string sentence, bool isTriGram)
        {
            ArrayList al = new ArrayList();
            string token = string.Empty;
            
            for(int i=0;i<sentence.Length;i++) 
            {
                char ch=sentence[i];
                switch (Regex_Patterns.Instance.GetMarkType(ch.ToString()))
                {
                    case TypeMark.Sign:
                        if (ch == '↨' || !string.IsNullOrWhiteSpace(token.ToString()))
                            al.Add(ch=='↨'?"↨":token);
                        token = string.Empty;
                        break;
                    case TypeMark.EndMark:
                        if (!string.IsNullOrWhiteSpace(token))
                            al.Add(token);
                        al.Add(ch.ToString());
                        token = string.Empty;
                        break;
                    default:
                        if (!string.IsNullOrWhiteSpace(ch.ToString()))
                            token += ch.ToString();
                        break;
                }
            }
            return al;
        }

        public ArrayList Seperator_ChangeCorpus(string sentence)
        {
            ArrayList al = new ArrayList();
            string token = string.Empty;

            for (int i = 0; i < sentence.Length; i++)
            {
                char ch = sentence[i];
                switch (Regex_Patterns.Instance.GetMarkType(ch.ToString()))
                {
                    case TypeMark.Sign:
                        if (ch == '↨' || !string.IsNullOrWhiteSpace(token.ToString()))
                            al.Add(ch == '↨' ? "↨" : token);
                        
                            if (!string.IsNullOrWhiteSpace(ch.ToString()))
                            al.Add(ch.ToString());  
                        token = string.Empty;
                        break;
                    case TypeMark.EndMark:
                        if (!string.IsNullOrWhiteSpace(token))
                            al.Add(token);
                        al.Add(ch.ToString());
                        token = string.Empty;
                        break;
                    default:
                        if (!string.IsNullOrWhiteSpace(ch.ToString()))
                            token += ch.ToString();
                        break;
                }
            }
            return al;
        }
    }
}
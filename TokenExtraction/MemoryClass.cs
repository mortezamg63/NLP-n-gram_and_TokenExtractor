using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TokenExtraction1
{
    public class MemoryClass
    {
        public static MemoryClass Instance;
        static MemoryClass()
        {
            Instance = new MemoryClass();
        }
        
        List<StructData_BiGram> ListBigram = null;
        List<StructData_TriGram> ListTrigram = null;
        ArrayList Old_Corpus = new ArrayList();
        private Hashtable WordList = new Hashtable();
        private Hashtable DigitList = new Hashtable();
        private Hashtable EndMarkList = new Hashtable();
        private Hashtable SignList = new Hashtable();

        public void Fill_SignList(Hashtable hash)
        {
            SignList = hash;
        }
        public void Fill_EndMarkList(Hashtable hash)
        {
            EndMarkList = hash;
        }
        public void Fill_DigitList(Hashtable hash)
        {
            DigitList = hash;
        }
        public void Fill_wordList(Hashtable hash)
        {
            WordList = hash;
        }
        public void Fill_oldCorpus(ArrayList corpus)
        {
            Old_Corpus = corpus;
        }
        
        public void FillBigramList(List<StructData_BiGram> bigram)
        {            
            ListBigram=bigram;
        }

        public void FillTrigramList(List<StructData_TriGram> trigram)
        {         
            ListTrigram = trigram;
        }

        public bool IsEmptyBigramList()
        {
            if (ListBigram == null || ListBigram.Count==0)
                return true;
            return false;
        }
        public bool isEmptyTrigramList()
        {
            if (ListTrigram == null || ListTrigram.Count==0)
                return true;
            return false;
        }
        public List<StructData_TriGram> GetListTrigram
        {
            get
            {
                return ListTrigram;
            }
        }

        public List<StructData_BiGram> GetListBigram
        {
            get
            {
                return ListBigram;
            }
        }

        public ArrayList Get_OldCorpus
        {
            get
            {
                return Old_Corpus;
            }
        }

        public Hashtable Get_WordList
        {
            get
            {
                return WordList;
            }
        }
        public Hashtable Get_DigitList
        {
            get
            {
                return DigitList;
            }
        }
        public Hashtable Get_SignList
        {
            get
            {
                return SignList;
            }
        }
        public Hashtable Get_EndMarkList
        {
            get
            {
                return EndMarkList;
            }
        }


        public int Get_CountOfSpecialToken(string SrchToken)
        {
            //int count=0;
            if (DigitList.Contains(SrchToken))
                return int.Parse(DigitList[SrchToken].ToString());
            else if (WordList.Contains(SrchToken.ToLower()))
                return int.Parse(WordList[SrchToken.ToLower()].ToString());
            else if (EndMarkList.Contains(SrchToken))
                return int.Parse(EndMarkList[SrchToken].ToString());
            else if (SrchToken == "↨")
                return 1;
            else
                return int.Parse(SignList[SrchToken].ToString());
        }

        public int Get_CountOfSpecialToken_Unigram(string SrchToken)
        {
            if (DigitList.Contains(SrchToken))
                return int.Parse(DigitList[SrchToken].ToString());
            else if (WordList.Contains(SrchToken.ToLower()))
                return int.Parse(WordList[SrchToken.ToLower()].ToString());
            else if (EndMarkList.Contains(SrchToken))
                return int.Parse(EndMarkList[SrchToken].ToString());
            else if (SrchToken == "↨")
                return 1;
            else if (SignList.Contains(SrchToken))
                return int.Parse(SignList[SrchToken].ToString());
            else return -1;
        }

        public int Get_CountOfTotalTokens()
        {
            int count = 0;
            foreach (object key in WordList.Keys)
                count += int.Parse(WordList[key].ToString());

            foreach (object key in SignList.Keys)
                count += int.Parse(SignList[key].ToString());

            foreach (object key in EndMarkList.Keys)
                count += int.Parse(EndMarkList[key].ToString());

            foreach (object key in DigitList.Keys)
                count += int.Parse(DigitList[key].ToString());


            return count;
        }

        public string Get_Sentences_From_OldCorpus()
        {
            string sentences = string.Empty;
            foreach (string sentence in Old_Corpus)
                sentences += sentence;
            return sentences;
        }

        public void ClearAll()
        {
            Old_Corpus.Clear();
            SignList.Clear();
            WordList.Clear();
            EndMarkList.Clear();
            DigitList.Clear();
            ListBigram = null;
            ListTrigram = null;
        }
    }
}

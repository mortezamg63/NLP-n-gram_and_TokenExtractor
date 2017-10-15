using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

namespace TokenExtraction1
{
    public enum TypeMark
    {
        Word,
        Digit,
        EndMark,
        Sign,
        Null
    }
    public class Regex_Patterns
    {
        public static Regex_Patterns Instance;
        static Regex_Patterns()
        {
            Instance = new Regex_Patterns();

        }
        public string Tape;
        public int HeadTape;
        private Hashtable WordList = new Hashtable();
        private Hashtable DigitList = new Hashtable();
        private Hashtable EndMarkList = new Hashtable();
        private Hashtable SignList = new Hashtable();
        private ArrayList SentenceList = new ArrayList();
        private string CurrentSentence;

        public string GetCurrentSentence
        {
            get
            {
                return CurrentSentence;
            }
        }

        public void ClearCurrentSentence()
        {
            CurrentSentence = string.Empty;
        }

        public ArrayList GetSentenceList
        {
            get
            {
                return SentenceList;
            }
        }

        public void ClearSentenceList()
        {
            SentenceList.Clear();
        }

        public Hashtable GetWordList
        {
            get
            {
                return WordList;
            }
        }

        public void ClearWordList()
        {
            WordList.Clear();
        }

        public Hashtable GetDigitList
        {
            get
            {
                return DigitList;
            }
        }

        public void ClearDigitList()
        {
            DigitList.Clear();
        }

        public Hashtable GetEndMarkList
        {
            get
            {
                return EndMarkList;
            }
        }

        public void ClearEndMarkList()
        {
            EndMarkList.Clear();
        }

        public Hashtable GetSignList
        {
            get
            {
                return SignList;
            }
        }

        public void ClearSignList()
        {
            SignList.Clear();
        }


        public string CurrentToken;

        #region Segmentation characters
        private Regex WordMarks = new Regex(@"([a-z|A-Z]|([ض|ص|ث|ق|ف|غ|ع|ه|خ|ح|ج|چ|پ|گ|ک|م|ن|ت|ا|ل|ب|ی|س|ش|ظ|ط|ز|ر|ذ|د|ئ|و|آ|ژ|ي|ۀ|ء|أ|ة|ك|ؤ|ى|]))");
       // private Regex WordMarks = new Regex(@"[ض|ص|ث|ق|ف|غ|ع|ه|خ|ح|ج|چ|پ|گ|ک|م|ن|ت|ا|ل|ب|ی|س|ش|ظ|ط|ز|ر|ذ|د|ئ|ژ|آ|و]");
        private Regex DigitMarks = new Regex(@"[0-9۰-۹]");
        private Regex EndMarks = new Regex(@"[؟|?|;|!|.]");
        #endregion

        public void AddTOCurrentToken(string ch)
        {
            CurrentToken += ch;
        }
        public void AddSign(string str)
        {
            if (!String.IsNullOrWhiteSpace(str))
            {
                if (SignList.Contains(str.ToLower()))
                    SignList[str.ToLower()] = int.Parse(SignList[str.ToLower()].ToString()) + 1;
                else
                    SignList.Add(str.ToLower(), 1);

            }

        }
        public void AddWord(string str, string sign)
        {
            if (!String.IsNullOrWhiteSpace(str))
            {
                if (WordList.Contains(str.ToLower()))
                    WordList[str.ToLower()] = int.Parse(WordList[str.ToLower()].ToString()) + 1;
                else
                    WordList.Add(str.ToLower(), 1);                
            }
            AddCurrentSentence(CurrentToken + sign);
            CurrentToken = "";
        }

        public void AddDigit(string str, string sign)
        {
            if (!String.IsNullOrWhiteSpace(str))
            {
                if (DigitList.Contains(str.ToLower()))
                    DigitList[str.ToLower()] = int.Parse(DigitList[str.ToLower()].ToString()) + 1;
                else
                    DigitList.Add(str.ToLower(), 1);

            }
            AddCurrentSentence(CurrentToken + sign);
            CurrentToken = "";
        }

        public void AddEndMark(string str)
        {
            if (!String.IsNullOrWhiteSpace(str))
            {
                if (EndMarkList.Contains(str.ToLower()))
                    EndMarkList[str.ToLower()] = int.Parse(EndMarkList[str.ToLower()].ToString()) + 1;
                else
                    EndMarkList.Add(str.ToLower(), 1);

            }
            //CurrentToken = "";
        }
        public void AddCurrentSentence(string ch)
        {
            //if(ch!=".")
                CurrentSentence += ch;
        }

        public void AddSentenceList()
        {
            if(CurrentSentence.Length>10 && !string.IsNullOrWhiteSpace(CurrentSentence) && CurrentSentence!="")
                SentenceList.Add(CurrentSentence);
            CurrentSentence = string.Empty;
        }

        public TypeMark GetMarkType(string ch)
        {
            if (!String.IsNullOrEmpty(ch))
            {
                if (ch == "None")
                    return TypeMark.Null;
                else if (WordMarks.Match(ch).Success)
                    return TypeMark.Word;
                else if (DigitMarks.Match(ch).Success)
                    return TypeMark.Digit;
                else if (EndMarks.Match(ch).Success)
                    return TypeMark.EndMark;
                else return TypeMark.Sign;
            }
            else
                return TypeMark.Null;
        }
        public string ReadTape()
        {
            try
            {
                string chr = Tape[++HeadTape].ToString();
                //char ch = Tape[HeadTape];
                chr = (chr == "ي" || chr == "ى") ? "ی" : chr;
                chr = (chr == "ك") ? "ک" : chr;
                chr = (chr == "ة") ? "ه" : chr;
                chr = chr == "٠" ? "0" : chr;
                chr = chr == "١" ? "1" : chr;
                chr = chr == "٢" ? "2" : chr;
                chr = chr == "٣" ? "3" : chr;
                chr = chr == "۴" ? "4" : chr;
                chr = chr == "۵" ? "5" : chr;
                chr = chr == "۶" ? "6" : chr;
                chr = chr == "٧" ? "7" : chr;
                chr = chr == "٨" ? "8" : chr;
                chr = chr == "٩" ? "9" : chr;
                return chr.ToLower();
            }
            catch
            {
                return "None";
            }
        }

        public string ReadTapeNoMove()
        {
            return Tape[HeadTape].ToString();
        }
        public void Back_HeadTape()
        {
            HeadTape--;
        }

        public int GetTotalTokens_count()
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

        public int Get_CountOfSpecialToken(string SrchToken)
        {
            //int count=0;
            if (GetDigitList.Contains(SrchToken))
                return int.Parse(GetDigitList[SrchToken].ToString());
            else if (GetWordList.Contains(SrchToken.ToLower()))
                return int.Parse(GetWordList[SrchToken.ToLower()].ToString());
            else if (GetEndMarkList.Contains(SrchToken))
                return int.Parse(GetEndMarkList[SrchToken].ToString());
            else if (SrchToken == "↨")
                return 1;
            else if (GetSignList.Contains(SrchToken))
                return int.Parse(GetSignList[SrchToken].ToString());
            else
                return 0;
        }

        public string Get_Corpus()
        {
            string sentences = string.Empty;
            foreach (string sentence in SentenceList)
                sentences += sentence;
            return sentences;
        }
    }
}

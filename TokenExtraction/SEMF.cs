using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TokenExtraction1
{
    class SEMF
    {
        public void Compute(string sender)
        {            

            switch (sender)
            {
                case "SD":
                    Regex_Patterns.Instance.AddDigit(Regex_Patterns.Instance.CurrentToken, Regex_Patterns.Instance.ReadTapeNoMove());
                    Regex_Patterns.Instance.AddSentenceList();
                    break;
                case "SW":
                    Regex_Patterns.Instance.AddWord(Regex_Patterns.Instance.CurrentToken, Regex_Patterns.Instance.ReadTapeNoMove());
                    Regex_Patterns.Instance.AddSentenceList();
                    break;
                case "S0":
                    Regex_Patterns.Instance.AddSentenceList();
                    break;
            }            
        }
    }
}

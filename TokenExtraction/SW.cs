using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TokenExtraction1
{
    class SW
    {
        public void Compute()
        {
            string ch = Regex_Patterns.Instance.ReadTape();            

            switch (Regex_Patterns.Instance.GetMarkType(ch))
            {
                case TypeMark.Digit:
                case TypeMark.Word:
                    Regex_Patterns.Instance.AddTOCurrentToken(ch);
                    Compute();
                    break;               
                case TypeMark.Sign:
                    Regex_Patterns.Instance.AddSign(ch);
                    (new SWF()).Compute();
                    break;
                case TypeMark.EndMark:
                    Regex_Patterns.Instance.AddEndMark(ch);
                    (new SEMF()).Compute("SW");
                    break;
                default:
                    Regex_Patterns.Instance.AddWord(Regex_Patterns.Instance.CurrentToken, string.Empty);
                    break;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TokenExtraction1
{
    class SD
    {
        public void Compute()
        {
            string ch = Regex_Patterns.Instance.ReadTape();            
            
            switch (Regex_Patterns.Instance.GetMarkType(ch))
            {
                case TypeMark.Digit:
                    Regex_Patterns.Instance.AddTOCurrentToken(ch);
                    Compute();
                    break;
                case TypeMark.Word:
                    Regex_Patterns.Instance.AddTOCurrentToken(ch);
                    (new SW()).Compute();
                    break;
                case TypeMark.Sign:
                    Regex_Patterns.Instance.AddSign(ch);
                    (new SDF()).Compute();
                    break;
                case TypeMark.EndMark:
                    Regex_Patterns.Instance.AddEndMark(ch);
                    (new SEMF()).Compute("SD");
                    break;
                default:
                    Regex_Patterns.Instance.AddDigit(Regex_Patterns.Instance.CurrentToken,string.Empty);
                    break;
            }
        }
    }
}

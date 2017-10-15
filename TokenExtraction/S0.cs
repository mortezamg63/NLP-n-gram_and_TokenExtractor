using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TokenExtraction1
{
    class S0
    {
        public void Compute()
        {
            string ch = Regex_Patterns.Instance.ReadTape();
                                       
           switch(Regex_Patterns.Instance.GetMarkType(ch))
           {
               case TypeMark.Digit:
                   Regex_Patterns.Instance.AddTOCurrentToken(ch);
                   (new SD()).Compute();
                   break;
               case TypeMark.Word:
                   Regex_Patterns.Instance.AddTOCurrentToken(ch);
                   (new SW()).Compute();
                   break;
               case TypeMark.EndMark:
                   Regex_Patterns.Instance.AddEndMark(ch);
                   Regex_Patterns.Instance.AddCurrentSentence(ch);
                   (new SEMF()).Compute("S0");
                   break;
               case TypeMark.Sign:
                   Regex_Patterns.Instance.AddSign(ch);
                   if(ch!=" ")
                        Regex_Patterns.Instance.AddCurrentSentence(ch);

                   (new SSF()).Compute();
                   break;
               case TypeMark.Null:
                   break;
           }
        }
    }
}

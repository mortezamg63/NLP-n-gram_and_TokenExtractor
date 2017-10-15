using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TokenExtraction1
{
    class SSF
    {
        public void Compute()
        {            
            string ch = Regex_Patterns.Instance.ReadTape();            

            switch(Regex_Patterns.Instance.GetMarkType(ch))
            {               
                case TypeMark.Sign:
                    Regex_Patterns.Instance.AddSign(ch);
                    Compute();
                    break;
                default:
                    Regex_Patterns.Instance.Back_HeadTape();
                    break;
            }            
        }
    }
}

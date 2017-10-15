using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TokenExtraction1
{
    class SDF
    {
        public void Compute()
        {
            Regex_Patterns.Instance.AddDigit(Regex_Patterns.Instance.CurrentToken,Regex_Patterns.Instance.ReadTapeNoMove());                        
        }
    }
}

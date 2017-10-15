using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TokenExtraction1
{
    class SWF
    {
        public void Compute()
        {
            Regex_Patterns.Instance.AddWord(Regex_Patterns.Instance.CurrentToken, Regex_Patterns.Instance.ReadTapeNoMove());
        }
    }
}

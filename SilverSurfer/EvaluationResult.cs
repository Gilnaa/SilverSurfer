using SilverSurferLib.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverSurferLib
{
    public class EvaluationResult
    {
        public class VoidResult
        {
            internal VoidResult()
            {
            }
            public override string ToString()
            {
                return string.Empty;
            }
        }
        private static VoidResult @void = new VoidResult();
        public static VoidResult Void
        {
            get
            {
                return @void;
            }
        }
    }
    public class TokenResult
    {
        public Token RootToken { get; set; }
        public override string ToString()
        {
            return RootToken.ToString();
        }
    }
    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverSurferLib.Tokens
{
    public abstract class Token
    {
        internal abstract double Evaluate(Evaluator evaluator);
        public abstract Token Minify(Evaluator evaluator);
        public abstract IEnumerable<string> Variables { get; }
    }
}

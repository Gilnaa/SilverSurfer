using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverSurferLib.Tokens
{
    public class ConstantToken : Token
    {
        public double Value { get; set; }
        public ConstantToken()
        {
            Value = 0.0;
        }
        public ConstantToken(double value)
        {
            Value = value;
        }
        public override double Evaluate(Evaluator evaluator)
        {
            return Value;
        }

        public override Token Minify(Evaluator evaluator)
        {
            return this;
        }
        public override string ToString()
        {
            return Value.ToString();
        }
        public override IEnumerable<string> Variables
        {
            get { yield break; }
        }
    }
}

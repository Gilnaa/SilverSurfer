using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverSurferLib.Tokens
{
    public class FunctionToken : Token
    {
        public string FunctionName { get; set; }
        public Token[] Arguments { get; set; }
        public override double Evaluate(Evaluator evaluator)
        {
            FunctionCallback function;
            bool success = evaluator.Functions.TryGetValue(FunctionName, out function);
            if (!success)
                throw new EvaluationException(string.Format("Could not find a function named `{0}`.", FunctionName), this);
            return function(this, evaluator);
        }

        public override Token Minify(Evaluator evaluator)
        {
            Arguments = Arguments.Select(arg => arg.Minify(evaluator)).ToArray();
            if (!Arguments.All(arg => arg is ConstantToken))
                return this;
            return new ConstantToken(Evaluate(evaluator));
        }
        public override string ToString()
        {
            return string.Format("{0}({1})",
                FunctionName,
                string.Join(", ", (IEnumerable<Token>)Arguments)
                );
        }
        public override IEnumerable<string> Variables
        {
            get 
            {
                foreach (Token arg in Arguments)
                    foreach (string variable in arg.Variables)
                        yield return variable;
                yield break;
            }
        }
    }
}

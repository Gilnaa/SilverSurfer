using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverSurferLib.Tokens
{
    public class VariableToken : Token
    {
        public string VariableName { get; set; }
        internal override double Evaluate(Evaluator evaluator)
        {
            double result;
            bool success = evaluator.Variables.TryGetValue(VariableName, out result);
            
            if (!success)
            {
                if (evaluator.Settings.ReplaceMissingVariables)
                    return evaluator.Settings.MissingVariablesReplacement;
                throw new EvaluationException(string.Format("Could not find a variable named `{0}`.", VariableName), this);
            }
            return result;
        }

        public override Token Minify(Evaluator evaluator)
        {
            return this;
        }
        public override string ToString()
        {
            return VariableName;
        }
        public override IEnumerable<string> Variables
        {
            get { yield return VariableName; yield break; }
        }
    }
}

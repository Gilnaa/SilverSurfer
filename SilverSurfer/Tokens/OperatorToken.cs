using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverSurferLib.Tokens
{
    public class OperatorToken : Token
    {
        public Token LeftSideOperand { get; set; }
        public Token RightSideOperand { get; set; }
        public Operators Operator { get; set; }

        public override double Evaluate(Evaluator evaluator)
        {
            if (Operator == Operators.Set)
            {
                VariableToken variable = LeftSideOperand as VariableToken;
                if (variable == null)
                    throw new EvaluationException(
                        string.Format("Could assign a value to a token of type `{0}`.", 
                        LeftSideOperand.GetType().Name),
                        this);
                double result = RightSideOperand.Evaluate(evaluator);
                evaluator.Variables[variable.VariableName] = result;
                return result;
            }

            double a, b;
            a = LeftSideOperand.Evaluate(evaluator);
            b = RightSideOperand.Evaluate(evaluator);
            switch (Operator)
            {
                case Operators.Addition:
                    return a + b;
                case Operators.Substraction:
                    return a - b;
                case Operators.Multiplication:
                    return a * b;
                case Operators.Division:
                    return a / b;
                case Operators.Carry:
                    return a % b;
                case Operators.Exponentiation:
                    return Math.Pow(a, b);
                default:
                    throw new Exception();
            }
        }

        public override Token Minify(Evaluator evaluator)
        {
            LeftSideOperand = LeftSideOperand.Minify(evaluator);
            RightSideOperand = RightSideOperand.Minify(evaluator);
            if (LeftSideOperand is ConstantToken && RightSideOperand is ConstantToken)
            {
                return new ConstantToken(Evaluate(evaluator));
            }
            return this;
        }
        public static Operators GetOperator(char op)
        {
            return (Operators)op;
        }
        public enum Operators
        {
            Addition = '+',
            Substraction = '-',
            Multiplication = '*',
            Division = '/',
            Exponentiation = '^',
            Carry = '%',
            Set = '='
        }
        public override string ToString()
        {
            return string.Format("{0} {1} {2}", LeftSideOperand, (char)Operator, RightSideOperand);
        }
        public override IEnumerable<string> Variables
        {
            get 
            {
                foreach (string v in LeftSideOperand.Variables)
                    yield return v;
                foreach (string v in RightSideOperand.Variables)
                    yield return v;
                yield break;
            }
        }
    }
}

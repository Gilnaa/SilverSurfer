//
//  Evaluator.cs
//
//  Author:
//       Gilad Naaman <gilad.doom@gmail.com>
//
//  Copyright (c) 2013 Gilad Naaman
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Collections.ObjectModel;
using SilverSurferLib.Tokens;

namespace SilverSurferLib
{
    public delegate double FunctionCallback(FunctionToken func, Evaluator evaluator);
    /// <summary>
    /// A mathematical expression evaluator.
    /// </summary>
    public class Evaluator
    {
        /// <summary>
        /// Gets a dictionary of functions to use when evaluating.
        /// </summary>
        public Dictionary<string, FunctionCallback> Functions { get; private set; }

        /// <summary>
        /// Gets a dictionary of variables to use when evaluating.
        /// </summary>
        public Dictionary<string, double> Variables { get; private set; }

        /// <summary>
        /// Gets the `Settings` instance used by the evaluation process.
        /// </summary>
        /// <value>
        /// A `Settings` object.
        /// </value>
        public Settings Settings { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverSurfer.Evaluator"/> class,
        /// initializing <see>Functions</see> and <see>Variables</see> to default values.
        /// </summary>
        public Evaluator()
        {
            Functions = new Dictionary<string, FunctionCallback>
            {
                {"abs", DefaultFunctions.Abs},
                {"sin", DefaultFunctions.Sin},
                {"cos", DefaultFunctions.Cos},
                {"tan", DefaultFunctions.Tan},
                {"asin", DefaultFunctions.Asin},
                {"acos", DefaultFunctions.Acos},
                {"atan", DefaultFunctions.Atan},
                {"min", DefaultFunctions.Min},
                {"max", DefaultFunctions.Max},
                {"log", DefaultFunctions.Log},
                {"log10", DefaultFunctions.Log10},
            };
            Variables = new Dictionary<string, double>
            {
                {"e", Math.E},
                {"pi", Math.PI}
            };
            Settings = new Settings();
        }
        /// <summary>
        /// Evaluates the specified expression using this object's data.
        /// </summary>
        /// <param name='e'>
        /// The expression to evaluate.
        /// </param>
        /// <returns>
        /// The result of the evaluated expression.
        /// </returns>
        public double Evaluate(Token e)
        {
            double ans = e.Evaluate(this);
            Variables["ans"] = ans;
            return ans;
        }
        public static Token Parse(string expression)
        {
            var Parsed = ConvertToPostfix(expression);
            return Tokenize(Parsed);
        }
        static Token Tokenize(Stack<string> postfixExpression)
        {
            if (postfixExpression == null || postfixExpression.Count == 0)
                return null;

            string t = postfixExpression.Pop();

            if (t.IsNumber())
                return new ConstantToken(double.Parse(t));
            else if (t.IsIdentifier())
            {
                // Variable
                if (t.StartsWith("@"))
                {
                    t = t.Substring(1);
                    return new VariableToken { VariableName = t };
                }
                // Function
                else
                {
                    List<Token> arguments = new List<Token>();
                    int sepIdx = t.IndexOf("$");
                    int argCount = Convert.ToInt32(t.Substring(sepIdx + 1));

                    Token arg = null;
                    do
                    {
                        arg = Tokenize(postfixExpression);
                        if (arg == null)
                            break;
                        arguments.Add(arg);
                    } while (argCount-- > 0);

                    return new FunctionToken
                    {
                        FunctionName = t.Substring(0, sepIdx),
                        Arguments = arguments.ToArray()
                    };
                }
            }
            else if (t.IsOperator())
            {
                Token rightSide = Tokenize(postfixExpression);
                Token leftSide = Tokenize(postfixExpression);
                return new OperatorToken
                {
                    LeftSideOperand = leftSide,
                    RightSideOperand = rightSide,
                    Operator = OperatorToken.GetOperator(t[0])
                };
            }

            return null;
        }
        static Stack<string> ConvertToPostfix(string e)
        {
            int funcDepth = 0;
            Stack<string> output = new Stack<string>();
            Stack<string> stack = new Stack<string>();
            Stack<int> argumentCount = new Stack<int>();
            var tokens = GetTokens(e);
            while (tokens.Count > 0)
            {
                var t = tokens.Dequeue();
                if (t == ",")
                {
                    while (stack.Peek() != "(" && stack.Count > 0)
                        output.Push(stack.Pop());
                    argumentCount.Push(argumentCount.Pop() + 1);
                }
                else if (t == ")")
                {
                    while (stack.Count > 0 && stack.Peek() != "(" && stack.Peek().IsOperator())
                    {
                        output.Push(stack.Pop());
                    }
                    if (stack.Count > 0 && stack.Peek() == "(")
                        stack.Pop();
                    if (stack.Count > 0 && stack.Peek().IsIdentifier())
                        output.Push(stack.Pop() + "$" + argumentCount.Pop());
                    funcDepth--;
                }
                else if (t.IsIdentifier())
                {
                    if (t.StartsWith("@"))
                    {
                        output.Push(t);
                    }
                    else if (tokens.Count == 0 || tokens.Peek() != "(")
                    {
                        output.Push("@" + t);
                    }
                    else
                    {
                        stack.Push(t);      // `t` is a function or a LParen.
                    }
                }
                else if (t == "(")
                {
                    stack.Push("(");
                    if (tokens.Peek() == ")")
                        argumentCount.Push(0);
                    else
                        argumentCount.Push(1);
                    funcDepth++;
                }
                else if (t.IsNumber())
                {
                    output.Push(t);
                }
                else if (t.IsOperator())
                {
                    while (stack.Count > 0 && stack.Peek().IsOperator())
                    {
                        if ((t.GetAssociativity() == Associativity.LeftToRight && Utils.Presedence(t, stack.Peek()) == 0) ||
                           Utils.Presedence(t, stack.Peek()) < 0)
                            output.Push(stack.Pop());
                        else
                            break;

                    }
                    stack.Push(t);
                }
            }
            while (stack.Count > 0)
            {
                if (stack.Peek() == "(")
                    stack.Pop();
                else
                    output.Push(stack.Pop());
            }
            return output;
        }
        static Queue<string> GetTokens(string e)
        {
            string outp = string.Empty;
            string last_token = string.Empty;
            Queue<string> tokens = new Queue<string>();
            for (int i = 0; i < e.Length; i++)
            {
                char c = e[i];

                if (char.IsWhiteSpace(c))
                    continue;
                else if (c == '(' || c == ')' || c == ',' || c.IsOperator() && c != '-')
                {
                    if (outp.Length > 0)
                    {
                        tokens.Enqueue(outp);
                        outp = string.Empty;
                    }
                    tokens.Enqueue(last_token = c.ToString());
                }
                else if (c == '-')
                {
                    if (outp == string.Empty && (string.IsNullOrEmpty(last_token) || last_token.IsOperator() ||
                                                last_token == "," || last_token == "("))
                    {
                        outp += c;
                    }
                    else
                    {
                        tokens.Enqueue(outp);
                        outp = string.Empty;
                        tokens.Enqueue(last_token = c.ToString());
                    }
                }
                else
                    outp += c;
            }
            if (outp.Length > 0)
                tokens.Enqueue(outp);
            return tokens;
        }
    }
}

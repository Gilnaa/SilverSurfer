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
        public double Evaluate(SilverExpression e)
        {
            double ans = e.RootToken.Evaluate(this);
            Variables["ans"] = ans;
            return ans;
        }
    }
}

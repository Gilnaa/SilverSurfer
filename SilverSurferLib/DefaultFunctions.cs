//
//  DefaultFunctions.cs
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

using SilverSurferLib.Tokens;
using System.Linq;
using System;

namespace SilverSurferLib
{
    static class DefaultFunctions
    {
        public static double Abs(FunctionToken func, Evaluator evaluator)
        {
            var args = func.Arguments;
            var num = args[0].Evaluate(evaluator);
            return Math.Abs(num);
        }
        public static double Sin(FunctionToken func, Evaluator evaluator)
        {
            var args = func.Arguments;
            var num = args[0].Evaluate(evaluator);
            return Math.Sin(num.ToRadians(evaluator.Settings.Mode));
        }
        public static double Cos(FunctionToken func, Evaluator evaluator)
        {
            var args = func.Arguments;
            var num = args[0].Evaluate(evaluator);
            return Math.Cos(num.ToRadians(evaluator.Settings.Mode));
        }
        public static double Tan(FunctionToken func, Evaluator evaluator)
        {
            var args = func.Arguments;
            var num = args[0].Evaluate(evaluator);
            return Math.Tan(num.ToRadians(evaluator.Settings.Mode));
        }
        public static double Asin(FunctionToken func, Evaluator evaluator)
        {
            var args = func.Arguments;
            var num = args[0].Evaluate(evaluator);
            return Math.Asin(num.ToRadians(evaluator.Settings.Mode));
        }
        public static double Acos(FunctionToken func, Evaluator evaluator)
        {
            var args = func.Arguments;
            var num = args[0].Evaluate(evaluator);
            return Math.Acos(num.ToRadians(evaluator.Settings.Mode));
        }
        public static double Atan(FunctionToken func, Evaluator evaluator)
        {
            var args = func.Arguments;
            var num = args[0].Evaluate(evaluator);
            return Math.Atan(num.ToRadians(evaluator.Settings.Mode));
        }
        public static double Min(FunctionToken func, Evaluator evaluator)
        {
            var args = func.Arguments;
            return args.Select(ar => ar.Evaluate(evaluator)).Min();
        }
        public static double Max(FunctionToken func, Evaluator evaluator)
        {
            var args = func.Arguments;
            return args.Select(ar => ar.Evaluate(evaluator)).Max();
        }
        public static double Log10(FunctionToken func, Evaluator evaluator)
        {
            var args = func.Arguments;
            var num = args[0].Evaluate(evaluator);
            return Math.Log10(num);
        }
        public static double Log(FunctionToken func, Evaluator evaluator)
        {
            var args = func.Arguments;
            var a = args[0].Evaluate(evaluator);
            if (args.Length == 1)
                return Math.Log(a);
            var @base = args[1].Evaluate(evaluator);
            return Math.Log(a, @base);
        }
        public static double DegToRad(FunctionToken func, Evaluator evaluator)
        {
            var args = func.Arguments;
            var num = args[0].Evaluate(evaluator);
            return num.ToRadians(Mode.Deg);
        }
        public static double GradToRad(FunctionToken func, Evaluator evaluator)
        {
            var args = func.Arguments;
            var num = args[0].Evaluate(evaluator);
            return num.ToRadians(Mode.Grad);
        }
    }
}

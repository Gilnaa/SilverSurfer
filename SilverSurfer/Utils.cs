//
//  Utils.cs
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
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace SilverSurferLib
{
    
    public static class Utils
    {
        public static double ToRadians(this double @this, Mode sourceMode)
        {
            switch(sourceMode)
                {
                    case Mode.Deg:
                        return Math.PI * @this / 180;
                    case Mode.Grad:
                        return Math.PI * @this / 200;
                    case Mode.Potato:
                        return Math.PI * @this / 21; // A circle has 42 potatoes.
                    case Mode.Rad:
                    default:
                        return @this;
                }
        }
        public static bool IsOperator(this char c)
        {
            return IsOperator(c.ToString());
        }
        public static bool IsOperator(this string s)
        {
            return Regex.IsMatch(s ?? string.Empty, @"^[=\+\-\*\/\%\^]{1}$");
        }
        public static bool IsIdentifier(this string name)
        {
            return Regex.IsMatch(name, @"^@?[a-zA-Z_]+[a-zA-Z_0-9]*(\$[0-9]*)?$");
        }
        public static bool IsVariable(this string name)
        {
            return name.StartsWith("@");
        }
        public static bool IsNumber(this string name)
        {
			return Regex.IsMatch(name, @"^\-?[0-9]+\.?[0-9]*([eE]{1}\-?[0-9]+\.?[0-9]*)?$");
        }
        public static int Presedence(string op1, string op2)
        {
            if (!IsOperator(op1) || !IsOperator(op2))
                throw new ArgumentException();
            if (op1 == op2)
                return 0;
            return GetPresedence(op1) - GetPresedence(op2);
        }
        public static int Presedence(SilverSurferLib.Tokens.OperatorToken.Operators op1, SilverSurferLib.Tokens.OperatorToken.Operators op2)
        {
            if (op1 == op2)
                return 0;
            return GetPresedence(op1) - GetPresedence(op2);
        }
        public static int GetPresedence(this string op)
        {
            switch (op)
            {
                case "^":
                    return 4;
                case "*":
                case "/":
                case "%":
                    return 3;
                case "+":
                case "-":
                    return 2;
                case "=":
                default:
                    return 0;
            }
        }
        public static int GetPresedence(this SilverSurferLib.Tokens.OperatorToken.Operators op)
        {
            return ((char)op).ToString().GetPresedence();
        }
        public static Associativity GetAssociativity(this string op)
        {
            switch(op)
            {
                case "^":
                case "=":
                    return Associativity.RightToLeft;
                default:
                    return Associativity.LeftToRight;
            }
        }
    }	
}

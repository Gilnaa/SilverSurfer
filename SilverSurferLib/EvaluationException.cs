using SilverSurferLib.Tokens;
//
//  EvaluationException.cs
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

namespace SilverSurferLib
{
    public class EvaluationException : Exception
    {
        public Token Expression {get; set;}
        public EvaluationException() :this(null)
        {
            
        }
        public EvaluationException(string message)
            : this(message, null)
        {

        }
        public EvaluationException(string message, Token expression)
            : base(message)
        {
            Expression = expression;
        }
    }
}
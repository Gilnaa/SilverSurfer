//
//  Settings.cs
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

namespace SilverSurferLib
{
    
    public class Settings
    {
        /// <summary>
        /// Gets or sets the angle mode of the evaluator.
        /// </summary>
        public Mode Mode {get; set;}
        /// <summary>
        /// Gets or sets a value indicating whether the evaluator should replace missing variables with a specified value.
        /// </summary>
        /// <see cref="MissingVariablesReplacement"/>
        public bool ReplaceMissingVariables {get; set;}
        public double MissingVariablesReplacement {get; set;}
        /// <summary>
        /// Gets or sets a value indicating whether the evaluator should prefer variables over functions
        /// when an identifier could not be resolved.
        /// </summary>
        /// <value>
        /// <c>true</c> if variables over functions; otherwise, <c>false</c>.
        /// </value>
        public bool VariablesOverFunctions {get; set;}
        public int Fix {get; set;}
        public Settings()
        {
            Mode = SilverSurferLib.Mode.Rad;
            ReplaceMissingVariables = false;
            MissingVariablesReplacement = 0;
            Fix = 10;
            VariablesOverFunctions = true;
        }
    }
}

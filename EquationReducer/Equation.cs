using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationReducer
{
    /// <summary>
    /// Equation Object, used for simplifying equations.
    /// </summary>
    public class Equation
    {
        public string OriginalEquation;
        public string ReducedEquation;

        /// <summary>
        /// Creates new Equation Object with OriginalEquation and ReducedEquation
        /// </summary>
        /// <param name="inputtedEquation">equivalent string equation</param>
        public Equation(string inputtedEquation)
        {
            #region Validation

            if (String.IsNullOrWhiteSpace(inputtedEquation))
            {
                throw new ArgumentException("Null or empty equation.", inputtedEquation);
            }

            #endregion

            ReducedEquation = ParseEquation(inputtedEquation);
            OriginalEquation = inputtedEquation;
        }

        /// <summary>
        /// Parses equation to simplified form
        /// </summary>
        /// <param name="inputtedEquation">String equation not in simplified form</param>
        /// <returns>Simplifed Equation</returns>
        private string ParseEquation(string inputtedEquation)
        {
            Dictionary<string, double> terms = new Dictionary<string, double>();

            double number = 1; //default number before is 1 if not specified
            double signChanger = 1; // sign changer is multiplied to the number, if negative number becomes negative
            double signChanger2 = 1;
            for (int i = 0; i < inputtedEquation.Length; i++)
            {
                // number before the variable
                if (Char.IsNumber(inputtedEquation[i]))
                {
                    int j = i;
                    while (j < inputtedEquation.Length && (Char.IsNumber(inputtedEquation[j]) || inputtedEquation[j] == '.'))
                    {
                        j++;
                    }

                    if (!Double.TryParse(inputtedEquation.Substring(i, j-i), out number))
                    {
                        throw new ArgumentException("Invalid Double.", inputtedEquation);
                    }
                    i = j-1;
                }
                // no variable
                else if (inputtedEquation[i] == ' ')
                {
                    // takes into account if it's a number without a variable: e.g. the five in 10x + 5 = 0
                    if (number != 1)
                    {
                        double currentValue = 0;
                        if (terms.ContainsKey(""))
                        {
                            terms.TryGetValue("", out currentValue);
                            terms.Remove("");
                        }
                        terms.Add("", number * signChanger * signChanger2 + currentValue);
                        number = 1;
                    }
                }
                // change negative
                else if (inputtedEquation[i] == '-')
                {
                    signChanger = -1;
                }
                // change positive
                else if (inputtedEquation[i] == '+')
                {
                    signChanger = 1;
                }
                else if (inputtedEquation[i] == '=')
                {
                    signChanger2 = -1;
                }
                // this is a start of a new term with variables
                else
                {
                    List<string> variables = new List<string>();// list of variables
             
                    while (i < inputtedEquation.Length && inputtedEquation[i] != ' ')
                    {
                        int j = i+1;
                        while (j < inputtedEquation.Length && (Char.IsNumber(inputtedEquation[j]) || inputtedEquation[j] == '^'))
                        {
                            j++;
                        }
                        variables.Add(inputtedEquation.Substring(i,j-i));
                        i = j;
                    }
                    StringBuilder variableKeyCreator = new StringBuilder();
                    variables.Sort(); // acounts for unordered variables e.g. x^2y is the same as yx^2
                    foreach (var variable in variables)
                    {
                        variableKeyCreator.Append(variable);
                    }

                    if (number != 0)
                    {
                        double currentValue = 0;
                        if (terms.ContainsKey(variableKeyCreator.ToString()))
                        {
                            terms.TryGetValue(variableKeyCreator.ToString(), out currentValue);
                            terms.Remove(variableKeyCreator.ToString());
                        }
                        terms.Add(variableKeyCreator.ToString(), number * signChanger * signChanger2 + currentValue);
                    }
                    number = 1;
                }
            }

            // If last character is number
            if (number != 1)
            {
                double currentValue = 0;
                if (terms.ContainsKey(""))
                {
                    terms.TryGetValue("", out currentValue);
                    terms.Remove("");
                }
                terms.Add("", number * signChanger * signChanger2 + currentValue);
            }

            bool hasSomeValues = false;
            // build new equation here
            StringBuilder sb = new StringBuilder();
            foreach (var term in terms)
            {
                if (term.Value != 0)
                {
                    hasSomeValues = true;
                    if (term.Value < 0)
                    {
                        if (sb.Length == 0)
                        {
                            sb.Append("-");
                        }
                        else
                        {
                            sb.Append(" - ");
                        }
                    }
                    else
                    {
                        if (sb.Length != 0)
                        {
                            sb.Append(" + ");
                        }
                    }

                    if (Math.Abs(term.Value) == 1)
                    {
                        sb.Append(term.Key);
                    }
                    else
                    {
                        sb.Append(Math.Abs(term.Value) + term.Key);
                    }
                }
            }

            // Everything simplified to 0
            if (!hasSomeValues)
            {
                return "0 = 0";
            }

            sb.Append(" = 0");
            return sb.ToString();
        }


    }
}

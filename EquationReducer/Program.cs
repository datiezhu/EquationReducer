using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationReducer
{
    class Program
    {
        static void Main(string[] args)
        {
            bool interactiveMode = true;
            string inputFilePath = "";
            string outputFilePath = "result.out"; //default out file path

            if (args.Length == 1)
            {
                inputFilePath = args[0];
                interactiveMode = false;
            }
            if (args.Length == 2)
            {
                inputFilePath = args[0];
                outputFilePath = args[1];
                interactiveMode = false;
            }
            else
            {
                Console.WriteLine("No or invalid arguments given, interactive mode selected as default.");
            }

            if (interactiveMode)
            {
                while (true)
                {
                    Console.WriteLine("Enter Equation: ");
                    string input = Console.ReadLine();

                    try
                    {
                        Equation equation = new Equation(input);
                        Console.WriteLine("Reduced Equation is: {0}", equation.ReducedEquation);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception Caught: {0}", ex.Message);
                    }
                }
            }
            else
            {
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(outputFilePath))
                {
                    string line;

                    System.IO.StreamReader file =
                    new System.IO.StreamReader(inputFilePath);
                    while ((line = file.ReadLine()) != null)
                    {
                        try
                        {
                            Equation equation = new Equation(line);
                            writer.WriteLine(equation.ReducedEquation);
                        }
                        catch (Exception ex)
                        {
                            writer.WriteLine("Exception Caught: {0}", ex.Message);
                        }
                    }
                }
            }
        }
    }
}

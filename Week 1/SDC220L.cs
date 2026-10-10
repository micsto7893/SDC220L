using System;
using System.Globalization;

namespace SDC220L
{
    /// <summary>
    /// Project Week 1 - Multi-Function Calculator
    /// Student: Michael Stoller
    /// Description: This calculator demonstrates basic console input and output, addition, and subtraction.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            DisplayHeader();
            DisplayWelcome();
            PerformIntegerAddition();
            PerformDecimalSubtraction();
            DisplayGoodbye();
        }

        /// <summary>
        /// Displays the information.
        /// </summary>
        static void DisplayHeader()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("        SDC 220L PROJECT WEEK 1");
            Console.WriteLine("       MULTI-FUNCTION CALCULATOR");
            Console.WriteLine("           Michael Stoller");
            Console.WriteLine("========================================");
            Console.WriteLine();
        }

        /// <summary>
        /// Welcome message and how to for the calculator.
        /// </summary>
        static void DisplayWelcome()
        {
            Console.WriteLine("Welcome to the Multi-Function Calculator!");
            Console.WriteLine();
            Console.WriteLine("This application performs basic math.");
            Console.WriteLine("Follow the prompts to enter your numbers.");
            Console.WriteLine("The calculator will display each equation");
            Console.WriteLine("along with its calculated result.");
            Console.WriteLine();
        }

        /// <summary>
        /// Requests two numbers, adds them, and displays the equation.
        /// </summary>
        static void PerformIntegerAddition()
        {
            Console.WriteLine("--- INTEGER ADDITION ---");
            int firstNumber = ReadInteger("Enter first integer: ");
            int secondNumber = ReadInteger("Enter second integer: ");
            long result = (long)firstNumber + secondNumber;
            Console.WriteLine();
            Console.WriteLine($"{firstNumber} + {secondNumber} = {result}");
            Console.WriteLine();
        }

        /// <summary>
        /// Requests two numbers and subtracts the first from the second.
        /// </summary>
        static void PerformDecimalSubtraction()
        {
            Console.WriteLine("--- FLOATING-POINT SUBTRACTION ---");
            double firstNumber = ReadDouble("Enter first decimal number: ");
            double secondNumber = ReadDouble("Enter second decimal number: ");
            double result = secondNumber - firstNumber;
            Console.WriteLine();

            // Format the result to match the design document: up to two decimal places, if needed.
            Console.WriteLine(
                $"{FormatNumber(secondNumber)} - " +
                $"{FormatNumber(firstNumber)} = " +
                $"{FormatNumber(result)}");
            Console.WriteLine();
        }

        /// <summary>
        /// Reads an integer and repeats the prompt if the input is invalid.
        /// </summary>
        static int ReadInteger(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int value))
                {return value;}
                Console.WriteLine(
                    "Error: Please enter a valid number.");
            }
        }

        /// <summary>
        /// Reads a floating-point value and validates user input.
        /// </summary>
        static double ReadDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();
                if (double.TryParse(input, out double value)
                    && !double.IsNaN(value)
                    && !double.IsInfinity(value))
                {return value;}
                Console.WriteLine(
                    "Error: Please enter a valid numeric value.");
            }
        }

        /// <summary>
        /// Formats numbers with up to two decimal places.
        /// </summary>
        static string FormatNumber(double number)
        {return number.ToString("0.##", CultureInfo.CurrentCulture);}

        /// <summary>
        /// Thanks the user and ends the calculator session.
        /// </summary>
        static void DisplayGoodbye()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("Thank you for using the Calculator!");
            Console.WriteLine("                Goodbye!");
            Console.WriteLine("========================================");
        }
    }
}

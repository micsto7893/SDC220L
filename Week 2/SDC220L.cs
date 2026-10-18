using System;
using System.Globalization;

namespace SDC220L
{
    /// <summary>
    /// SDC 220L Project Week 2 - Multi-Function Calculator
    /// Student: Michael Stoller
    /// Provides a menu-driven console calculator with input validation.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            CalculatorUI.DisplayHeader();
            CalculatorUI.DisplayWelcome();

            bool running = true;
            while (running)
            {
                CalculatorUI.DisplayMenu();
                int selection = CalculatorUI.ReadMenuSelection();

                if (selection == 5)
                {
                    running = false;
                }
                else
                {
                    PerformSelectedOperation(selection);
                }
            }

            CalculatorUI.DisplayGoodbye();
        }

        //Gets two numbers and displays the selected arithmetic operation.
        static void PerformSelectedOperation(int selection)
        {
            string operation = selection switch
            {
                1 => "ADDITION",
                2 => "SUBTRACTION",
                3 => "MULTIPLICATION",
                4 => "DIVISION",
                _ => "UNKNOWN"
            };

            Console.WriteLine($"\n--- {operation} ---");
            double first = CalculatorUI.ReadNumber("Enter first number: ");
            double second = CalculatorUI.ReadNumber("Enter second number: ");

            //Prevent division by zero before calling the calculation method.
            if (selection == 4 && second == 0)
            {
                Console.WriteLine("Error: Cannot divide by zero. Returning to the menu.\n");
                return;
            }

            double result = Calculator.Calculate(first, second, selection);
            string symbol = selection switch
            {
                1 => "+",
                2 => "-",
                3 => "*",
                4 => "/",
                _ => "?"
            };

            Console.WriteLine($"\n{CalculatorUI.FormatNumber(first)} {symbol} " +
                $"{CalculatorUI.FormatNumber(second)} = {CalculatorUI.FormatNumber(result)}\n");
        }
    }

    //Contains mathematical operations separately from console input/output.
    internal static class Calculator
    {
        //Calculates a result using two values and a menu selection.
        public static double Calculate(double first, double second, int selection)
        {
            return selection switch
            {
                1 => first + second,
                2 => first - second,
                3 => first * second,
                4 => first / second,
                _ => throw new ArgumentOutOfRangeException(nameof(selection))
            };
        }
    }

    //Displays text, reads input, and formats output for the console.
    internal static class CalculatorUI
    {
        public static void DisplayHeader()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("        SDC 220L PROJECT WEEK 2");
            Console.WriteLine("       MULTI-FUNCTION CALCULATOR");
            Console.WriteLine("           Michael Stoller");
            Console.WriteLine("========================================\n");
        }

        public static void DisplayWelcome()
        {
            Console.WriteLine("Welcome to the Multi-Function Calculator!");
            Console.WriteLine("Choose an operation from the menu.");
            Console.WriteLine("Enter two numbers when prompted.");
            Console.WriteLine("Results display as equations with up to two decimal places.\n");
        }

        public static void DisplayMenu()
        {
            Console.WriteLine("--- MAIN MENU ---");
            Console.WriteLine("1. Addition");
            Console.WriteLine("2. Subtraction");
            Console.WriteLine("3. Multiplication");
            Console.WriteLine("4. Division");
            Console.WriteLine("5. Exit");
        }

        //Returns a valid menu selection from 1 to 5.
        public static int ReadMenuSelection()
        {
            while (true)
            {
                Console.Write("Select an option (1-5): ");
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= 5)
                    return choice;

                Console.WriteLine("Error: Please choose a number from 1 to 5.");
            }
        }

        //Reads a finite numeric value, retrying if invalid.
        public static double ReadNumber(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(Console.ReadLine(), out double number)
                    && !double.IsNaN(number) && !double.IsInfinity(number))
                    return number;

                Console.WriteLine("Error: Please enter a valid number.");
            }
        }

        //Formats a result to at most two decimal places.
        public static string FormatNumber(double value)
        {
            return value.ToString("0.##", CultureInfo.CurrentCulture);
        }

        public static void DisplayGoodbye()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("Thank you for using the Calculator!");
            Console.WriteLine("                Goodbye!");
            Console.WriteLine("========================================");
        }
    }
}

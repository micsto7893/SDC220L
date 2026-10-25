using System;
using System.Collections.Generic;
using System.Globalization;

namespace SDC220L
{
    // SDC 220L Project Week 3 - Multi-Function Calculator
    // Student: Michael Stoller
    // Adds single-number memory and an integer collection (maximum 10 values).
    internal class Program
    {
        static void Main(string[] args)
        {
            MemoryManager memory = new MemoryManager();
            CalculatorUI.DisplayHeader();
            CalculatorUI.DisplayWelcome();

            bool running = true;
            while (running)
            {
                CalculatorUI.DisplayMenu();
                int selection = CalculatorUI.ReadChoice("Select an option (1-7): ", 1, 7);

                switch (selection)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        PerformSelectedOperation(selection);
                        break;
                    case 5:
                        HandleSingleMemory(memory);
                        break;
                    case 6:
                        HandleIntegerCollection(memory);
                        break;
                    case 7:
                        running = false;
                        break;
                }
            }

            CalculatorUI.DisplayGoodbye();
        }

        // Gets two numeric values and performs the operation selected on the menu.
        static void PerformSelectedOperation(int selection)
        {
            string operation = selection switch
            {
                1 => "ADDITION", 2 => "SUBTRACTION", 3 => "MULTIPLICATION",
                4 => "DIVISION", _ => "UNKNOWN"
            };

            Console.WriteLine($"\n--- {operation} ---");
            double first = CalculatorUI.ReadNumber("Enter first number: ");
            double second = CalculatorUI.ReadNumber("Enter second number: ");

            if (selection == 4 && second == 0)
            {
                Console.WriteLine("Error: Cannot divide by zero. Returning to the menu.\n");
                return;
            }

            double result = Calculator.Calculate(first, second, selection);
            string symbol = selection switch
            {
                1 => "+", 2 => "-", 3 => "*", 4 => "/", _ => "?"
            };

            Console.WriteLine($"\n{CalculatorUI.FormatNumber(first)} {symbol} " +
                $"{CalculatorUI.FormatNumber(second)} = {CalculatorUI.FormatNumber(result)}\n");
        }

        // Shows the single-value memory menu until the user returns to the main menu.
        static void HandleSingleMemory(MemoryManager memory)
        {
            bool inMenu = true;
            while (inMenu)
            {
                Console.WriteLine("\n--- SINGLE-VALUE MEMORY ---");
                Console.WriteLine("1. Store / replace a value");
                Console.WriteLine("2. Retrieve stored value");
                Console.WriteLine("3. Clear stored value");
                Console.WriteLine("4. Return to main menu");
                int choice = CalculatorUI.ReadChoice("Select an option (1-4): ", 1, 4);

                switch (choice)
                {
                    case 1:
                        double value = CalculatorUI.ReadNumber("Enter a number to store: ");
                        bool replacing = memory.HasStoredValue;
                        memory.StoreValue(value);
                        Console.WriteLine(replacing ? "Stored value replaced.\n" : "Value stored.\n");
                        break;
                    case 2:
                        if (memory.TryGetValue(out double stored))
                            Console.WriteLine($"Stored value: {CalculatorUI.FormatNumber(stored)}\n");
                        else
                            Console.WriteLine("Memory is empty.\n");
                        break;
                    case 3:
                        memory.ClearValue();
                        Console.WriteLine("Stored value cleared.\n");
                        break;
                    case 4:
                        inMenu = false;
                        break;
                }
            }
        }

        // Allows the user to manage up to ten integer values.
        static void HandleIntegerCollection(MemoryManager memory)
        {
            bool inMenu = true;
            while (inMenu)
            {
                Console.WriteLine("\n--- INTEGER COLLECTION MEMORY ---");
                Console.WriteLine("1. Add integer(s)");
                Console.WriteLine("2. Display all values");
                Console.WriteLine("3. Display count");
                Console.WriteLine("4. Remove a value by position");
                Console.WriteLine("5. Sum of values");
                Console.WriteLine("6. Average of values");
                Console.WriteLine("7. Difference (first minus last)");
                Console.WriteLine("8. Return to main menu");
                int choice = CalculatorUI.ReadChoice("Select an option (1-8): ", 1, 8);

                switch (choice)
                {
                    case 1:
                        int available = MemoryManager.MaxValues - memory.Count;
                        if (available == 0)
                        {
                            Console.WriteLine("Collection is full (10 values). Remove one before adding more.\n");
                            break;
                        }
                        int howMany = CalculatorUI.ReadChoice(
                            $"How many integers would you like to add (1-{available})? ", 1, available);
                        for (int i = 1; i <= howMany; i++)
                        {
                            int number = CalculatorUI.ReadInteger($"Enter integer {i}: ");
                            memory.AddInteger(number);
                        }
                        Console.WriteLine($"Added {howMany} value(s). Count: {memory.Count}/10.\n");
                        break;
                    case 2:
                        if (memory.Count == 0)
                            Console.WriteLine("Collection is empty.\n");
                        else
                        {
                            Console.WriteLine("Stored values (position: value):");
                            for (int i = 0; i < memory.Count; i++)
                                Console.WriteLine($"{i + 1}: {memory.Values[i]}");
                            Console.WriteLine();
                        }
                        break;
                    case 3:
                        Console.WriteLine($"Values stored: {memory.Count}/10\n");
                        break;
                    case 4:
                        if (memory.Count == 0)
                        {
                            Console.WriteLine("Collection is empty; nothing to remove.\n");
                            break;
                        }
                        Console.WriteLine("Enter the position shown by 'Display all values'.");
                        int position = CalculatorUI.ReadChoice(
                            $"Position to remove (1-{memory.Count}): ", 1, memory.Count);
                        int removed = memory.RemoveAt(position - 1);
                        Console.WriteLine($"Removed value {removed} from position {position}.\n");
                        break;
                    case 5:
                        Console.WriteLine($"Sum: {memory.Sum()}\n");
                        break;
                    case 6:
                        if (memory.Count == 0)
                            Console.WriteLine("Cannot calculate an average: collection is empty.\n");
                        else
                            Console.WriteLine($"Average: {CalculatorUI.FormatNumber(memory.Average())}\n");
                        break;
                    case 7:
                        if (memory.Count == 0)
                            Console.WriteLine("Cannot calculate a difference: collection is empty.\n");
                        else
                            Console.WriteLine($"First - last: {memory.Values[0]} - " +
                                $"{memory.Values[memory.Count - 1]} = {memory.FirstMinusLast()}\n");
                        break;
                    case 8:
                        inMenu = false;
                        break;
                }
            }
        }
    }

    // Keeps the arithmetic logic separate from the console interface.
    internal static class Calculator
    {
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

    // Stores one optional decimal value and a separate list of up to ten integers.
    internal class MemoryManager
    {
        public const int MaxValues = 10;
        private double? storedValue;
        private readonly List<int> integers = new List<int>();

        public bool HasStoredValue => storedValue.HasValue;
        public int Count => integers.Count;
        public IReadOnlyList<int> Values => integers.AsReadOnly();

        public void StoreValue(double value) => storedValue = value;
        public bool TryGetValue(out double value)
        {
            value = storedValue.GetValueOrDefault();
            return storedValue.HasValue;
        }
        public void ClearValue() => storedValue = null;

        public bool AddInteger(int value)
        {
            if (integers.Count >= MaxValues) return false;
            integers.Add(value);
            return true;
        }
        public int RemoveAt(int index)
        {
            int value = integers[index];
            integers.RemoveAt(index);
            return value;
        }
        public long Sum()
        {
            long total = 0;
            foreach (int value in integers) total += value;
            return total;
        }
        public double Average() => (double)Sum() / integers.Count;
        public long FirstMinusLast() => (long)integers[0] - integers[integers.Count - 1];
    }

    // Handles console output, menu selection, validation, and number formatting.
    internal static class CalculatorUI
    {
        public static void DisplayHeader()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("        SDC 220L PROJECT WEEK 3");
            Console.WriteLine("       MULTI-FUNCTION CALCULATOR");
            Console.WriteLine("           Michael Stoller");
            Console.WriteLine("========================================\n");
        }

        public static void DisplayWelcome()
        {
            Console.WriteLine("Welcome to the Multi-Function Calculator!");
            Console.WriteLine("Choose a math operation or a memory function.");
            Console.WriteLine("Single memory stores one number; collection memory stores up to 10 integers.");
            Console.WriteLine("Use the numbered menus to work with your values, then choose Exit.\n");
        }

        public static void DisplayMenu()
        {
            Console.WriteLine("--- MAIN MENU ---");
            Console.WriteLine("1. Addition");
            Console.WriteLine("2. Subtraction");
            Console.WriteLine("3. Multiplication");
            Console.WriteLine("4. Division");
            Console.WriteLine("5. Single-value memory");
            Console.WriteLine("6. Integer collection memory");
            Console.WriteLine("7. Exit");
        }

        public static int ReadChoice(string prompt, int minimum, int maximum)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int choice) &&
                    choice >= minimum && choice <= maximum)
                    return choice;
                Console.WriteLine($"Error: Please enter a whole number from {minimum} to {maximum}.");
            }
        }

        public static int ReadInteger(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int value)) return value;
                Console.WriteLine("Error: Please enter a valid integer.");
            }
        }

        public static double ReadNumber(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(Console.ReadLine(), out double number) &&
                    !double.IsNaN(number) && !double.IsInfinity(number)) return number;
                Console.WriteLine("Error: Please enter a valid number.");
            }
        }

        public static string FormatNumber(double value) =>
            value.ToString("0.##", CultureInfo.CurrentCulture);

        public static void DisplayGoodbye()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("Thank you for using the Calculator!");
            Console.WriteLine("                Goodbye!");
            Console.WriteLine("========================================");
        }
    }
}

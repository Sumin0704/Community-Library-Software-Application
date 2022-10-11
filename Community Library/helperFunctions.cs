using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3
{
    class helperFunctions
    {
        /// <summary>
        /// Prompts user to enter a line of text.
        /// </summary>
        /// <param name="prompt">The label displayed to request input.</param>
        /// <returns>The next line of input from the standard input stream.</returns>
        public static string GetInput(string prompt)
        {
            Console.Write("{0}: ", prompt);
            return Console.ReadLine().Trim();
        }

        public static string CheckStringLength(string field,string input)
        {
            while (input.Length == 0)
            {
                helperFunctions.printInColor($"{field} can't be blank.", ConsoleColor.Red);
                input = helperFunctions.GetInput(field);
            }
            return input;
        }
        public static List<string> CheckFullNameLength(string fullName)
        {
            List<string> name = new List<string>();
            string firstName = ""; string lastName = "";
            try
            {
                string[] arr = fullName.Split(' ');

                firstName = arr[0];
                lastName = arr[1];
                foreach (string i in arr)
                {
                    if (i != String.Empty)
                    {
                        name.Add(i);
                    }
                }
            }
            catch (IndexOutOfRangeException e)
            {
               // helperFunctions.printInColor("Please enter a valid first and last name.", ConsoleColor.Red);
            }
            return name;
        }

        /// <summary>
        /// Prompts the user for an integer between lower and upper bounds, inclusive.
        /// </summary>
        /// <param name="min">The lower bound.</param>
        /// <param name="max">The upper bound.</param>
        /// <returns>Returns ('value entered by user' - 1).</returns>
        public static int GetOption(int min, int max)
        {
            while (true)
            {
                var key = GetInput($"Please enter an option between {min} and {max}");

                if (int.TryParse(key, out var option))
                {
                    if (min <= option && option <= max)
                        return option - 1;
                }

                printInColor("Invalid option",ConsoleColor.Red);
            }
        }

        /// <summary>
        /// Prompts user to get an integer value, blocking until valid input is obtained.
        /// </summary>
        /// <param name="prompt">The label displayed to request input.</param>
        /// <returns>The next line of input from the standard input stream, parsed into an int.</returns>
        public static int GetInteger(string prompt)
        {
            while (true)
            {
                var response = helperFunctions.GetInput(prompt);

                if (int.TryParse(response, out int intVal))
                {
                    return intVal;
                }
                else
                {
                    helperFunctions.printInColor("Invalid number",ConsoleColor.Red);
                }
            }
        }


        public static void printInColor(string text, ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

      
        public static bool AuthenticateUser(string userName, string password)
        {
            string userNameEntered = GetInput("Username");
            string passwordEntered = GetInput("Password");
            if (userName == userNameEntered && password == passwordEntered)
            {
                printInColor("Login Successful!", ConsoleColor.Green);
                return true;
            }
            printInColor("Incorrect username or password", ConsoleColor.Red);
            return false;
        }
    }
}

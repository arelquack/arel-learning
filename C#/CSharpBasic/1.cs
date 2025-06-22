using System;

class Program
{
    static void Main(string[] args) 
    {
        // Hello World + Input
        Console.Write("Input your name: ");
        string name = Console.ReadLine();
        Console.WriteLine($"Yo {name}, let's start this incredible journey!");

        // Numeric & Text Data Types
        int age = 0;
        Console.Write("Input your age: ");
        string ageInput = Console.ReadLine();

        // Converting String to Number + Try-Catch
        try {
            age = int.Parse(ageInput);
            if (age < 0) throw new Exception("Age can not be negative, bro!");
        } catch (FormatException) {
            Console.WriteLine("Error: Please input a number for your age.");
            return;
        } catch (Exception e) {
            Console.WriteLine($"Error: {e.Message}");
            return;
        }

        // If-else + Boolean
        bool isAdult = age >= 18;
        if (isAdult) {
            Console.WriteLine($"You are {age} years old, so you are eligible!");
        } else {
            Console.WriteLine($"You are {age} years old, so you are not eligible yet.");
        }

        // Switch Statement
        Console.Write("Input number of day (1-7): ");
        string dayInput = Console.ReadLine();
        try {
            int day = int.Parse(dayInput);
            switch (day) {
                case 1:
                    Console.WriteLine("Monday");
                    break;
                case 2:
                    Console.WriteLine("Tuesday");
                    break;
                case 3:
                    Console.WriteLine("Wednesday");
                    break;
                case 4:
                    Console.WriteLine("Thursday");
                    break;
                case 5:
                    Console.WriteLine("Friday");
                    break;
                case 6:
                    Console.WriteLine("Saturday");
                    break;
                case 7:
                    Console.WriteLine("Sunday");
                    break;
                default:
                    Console.WriteLine("Invalid day number. Please input a number between 1 and 7.");
                    break;
            }
        } catch {
            Console.WriteLine("Error: Please input a valid number for the day.");
        }
    
        // Odd/Even Checker
        Console.Write("Input an odd/even number: ");
        string numberInput = Console.ReadLine();
        try {
            int number = int.Parse(numberInput);
            string result = (number % 2 == 0) ? "It's an even number!" : "It's an odd number!";
            Console.WriteLine(result);
        } catch {
            Console.WriteLine("Error: Please input a valid number.");
        }
    }
}
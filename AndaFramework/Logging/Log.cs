namespace AndaFramework.Logging;

using System;

public class Logger
{

    public static void Log(string prefix, string message)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write($"[ LOG : {prefix}] ");

        Console.ResetColor();
        Console.WriteLine(message);
    }

    public static void Err(string prefix, string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"[ ERROR : {prefix}] ");

        Console.ResetColor();
        Console.WriteLine(message);
    }
}

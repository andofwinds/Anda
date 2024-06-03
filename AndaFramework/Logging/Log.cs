namespace AndaFramework.Logging;

using System;

public class Logger
{
    
    public static void Log(string prefix, string Message)
    {
        Console.Write($"[ LOG : {prefix}] ");

        Console.WriteLine(Message);
    }
}
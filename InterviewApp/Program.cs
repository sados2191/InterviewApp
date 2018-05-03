using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace InterviewApp
{
    class Program
    {
        static void Main(string[] args)
        {
            char option = '0';
            do
            {
                #region Menu
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("InterviewApp");
                Console.WriteLine("Simple Logger");
                Console.ResetColor();
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1 - Save a log to the file");
                Console.WriteLine("2 - Save a log to the EventLog");
                Console.WriteLine("3 - Save s log to the registry");
                Console.WriteLine("4 - Read saved entries from the file ");
                Console.WriteLine("5 - Read saved entries from the EventLog");
                Console.WriteLine("6 - Read saved entries from the Registry");
                Console.WriteLine("0 - Exit");
                char.TryParse(Console.ReadLine(), out option);
                Console.Clear();
                #endregion

                switch (option)
                {
                    case '1':
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"Adding log to the file, path: {Loger.FilePath}");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.WriteLine("Write a content of the log");

                        if(Loger.LogToFile(Console.ReadLine()))
                            Console.WriteLine("Log was added");
                        else
                            Console.WriteLine("Adding the log failed");

                        break;
                    case '2':
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"Adding log to the EventLog as source: {Loger.EventLogSource}");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.WriteLine("Write a content of the log");

                        if(Loger.LogToEventLog(Console.ReadLine()))
                            Console.WriteLine("Log was added");
                        else
                            Console.WriteLine("Adding the log failed");

                        break;
                    case '3':
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"Adding log to the Registry, path: {Loger.RegistryKey}");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.WriteLine("Write a content of the log");

                        if (Loger.LogToRegistry(Console.ReadLine()))
                            Console.WriteLine("Log was added");
                        else
                            Console.WriteLine("Adding the log failed");

                        break;
                    case '4':
                        Loger.ReadFromFile();

                        break;
                    case '5':
                        List<EventLogEntry> entries = Loger.ReadFromEventLog();
                        if (entries.Count > 0)
                        {
                            foreach (var entry in entries)
                            {
                                Console.WriteLine($"{entry.TimeWritten} {entry.Message}");
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Add something to the EventLog before reading form it");
                            Console.ResetColor();
                        }

                        break;
                    case '6':
                        Loger.ReadFromRegistry();

                        break;
                    case '0':
                        Console.WriteLine("Exiting...");

                        break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            } while (option != '0');
        }
    }
}

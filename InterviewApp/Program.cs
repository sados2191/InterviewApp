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
                Console.WriteLine("1 - Add a log to the file");
                Console.WriteLine("2 - Add a log to the EventLog");
                Console.WriteLine("3 - Add a log to the Registry");
                Console.WriteLine("4 - Read saved entries from the file ");
                Console.WriteLine("5 - Read saved entries from the EventLog");
                Console.WriteLine("6 - Read saved entries from the Registry");
                Console.WriteLine("0 - Exit");
                char.TryParse(Console.ReadLine(), out option);
                Console.Clear();
                #endregion

                #region Switch
                switch (option)
                {
                    case '1':
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"Adding log to the file, path: {Logger.FilePath}");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.WriteLine("Write a content of the log");

                        if(Logger.LogToFile(Console.ReadLine()))
                            Console.WriteLine("Log was added");
                        else
                            Console.WriteLine("Adding the log failed");

                        break;
                    case '2':
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"Adding log to the EventLog as source: {Logger.EventLogSource}");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.WriteLine("Write a content of the log");

                        if(Logger.LogToEventLog(Console.ReadLine()))
                            Console.WriteLine("Log was added");
                        else
                            Console.WriteLine("Adding the log failed");

                        break;
                    case '3':
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"Adding log to the Registry, path: {Logger.RegistryKey}");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.WriteLine("Write a content of the log");

                        if (Logger.LogToRegistry(Console.ReadLine()))
                            Console.WriteLine("Log was added");
                        else
                            Console.WriteLine("Adding the log failed");

                        break;
                    case '4':
                        Logger.ReadFromFile();

                        break;
                    case '5':
                        List<EventLogEntry> entries = Logger.ReadFromEventLog();
                        if (entries.Count > 0)
                        {
                            foreach (var entry in entries)
                            {
                                Console.WriteLine($"{entry.TimeWritten} {entry.Message}");
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("No logs found.");
                            Console.WriteLine("Add logs to the EventLog before reading form it.");
                            Console.ResetColor();
                        }

                        break;
                    case '6':
                        Logger.ReadFromRegistry();

                        break;
                    case '0':
                        Console.WriteLine("Exiting...");

                        break;
                    default:
                        Console.WriteLine("Wrong option!");
                        break;
                }
                #endregion

                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            } while (option != '0');
        }
    }
}
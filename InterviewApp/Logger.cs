using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace InterviewApp
{
    public static class Logger
    {
        #region Properties
        /// <summary>
        /// Returns the path of the log file
        /// </summary>
        public static string FilePath
        {
            get
            {
                return $@"{AppDomain.CurrentDomain.BaseDirectory}logs.txt";
            }
        }

        /// <summary>
        /// Returns the name of the EventLog source
        /// </summary>
        public static string EventLogSource
        {
            get
            {
                return "Interview";
            }
        }

        /// <summary>
        /// Returns the path of the Registry log key
        /// </summary>
        public static string RegistryKey
        {
            get
            {
                return @"HKEY_CURRENT_USER\SOFTWARE\InterviewApp\Logs";
            }
        }
        #endregion

        #region SavingMethods
        ///<summary>
        ///Adds a log in the EventLog, returns true if adding was successful
        ///</summary>
        public static bool LogToEventLog(string message)
        {
            try
            {
                //checks if the source name exists in the EventLog
                if (!EventLog.SourceExists(EventLogSource))
                    EventLog.CreateEventSource(EventLogSource, "Application");

                EventLog.WriteEntry(EventLogSource, message);
                return true;
            }
            //catches administrator permission exception
            catch (System.Security.SecurityException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {e.Message}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Start the application with the administrator permission");
                Console.ResetColor();
                return false;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {e.Message}");
                Console.ResetColor();
                return false;
            }
        }

        ///<summary>
        ///Adds a log to a file, returns true if adding was successful
        ///</summary>
        public static bool LogToFile(string message)
        {   
            try
            {
                using (StreamWriter sw = new StreamWriter(FilePath, true))
                {
                    sw.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} \"{message}\"");
                    sw.Close();
                }
                return true;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {e.Message}");
                Console.ResetColor();
                return false;
            }
        }

        ///<summary>
        ///Adds a log to the Registry, returns true if adding was successful
        ///</summary>
        public static bool LogToRegistry(string message)
        {
            try
            {
                Registry.SetValue(RegistryKey, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), message);
               
                return true;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {e.Message}");
                Console.ResetColor();
                return false;
            }
        }
        #endregion

        #region ReadingMethods
        /// <summary>
        /// Displays logs from the file in the console
        /// </summary>
        public static void ReadFromFile()
        {
            try
            {
                using (StreamReader sr = new StreamReader(FilePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            catch(FileNotFoundException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {e.Message}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("No logs found.");
                Console.WriteLine("Add some logs to the file before reading from it.");
                Console.ResetColor();
            }
            catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {e.Message}");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Returns the list of the saved entries in the EventLog
        /// </summary>
        public static List<EventLogEntry> ReadFromEventLog()
        {
            EventLog eventLog = new EventLog("Application");
            return eventLog.Entries.Cast<EventLogEntry>()
                .Where(e => e.Source == EventLogSource).ToList();
        }

        /// <summary>
        /// Display logs form the Registry in the console
        /// </summary>
        public static void ReadFromRegistry()
        {
            RegistryKey rkey = Registry.CurrentUser.OpenSubKey(RegistryKey.Replace(@"HKEY_CURRENT_USER\", ""));
            if (rkey != null)
            {
                var values = rkey.GetValueNames();
                foreach (var value in values)
                {
                    Console.WriteLine($"{value} {rkey.GetValue(value)}");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("No logs found.");
                Console.WriteLine("Add some logs to the Registry before reading from it.");
                Console.ResetColor();
            }
        }
        #endregion
    }
}

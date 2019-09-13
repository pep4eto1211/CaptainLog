using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaptainLog.Core.Services;

namespace CapLog
{
    class Program
    {
        static void Main(string[] args)
        {
            LogsCrudService crudService = new LogsCrudService();
            ExecuteCommands(args, crudService);
#if DEBUG
            Console.WriteLine("Ready");
            Console.ReadLine();
#endif
        }

        private static void ExecuteCommands(string[] args, LogsCrudService crudService)
        {
            if (args[0] == "create")
            {
                if (!crudService.DoesLogExists(args[1]))
                {
                    crudService.CreateLog(args[1]);
                }
                else
                {
                    ConsoleColor oldColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Log named \"{args[1]}\" already exists.");
                    Console.ForegroundColor = oldColor;
                }
            }
            else if (args[0] == "template")
            {
                crudService.CreateLogTemplate(args[1], String.Join(" ", args.AsEnumerable().Skip(2)));
            }
            else if (args[0] == "deletetemplate")
            {
                crudService.DeleteTemplate(args[1]);
            }
            else if (args[0] == "view")
            {
                if (crudService.DoesLogExists(args[1]))
                {
                    Console.WriteLine(crudService.ReadLog(args[1]));
                }
                else
                {
                    ShowCreateLogHelp(args[1]);
                }
            }
            else if (args[0] == "lines")
            {
                List<string> lines = crudService.ReadLogLines(args[1]);
                for (int i = 0; i < lines.Count; i++)
                {
                    Console.WriteLine($"[{i + 1}] {lines[i]}");
                }
            }
            else if (args[0] == "editline")
            {
                crudService.ReplaceLine(args[1], int.Parse(args[2]), String.Join(" ", args.AsEnumerable().Skip(3)));
            }
            else if (args[0] == "deleteline")
            {
                crudService.DeleteLine(args[1], int.Parse(args[2]));
            }
            else if (args[0] == "ls")
            {
                foreach (var item in crudService.ListLogs())
                {
                    Console.WriteLine(item);
                }
            }
            else if (args[0] == "purge")
            {
                if (crudService.DoesLogExists(args[1]))
                {
                    crudService.PurgeLog(args[1]);
                }
            }
            else if (args[0] == "delete")
            {
                if (crudService.DoesLogExists(args[1]))
                {
                    crudService.DeleteLog(args[1]);
                }
            }
            else if (args[0] == "edit")
            {
                if (crudService.DoesLogExists(args[1]))
                {
                    crudService.OpenLogForEditing(args[1]);
                }
                else
                {
                    ShowCreateLogHelp(args[1]);
                }
            }
            else
            {
                if (crudService.DoesLogExists(args[0]))
                {
                    crudService.WriteLineToLog(args[0], String.Join(" ", args.AsEnumerable().Skip(1)));
                }
                else
                {
                    ShowCreateLogHelp(args[0]);
                }
            }
        }

        private static void ShowCreateLogHelp(string logName)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Log named \"{logName}\" does not exist. To create it run:");
            Console.ForegroundColor = oldColor;
            Console.WriteLine($"caplog create {logName}");
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaptainLog.Core.Models;

namespace CaptainLog.Core.Utils
{
    internal static class Files
    {
        static Files()
        {
            Directory.CreateDirectory(Config.LogsLocation);
            Directory.CreateDirectory(Config.TemplatesLocation);
        }

        internal static void CreateLogFiles(string logName)
        {
            using (var stream = File.CreateText(logName.GetLogTextFileName(Config.LogsLocation)))
            {

            }
        }

        internal static void CreateTemplateFile(string logName, string template)
        {
            File.WriteAllText(logName.GetLogTemplateFileName(Config.TemplatesLocation), template);
        }

        internal static bool DoesLogFilesExist(string logName)
        {
            return File.Exists(logName.GetLogTextFileName(Config.LogsLocation));
        }

        private static bool DoesTemplateExists(string logName)
        {
            return File.Exists(logName.GetLogTemplateFileName(Config.TemplatesLocation));
        }

        internal static List<string> GetLines(string logName)
        {
            return File.ReadAllLines(logName.GetLogTextFileName(Config.LogsLocation)).ToList<string>();
        }

        internal static void WriteLine(string logName, string line)
        {
            if (DoesLogFilesExist(logName))
            {
                if (DoesTemplateExists(logName))
                {
                    string templateText = File.ReadAllText(logName.GetLogTemplateFileName(Config.TemplatesLocation));
                    line = ReplaceTemplatedParts(templateText, logName, line);
                }

                File.AppendAllText(logName.GetLogTextFileName(Config.LogsLocation), line + Environment.NewLine);
            }
        }

        internal static void DeleteLine(string logName, int lineNumber)
        {
            List<string> lines = GetLines(logName);
            lines.RemoveAt(lineNumber - 1);
            File.WriteAllLines(logName.GetLogTextFileName(Config.LogsLocation), lines);
        }

        internal static void ReplaceLine(string logName, int lineNumber, string newLine)
        {
            List<string> lines = GetLines(logName);
            lines[lineNumber - 1] = newLine;
            File.WriteAllLines(logName.GetLogTextFileName(Config.LogsLocation), lines);
        }

        internal static void DeleteLogFile(string logName)
        {
            File.Delete(logName.GetLogTextFileName(Config.LogsLocation));
            if (DoesTemplateExists(logName))
            {
                File.Delete(logName.GetLogTemplateFileName(Config.TemplatesLocation));
            }
        }

        internal static void DeleteTemplateFile(string logName)
        {
            if (DoesTemplateExists(logName))
            {
                File.Delete(logName.GetLogTemplateFileName(Config.TemplatesLocation));
            }
        }

        internal static void PurgeLogFile(string logName)
        {
            File.WriteAllText(logName.GetLogTextFileName(Config.LogsLocation), string.Empty);
        }

        internal static string ReplaceTemplatedParts(string templateText, string logName, string line)
        {
            return templateText.Replace("{log}", line)
                .Replace("{logName}", logName)
                .Replace("{date}", DateTime.Now.ToString("dd.MM.yyyy"))
                .Replace("{time}", DateTime.Now.ToString("HH:mm:ss"))
                .Replace("{formattedTime}", DateTime.Now.ToString())
                .Replace("{shortFormattedTime}", DateTime.Now.ToShortTimeString())
                .Replace("{shortFormattedDate}", DateTime.Now.ToShortDateString());

        }

        internal static List<string> GetLogFiles()
        {
            List<string> fileNames = new List<string>();
            foreach (var item in Directory.GetFiles(Config.LogsLocation, "*.txt"))
            {
                fileNames.Add(Path.GetFileNameWithoutExtension(item));
            }

            return fileNames;
        }

        internal static List<LogData> GetLogFilesData()
        {
            List<LogData> logs = new List<LogData>();
            foreach (var item in Directory.GetFiles(Config.LogsLocation, "*.txt"))
            {
                logs.Add(new LogData(Path.GetFileNameWithoutExtension(item), 
                    File.GetCreationTime(item).ToShortDateString(), 
                    File.GetLastWriteTime(item).ToShortDateString()));
            }

            return logs;
        }
        internal static string GetContents(string logName)
        {
            return File.ReadAllText(logName.GetLogTextFileName(Config.LogsLocation));
        }

        internal static string GetLogTextFileName(this string logName, string basePath)
        {
            return Path.Combine(basePath, logName + ".txt");
        }

        internal static string GetLogTemplateFileName(this string logName, string basePath)
        {
            return Path.Combine(basePath, logName + ".clt");
        }
    }
}

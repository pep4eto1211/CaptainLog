using CaptainLog.Core.Models;
using CaptainLog.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainLog.Core.Services
{
    public class LogsCrudService
    {
        public LogsCrudService()
        {
            
        }

        public void CreateLog(string logName)
        {
            Files.CreateLogFiles(logName);
        }

        public void CreateLogTemplate(string logName, string template)
        {
            Files.CreateTemplateFile(logName, template);
        }

        public bool DoesLogExists(string logName)
        {
            return Files.DoesLogFilesExist(logName);
        }

        public void WriteLineToLog(string logName, string line)
        {
            Files.WriteLine(logName, line);
        }

        public string ReadLog(string logName)
        {
            return Files.GetContents(logName);
        }

        public List<string> ReadLogLines(string logName)
        {
            return Files.GetLines(logName);
        }

        public List<string> ListLogs()
        {
            return Files.GetLogFiles();
        }

        public List<LogData> ListLogData()
        {
            return Files.GetLogFilesData();
        }

        public void OpenLogForEditing(string logName)
        {
            System.Diagnostics.Process.Start("notepad", $"{Path.GetFullPath(logName.GetLogTextFileName(Config.LogsLocation))}");
        }

        public void PurgeLog(string logName)
        {
            Files.PurgeLogFile(logName);
        }

        public void DeleteLog(string logName)
        {
            Files.DeleteLogFile(logName);
        }

        public void DeleteTemplate(string logName)
        {
            Files.DeleteTemplateFile(logName);
        }

        public void ReplaceLine(string logName, int lineNumber, string newLine)
        {
            Files.ReplaceLine(logName, lineNumber, newLine);
        }

        public void DeleteLine(string logName, int lineNumber)
        {
            Files.DeleteLine(logName, lineNumber);
        }
    }
}

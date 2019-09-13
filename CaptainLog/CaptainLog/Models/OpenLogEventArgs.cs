using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainLog.Models
{
    public class OpenLogEventArgs : EventArgs
    {
        public OpenLogEventArgs(string logName)
        {
            this.LogName = logName;
        }

        public string LogName
        {
            get; set;
        }
    }
}

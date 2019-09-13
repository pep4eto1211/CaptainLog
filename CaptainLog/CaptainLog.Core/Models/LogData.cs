using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainLog.Core.Models
{
    public class LogData
    {
        public LogData(string logName, string createdDate, string lastModifiedDate)
        {
            this.LogName = logName;
            this.CreatedDate = createdDate;
            this.LastModifiedDate = lastModifiedDate;
        }

        public string LogName
        {
            get; set;
        }

        public string CreatedDate
        {
            get; set;
        }

        public string LastModifiedDate
        {
            get; set;
        }
    }
}

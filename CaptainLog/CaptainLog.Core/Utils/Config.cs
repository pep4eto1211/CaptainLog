using CaptainLog.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainLog.Core.Utils
{
    internal static class Config
    {
        private static ConfigData _configData;

        static Config()
        {
            PopulateConfigData();
        }

        private static void PopulateConfigData()
        {
            string configDataJson = File.ReadAllText(@"Files\config.json");
            _configData = JsonConvert.DeserializeObject<ConfigData>(configDataJson);
        }

        internal static ConfigData ConfigData { get => _configData; set => _configData = value; }

        internal static string LogsLocation
        {
            get
            {
                return _configData.LogsLocation;
            }
            set
            {
                _configData.LogsLocation = value;
            }
        }

        internal static string TemplatesLocation
        {
            get
            {
                return _configData.TemplatesLocation;
            }
            set
            {
                _configData.TemplatesLocation = value;
            }
        }
    }
}

using log4net.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace flight_management_system
{
    public class flightManagementSystemAppConfig
    {
        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string m_file_name;
        private JObject m_configRoot;

        public string ConnectionString { get; set; }
        public bool AllowDBWrite { get; set; }
        public string AppName { get; set; }

        public flightManagementSystemAppConfig()
        {
            

            Init("C:\\Users\\liron\\Source\\Repos\\flight management system\\flight management system\\flightManagementSystem.config.json");
        }
        internal void Init(string file_name)
        {
            m_file_name = file_name;

            if (!File.Exists(m_file_name))
            {
                my_logger.Error($"File {m_file_name} not exist!");
                Console.WriteLine($"File {m_file_name} not exist!");
                Environment.Exit(-1);
            }

            var reader = File.OpenText(m_file_name);
            string json_string = reader.ReadToEnd();

            JObject jo = (JObject)JsonConvert.DeserializeObject(json_string);
            m_configRoot = (JObject)jo["flightManagementSystemConfig"];
            ConnectionString = m_configRoot["ConnectionString"].Value<string>();
            AllowDBWrite = m_configRoot["AllowDBWrite"].Value<bool>();
            AppName = m_configRoot["AppName"].Value<string>();
        }
    }
}


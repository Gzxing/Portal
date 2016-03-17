using log4net;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using log4net.Config;

namespace Portal.Framework.Logging
{
    public class Log4netFactory 
    {
        private static bool _isFileWatched = false;


        public Log4netFactory(string configFilename)
        {
            if (!_isFileWatched && !string.IsNullOrWhiteSpace(configFilename))
            {
                // Only monitor configuration file in full trust
                //XmlConfigurator.Configure(new FileInfo(configFilename));
                _isFileWatched = true;
            }
        }

        public ILogger Create(string name, LogLevel level)
        {
            throw new NotSupportedException("Logger levels cannot be set at runtime. Please review your configuration file.");
        }

        public ILogger Create(string name)
        {
            return new Log4netLogger(LogManager.GetLogger(name), this);
        }
    }
}

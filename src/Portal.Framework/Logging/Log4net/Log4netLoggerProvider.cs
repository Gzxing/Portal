using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Framework.Logging.Log4net
{
    public class Log4NetLoggerProvider : ILoggerProvider
    {
        private readonly Log4netFactory _log4NetLoggerFactory;

        public Log4NetLoggerProvider(string configFilename)
        {
            _log4NetLoggerFactory = new Log4netFactory(configFilename);
        }

        public Log4NetLoggerProvider(Log4netFactory log4NetLoggerFactory)
        {
            _log4NetLoggerFactory = log4NetLoggerFactory;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _log4NetLoggerFactory.Create(categoryName);
        }

        public void Dispose()
        {
        }
    }
}

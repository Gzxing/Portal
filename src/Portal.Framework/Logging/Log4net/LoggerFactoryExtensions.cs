using Portal.Framework.Logging;
using Portal.Framework.Logging.Log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Logging
{
    public  static partial class  LoggerFactoryExtensions
    {
        public static ILoggerFactory AddLog4Net(this ILoggerFactory factory,string configFilename)
        {
            factory.AddProvider(new Log4NetLoggerProvider(configFilename));
            return factory;
        }
    }
}

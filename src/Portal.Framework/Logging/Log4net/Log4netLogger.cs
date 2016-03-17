using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Microsoft.Extensions.Logging;

namespace Portal.Framework.Logging
{
    public class Log4netLogger: ILogger
    {
        private ILog log;
        private Log4netFactory log4netFactory;

        public Log4netLogger(ILog log, Log4netFactory log4netFactory)
        {
            this.log = log;
            this.log4netFactory = log4netFactory;
        }

        public void Log(LogLevel logLevel, int eventId, object state, Exception exception, Func<object, Exception, string> formatter)
        {
            if (!this.IsEnabled(logLevel))
                return;
            string str = string.Empty;
            ILogValues logValues = state as ILogValues;
            string message;
            if (formatter != null)
                message = formatter(state, exception);
            else if (logValues != null)
            {
                StringBuilder builder = new StringBuilder();
                this.FormatLogValues(builder, logValues, 1, false);
                message = builder.ToString();
            }
            else
                message = LogFormatter.Formatter(state, exception);
            if (string.IsNullOrEmpty(message))
                return;

            switch (logLevel)
            {
                case LogLevel.Debug:
                case LogLevel.Verbose:
                    log.Debug(message, exception);
                    break;
                case LogLevel.Information:
                    log.Info(message, exception);
                    break;
                case LogLevel.Warning:
                    log.Warn(message, exception);
                    break;
                case LogLevel.Error:
                    log.Error(message, exception);
                    break;
                case LogLevel.Critical:
                    log.Fatal(message, exception);
                    break;
            }

        }

        public bool IsEnabled(LogLevel logLevel)
        {
            switch (logLevel)
            {
                
                case LogLevel.Debug:
                case LogLevel.Verbose:
                    return log.IsDebugEnabled;
                case LogLevel.Information:
                    return log.IsInfoEnabled;
                case LogLevel.Warning:
                    return log.IsWarnEnabled;
                case LogLevel.Error:
                    return log.IsErrorEnabled;
                case LogLevel.Critical:
                    return log.IsFatalEnabled;
                default:
                    return false;
            }
        }

        /// <summary>
        ///  开启一个逻辑操作作用域
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public IDisposable BeginScopeImpl(object state)
        {
            return null;
        }

        private void FormatLogValues(StringBuilder builder, ILogValues logValues, int level, bool bullet)
        {
            IEnumerable<KeyValuePair<string, object>> values = logValues.GetValues();
            if (values == null)
                return;
            bool flag = true;
            foreach (KeyValuePair<string, object> keyValuePair in values)
            {
                builder.AppendLine();
                if (bullet & flag)
                    builder.Append(' ', level * 2 - 1).Append('-');
                else
                    builder.Append(' ', level * 2);
                builder.Append(keyValuePair.Key).Append(": ");
                if (keyValuePair.Value is IEnumerable && !(keyValuePair.Value is string))
                {
                    foreach (object obj in (IEnumerable)keyValuePair.Value)
                    {
                        if (obj is ILogValues)
                            this.FormatLogValues(builder, (ILogValues)obj, level + 1, true);
                        else
                            builder.AppendLine().Append(' ', (level + 1) * 2).Append(obj);
                    }
                }
                else if (keyValuePair.Value is ILogValues)
                    this.FormatLogValues(builder, (ILogValues)keyValuePair.Value, level + 1, false);
                else
                    builder.Append(keyValuePair.Value);
                flag = false;
            }
        }
    }
}

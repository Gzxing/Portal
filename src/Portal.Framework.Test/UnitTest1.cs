using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Logging;

namespace Portal.Framework.Test
{
    [TestClass]
    public class CastleLoggerTest
    {
        [TestMethod]
        public void CastleLoggerProvider()
        {
            ILoggerFactory loggerFactory = new LoggerFactory();
        }
    }
}

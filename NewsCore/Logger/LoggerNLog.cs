using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsCore
{
    public class LoggerNLog:ILogger
    {
        Logger newsLogger;

        public LoggerNLog()
        {
            this.newsLogger = LogManager.GetLogger("News_logger");
        }
        void ILogger.Log(string msg)
        {
            newsLogger.Debug(msg);
        }
    }
}

using NewsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            SupportLogger();
            Console.ReadKey();
        }

        static void SupportLogger()
        {
            ILogger theLogger = new LoggerNLog();
            theLogger.Log("NewsConsole: SupportLogger");
        }

    }
}

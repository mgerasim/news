using NewsCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace NewsServiceGrabber
{
    public partial class NewsServiceGrabber : ServiceBase
    {
        public NewsServiceGrabber()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ILogger theLogger = new LoggerNLog();
            theLogger.Log("NewsServiceGrabber: OnStart");
        }

        protected override void OnStop()
        {
            ILogger theLogger = new LoggerNLog();
            theLogger.Log("NewsServiceGrabber: OnStop");
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            try
            {
                ILogger theLogger = new LoggerNLog();
                theLogger.Log("NewsServiceGrabber: TimerTick");
            }
            catch (Exception ex)
            {
            }
        }
    }
}

using NewsCore;
using NewsCore.Grabber;
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
            ILogger theLogger = new LoggerNLog();
            try
            {
                theLogger.Log("NewsServiceGrabber: TimerTick");
                IGrabber theGrabberPrimpogodaLenta = new GrabberPrimpogodaLenta(theLogger);
                theGrabberPrimpogodaLenta.Run();
                IGrabber theGrabberDvrcpod = new GrabberDvrcpodNews(theLogger);
                theGrabberDvrcpod.Run();
                IGrabber theGrabberKhabmeteoHydrology = new GrabberKhabmeteoHydrology(theLogger);
                theGrabberKhabmeteoHydrology.Run();
                IGrabber theGrabberKhabkrai = new GrabberKhabkrai(theLogger);
                theGrabberKhabkrai.Run();
                IGrabber theGrabberGeoStorm = new GrabberGeoStorm(theLogger);
                theGrabberGeoStorm.Run();
            }
            catch (Exception ex)
            {
                theLogger.Log(ex.Message);
                theLogger.Log(ex.StackTrace);
            }
        }
    }
}

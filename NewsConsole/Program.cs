﻿using NewsCore;
using NewsCore.Grabber;
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
            //SupportUpdateSchema();
          //  SupportLogger();
            //SupportGrabber();
            //SupportGrabberPrimpogodaWeatherNow();
           //SupportGrabberPrimpogodaLenta();
            //SupportGrabberDvrcpodLenta();
            //SupportGrabberKhabmeteoHydrology();
//          SupportGrabberKhabkrai();
            SupportGrabberGeoStorm();
            Console.ReadKey();
        }
        static void SupportGrabberGeoStorm()
        {
            ILogger theLogger = new LoggerConsole();
            IGrabber theGrabber = new GrabberGeoStorm(theLogger);
            theGrabber.Run();
        }
        static void SupportGrabberKhabkrai()
        {
            ILogger theLogger = new LoggerConsole();
            IGrabber theGrabber = new GrabberKhabkrai(theLogger);
            theGrabber.Run();
        }
        static void SupportGrabberKhabmeteoHydrology()
        {
            ILogger theLogger = new LoggerConsole();
            IGrabber theGrabber = new GrabberKhabmeteoHydrology(theLogger);
            theGrabber.Run();
        }
        static void SupportGrabberDvrcpodLenta()
        {
            ILogger theLogger = new LoggerConsole();
            IGrabber theGrabber = new GrabberDvrcpodNews(theLogger);
            theGrabber.Run();
        }
        static void SupportGrabberPrimpogodaLenta()
        {
            ILogger theLogger = new LoggerConsole();
            IGrabber theGrabber = new GrabberPrimpogodaLenta(theLogger);
            theGrabber.Run();
        }
        static void SupportGrabberPrimpogodaWeatherNow()
        {
            ILogger theLogger = new LoggerConsole();
            IGrabber theGrabber = new GrabberPrimpogodaWeatherNow(theLogger);
            theGrabber.Run();
        }
        static void SupportGrabber()
        {
            ILogger theLogger = new LoggerConsole();
            IGrabber theGrabber = new GrabberPrimpogoda(theLogger);
            theGrabber.Run();
        }
        static void SupportLogger()
        {
            ILogger theLogger = new LoggerNLog();
            theLogger.Log("NewsConsole: SupportLogger");
        }

        static void SupportUpdateSchema()
        {
            NewsEntity.Common.NHibernateHelper.UpdateSchema();
        }

    }
}

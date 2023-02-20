using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Edis.Diagnostics
{
    public static class Log
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void Spam(string message)
        {
            Logger.Trace(message);
        }

        public static void Spam(string message, Exception ex)
        {
            Logger.Trace(ex, message);
        }

        public static void Debug(string message)
        {
            Logger.Debug(message);
        }

        public static void Debug(string message, Exception ex)
        {
            Logger.Debug(ex, message);
        }

        public static void Info(string message)
        {
            Logger.Info(message);
        }

        public static void Info(string message, Exception ex)
        {
            Logger.Info(ex, message);
        }

        public static void Warning(string message)
        {
            Logger.Warn(message);
        }

        public static void Warning(string message, Exception ex)
        {
            Logger.Warn(ex, message);
        }

        public static void Error(string message)
        {
            Logger.Error(message);
        }

        public static void Error(string message, Exception ex)
        {
            Logger.Error(ex, message);
        }

        public static void Fatal(string message)
        {
            Logger.Fatal(message);
        }

        public static void Fatal(string message, Exception ex)
        {
            Logger.Fatal(ex, message);
        }
    }
}

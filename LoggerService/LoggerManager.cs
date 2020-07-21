﻿using Contracts;
using NLog;

namespace LoggerService
{
    public class LoggerManager : ILoggerManager
    {
        #region Private Variables

        private static ILogger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region ILoggerManger Implementation 

        public void LogDebug(string message)
        {
            logger.Debug(message);
        }

        public void LogError(string message)
        {
            logger.Error(message);
        }

        public void LogInfo(string message)
        {
            logger.Info(message);
        }

        public void LogWarn(string message)
        {
            logger.Warn(message);
        }

        #endregion
    }
}

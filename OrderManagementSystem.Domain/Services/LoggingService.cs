using System;
using System.Threading.Tasks;
using OrderManagementSystem.Domain.Interfaces;
using Serilog;

namespace OrderManagementSystem.Domain.Services
{
    public class LoggingService : ILoggingService
    {
        public void LogInformation(string message)
        {
            Log.Information(message);
        }

        public void LogWarning(string message)
        {
            Log.Warning(message);
        }

        public void LogError(string message, Exception exception = null)
        {
            if (exception != null)
                Log.Error(exception, message);
            else
                Log.Error(message);
        }

        public void LogDebug(string message)
        {
            Log.Debug(message);
        }
    }
} 
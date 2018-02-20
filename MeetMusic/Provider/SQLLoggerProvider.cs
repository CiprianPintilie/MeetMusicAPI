using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace MeetMusic.Provider
{
    public class SQLLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new SQLLoger();
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        private class SQLLoger : ILogger
        {
            public IDisposable BeginScope<TState>(TState state)
            {
                return null;// throw new NotImplementedException();
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                //throw new NotImplementedException();
                Debug.WriteLine($"SQL: ${formatter(state, exception)}");
            }
        }
    }
}

using System;
using Microsoft.Extensions.Logging;

namespace MinimalisticFileServerTest.TestDoubles
{
    public class LoggerDummy<T> : ILogger<T>
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return false;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            // chrrrr
        }
    }
}
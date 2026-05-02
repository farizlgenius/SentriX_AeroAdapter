using System;
using System.Runtime.CompilerServices;
using Serilog.Core;
using Serilog.Events;

namespace AeroAdapter.Api.Logging;

public sealed class CustomLog : ILogEventEnricher
{

      public static void Error(ILogger logger, string message)
      {
            logger.LogError(message);
      }

      public static void Info(ILogger logger, string message)
      {
            logger.LogInformation(message);
      }

      public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
      {
            if (logEvent.Properties.TryGetValue("SourceContext", out var value))
            {
                  var fullName = value.ToString().Trim('"'); // remove quotes
                  var shortName = fullName.Split('.').Last(); // take last part
                  logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty("ShortSourceContext", shortName));
            }
      }

      public static void LogCri<T>(ILogger<T> logger, string message, [CallerMemberName] string methodName = "")
      {
            logger.LogCritical("[{Method}] {message}", methodName, message);
      }

      public static void LogErr<T>(ILogger<T> logger, string message, [CallerMemberName] string methodName = "")
      {
            logger.LogError("[{Method}] {message}", methodName, message);
      }

      public static void LogInfo<T>(ILogger<T> logger, string message, [CallerMemberName] string methodName = "")
      {
            logger.LogInformation("[{Method}] {message}", methodName, message);
      }

      public static void LogWarn<T>(ILogger<T> logger, string message, [CallerMemberName] string methodName = "")
      {
            logger.LogWarning("[{Method}] {message}", methodName, message);
      }
}

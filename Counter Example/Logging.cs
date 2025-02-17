using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ServiceLifetimeDemo.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly string LogFilePath = "wwwroot/logs/ButtonClickLogs.csv"; // File path

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var requestPath = context.Request.Path.ToString();

            // Log only button click requests
            if (requestPath.StartsWith("/api/counter/increment"))
            {
                var counterType = requestPath.Split('/').Last(); // Extract counter type
                LogToCsv(counterType);
            }

            // Continue middleware pipeline
            await _next(context);
        }

        private void LogToCsv(string counterType)
        {
            // Ensure directory exists
            var directory = Path.GetDirectoryName(LogFilePath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            // Prepare log entry
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss},{counterType}{Environment.NewLine}";

            // Append to CSV file
            File.AppendAllText(LogFilePath, logEntry);
        }
    }
}

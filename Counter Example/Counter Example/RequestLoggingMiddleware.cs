using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ServiceLifetimeDemo.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly string LogFilePath = "wwwroot/logs/ButtonClickLogs.csv"; 

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var requestPath = context.Request.Path.ToString();

            if (requestPath.StartsWith("/api/counter/increment"))
            {
                var counterType = requestPath.Split('/').Last();
                LogToCsv(counterType);
            }

            await _next(context);
        }

        private void LogToCsv(string counterType)
        {
            var directory = Path.GetDirectoryName(LogFilePath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss},{counterType}{Environment.NewLine}";

            File.AppendAllText(LogFilePath, logEntry);
        }
    }
}

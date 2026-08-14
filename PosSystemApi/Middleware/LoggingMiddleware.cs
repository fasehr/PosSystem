using System.Diagnostics;
using PosSystemApi.Data;
using PosSystemApi.Models;

namespace PosSystemApi.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            AppDbContext dbContext)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            await _next(context);

            stopwatch.Stop();

            Log log = new Log
            {
                Method = context.Request.Method,
                Path = context.Request.Path,
                StatusCode = context.Response.StatusCode,
                UserName = context.User.Identity?.Name,
                DurationMs = stopwatch.ElapsedMilliseconds,
                CreatedAt = DateTime.UtcNow
            };

            await dbContext.Logs.AddAsync(log);
            await dbContext.SaveChangesAsync();
        }
    }
}
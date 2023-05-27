using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace FlowerShop.Middleware
{
    public class LogRequestMiddleware
    {
        private readonly RequestDelegate _next;
        private string logFileName;
        private readonly Serilog.ILogger _logger;
        private readonly MiddlewareOptions _options;
        public LogRequestMiddleware(IOptions<MiddlewareOptions> options, RequestDelegate next,
            ILogger<LogRequestMiddleware> logger)
        {
            _next = next;
            logFileName = $"Logs/log_{DateTime.Now:yyyyMMddHHmmss}.txt";
            _logger = Log.Logger = new LoggerConfiguration()
                .WriteTo.File(logFileName)
                .CreateLogger();
            _options = options.Value;
        }
        
        public async Task InvokeAsync(HttpContext context)
        {
            
            if (_options.EnableRequestLogging)
            {
                string username = context.User.Identity.Name;
                if(!username.IsNullOrEmpty()){
                    string roles = string.Join(", ", context.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value));
                    string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
                    string className = context.Request.Path;
                    string methodName = context.Request.Method;

                    await Task.Run(() =>
                    {
                    _logger.Information($"Request - Username: {username}, Roles: {roles}, Timestamp: {timestamp}, Class: {className}, Method: {methodName}");
                    });
                }

            }

            await _next(context);
        }
    }
}
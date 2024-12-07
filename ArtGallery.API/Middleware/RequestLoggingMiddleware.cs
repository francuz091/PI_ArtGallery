using System.Text;

namespace ArtGallery.API.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await LogRequest(context);
                await _next(context);
                await LogResponse(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request");
                throw;
            }
        }

        private async Task LogRequest(HttpContext context)
        {
            context.Request.EnableBuffering();

            var requestBody = string.Empty;
            if (context.Request.ContentLength > 0)
            {
                using var reader = new StreamReader(
                    context.Request.Body,
                    encoding: Encoding.UTF8,
                    detectEncodingFromByteOrderMarks: false,
                    leaveOpen: true);

                requestBody = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;
            }

            _logger.LogInformation(
                "HTTP Request Information:\n" +
                "Schema:{Schema}\n" +
                "Host: {Host}\n" +
                "Path: {Path}\n" +
                "QueryString: {QueryString}\n" +
                "Request Body: {RequestBody}\n" +
                "Method: {Method}\n" +
                "ContentType: {ContentType}\n" +
                "Timestamp: {Timestamp}",
                context.Request.Scheme,
                context.Request.Host,
                context.Request.Path,
                context.Request.QueryString,
                requestBody,
                context.Request.Method,
                context.Request.ContentType,
                DateTime.UtcNow);
        }

        private async Task LogResponse(HttpContext context)
        {
            _logger.LogInformation(
                "HTTP Response Information:\n" +
                "StatusCode: {StatusCode}\n" +
                "ContentType: {ContentType}\n" +
                "Timestamp: {Timestamp}",
                context.Response.StatusCode,
                context.Response.ContentType,
                DateTime.UtcNow);
        }
    }

    // Extension method za lakše dodavanje middleware-a
    public static class RequestLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLoggingMiddleware>();
        }
    }
}

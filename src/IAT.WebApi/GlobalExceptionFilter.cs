using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Serilog;

namespace IAT.WebApi
{
    /// <summary>
    /// Global exception filter for handling all unhandled exceptions
    /// </summary>
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var exception = context.Exception;

            // Log the exception
            try
            {
                Log.Error(exception, "An unhandled exception occurred. RequestUri: {RequestUri}, Message: {Message}",
                    context.ActionContext.Request.RequestUri,
                    exception.Message);
            }
            catch
            {
                // Catch logging failures silently
            }

            // Create error response
            var errorResponse = new ErrorResponse
            {
                Message = GetUserFriendlyMessage(exception),
                ExceptionType = exception.GetType().Name,
                StackTrace = GetStackTrace(exception),
                Timestamp = DateTime.UtcNow,
                RequestUri = context.ActionContext.Request.RequestUri.ToString()
            };

            // Set HTTP status code
            var statusCode = GetStatusCode(exception);
            
            context.Response = context.Request.CreateResponse(statusCode, errorResponse);
        }

        private HttpStatusCode GetStatusCode(Exception exception)
        {
            if (exception is ArgumentException)
                return HttpStatusCode.BadRequest;
            
            if (exception is InvalidOperationException)
                return HttpStatusCode.BadRequest;
            
            if (exception is UnauthorizedAccessException)
                return HttpStatusCode.Unauthorized;
            
            return HttpStatusCode.InternalServerError;
        }

        private string GetUserFriendlyMessage(Exception exception)
        {
            if (exception is ArgumentException argEx)
                return argEx.Message;
            
            if (exception is InvalidOperationException invEx)
                return invEx.Message;
            
            return "An unexpected error occurred. Please try again later or contact support.";
        }

        private string GetStackTrace(Exception exception)
        {
#if DEBUG
            return exception.StackTrace;
#else
            return null; // Don't expose stack traces in production
#endif
        }
    }

    /// <summary>
    /// Error response model
    /// </summary>
    public class ErrorResponse
    {
        public string Message { get; set; }
        public string ExceptionType { get; set; }
        public string StackTrace { get; set; }
        public DateTime Timestamp { get; set; }
        public string RequestUri { get; set; }
    }
}

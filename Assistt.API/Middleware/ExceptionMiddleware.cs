using Assistt.Application.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace Assistt.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }


        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "Internal Server Error";

            if(exception is DataNotFoundException)
            {
                statusCode = HttpStatusCode.NotFound;
                message = exception.Message;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var result = JsonConvert.SerializeObject(new { error = message });
            return context.Response.WriteAsync(result);
        }
    }
}

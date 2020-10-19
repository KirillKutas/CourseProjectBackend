using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace API.Common.Exceptions
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
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
            var response = GetExceptionResponse(exception);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = response.Code;

            return context.Response.WriteAsync(response.ToString());
        }

        private ExceptionResponse GetExceptionResponse(Exception exception)
        {
            // if method is increased, it makes sense to consider creating objects dynamically with reflection.

            ExceptionResponse response;

            response = new ExceptionResponse(exception);

            return response;
        }
    }
}

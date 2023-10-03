using BodyTemp.Entities.Exceptions;
using BodyTemp.WebAPI.ViewModels.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Qless.WebAPI.Middleware
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        private async Task HandleError<TException>(TException e, HttpContext context, HttpStatusCode code)
            where TException : Exception
        {
            _logger.LogError(e, e.Message);
            context.Response.StatusCode = (int)code;

            string json = JsonSerializer.Serialize(new BaseResponse
            {
                Success = false,
                Message = e.Message,
            });

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (BodyTempException e)
            {
               await HandleError<BodyTempException>(e, context, HttpStatusCode.BadRequest);
            }
            catch (NotFoundException e)
            {
                await HandleError<NotFoundException>(e, context, HttpStatusCode.NotFound);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ProblemDetails problem = new ProblemDetails
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = "Server Error",
                    Title = "Server Error",
                    Detail = "An internal server error occured."
                };

                string json = JsonSerializer.Serialize(problem);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);
            }
        }
    }
}

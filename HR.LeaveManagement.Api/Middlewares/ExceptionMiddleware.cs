using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace HR.LeaveManagement.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context) 
        {
            try
            {
                await _next(context);
            }catch (Exception ex)
            {
                await ExceptionHandler(context,ex);
            }
        }

        private async Task ExceptionHandler(HttpContext context,Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            CustomValidationProblemDetails problem = new();

            switch (ex)
            {
                case BadRequestException badRequest:
                    statusCode = HttpStatusCode.BadRequest;
                    problem = new CustomValidationProblemDetails
                    {
                        Status = (int)statusCode,
                        Title = badRequest.Message,
                        Detail = badRequest.InnerException?.Message,
                        Type = nameof(BadRequestException),
                        Errors = badRequest.ValidationErrors

                    };
                    break;
                case NotFoundException notFound: 
                    statusCode = HttpStatusCode.NotFound;
                    problem = new CustomValidationProblemDetails
                    {
                        Status = (int)statusCode,
                        Title = notFound.Message,
                        Detail = notFound.InnerException?.Message,
                        Type = nameof(NotFoundException),
                    };
                    break;
                default:
                    problem = new CustomValidationProblemDetails
                    {
                        Status = (int)statusCode,
                        Title = ex.Message,
                        Detail = ex.InnerException?.Message,
                        Type = nameof(HttpStatusCode.InternalServerError),
                    };
                    break;
            }

            context.Response.StatusCode = (int)statusCode;
            var problemText = JsonConvert.SerializeObject(problem);
            _logger.LogError(problemText);
            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}

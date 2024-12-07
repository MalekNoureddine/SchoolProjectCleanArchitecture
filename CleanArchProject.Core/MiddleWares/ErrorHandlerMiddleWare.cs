using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using CleanArchProject.Core.Bases;
using FluentValidation;
using Microsoft.Data.SqlClient;

namespace CleanArchProject.Core.MiddleWares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string>() { Succeeded = false, Message = error?.Message };
                //TODO:: cover all validation errors
                switch (error)
                {
                    case UnauthorizedAccessException e:
                        // custom application error
                        responseModel.Message = error.Message;
                        responseModel.StatusCode = HttpStatusCode.Unauthorized;
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;

                    case ValidationException e:
                        // custom validation error
                        responseModel.Message = error.Message;
                        responseModel.StatusCode = HttpStatusCode.BadRequest;
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    case KeyNotFoundException e:
                        // not found error
                        responseModel.Message = error.Message; ;
                        responseModel.StatusCode = HttpStatusCode.NotFound;
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case Exception e:
                        if (e is DbUpdateException dbUpdateEx)
                        {
                            //context.Response.StatusCode = StatusCodes.Status409Conflict;
                            responseModel.Message += e.Message;
                            responseModel.Message += e.InnerException == null ? "" : "\n" + e.InnerException.Message;
                            responseModel.StatusCode = HttpStatusCode.Conflict;
                            response.StatusCode = (int)HttpStatusCode.Conflict;
                        }
                        else
                        {
                            responseModel.Message = e.Message;
                            responseModel.Message += e.InnerException == null ? "" : "\n" + e.InnerException.Message;

                            responseModel.StatusCode = HttpStatusCode.InternalServerError;
                            response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        }
                        break;
                    default:
                        // unhandled error
                        responseModel.Message = error.Message;
                        responseModel.StatusCode = HttpStatusCode.InternalServerError;
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                var result = JsonSerializer.Serialize(responseModel);

                await response.WriteAsync(result);
            }
        }
    }



    //app.UseMiddleware<ErrorHandlerMiddleware>();
}

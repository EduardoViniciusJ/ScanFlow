using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ScanFlowAWS.Application.DTOs.Responses;
using ScanFlowAWS.Application.Exceptions;
using System.Net;

namespace ScanFlowAWS.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ScanFlowAWSApplicationException)
            {
                HandleProjectException(context);
            }
            else
            {

            }
        }

        public void HandleProjectException(ExceptionContext context)
        {
            if (context.Exception is ErrorOnValidationException exception)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new BadRequestObjectResult(new ResponseErrorsJson(exception.ErrorsMessage));
            }
        }

        public void ThrowUnknowException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new ObjectResult(new ResponseErrorsJson("Errro Desconhecido"));
        }
    }
}

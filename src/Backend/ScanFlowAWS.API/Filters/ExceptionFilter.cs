using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ScanFlowAWS.Application.DTOs.Responses;
using ScanFlowAWS.Application.Exceptions;
using System.Net;

namespace ScanFlowAWS.API.Filters
{
    /// <summary>
    /// Filtro global de exceções da API.
    /// Intercepta exceções lançadas durante a execução da aplicação e
    /// transforma em respostas HTTP.
    /// </summary>  
    public class ExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// Método disparado automaticamente quando ocorre uma exceção em algum endpoint.
        /// </summary>
        /// <param name="context">Exceção capturada.</param>
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ScanFlowAWSApplicationException)
            {
                // Se a exceção for uma exceção da aplicação customizada, trata de forma específica
                HandleProjectException(context);
            }
            else
            {
                // Caso contrário, pode ser tratado como erro desconhecido
                ThrowUnknowException(context);
            }
        }

        /// <summary>
        /// Trata exceções customizadas.
        /// </summary>
        /// <param name="context">Exceção capturada.</param>
        public void HandleProjectException(ExceptionContext context)
        {
            if (context.Exception is ScanFlowAWSApplicationException exception)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                if (exception is ErrorOnValidationException validationException)
                {
                    context.Result = new BadRequestObjectResult(
                        new ResponseErrorsJson(validationException.ErrorsMessage)
                    );
                }
                else
                {
                    context.Result = new BadRequestObjectResult(
                        new ResponseErrorsJson(exception.Message)
                    );
                }
            }
        }

        /// <summary>
        /// Trata exceções não customizadas.
        /// </summary>
        /// <param name="context">Contexto da exceção.</param>
        public void ThrowUnknowException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            context.Result = new ObjectResult(
                new ResponseErrorsJson("Erro Desconhecido")
            );
        }
    }
}

using SalesSystem.Api.CustomExceptionHandler.Abstracts;
using SalesSystem.Core.DTOs;
using SalesSystem.Core.Exceptions;
using System.Net;
using System.Net.Mime;

namespace SalesSystem.Api.CustomExceptionHandler.Concretes
{
    public class BadRequestExceptionHandler : IExceptionHandler
    {
        public ExceptionResultDto Handle(Exception exception)
        {
            var badRequestException = (BadRequestException)exception;

            return new ExceptionResultDto(MediaTypeNames.Text.Plain, (int)HttpStatusCode.BadRequest, badRequestException.Message);
        }
    }
}

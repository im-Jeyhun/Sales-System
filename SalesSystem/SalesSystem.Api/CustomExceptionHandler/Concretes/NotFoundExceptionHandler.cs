using SalesSystem.Api.CustomExceptionHandler.Abstracts;
using SalesSystem.Core.DTOs;
using SalesSystem.Core.Exceptions;
using System.Net;
using System.Net.Mime;

namespace SalesSystem.Api.CustomExceptionHandler.Concretes;


public class NotFoundExceptionHandler : IExceptionHandler
{
    public ExceptionResultDto Handle(Exception exception)
    {
        var notFoundException = (NotFoundException)exception;

        return new ExceptionResultDto(MediaTypeNames.Text.Plain, (int)HttpStatusCode.NotFound, notFoundException.Message);
    }
}
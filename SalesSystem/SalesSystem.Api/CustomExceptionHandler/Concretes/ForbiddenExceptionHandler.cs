using SalesSystem.Api.CustomExceptionHandler.Abstracts;
using SalesSystem.Core.DTOs;
using SalesSystem.Core.Exceptions;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace SalesSystem.Api.CustomExceptionHandler.Concretes;

public class ForbiddenExceptionHandler : IExceptionHandler
{
    public ExceptionResultDto Handle(Exception exception)
    {
        var forbiddenException = (ForbiddenException)exception;

        return new ExceptionResultDto(MediaTypeNames.Application.Json, (int)HttpStatusCode.Forbidden, JsonSerializer.Serialize(forbiddenException.Message));
    }
}

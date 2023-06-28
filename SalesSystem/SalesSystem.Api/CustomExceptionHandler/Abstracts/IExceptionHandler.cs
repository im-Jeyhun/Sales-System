using SalesSystem.Core.DTOs;

namespace SalesSystem.Api.CustomExceptionHandler.Abstracts
{
    public interface IExceptionHandler
    {
        public ExceptionResultDto Handle(Exception exception);

    }
}

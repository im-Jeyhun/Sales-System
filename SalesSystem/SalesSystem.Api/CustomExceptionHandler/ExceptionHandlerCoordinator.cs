using SalesSystem.Api.CustomExceptionHandler.Abstracts;
using SalesSystem.Api.CustomExceptionHandler.Concretes;
using SalesSystem.Core.DTOs;
using SalesSystem.Core.Exceptions;

namespace SalesSystem.Api.CustomExceptionHandler
{
    public class ExceptionHandlerCoordinator
    {
        private Dictionary<Type, IExceptionHandler> _exceptionHandlers = new Dictionary<Type, IExceptionHandler>();

        public ExceptionHandlerCoordinator(
            //ValidationExceptionHandler validationExceptionHandler,
            BadRequestExceptionHandler badRequestExceptionHandler,
            NotFoundExceptionHandler notFoundExceptionHandler
            //ForbiddenExceptionHandler forbiddenExceptionHandler,
            //UnauthorizedExceptionHandler unauthorizedExceptionHandler,
            //GeneralExceptionHandler generalExceptionHandler)
            )
        {
            //RegisterHandler<ValidationException>(validationExceptionHandler);
            RegisterHandler<BadRequestException>(badRequestExceptionHandler);
            RegisterHandler<NotFoundException>(notFoundExceptionHandler);
            //RegisterHandler<ForbiddenException>(forbiddenExceptionHandler);
            //RegisterHandler<UnauthorizedException>(unauthorizedExceptionHandler);
            //RegisterHandler<Exception>(generalExceptionHandler);
        }

        public void RegisterHandler<TException>(IExceptionHandler handler)
            where TException : Exception
        {
            ArgumentNullException.ThrowIfNull(handler);

            _exceptionHandlers[typeof(TException)] = handler;
        }

        public ExceptionResultDto Handle(Exception exception)
        {
            ArgumentNullException.ThrowIfNull(exception);

            var exceptionType = exception is ApplicationException ? exception.GetType() : typeof(Exception);

            try
            {
                return _exceptionHandlers[exceptionType].Handle(exception);
            }
            catch (KeyNotFoundException e)
            {
                throw new Exception($"Exception handler for ({exceptionType.Name}) is not registered in coordinator. \n {e.Message}");
            }
        }
    }
}

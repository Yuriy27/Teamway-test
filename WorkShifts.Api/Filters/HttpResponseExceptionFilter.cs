using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WorkShifts.Domain.Exceptions;

namespace WorkShifts.Api.Filters
{
    public class HttpResponseExceptionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is DomainException exception)
            {
                context.Result = new JsonResult(exception.Message)
                {
                    StatusCode = exception.StatusCode,
                };
                context.ExceptionHandled = true;
            }
        }
    }
}

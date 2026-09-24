using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Train_Project.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DateRangeFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ActionArguments.TryGetValue("from", out var fromValue) ||
                !context.ActionArguments.TryGetValue("to", out var toValue))
            {
                return;
            }

            if (fromValue is DateOnly from && toValue is DateOnly to && from > to)
            {
                context.Result = new BadRequestObjectResult(new
                {
                    message = "'from' date must be earlier than or equal to 'to' date."
                });
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ComputerHardwareStore.Presentation.ActionFilters
{
    public class BindingValidationFilterAttribute : IActionFilter
    {
        public BindingValidationFilterAttribute()
        {
            
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var action = context.RouteData.Values["action"];
            var controller = context.RouteData.Values["controller"];

            var param = context.ActionArguments
                .SingleOrDefault(x => x.Value.ToString().Contains("Dto")).Value;

            if (param is null)
            {
                context.Result = new BadRequestObjectResult($"Dto object is null. Controller: {controller}. Action: {action}");
            }
        }
    }
}

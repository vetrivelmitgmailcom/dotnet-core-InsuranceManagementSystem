using InsuranceManagementSystemMVC.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace InsuranceManagementSystemMVC.Filters
{
    public class ExcludeHomeControllerAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Controller is HomeController)
            {
                return;     // Skip the filter for HomeController
            }

            base.OnActionExecuting(context);
        }
    }

}

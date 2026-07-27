using InsuranceManagementSystemMVC.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace InsuranceManagementSystemMVC.Filters
{
    public class AdminAuthenticationFilter : IActionFilter
    {
        //[ExcludeHomeController]-->it is not working
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Controller is HomeController || context.Controller is LoginController)  //context.Controller.GetType().Name == "HomeController"
            { 
                return;            // Skip the AdminAuthenticationFilter for HomeController and Login Controller
            }

            var adminId = context.HttpContext.Session.GetString("AdminId");
            if (adminId == null)
            {
                context.Result = new RedirectResult("/");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // This method is optional and can be used for post-processing tasks after the action method is executed.
        }
    }
}

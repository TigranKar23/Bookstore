using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Bookstore.BLL.Constants;
using Bookstore.BLL.Helpers;
using Bookstore.DTO;
using Microsoft.AspNetCore.Http;

public class AdminRoleAttribute : Attribute, IAsyncActionFilter
{
    private readonly ErrorHelper _errorHelper;

    public AdminRoleAttribute(ErrorHelper errorHelper)
    {
        _errorHelper = errorHelper;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var userRole = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        if (userRole != "Admin")
        {
            var response = new ResponseDto<string>
            {
                Errors = new List<ErrorModelDto>()
            };

            response = await _errorHelper.SetError(response, ErrorConstants.BookIsOver);

            context.Result = new JsonResult(response)
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
            return;
        }

        await next();
    }
}
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentDB_starterAPI.Filters
{
    public class CheckIdFilterAttribute : ActionFilterAttribute
    {

        /// <summary>
        /// Check if the id into the request exists
        /// </summary>
        /// <param name="actionContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            object idVal;
            //Check if there is an ID
            if (actionContext.ActionArguments.TryGetValue("id", out idVal))
            {
                return base.OnActionExecutingAsync(actionContext, cancellationToken);
            }
            else
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "ID is missing");
                return base.OnActionExecutingAsync(actionContext, cancellationToken);
            }
        }

    }
}
using ApiWithAuthentication.Configurations;
using ApiWithAuthentication.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Net.Mime;

namespace ApiWithAuthentication.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext
                .Request
                .Headers
                .TryGetValue(
                ConfigurationConstants.ApiKey,
                out var apiKey
                )
            )
            {
                context.Result = new ContentResult
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Content = "Api Key is required.",
                    ContentType = MediaTypeNames.Application.Json
                };
                return;
            }

            if (apiKey != ApiKeyConfiguration.ApiKey)
            {
                context.Result = new ContentResult
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Content = "Invalid Api Key.",
                    ContentType = MediaTypeNames.Application.Json
                };
                return;
            }

            await next();
        }
    }
}

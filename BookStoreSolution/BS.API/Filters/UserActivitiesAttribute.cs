using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BS.Domain.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BS.API.Filters
{
    public class UserActivitiesAttribute : IActionFilter
    {
        private readonly ILogger<UserActivitiesAttribute> _logger;

        public UserActivitiesAttribute(ILogger<UserActivitiesAttribute> logger)
        {
            _logger = logger;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string areaName = string.IsNullOrEmpty(Convert.ToString(context.RouteData.Values["area"])) ? "" : context.RouteData.Values["area"].ToString().ToLower();
            string controllerName = context.RouteData.Values["controller"].ToString().ToLower();
            string actionName = context.RouteData.Values["action"].ToString().ToLower();

            #region Log User Activity
            try
            {
                LogUserActivity logActivity = new LogUserActivity();
                logActivity.Id = Guid.NewGuid();
                logActivity.CreatedDateTime = DateTime.UtcNow;
                logActivity.IPAddress = context.HttpContext.Connection.RemoteIpAddress.ToString();
                logActivity.Browser = context.HttpContext.Request.Headers["User-Agent"].ToString();
                logActivity.UrlData = string.Format("{0}://{1}{2}{3}", context.HttpContext.Request.Scheme, context.HttpContext.Request.Host, context.HttpContext.Request.Path, context.HttpContext.Request.QueryString);


                if (context.HttpContext.User.Identity.IsAuthenticated)
                {
                    logActivity.UserId = Guid.Parse(context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                }

                logActivity.HttpMethod = context.HttpContext.Request.Method;

                if (context.HttpContext.Request.Path != "/")
                {
                    logActivity.UserData +=
                        JsonConvert.SerializeObject(
                            new
                            {
                                context.HttpContext.Request.Path
                            });
                }
                if (context.HttpContext.Request.QueryString.HasValue)
                {
                    logActivity.UserData +=
                        JsonConvert.SerializeObject(
                            new
                            {
                                context.HttpContext.Request.QueryString
                            });
                }
                logActivity.UserData +=
                    JsonConvert.SerializeObject(
                        new
                        {
                            context.RouteData.Values
                        });


                if (context.HttpContext.Request.HasFormContentType)
                {
                    logActivity.UserData =
                        JsonConvert.SerializeObject(
                            new
                            {
                                context.HttpContext.Request.Form
                            });
                }


                //var _logUserActivityServices = commonServices.ServiceProvider.GetRequiredService<ILogActivityService>();

                // await  _logUserActivityServices.AddAsync(logActivity);
                _logger.LogInformation(JsonConvert.SerializeObject(logActivity));
                //FileHelper.WriteToFile(JsonConvert.SerializeObject(logActivity));

            }
            catch (Exception ex)
            {
            }

            #endregion

        }
    }
}

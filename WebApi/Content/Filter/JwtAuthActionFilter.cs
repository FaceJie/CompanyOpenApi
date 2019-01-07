using APIHelperLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApi.Content.Filter
{
    public class JwtAuthActionFilter : AuthorizeAttribute
    {
        /// <summary>
        /// 验证身份
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var authHeader = from t in actionContext.Request.Headers where t.Key == "token" select t.Value.FirstOrDefault();
            if (authHeader != null)
            {
                string token = authHeader.FirstOrDefault();
                try
                {
                    var json = YQOpenApiHelper.ValidateToken(token);
                    if (json.scu)
                    {
                        actionContext.RequestContext.RouteData.Values.Add("token", json);
                        return true;
                    }
                    else
                    {
                        setErrorResponse(actionContext, "验证错误！");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    setErrorResponse(actionContext, ex.Message);
                    return false;
                }
            }
            else
            {
                setErrorResponse(actionContext, "验证错误！");
                return false;
            }
        }
        private static void setErrorResponse(HttpActionContext actionContext, string message)
        {
            var response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, message);
            actionContext.Response = response;
        }
    }
}
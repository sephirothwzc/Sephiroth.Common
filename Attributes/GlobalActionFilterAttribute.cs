using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Sephiroth.Infrastructure.Common.Attributes
{
    /// <summary>
    /// 全局校验拦截器
    /// </summary>
    public class GlobalActionFilterAttributeValid: ActionFilterAttribute
    {
        /// <summary>
        /// 校验action的参数序列
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid == false)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
            }
        }
    }
}
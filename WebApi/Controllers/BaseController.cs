using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApi.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        /// 执行控制器中的方法之前先执行该方法。
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            #region 登录用户验证
            //1、判断Session对象是否存在
            if (filterContext.HttpContext.Session == null)
            {
                filterContext.HttpContext.Response.Write(
                       " <script type='text/javascript'> alert('~登录已过期，请重新登录');window.top.location='/'; </script>");
                filterContext.RequestContext.HttpContext.Response.End();
                filterContext.Result = new EmptyResult();
                return;
            }
            //2、登录验证
            //if (this.CurrentUser == null)
            //{
            //    filterContext.HttpContext.Response.Write(
            //        " <script type='text/javascript'> alert('登录已过期，请重新登录'); window.top.location='/';</script>");
            //    filterContext.RequestContext.HttpContext.Response.End();
            //    filterContext.Result = new EmptyResult();
            //    return;
            //}
            #endregion
        }
        #region 用户对象
        /// <summary>
        /// 获取当前用户对象
        /// </summary>
        //public AuthUser CurrentUser
        //{
        //    get
        //    {
        //        //从Session中获取用户对象
        //        if (SessionHelper.GetSession("CurrentUser") != null)
        //        {
        //            return SessionHelper.GetSession("CurrentUser") as AuthUser;
        //        }
        //        //Session过期 通过Cookies中的信息 重新获取用户对象 并存储于Session中

        //        return null;
        //    }
        //}


        #endregion
    }
}
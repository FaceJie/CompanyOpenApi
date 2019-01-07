using APIHelperLib;
using APIModel;
using Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApi.Controllers
{
    public class LoginController : Controller
    {
        AuthUserBiz authUserBiz = new AuthUserBiz();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult CheckLogion(string userName, string userPwd)
        {
            MsgModel ret = new MsgModel();
            AuthUser result = authUserBiz.CheckLogion(userName,userPwd);
            if (result == null)
            {
                ret.scu = false;
                ret.msg = "用户密码错误！";
            }
            else
            {
                Session["CurrentUser"] = result;
                ret.scu = true;
                ret.msg = "登录成功！";
            }
            return Json(ret, JsonRequestBehavior.AllowGet);
        }
    }
}
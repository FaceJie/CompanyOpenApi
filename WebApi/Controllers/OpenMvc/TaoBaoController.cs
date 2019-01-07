using APIHelperLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Top.Api;
using Top.Api.Request;
using Top.Api.Response;
using Top.Api.Util;

namespace WebApi.Controllers
{
    public class TaoBaoController : Controller
    {
        public ActionResult Index()
        {
            //重定向获取token
            return new RedirectResult("https://oauth.taobao.com/authorize?response_type=code&client_id=25533407&redirect_uri=http://www.easygo-go.com/code.html&state=123456&view=web");
        }
        public string GetAccess_Token()
        {
            string code = Request.QueryString["code"].ToString();
            string str = "";
            if (!string.IsNullOrEmpty(code))
            {
                str = FZOpenApiHelper.ClientAuthOpenApi(code);
                JObject result = JObject.Parse(str);
                Session["access_token"] = result.Value<string>("access_token").ToString();
            }
            else
            {
                str = "授权码无效";
            }

            return str;
        }

        public string RequestApi()
        {
            string session = Session["access_token"].ToString();
            IDictionary<string, string> param = new Dictionary<string, string>();
            param.Add("item_id", "1234L");
            param.Add("out_product_id", "1111");
            string str = FZOpenApiHelper.RquestOpenApi(session, "taobao.alitrip.travel.item.single.query", param);
            return str;
        }

        public string SerachOrder()
        {
            string session = Session["access_token"].ToString();
            string str = FZOpenApiHelper.GetTravelSearch(session);
            return str;
        }
    }
}
   
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NFinal.OAuth
{
    public class AliPayHelper
    {
        /// <summary>
        /// 取得Access Token
        /// </summary>
        /// <param name="code">临时Authorization Code</param>
        /// <returns>Dictionary</returns>
        public static Dictionary<string, object> get_access_token(OAuthConfig config, string return_url)
        {
            string send_url = "https://openauth.alipay.com/oauth2/publicAppAuthorize.htm?app_id="+config.oauth_app_id+ "&auth_skip=false&scope=auth_userinfo&redirect_uri="
                + Uri.EscapeUriString(NFinal.Config.Configration.globalConfig.server.url + return_url);
            //发送并接受返回值
            string result = HttpUtility.HttpGet(send_url);
            if (result.Contains("error"))
            {
                return null;
            }
            try
            {
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(result);
                return dic;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取登录用户自己的详细信息
        /// </summary>
        /// <param name="access_token">临时的Access Token</param>
        /// <returns>JsonData</returns>
        public static Dictionary<string, object> get_info(string access_token,string param)
        {
            string send_url = "https://openapi.alipay.com/gateway.do";
            //发送并接受返回值
            string result = HttpUtility.HttpPost(send_url,param);
            if (result.Contains("error"))
            {
                return null;
            }
            try
            {
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(result);
                return dic;
            }
            catch
            {
                return null;
            }
        }
    }
}

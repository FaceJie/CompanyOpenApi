using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.OAuth
{
    public class FeiXinHelper
    {
        public FeiXinHelper()
        { }
       
        /// <summary>
        /// 取得Access Token
        /// </summary>
        /// <param name="code">临时Authorization Code</param>
        /// <returns>Dictionary</returns>
        public static Dictionary<string, object> get_access_token(OAuthConfig config,string return_url,string code)
        {
            string send_url = "https://i.feixin.10086.cn/oauth2/access_token?grant_type=authorization_code&code=" + code + "&client_id=" + config.oauth_app_id + "&client_secret=" + config.oauth_app_key + "&redirect_uri="
                 + Uri.EscapeUriString(NFinal.Config.Configration.globalConfig.server.url + return_url);
            //发送并接受返回值
            string result = HttpUtility.HttpGet(send_url);
            if (result.Contains("error"))
            {
                return null;
            }
            try
            {
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string,object>>(result);
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
        public static Dictionary<string, object> get_info(string access_token)
        {
            string send_url = "https://i.feixin.10086.cn/api/user.json?access_token=" + access_token;
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

    }
}

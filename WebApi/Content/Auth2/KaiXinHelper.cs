using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.OAuth
{
    public class KaiXinHelper
    {
        public KaiXinHelper()
        { }

        /// <summary>
        /// 取得Access Token
        /// </summary>
        /// <param name="code">临时Authorization Code，官方提示10分钟过期</param>
        /// <param name="state">防止CSRF攻击，成功授权后回调时会原样带回</param>
        /// <returns>Dictionary</returns>
        public static Dictionary<string, object> get_access_token(OAuthConfig config,string return_url, string code, string state)
        {
            string send_url = "https://api.kaixin001.com/oauth2/access_token?grant_type=authorization_code&code=" + code + "&client_id=" + config.oauth_app_id + "&client_secret=" + config.oauth_app_key + "&state=" + state + "&redirect_uri="
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
                if (dic.Count > 0)
                {
                    return dic;
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

        /// <summary>
        /// 获取登录用户自己的详细信息
        /// </summary>
        /// <param name="access_token">临时的Access Token</param>
        /// <param name="open_id">用户属性，以英文逗号分隔</param>
        /// <returns>Dictionary</returns>
        public static Dictionary<string, object> get_info(string access_token, string fields)
        {
            string send_url = "https://api.kaixin001.com/users/me.json?fields=" + fields + "&access_token=" + access_token;
            //发送并接受返回值
            string result = HttpUtility.HttpGet(send_url);
            if (result.Contains("error"))
            {
                return null;
            }
            try
            {
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(result);
                if (dic.Count > 0)
                {
                    return dic;
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

    }
}

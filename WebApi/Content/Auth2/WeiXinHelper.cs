using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NFinal.OAuth
{
    public class WeiXinHelper
    {
        /// <summary>
        /// 取得Access Token
        /// </summary>
        /// <param name="code">临时Authorization Code</param>
        /// <returns>Dictionary</returns>
        public static Dictionary<string, object> get_access_token(OAuthConfig config, string return_url, string code)
        {
            string send_url = "https://api.weixin.qq.com/sns/oauth2/access_token?appid="+config.oauth_app_id
                +"&secret="+config.oauth_app_key+"&code="+code+"&grant_type=authorization_code";
            //发送并接受返回值
            string result = HttpUtility.HttpGet(send_url);
            if (result.Contains("errcode"))
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
        /// <returns>Dictionary</returns>
        public static Dictionary<string, object> get_info(string access_token, string openid)
        {
            string send_url = "https://api.weixin.qq.com/sns/userinfo?access_token=" + access_token + "&openid=" + openid + "&lang=zh_CN";
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

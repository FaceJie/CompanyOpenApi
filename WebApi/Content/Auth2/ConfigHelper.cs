using System;
using System.Xml;
using System.Text;
using SimpleJSON;

namespace NFinal.OAuth
{
    public class ConfigHelper
    {
        public ConfigHelper()
        { }
        /// <summary>
        /// 获取OAuth配置信息
        /// </summary>
        /// <param name="oauth_name"></param>
        public static OAuthConfig get_config(NFinal.Config.Plug.PlugConfig plugConfig,string oauthName)
        {
            //赋值
            OAuthConfig config = new OAuthConfig();
            JSONNode oauthNode = plugConfig.JsonObject["oauth"];
            JSONNode oauthConfigNode=null;
            if (oauthNode == null)
            {
                throw new ArgumentNullException("oauth", "插件配置文件中的oauth节点不存在");
            }
            oauthConfigNode = oauthNode[oauthName];
            if (oauthNode == null)
            {
                throw new ArgumentNullException(oauthName, "插件配置文件中的oauth." + oauthName + "不存在");
            }
            config.oauth_name = oauthConfigNode["oauth_name"];
            if (config.oauth_name == null)
            {
                throw new ArgumentNullException("oauth_name", "插件配置文件中的oauth." + oauthName + ".name不存在");
            }
            config.oauth_app_id = oauthConfigNode["oauth_app_id"];
            if (config.oauth_app_id == null)
            {
                throw new ArgumentNullException("oauth_app_id", "插件配置文件中的oauth." + oauthName + ".appId不存在");
            }
            config.oauth_app_key = oauthConfigNode["oauth_app_key"];
            if (config.oauth_app_key == null)
            {
                throw new ArgumentNullException("oauth_app_key", "插件配置文件中的oauth." + oauthName + ".appKey不存在");
            }
            return config;
        }
    }
}

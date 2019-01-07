using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NFinal.OAuth
{
    public class UrlHelper
    {
        public string GetUrl<TController>(string methodName)
        {
            return NFinal.Config.Configration.globalConfig.server.url + NFinal.Url.ActionUrlHelper.Format(NFinal.Url.ActionUrlHelper.formatControllerDictionary[typeof(TController).TypeHandle][methodName].formatUrl);
        }
        public string GetFeiXinIndexUrl()
        {
            return GetUrl<Controllers.FeiXinController>("Index");
        }
        public string GetFeiXinReturnUrl()
        {
            return GetUrl<Controllers.FeiXinController>("ReturnUrl");
        }
    }
}

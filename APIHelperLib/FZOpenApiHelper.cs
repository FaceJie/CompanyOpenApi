using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Top.Api;
using Top.Api.Request;
using Top.Api.Response;
using Top.Api.Util;

namespace APIHelperLib
{
    public class FZOpenApiHelper
    {
        #region 公共变量和参数

        static string appkey = "25533407";
        static string secret = "83f3d0d698dbc6fbd1f7451095d92129";
        static string session = "";

        #endregion

        #region 原生帮助类请求api
        #region 用code换取access_token
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="view">手机端还是web端</param>
        /// <returns></returns>
        
        public static string ClientAuthOpenApi(string code)
        {

            WebUtils webUtils = new WebUtils();
            IDictionary<string, string> pout = new Dictionary<string, string>();
            pout.Add("grant_type", "authorization_code");
            pout.Add("client_id", appkey);
            pout.Add("client_secret", secret);
            pout.Add("code", code);
            pout.Add("redirect_uri", "http://www.easygo-go.com/index.html");
            string output = "";
            try
            {
                output = webUtils.DoPost("https://oauth.taobao.com/token", pout);
            }
            catch (Exception ex)
            {
                output = ex.Message.ToString();
            }
            return output;
        }
        #endregion

        #region 请求任意的action
        public static string RquestOpenApi(string session, string action, IDictionary<string, string> param)
        {
            string queryResult = Post("http://gw.api.taobao.com/router/rest", appkey, secret, action, session, param);
            JObject result = JObject.Parse(queryResult);
            return result.ToString();
        }
        #endregion

        #region top签名
        protected static string CreateSign(IDictionary<string, string> parameters, string secret)
        {
            parameters.Remove("sign");
            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parameters);
            IEnumerator<KeyValuePair<string, string>> dem = sortedParams.GetEnumerator();
            StringBuilder query = new StringBuilder(secret);
            while (dem.MoveNext())
            {
                string key = dem.Current.Key;
                string value = dem.Current.Value;
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                {
                    query.Append(key).Append(value);
                }
            }
            query.Append(secret);
            MD5 md5 = MD5.Create();
            byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(query.ToString()));
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                string hex = bytes[i].ToString("X");
                if (hex.Length == 1)
                {
                    result.Append("0");
                }
                result.Append(hex);
            }
            return result.ToString();
        }
        #endregion

        #region post请求方法向body中添加参数
        protected static string PostData(IDictionary<string, string> parameters)
        {
            StringBuilder postData = new StringBuilder();
            bool hasParam = false;
            IEnumerator<KeyValuePair<string, string>> dem = parameters.GetEnumerator();
            while (dem.MoveNext())
            {
                string name = dem.Current.Key;
                string value = dem.Current.Value;
                // 忽略参数名或参数值为空的参数
                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
                {
                    if (hasParam)
                    {
                        postData.Append("&");
                    }
                    postData.Append(name);
                    postData.Append("=");
                    postData.Append(Uri.EscapeDataString(value));
                    hasParam = true;
                }
            }
            return postData.ToString();
        }
        #endregion

        #region post请求方法，添加公有参数
        public static string Post(string url, string appkey, string appSecret, string method, string session, IDictionary<string, string> param)
        {
            #region -----API系统参数----
            param.Add("app_key", appkey);
            param.Add("method", method);
            param.Add("session", session);
            param.Add("timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            param.Add("format", "xml");
            param.Add("v", "2.0");
            param.Add("sign_method", "md5");
            param.Add("sign", CreateSign(param, appSecret));
            #endregion

            string result = string.Empty;

            #region ---- 完成 HTTP POST 请求----
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.KeepAlive = true;
            req.Timeout = 300000;
            req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
            byte[] postData = Encoding.UTF8.GetBytes(PostData(param));
            Stream reqStream = req.GetRequestStream();
            reqStream.Write(postData, 0, postData.Length);
            reqStream.Close();
            HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
            Encoding encoding = Encoding.GetEncoding(rsp.CharacterSet);
            Stream stream = null;
            StreamReader reader = null;
            stream = rsp.GetResponseStream();
            reader = new StreamReader(stream, encoding);
            result = reader.ReadToEnd();
            if (reader != null) reader.Close();
            if (stream != null) stream.Close();
            if (rsp != null) rsp.Close();
            #endregion
            return Regex.Replace(result, @"[\x00-\x08\x0b-\x0c\x0e-\x1f]", "");
        }
        #endregion

        #endregion

        #region 使用飞猪sdk请求Api

        #region 用code获取access_token
        public static string GetTokenBySDK(string code)
        {

            ITopClient client = new DefaultTopClient("http://gw.api.taobao.com/router/rest", appkey, secret);
            TopAuthTokenCreateRequest req = new TopAuthTokenCreateRequest();
            req.Code = code;
            TopAuthTokenCreateResponse rsp = client.Execute(req);
            return rsp.Body;
        }

        public static string GetTravelSearch(string session)
        {
            ITopClient client = new DefaultTopClient("http://gw.api.taobao.com/router/rest", appkey, secret,"json");
            AlitripTravelTradesSearchRequest req =  new AlitripTravelTradesSearchRequest();
            req.PageSize = 10L;
            req.EndCreatedTime = DateTime.Parse("2018-01-10 12:00:00");
            req.StartCreatedTime = DateTime.Parse("2018-01-01 00:00:00");
            AlitripTravelTradesSearchResponse rsp = client.Execute(req, session);
            return rsp.Body;
        }

        #endregion

        #endregion


    }
}

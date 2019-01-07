using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Text;

namespace NFinal.OAuth
{
    public class HttpUtility
    {
        /// <summary>
        /// HTTP GET方式请求数据.
        /// </summary>
        /// <param name="url">URL.</param>
        /// <returns></returns>
        public static string HttpGet(string url)
        {
            HttpClient client = new HttpClient();
            string responseStr = null;
            try
            {
                responseStr = client.GetStringAsync(url).Result;
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                client.Dispose();
            }
            return responseStr;
        }
        /// <summary>
        /// HTTP POST方式请求数据
        /// </summary>
        /// <param name="url">URL.</param>
        /// <param name="param">POST的数据</param>
        /// <returns></returns>
        public static string HttpPost(string url, string param)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage message = null;
            string responseStr = null;
            try
            {
                message = client.PostAsync(url, new StringContent(param)).Result;
                responseStr = message.Content.ReadAsStringAsync().Result;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                client.Dispose();
                message.Dispose();
            }
            return responseStr;
        }

    }
}

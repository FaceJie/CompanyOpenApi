using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace APIHelperLib
{
    public class MFWOpenApiHelper
    {
        #region 公共变量和密钥

        //秘钥和appid
        //用partnerId、client_secret换取access_token地址为https://openapi.mafengwo.cn/oauth2/token
        const string partnerId = "10245";//商家appid
        const string client_id = "10245";
        const string client_secret = "741ef94e7006721204e747d702b95ebe";//公钥
        const string grant_type = "client_credentials";
        
        static int BLOCK_SIZE = 32;
        const string ase_key = "ajjGeyOFAh2y222F3KDltvKHoMr4x7Ym";//私钥

        #endregion

        #region 1、换取access_token
        /// <summary>
        /// 生成access_token
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static string AuthOpenApi()
        {
            string access_token = "";
            string url = $"https://openapi.mafengwo.cn/oauth2/token?grant_type={grant_type}&client_id={client_id}&client_secret={client_secret}";
            string queryResult = Get(url);
            JObject result = JObject.Parse(queryResult);
            if (result.Value<string>("access_token").ToString() != "")
            {
                access_token = result.Value<string>("access_token").ToString();
            }
            return access_token;
        }
        #endregion

        #region 请求任意的action
        public static string RquestOpenApi(string access_token, string action, string data, string contentType)
        {
            //数据加密
            string cryptoData = CryptoData(data);

            //随机数
            string nonce = CreateRandCdkeys(1);

            //获取时间戳
            TimeSpan ts = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1);
            Int32 ticks = System.Convert.ToInt32(ts.TotalSeconds);
            String timestamp = ticks.ToString();

            //签名
            string signStr = partnerId + action + timestamp + ase_key + nonce + cryptoData;
            string sign = MD5Sign(signStr);

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["partnerId"] = partnerId;
            dic["nonce"] = nonce;
            dic["action"] = action;
            dic["timestamp"] = timestamp;
            dic["data"] = cryptoData;
            dic["access_token"] = access_token;
            dic["sign"] = sign;
            string url = $"https://openapi.mafengwo.cn/deals/rest";
            string queryResult = Post(url, dic, contentType);
            return DecryptData(queryResult);
        }

        #endregion

        #region 2、请求数据加密和解密

        /// <summary>
        /// 请求数据加密
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string CryptoData(string strData)
        {
            string crypto = encrypt(strData);//规定加密方法
            return crypto;
        }

        public static string DecryptData(string cryptoStr)
        {
            string result = decrypt(cryptoStr);//规定解密方法
            return result;
        }
        #endregion

        #region MD5加密sign
        /// <summary>
        ///  sign = MD5(partnerId ＋ action ＋ timestamp ＋ ase_key + nonce + data),经过 md5 加密后的英文字符均应为小写字符
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        public static string MD5Sign(string strData)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string sign = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(strData)));
            return sign.Replace("-", "").ToLower();
        }

        #endregion

        #region 生成16位随机码

        /// <summary>
        /// 生成16位随机码
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static string CreateRandCdkeys(int x)
        {
            string[] codeSerial = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            Random rand = new Random();
            int temp = -1;
            string cdKey = string.Empty;
            for (int i = 0; i < 16; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(x + i * temp * unchecked((int)DateTime.Now.Ticks));
                }
                int randIndex = rand.Next(0, 35);
                temp = randIndex;
                cdKey += codeSerial[randIndex];
            }
            return cdKey;
        }

        #endregion

        #region Base64的编码和解码
        /// <summary>
        /// 编码 EncodeBase64("utf-8", "str");
        /// </summary>
        /// <param name="code_type"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string EncodeBase64(string code_type, string code)
        {
            string encode = "";
            byte[] bytes = Encoding.GetEncoding(code_type).GetBytes(code);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = code;
            }
            return encode;
        }
        /// <summary>
        ///  解码 DecodeBase64("utf-8", "PHRyPjx0ZD7kvaDlpb3llYo8L3RkPjwvdHI+");
        /// </summary>
        /// <param name="code_type"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string DecodeBase64(string code_type, string code)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(code);
            try
            {
                decode = Encoding.GetEncoding(code_type).GetString(bytes);
            }
            catch
            {
                decode = code;
            }
            return decode;
        }
        #endregion

        #region 请求access_token用GET方法
        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url">Get方法的URL</param>
        /// <returns></returns>
        private static string Get(string url)
        {
            string strResult = "";
            try
            {
                HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
                req.ContentType = "multipart/form-data";
                req.Accept = "*/*";
                req.UserAgent = "";
                req.Timeout = 10000;
                req.Method = "Get";
                req.KeepAlive = true;


                HttpWebResponse response = req.GetResponse() as HttpWebResponse;
                using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    strResult = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                strResult = ex.ToString();
            }
            return strResult;
        }
        #endregion

        #region 请求action用Post方法
        public static string Post(string url, Dictionary<string, string> dic, string contentType)
        {
            string resultCryptoStr = "";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
                using (var content = new MultipartFormDataContent())
                {
                    var formDatas = GetFormDataByteArrayContent(GetNameValueCollection(dic));
                    Action<List<ByteArrayContent>> act = (dataContents) =>
                    {
                        foreach (var byteArrayContent in dataContents)
                        {
                            content.Add(byteArrayContent);
                        }
                    };
                    act(formDatas);
                    try
                    {
                        HttpResponseMessage httpResponseMessage = client.PostAsync(url, content).Result;
                        resultCryptoStr = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    }
                    catch (Exception ex)
                    {
                        resultCryptoStr = ex.ToString();
                    }
                }
            }
            return resultCryptoStr;
        }
        /// <summary>
        /// 获取键值集合对应的ByteArrayContent集合
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        private static List<ByteArrayContent> GetFormDataByteArrayContent(NameValueCollection collection)
        {
            List<ByteArrayContent> list = new List<ByteArrayContent>();
            foreach (var key in collection.AllKeys)
            {
                var dataContent = new ByteArrayContent(Encoding.UTF8.GetBytes(collection[key]));
                dataContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    Name = key
                };
                list.Add(dataContent);
            }
            return list;
        }
        /// <summary>
        /// 从DataGridView中获取键值对集合
        /// </summary>
        /// <param name="gv"></param>
        /// <returns></returns>
        private static NameValueCollection GetNameValueCollection(Dictionary<string, string> dics)
        {
            NameValueCollection collection = new NameValueCollection();
            foreach (var dic in dics)
            {
                if (dic.Value != null)
                {
                    collection.Add(dic.Key.ToString(),
                        dic.Value == null ? string.Empty : dic.Value.ToString());
                }
            }
            return collection;
        }
        #endregion

        #region 加密方法与解密方法

        /**
        * 获得对明文进行补位填充的字节.
        *
        * @param count 需要进行填充补位操作的明文字节个数
        * @return 补齐用的字节数组
        */
        public static byte[] fillByte(int count)
        {
            // 计算需要填充的位数
            int amountToPad = BLOCK_SIZE - (count % BLOCK_SIZE);
            if (amountToPad == 0)
            {
                amountToPad = BLOCK_SIZE;
            }
            // 获得补位所用的字符
            char padChr = chr(amountToPad);
            String tmp = string.Empty;
            for (int index = 0; index < amountToPad; index++)
            {
                tmp += padChr;
            }
            return Encoding.UTF8.GetBytes(tmp);
        }


        /**
         * 删除解密后明文的补位字符
         *
         * @param decrypted 解密后的明文
         * @return 删除补位字符后的明文
         */
        public static byte[] removeByte(byte[] decrypted)
        {
            int pad = (int)decrypted[decrypted.Length - 1];
            if (pad < 1 || pad > 32)
            {
                pad = 0;
            }
            byte[] res = new byte[decrypted.Length - pad];
            Array.Copy(decrypted, 0, res, 0, decrypted.Length - pad);
            return res;
        }

        /**
         * 将数字转化成ASCII码对应的字符，用于对明文进行补码
         *
         * @param a 需要转化的数字
         * @return 转化得到的字符
         */
        static char chr(int a)
        {
            byte target = (byte)(a & 0xFF);
            return (char)target;
        }

        /// <summary>
        /// 解密算法
        /// </summary>
        /// <param name="text"></param>
        /// <param name="ase_key"></param>
        /// <returns></returns>
        public static string decrypt(String text)
        {
            byte[] Key = Encoding.UTF8.GetBytes(ase_key);
            byte[] Iv = new byte[16];
            Array.Copy(Key, Iv, 16);
            byte[] Text = Encoding.UTF8.GetBytes(text);
            return AES_decrypt(text, Iv, Key);
        }

        private static string AES_decrypt(String Input, byte[] Iv, byte[] Key)
        {
            RijndaelManaged aes = new RijndaelManaged();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.None;
            aes.Key = Key;
            aes.IV = Iv;
            var decrypt = aes.CreateDecryptor(aes.Key, aes.IV);
            byte[] xBuff = null;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
                {
                    byte[] xXml = Convert.FromBase64String(Input);
                    byte[] msg = new byte[xXml.Length + 32 - xXml.Length % 32];
                    Array.Copy(xXml, msg, xXml.Length);
                    cs.Write(xXml, 0, xXml.Length);
                }
                xBuff = removeByte(ms.ToArray());
            }
            return Encoding.UTF8.GetString(xBuff);
        }

        /// <summary>
        /// 加密算法
        /// </summary>
        /// <param name="text"></param>
        /// <param name="ase_key"></param>
        /// <returns></returns>
        public static string encrypt(string text)
        {
            byte[] Key = Encoding.UTF8.GetBytes(ase_key);
            byte[] Iv = new byte[16];
            Array.Copy(Key, Iv, 16);
            byte[] Text = Encoding.UTF8.GetBytes(text);
            return AES_encrypt(Text, Iv, Key);
        }

        private static String AES_encrypt(byte[] Input, byte[] Iv, byte[] Key)
        {
            var aes = new RijndaelManaged();
            //秘钥的大小，以位为单位
            aes.KeySize = 256;
            //支持的块大小
            aes.BlockSize = 128;
            //填充模式
            //aes.Padding = PaddingMode.PKCS7;
            aes.Padding = PaddingMode.None;
            aes.Mode = CipherMode.CBC;
            aes.Key = Key;
            aes.IV = Iv;
            var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] xBuff = null;

            #region 自己进行PKCS7补位，用系统自己带的不行
            byte[] msg = new byte[Input.Length + 32 - Input.Length % 32];
            Array.Copy(Input, msg, Input.Length);
            byte[] pad = fillByte(Input.Length);
            Array.Copy(pad, 0, msg, Input.Length, pad.Length);
            #endregion
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
                {
                    cs.Write(msg, 0, msg.Length);
                }
                xBuff = ms.ToArray();
            }

            String Output = Convert.ToBase64String(xBuff);
            return Output;
        }

        #endregion
    }
}

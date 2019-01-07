using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace APIHelperLib
{
    public class YQOpenApiHelper
    {
        public static string access_token = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";
        public static string[] secretSet = ConfigurationManager.AppSettings["secrets"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        static int BLOCK_SIZE = 32;
        const string ase_key = "ajjGeyOFAh2y222F3KDltvKHoMr4x7Ym";//私钥


        public static bool CheckClient(string appId, string appSecret)
        {
            bool exists = ((IList)secretSet).Contains(appId + ";" + appSecret);
            return exists;
        }
        #region 创建Token配置参数
        public static MsgModel CreateToken(string appId, string appSecret)
        {
            MsgModel ret = new MsgModel();
            try
            {
                if (!CheckClient(appId, appSecret))
                {
                    ret.scu = false;
                    ret.msg = "你提供的clentId,secret无效！";
                }
                else
                {
                    IDateTimeProvider provider = new UtcDateTimeProvider();
                    var now = provider.GetNow();
                    var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc); 
                    var secondsSinceEpoch = Math.Round((now - unixEpoch).TotalSeconds);
                    var payload = new Dictionary<string, object>
                    {
                        {"iss",appId},
                        {"access_token",access_token},
                        {"exp",secondsSinceEpoch+100},
                    };
                    IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
                    IJsonSerializer serializer = new JsonNetSerializer();
                    IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                    IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
                    var token = encoder.Encode(payload, access_token);
                    ret.scu = true;
                    ret.msg = token;
                }
            }
            catch (Exception ex)
            {
                ret.scu = false;
                ret.msg = ex.Message.ToString();
            }
            return ret;
        }
        #endregion


        public static MsgModel ValidateToken(string token)
        {
            MsgModel ret = new MsgModel();
            //解密解析
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);
                ret.scu = true;
                ret.msg = decoder.Decode(token, access_token, verify: true);
            }
            catch (TokenExpiredException)
            {
                ret.scu = false;
                ret.msg = "token过期了";
            }
            catch (SignatureVerificationException)
            {
                ret.scu = false;
                ret.msg = "签名无效";
            }
            return ret;
        }

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

    public class TokenModel
    {
        /// <summary>
        /// 发行者
        /// </summary>
        public string Iss { get; set; }
        /// <summary>
        /// 发行时间
        /// </summary>
        public DateTime Iat { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public int Exp { get; set; }
    }

    public class MsgModel
    {
        public bool scu { get; set; }
        public string msg { get; set; }
    }
}

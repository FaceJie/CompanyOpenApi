using System;
namespace NFinal.OAuth
{
    /// <summary>
    /// OAuth基本信息
    /// </summary>
    [Serializable]
    public class OAuthConfig
    {
        public OAuthConfig()
        { }

        private string _oauth_name = string.Empty;
        private string _oauth_app_id = string.Empty;
        private string _oauth_app_key = string.Empty;
        //private string _return_url = string.Empty;

        /// <summary>
        /// OAuth名称
        /// </summary>
        public string oauth_name
        {
            set { _oauth_name = value; }
            get { return _oauth_name; }
        }

        /// <summary>
        /// APP ID
        /// </summary>
        public string oauth_app_id
        {
            set { _oauth_app_id = value; }
            get { return _oauth_app_id; }
        }

        /// <summary>
        /// APP KEY
        /// </summary>
        public string oauth_app_key
        {
            set { _oauth_app_key = value; }
            get { return _oauth_app_key; }
        }

        ///// <summary>
        ///// 回传的URL
        ///// </summary>
        //public string return_url
        //{
        //    set { _return_url = value; }
        //    get { return _return_url ; }
        //}

    }
}

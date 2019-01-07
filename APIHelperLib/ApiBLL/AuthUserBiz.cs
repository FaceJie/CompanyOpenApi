using APIModel;
using OpenApiLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz
{
    public class AuthUserBiz
    {
        public AuthUser CheckLogion(string userName, string userPwd)
        {
            AuthUser authUser = new AuthUser();
            try
            {
                string sql = string.Format("SELECT * FROM AuthUser WHERE Account='{0}' AND Password='{1}'",userName,userPwd);
                DataTable dt = SqlHelper.GetTableText(sql, null)[0];
                LoadEntity(dt.Rows[0], authUser);
            }
            catch (Exception ex)
            {
                authUser = null;
            }
            return authUser;
        }

        private void LoadEntity(DataRow row, AuthUser model)
        {
            model.WorkId = row["WorkId"] != DBNull.Value ? row["WorkId"].ToString() : string.Empty;
            model.Account = row["Account"] != DBNull.Value ? row["Account"].ToString() : string.Empty;
            model.UserName = row["UserName"] != DBNull.Value ? row["UserName"].ToString() : string.Empty;
            model.Password = row["Password"] != DBNull.Value ? row["Password"].ToString() : string.Empty;
            model.UserMobile = row["UserMobile"] != DBNull.Value ? row["UserMobile"].ToString() : string.Empty;
            model.DepartmentId = row["DepartmentId"] != DBNull.Value ? new Guid(row["DepartmentId"].ToString()) : Guid.Empty;
            model.RID = row["RID"] != DBNull.Value ? new Guid(row["RID"].ToString()) : Guid.Empty;
            model.RoleName = row["RoleName"] != DBNull.Value ? row["RoleName"].ToString() : string.Empty;
            model.District = row["District"] != DBNull.Value ? Convert.ToInt32(row["District"].ToString()) : 0;
        }
    }
}

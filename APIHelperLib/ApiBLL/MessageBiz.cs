using OpenApiLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz
{
    public class MessageBiz 
    {

        public DataTable GetMeseege(int page, int pagesize, string MsgState)
        {
            int startIndex = (page - 1) * pagesize + 1;
            int endIndex = page * pagesize;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            string orderby = "Id desc";
            string strWhere = " 1=1 ";

            if (!string.IsNullOrEmpty(MsgState.Trim()))
            {
                strWhere += "and MsgState='"+ MsgState+"'";
            }
                    
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.Id desc");
            }
            strSql.Append(")AS Row, T.*  from Message T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return SqlHelper.GetTableText(strSql.ToString(), null)[0];
        }
    }
}

using APIModel;
using OpenApiLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz
{
    public class OrdersBiz 
    {
        public List<Orders> PageOrderList(int page, int pagesize, string timeType, string OrderState, string tradingPlatform, string saleName, string orderNo, string replyResult)
        {
            int startIndex = (page - 1) * pagesize + 1;
            int endIndex = page * pagesize;
            StringBuilder strSql = new StringBuilder();
            string orderby = "Id desc";
            strSql.Append("select *  ");

            strSql.Append(" FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            strSql.Append(")AS Row, T.*  from Orders T ");
            string strWhere = " 1=1 ";
            if (!string.IsNullOrEmpty(OrderState))
            {
                strWhere += "and OrderState='" + OrderState + "'";
            }
            if (!string.IsNullOrEmpty(tradingPlatform))
            {
                strWhere += "and PaymentPlatform='" + tradingPlatform + "'";
            }
            if (!string.IsNullOrEmpty(saleName))
            {
                strWhere += "and WaitorName like '%" + saleName + "%'";
            }
            if (!string.IsNullOrEmpty(orderNo))
            {
                strWhere += "and OrderNo like '%" + orderNo + "%'";
            }
            if (!string.IsNullOrEmpty(replyResult))
            {
                strWhere += "and ReplyResult='" + replyResult + "'";
            }
            switch (timeType)
            {
                case "0":
                    strWhere += "and datediff(day,GuestOrderTime,getdate())=0";//今日的数据
                    break;
                case "1":
                    strWhere += "and datediff(day,GuestOrderTime,getdate()-3)=0";//前三天的数据
                    break;
                case "2":
                    strWhere += "and datediff(week,GuestOrderTime,getdate())=0";//本周的数据
                    break;
                case "3":
                    strWhere += "and datediff(month,GuestOrderTime,getdate())=0";//本月的数据
                    break;
                case "4":
                    strWhere += "and datediff(qq,GuestOrderTime,getdate())=0";//本季度的数据
                    break;
                case "5":
                    strWhere += "and datediff(yy,GuestOrderTime,getdate())=0";//本年的数据
                    break;
                default:
                    //查询所有
                    break;
            }
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return SqlHelper.QueryList<Orders>(strSql.ToString(), null);
        }

        public int GetListCount(string timeType, string OrderState, string tradingPlatform, string saleName, string orderNo, string replyResult)
        {
            string strSql = "SELECT COUNT(*) as listCount FROM Orders";
            string strWhere = " 1=1 ";
            if (!string.IsNullOrEmpty(OrderState))
            {
                strWhere += "and OrderState='" + OrderState + "'";
            }
            if (!string.IsNullOrEmpty(tradingPlatform))
            {
                strWhere += "and PaymentPlatform='" + tradingPlatform + "'";
            }
            if (!string.IsNullOrEmpty(saleName))
            {
                strWhere += "and WaitorName like '%" + saleName + "%'";
            }
            if (!string.IsNullOrEmpty(orderNo))
            {
                strWhere += "and OrderNo like '%" + orderNo + "%'";
            }
            if (!string.IsNullOrEmpty(replyResult))
            {
                strWhere += "and ReplyResult='" + replyResult + "'";
            }
            switch (timeType)
            {
                case "0":
                    strWhere += "and datediff(day,GuestOrderTime,getdate())=0";//今日的数据
                    break;
                case "1":
                    strWhere += "and datediff(day,GuestOrderTime,getdate()-3)=0";//前三天的数据
                    break;
                case "2":
                    strWhere += "and datediff(week,GuestOrderTime,getdate())=0";//本周的数据
                    break;
                case "3":
                    strWhere += "and datediff(month,GuestOrderTime,getdate())=0";//本月的数据
                    break;
                case "4":
                    strWhere += "and datediff(qq,GuestOrderTime,getdate())=0";//本季度的数据
                    break;
                case "5":
                    strWhere += "and datediff(yy,GuestOrderTime,getdate())=0";//本年的数据
                    break;
                default:
                    //查询所有
                    break;
            }
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql += " WHERE " + strWhere;
            }
            DataTable dt = SqlHelper.GetTableText(strSql.ToString(), null)[0];
            return Convert.ToInt32(dt.Rows[0]["listCount"].ToString());
        }


        public string insertOrder(Orders model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into Orders(");
                strSql.Append("OrderNo,PaymentPlatform,GroupNo,ProductName,ProductId,ProductType,GuestUseTime,PurchaseNum,OrderAmount,ReallyPay,PlatformActivity,GuestOrderTime,WaitorOrderTime,WaitorConfirmTime,ReserveTime,DiningTime,DiningShop,CheckMoneyTime,RefundAmout,GuestRefundApplyTime,WaitorRefundApplyTime,WaitorName,IsPraise,RefundReason,WaitorRemark,JpOrderNo,OrderWay,OperOrderTime,JpConfirmTime,ReplyWaitorConfirmTime,ReplyResult,SettlePrice,ExchangeRate,OperRemark,RealIntoAccountTime,TyperName,Commission,WaitorCommision,AdminRemark,OperName,GuestInfoTypedIn,MoneyType,ComboName,DepartureDate,OrderState,RefundState,OrderColor,LabelRemark");
                strSql.Append(") values (");
                strSql.Append("@OrderNo,@PaymentPlatform,@GroupNo,@ProductName,@ProductId,@ProductType,@GuestUseTime,@PurchaseNum,@OrderAmount,@ReallyPay,@PlatformActivity,@GuestOrderTime,@WaitorOrderTime,@WaitorConfirmTime,@ReserveTime,@DiningTime,@DiningShop,@CheckMoneyTime,@RefundAmout,@GuestRefundApplyTime,@WaitorRefundApplyTime,@WaitorName,@IsPraise,@RefundReason,@WaitorRemark,@JpOrderNo,@OrderWay,@OperOrderTime,@JpConfirmTime,@ReplyWaitorConfirmTime,@ReplyResult,@SettlePrice,@ExchangeRate,@OperRemark,@RealIntoAccountTime,@TyperName,@Commission,@WaitorCommision,@AdminRemark,@OperName,@GuestInfoTypedIn,@MoneyType,@ComboName,@DepartureDate,@OrderState,@RefundState,@OrderColor,@LabelRemark");
                strSql.Append(") ");
                strSql.Append(";select @@IDENTITY isScu");
                SqlParameter[] parameters ={
                new SqlParameter("@OrderNo", SqlDbType.VarChar,50) ,
                        new SqlParameter("@PaymentPlatform", SqlDbType.TinyInt,1) ,
                        new SqlParameter("@GroupNo", SqlDbType.VarChar,500) ,
                        new SqlParameter("@ProductName", SqlDbType.VarChar,200) ,
                        new SqlParameter("@ProductId", SqlDbType.Int,4) ,
                        new SqlParameter("@ProductType", SqlDbType.VarChar,50) ,
                        new SqlParameter("@GuestUseTime", SqlDbType.VarChar,100) ,
                        new SqlParameter("@PurchaseNum", SqlDbType.Int,4) ,
                        new SqlParameter("@OrderAmount", SqlDbType.Money,8) ,
                        new SqlParameter("@ReallyPay", SqlDbType.Money,8) ,
                        new SqlParameter("@PlatformActivity", SqlDbType.VarChar,100) ,
                        new SqlParameter("@GuestOrderTime", SqlDbType.DateTime) ,
                        new SqlParameter("@WaitorOrderTime", SqlDbType.DateTime) ,
                        new SqlParameter("@WaitorConfirmTime", SqlDbType.DateTime) ,
                        new SqlParameter("@ReserveTime", SqlDbType.DateTime) ,
                        new SqlParameter("@DiningTime", SqlDbType.DateTime) ,
                        new SqlParameter("@DiningShop", SqlDbType.VarChar,50) ,
                        new SqlParameter("@CheckMoneyTime", SqlDbType.DateTime) ,
                        new SqlParameter("@RefundAmout", SqlDbType.Money,8) ,
                        new SqlParameter("@GuestRefundApplyTime", SqlDbType.DateTime) ,
                        new SqlParameter("@WaitorRefundApplyTime", SqlDbType.DateTime) ,
                        new SqlParameter("@WaitorName", SqlDbType.VarChar,20) ,
                        new SqlParameter("@IsPraise", SqlDbType.VarChar,10) ,
                        new SqlParameter("@RefundReason", SqlDbType.Text) ,
                        new SqlParameter("@WaitorRemark", SqlDbType.Text) ,
                        new SqlParameter("@JpOrderNo", SqlDbType.VarChar,50) ,
                        new SqlParameter("@OrderWay", SqlDbType.VarChar,50) ,
                        new SqlParameter("@OperOrderTime", SqlDbType.DateTime) ,
                        new SqlParameter("@JpConfirmTime", SqlDbType.DateTime) ,
                        new SqlParameter("@ReplyWaitorConfirmTime", SqlDbType.DateTime) ,
                        new SqlParameter("@ReplyResult", SqlDbType.VarChar,20) ,
                        new SqlParameter("@SettlePrice", SqlDbType.Money,8) ,
                        new SqlParameter("@ExchangeRate", SqlDbType.Money,8) ,
                        new SqlParameter("@OperRemark", SqlDbType.Text) ,
                        new SqlParameter("@RealIntoAccountTime", SqlDbType.DateTime) ,
                        new SqlParameter("@TyperName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Commission", SqlDbType.Money,8) ,
                        new SqlParameter("@WaitorCommision", SqlDbType.Money,8) ,
                        new SqlParameter("@AdminRemark", SqlDbType.Text) ,
                        new SqlParameter("@OperName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@GuestInfoTypedIn", SqlDbType.Bit,1) ,
                        new SqlParameter("@MoneyType", SqlDbType.VarChar,20) ,
                        new SqlParameter("@ComboName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@DepartureDate", SqlDbType.DateTime,3) ,
                        new SqlParameter("@OrderState", SqlDbType.VarChar,20) ,
                        new SqlParameter("@RefundState", SqlDbType.VarChar,50) ,
                        new SqlParameter("@OrderColor", SqlDbType.Int,4) ,
                        new SqlParameter("@LabelRemark", SqlDbType.VarChar,100)
            };
                parameters[0].Value = model.OrderNo;
                parameters[1].Value = model.PaymentPlatform;
                parameters[2].Value = model.GroupNo;
                parameters[3].Value = model.ProductName;
                parameters[4].Value = model.ProductId;
                parameters[5].Value = model.ProductType;
                parameters[6].Value = model.GuestUseTime;
                parameters[7].Value = model.PurchaseNum;
                parameters[8].Value = model.OrderAmount;
                parameters[9].Value = model.ReallyPay;
                parameters[10].Value = model.PlatformActivity;
                parameters[11].Value = model.GuestOrderTime;
                parameters[12].Value = model.WaitorOrderTime;
                parameters[13].Value = model.WaitorConfirmTime;
                parameters[14].Value = model.ReserveTime;
                parameters[15].Value = model.DiningTime;
                parameters[16].Value = model.DiningShop;
                parameters[17].Value = model.CheckMoneyTime;
                parameters[18].Value = model.RefundAmout;
                parameters[19].Value = model.GuestRefundApplyTime;
                parameters[20].Value = model.WaitorRefundApplyTime;
                parameters[21].Value = model.WaitorName;
                parameters[22].Value = model.IsPraise;
                parameters[23].Value = model.RefundReason;
                parameters[24].Value = model.WaitorRemark;
                parameters[25].Value = model.JpOrderNo;
                parameters[26].Value = model.OrderWay;
                parameters[27].Value = model.OperOrderTime;
                parameters[28].Value = model.JpConfirmTime;
                parameters[29].Value = model.ReplyWaitorConfirmTime;
                parameters[30].Value = model.ReplyResult;
                parameters[31].Value = model.SettlePrice;
                parameters[32].Value = model.ExchangeRate;
                parameters[33].Value = model.OperRemark;
                parameters[34].Value = model.RealIntoAccountTime;
                parameters[35].Value = model.TyperName;
                parameters[36].Value = model.Commission;
                parameters[37].Value = model.WaitorCommision;
                parameters[38].Value = model.AdminRemark;
                parameters[39].Value = model.OperName;
                parameters[40].Value = model.GuestInfoTypedIn;
                parameters[41].Value = model.MoneyType;
                parameters[42].Value = model.ComboName;
                parameters[43].Value = model.DepartureDate;
                parameters[44].Value = model.OrderState;
                parameters[45].Value = model.RefundState;
                parameters[46].Value = model.OrderColor;
                parameters[47].Value = model.LabelRemark;
                DataTable dt = SqlHelper.GetTableText(strSql.ToString(), parameters)[0];
                return dt.Rows[0]["isScu"].ToString();

            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
    }
}

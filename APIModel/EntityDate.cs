namespace APIModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EntityDate : DbContext
    {
        public EntityDate()
            : base("name=EntityDate")
        {
        }

        public virtual DbSet<AuthUser> AuthUser { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<OrderExcel> OrderExcel { get; set; }
        public virtual DbSet<OrderFiles> OrderFiles { get; set; }
        public virtual DbSet<OrderGuest> OrderGuest { get; set; }
        public virtual DbSet<OrderInfo> OrderInfo { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<OrdersLogs> OrdersLogs { get; set; }
        public virtual DbSet<Enums_OrderInfo_OrderType> Enums_OrderInfo_OrderType { get; set; }
        public virtual DbSet<Enums_OrderInfo_PaymentPlatform> Enums_OrderInfo_PaymentPlatform { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthUser>()
                .Property(e => e.WorkId)
                .IsUnicode(false);

            modelBuilder.Entity<AuthUser>()
                .Property(e => e.Account)
                .IsUnicode(false);

            modelBuilder.Entity<AuthUser>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<AuthUser>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<AuthUser>()
                .Property(e => e.UserMobile)
                .IsUnicode(false);

            modelBuilder.Entity<AuthUser>()
                .Property(e => e.RoleName)
                .IsUnicode(false);

            modelBuilder.Entity<Message>()
                .Property(e => e.FromUser)
                .IsUnicode(false);

            modelBuilder.Entity<Message>()
                .Property(e => e.ToUser)
                .IsUnicode(false);

            modelBuilder.Entity<Message>()
                .Property(e => e.MsgContent)
                .IsUnicode(false);

            modelBuilder.Entity<Message>()
                .Property(e => e.MsgType)
                .IsUnicode(false);

            modelBuilder.Entity<Message>()
                .Property(e => e.MsgState)
                .IsUnicode(false);

            modelBuilder.Entity<Message>()
                .Property(e => e.OrderNo)
                .IsUnicode(false);

            modelBuilder.Entity<OrderExcel>()
                .Property(e => e.FileName)
                .IsUnicode(false);

            modelBuilder.Entity<OrderFiles>()
                .Property(e => e.OrigFileName)
                .IsUnicode(false);

            modelBuilder.Entity<OrderFiles>()
                .Property(e => e.FileName)
                .IsUnicode(false);

            modelBuilder.Entity<OrderGuest>()
                .Property(e => e.GuestId)
                .IsUnicode(false);

            modelBuilder.Entity<OrderGuest>()
                .Property(e => e.GuestType)
                .IsUnicode(false);

            modelBuilder.Entity<OrderGuest>()
                .Property(e => e.GuestName)
                .IsUnicode(false);

            modelBuilder.Entity<OrderGuest>()
                .Property(e => e.GuestNamePinYin)
                .IsUnicode(false);

            modelBuilder.Entity<OrderGuest>()
                .Property(e => e.GuestSex)
                .IsUnicode(false);

            modelBuilder.Entity<OrderGuest>()
                .Property(e => e.GuestPhone)
                .IsUnicode(false);

            modelBuilder.Entity<OrderGuest>()
                .Property(e => e.GuestWeChat)
                .IsUnicode(false);

            modelBuilder.Entity<OrderGuest>()
                .Property(e => e.GuestEMail)
                .IsUnicode(false);

            modelBuilder.Entity<OrderGuest>()
                .Property(e => e.GuestPassportNo)
                .IsUnicode(false);

            modelBuilder.Entity<OrderGuest>()
                .Property(e => e.GuestLastNightHotel)
                .IsUnicode(false);

            modelBuilder.Entity<OrderGuest>()
                .Property(e => e.GuestCountry)
                .IsUnicode(false);

            modelBuilder.Entity<OrderGuest>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OrderInfo>()
                .Property(e => e.OrderNo)
                .IsUnicode(false);

            modelBuilder.Entity<OrderInfo>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OrderInfo>()
                .Property(e => e.ProductName)
                .IsUnicode(false);

            modelBuilder.Entity<OrderInfo>()
                .Property(e => e.ExtraData)
                .IsUnicode(false);

            modelBuilder.Entity<OrderInfo>()
                .Property(e => e.OperatorName)
                .IsUnicode(false);

            modelBuilder.Entity<OrderInfo>()
                .Property(e => e.OperatorWorkId)
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .Property(e => e.OrderNo)
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .Property(e => e.GroupNo)
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .Property(e => e.ProductName)
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .Property(e => e.ProductType)
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .Property(e => e.GuestUseTime)
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .Property(e => e.OrderAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Orders>()
                .Property(e => e.ReallyPay)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Orders>()
                .Property(e => e.PlatformActivity)
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .Property(e => e.DiningShop)
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .Property(e => e.RefundAmout)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Orders>()
                .Property(e => e.WaitorName)
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .Property(e => e.IsPraise)
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .Property(e => e.RefundReason)
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .Property(e => e.WaitorRemark)
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .Property(e => e.JpOrderNo)
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .Property(e => e.OrderWay)
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .Property(e => e.ReplyResult)
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .Property(e => e.SettlePrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Orders>()
                .Property(e => e.ExchangeRate)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Orders>()
                .Property(e => e.OperRemark)
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .Property(e => e.TyperName)
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .Property(e => e.Commission)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Orders>()
                .Property(e => e.WaitorCommision)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Orders>()
                .Property(e => e.AdminRemark)
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .Property(e => e.OperName)
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .Property(e => e.MoneyType)
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .Property(e => e.ComboName)
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .Property(e => e.OrderState)
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .Property(e => e.RefundState)
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .Property(e => e.LabelRemark)
                .IsUnicode(false);

            modelBuilder.Entity<OrdersLogs>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<OrdersLogs>()
                .Property(e => e.WorkId)
                .IsUnicode(false);

            modelBuilder.Entity<OrdersLogs>()
                .Property(e => e.OrderNo)
                .IsUnicode(false);

            modelBuilder.Entity<Enums_OrderInfo_OrderType>()
                .Property(e => e.TypeName)
                .IsUnicode(false);

            modelBuilder.Entity<Enums_OrderInfo_PaymentPlatform>()
                .Property(e => e.PlateName)
                .IsUnicode(false);
        }
    }
}

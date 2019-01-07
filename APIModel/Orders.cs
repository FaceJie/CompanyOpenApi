namespace APIModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Orders
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string OrderNo { get; set; }

        public byte PaymentPlatform { get; set; }

        [StringLength(500)]
        public string GroupNo { get; set; }

        [StringLength(200)]
        public string ProductName { get; set; }

        public int? ProductId { get; set; }

        [StringLength(50)]
        public string ProductType { get; set; }

        [StringLength(100)]
        public string GuestUseTime { get; set; }

        public int? PurchaseNum { get; set; }

        [Column(TypeName = "money")]
        public decimal? OrderAmount { get; set; }

        [Column(TypeName = "money")]
        public decimal? ReallyPay { get; set; }

        [StringLength(100)]
        public string PlatformActivity { get; set; }

        public DateTime? GuestOrderTime { get; set; }

        public DateTime? WaitorOrderTime { get; set; }

        public DateTime? WaitorConfirmTime { get; set; }

        public DateTime? ReserveTime { get; set; }

        public DateTime? DiningTime { get; set; }

        [StringLength(50)]
        public string DiningShop { get; set; }

        public DateTime? CheckMoneyTime { get; set; }

        [Column(TypeName = "money")]
        public decimal? RefundAmout { get; set; }

        public DateTime? GuestRefundApplyTime { get; set; }

        public DateTime? WaitorRefundApplyTime { get; set; }

        [StringLength(20)]
        public string WaitorName { get; set; }

        [StringLength(10)]
        public string IsPraise { get; set; }

        [Column(TypeName = "text")]
        public string RefundReason { get; set; }

        [Column(TypeName = "text")]
        public string WaitorRemark { get; set; }

        [StringLength(50)]
        public string JpOrderNo { get; set; }

        [StringLength(50)]
        public string OrderWay { get; set; }

        public DateTime? OperOrderTime { get; set; }

        public DateTime? JpConfirmTime { get; set; }

        public DateTime? ReplyWaitorConfirmTime { get; set; }

        [StringLength(20)]
        public string ReplyResult { get; set; }

        [Column(TypeName = "money")]
        public decimal? SettlePrice { get; set; }

        [Column(TypeName = "money")]
        public decimal? ExchangeRate { get; set; }

        [Column(TypeName = "text")]
        public string OperRemark { get; set; }

        public DateTime? RealIntoAccountTime { get; set; }

        [StringLength(50)]
        public string TyperName { get; set; }

        [Column(TypeName = "money")]
        public decimal? Commission { get; set; }

        [Column(TypeName = "money")]
        public decimal? WaitorCommision { get; set; }

        [Column(TypeName = "text")]
        public string AdminRemark { get; set; }

        [StringLength(50)]
        public string OperName { get; set; }

        public bool? GuestInfoTypedIn { get; set; }

        [StringLength(20)]
        public string MoneyType { get; set; }

        [StringLength(50)]
        public string ComboName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DepartureDate { get; set; }

        [StringLength(20)]
        public string OrderState { get; set; }

        [StringLength(50)]
        public string RefundState { get; set; }

        public int? OrderColor { get; set; }

        [StringLength(100)]
        public string LabelRemark { get; set; }
    }
}

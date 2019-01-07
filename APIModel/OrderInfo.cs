namespace APIModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderInfo")]
    public partial class OrderInfo
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string OrderNo { get; set; }

        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }

        public byte OrderType { get; set; }

        public byte PaymentPlatform { get; set; }

        [StringLength(200)]
        public string ProductName { get; set; }

        public DateTime? OrderTime { get; set; }

        public DateTime? EntryTime { get; set; }

        [Column(TypeName = "text")]
        public string ExtraData { get; set; }

        public int? OrderExcelId { get; set; }

        public byte OrderInfoState { get; set; }

        [StringLength(50)]
        public string OperatorName { get; set; }

        [StringLength(50)]
        public string OperatorWorkId { get; set; }
    }
}

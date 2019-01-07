namespace APIModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Enums_OrderInfo_OrderType
    {
        [Key]
        [Column(Order = 0)]
        public byte TypeNo { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string TypeName { get; set; }
    }
}

namespace APIModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrdersLogs
    {
        public int id { get; set; }

        public byte? ActType { get; set; }

        [StringLength(100)]
        public string UserName { get; set; }

        public int? OrdersId { get; set; }

        public DateTime? EntryTime { get; set; }

        [StringLength(10)]
        public string WorkId { get; set; }

        [StringLength(50)]
        public string OrderNo { get; set; }
    }
}

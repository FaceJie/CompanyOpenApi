namespace APIModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderExcel")]
    public partial class OrderExcel
    {
        public int Id { get; set; }

        [StringLength(500)]
        public string FileName { get; set; }

        public DateTime? EntryTime { get; set; }
    }
}

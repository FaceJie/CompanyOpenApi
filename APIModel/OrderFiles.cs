namespace APIModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderFiles
    {
        public int Id { get; set; }

        [StringLength(500)]
        public string OrigFileName { get; set; }

        [StringLength(500)]
        public string FileName { get; set; }

        public DateTime? EntryTime { get; set; }

        public int? OrdersId { get; set; }
    }
}

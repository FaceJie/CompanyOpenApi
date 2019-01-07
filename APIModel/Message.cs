namespace APIModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Message")]
    public partial class Message
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string FromUser { get; set; }

        [StringLength(50)]
        public string ToUser { get; set; }

        [Column(TypeName = "text")]
        public string MsgContent { get; set; }

        [StringLength(10)]
        public string MsgType { get; set; }

        [StringLength(10)]
        public string MsgState { get; set; }

        [StringLength(50)]
        public string OrderNo { get; set; }

        public int? ReplyId { get; set; }

        public bool? IsUrgent { get; set; }

        public DateTime? EntryTime { get; set; }
    }
}

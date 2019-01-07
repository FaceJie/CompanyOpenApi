namespace APIModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderGuest")]
    public partial class OrderGuest
    {
        public int Id { get; set; }

        public int? OrdersId { get; set; }

        [StringLength(50)]
        public string GuestId { get; set; }

        [StringLength(20)]
        public string GuestType { get; set; }

        [StringLength(20)]
        public string GuestName { get; set; }

        [StringLength(50)]
        public string GuestNamePinYin { get; set; }

        [StringLength(10)]
        public string GuestSex { get; set; }

        [Column(TypeName = "date")]
        public DateTime? GuestBirthday { get; set; }

        [StringLength(20)]
        public string GuestPhone { get; set; }

        [StringLength(50)]
        public string GuestWeChat { get; set; }

        [StringLength(50)]
        public string GuestEMail { get; set; }

        [StringLength(30)]
        public string GuestPassportNo { get; set; }

        [StringLength(50)]
        public string GuestLastNightHotel { get; set; }

        [StringLength(10)]
        public string GuestCountry { get; set; }

        public int? Position { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }
    }
}

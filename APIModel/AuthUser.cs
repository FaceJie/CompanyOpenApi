namespace APIModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AuthUser")]
    public partial class AuthUser
    {
        [Key]
        [StringLength(50)]
        public string WorkId { get; set; }

        [StringLength(20)]
        public string Account { get; set; }

        [StringLength(100)]
        public string UserName { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        [StringLength(50)]
        public string UserMobile { get; set; }

        public Guid? DepartmentId { get; set; }

        public Guid? RID { get; set; }

        [StringLength(50)]
        public string RoleName { get; set; }

        public int? District { get; set; }
    }
}

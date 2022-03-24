namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DM_CAMNHAN_KHANG
    {
        [Key]
        public int CAMNHAN_KHANG_ID { get; set; }

        [Required]
        [StringLength(250)]
        public string TIEUDE { get; set; }

        [Required]
        [StringLength(550)]
        public string LINK_VIDEO { get; set; }

        public int STT { get; set; }

        public int IS_SHOW { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }
    }
}

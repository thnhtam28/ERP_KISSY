namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DM_NGHESY_TINDUNG
    {
        [Key]
        public int NGHESY_TINDUNG_ID { get; set; }

        [StringLength(150)]
        public string TEN_NGHESY { get; set; }

        [Required]
        public string NOIDUNG { get; set; }

        [Required]
        [StringLength(550)]
        public string HINHANH { get; set; }

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

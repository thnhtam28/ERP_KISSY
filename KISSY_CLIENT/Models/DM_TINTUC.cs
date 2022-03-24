namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DM_TINTUC
    {
        [Key]
        public int TINTUC_ID { get; set; }

        public int NHOMTIN_ID { get; set; }

        [Required]
        [StringLength(250)]
        public string TIEUDE { get; set; }

        public string TOMTAT { get; set; }

        [Required]
        public string NOIDUNG { get; set; }

        public int? STT { get; set; }

        public int? IS_NOIBAT { get; set; }

        [Required]
        [StringLength(550)]
        public string ANH_DAIDIEN { get; set; }

        public int IS_SHOW { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        [StringLength(500)]
        public string LIST_TAGS { get; set; }

        [StringLength(100)]
        public string META_TITLE { get; set; }

        [StringLength(400)]
        public string META_DESCRIPTION { get; set; }

        [StringLength(500)]
        public string URL_SLUG { get; set; }
    }
}

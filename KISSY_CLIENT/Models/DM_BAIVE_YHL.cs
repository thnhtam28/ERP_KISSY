namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DM_BAIVE_YHL
    {
        [Key]
        public int BAIVE_YHL_ID { get; set; }

        [StringLength(250)]
        public string CODE_LOAIBAI { get; set; }

        [Required]
        [StringLength(250)]
        public string TIEUDE { get; set; }

        [Required]
        public string TOMTAT { get; set; }

        [Required]
        public string NOIDUNG { get; set; }

        [Required]
        [StringLength(550)]
        public string HINHANH { get; set; }

        public int IS_SHOW { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        [StringLength(50)]
        public string lat { get; set; }

        [StringLength(50)]
        public string lng { get; set; }
    }
}
namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DM_LOAIBAI
    {
        [Key]
        public int LOAIBAI_ID { get; set; }

        [Required]
        [StringLength(250)]
        public string CODE_LOAIBAI { get; set; }

        [StringLength(250)]
        public string TEN_LOAIBAI { get; set; }

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

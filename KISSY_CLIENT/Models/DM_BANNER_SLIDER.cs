namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DM_BANNER_SLIDER
    {
        [Key]
        public int BANNER_SLIDER_ID { get; set; }

        [Required]
        [StringLength(550)]
        public string ANH_DAIDIEN { get; set; }

        public int STT { get; set; }

        [StringLength(550)]
        public string LINK { get; set; }

        public int IS_SHOW { get; set; }

        public int? IS_MOBILE { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }
    }
}

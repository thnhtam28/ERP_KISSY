namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DM_LOAISANPHAM
    {
        [Key]
        public int LOAISANPHAM_ID { get; set; }


        [Required]
        [StringLength(250)]
        public string TEN_LOAISANPHAM { get; set; }

        public int STT { get; set; }

        [Required]
        [StringLength(550)]
        public string ANH_DAIDIEN { get; set; }

        public int IS_SHOWMAIN { get; set; }

        public int? LOAISANPHAM_IDCHA { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }
        public string META_TITLE { get; set; }
        public string META_DESCRIPTION { get; set; }
        public string URL_SLUG { get; set; }



    }
}

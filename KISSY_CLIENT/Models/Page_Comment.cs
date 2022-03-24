namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Page_Comment
    {
        public string ID { get; set; }

        [StringLength(128)]
        public string UserID { get; set; }

        public int? ProductID { get; set; }
        [StringLength(300)]
        public string UserName { get; set; }
        public string Content { get; set; }

        public DateTime? DateCreated { get; set; }

        [StringLength(500)]
        public string F1 { get; set; }

        [StringLength(500)]
        public string F2 { get; set; }

        [StringLength(500)]
        public string F3 { get; set; }

        [StringLength(500)]
        public string F4 { get; set; }

        [StringLength(500)]
        public string F5 { get; set; }
    }
}

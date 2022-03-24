namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Page_New
    {
        public string ID { get; set; }

        [StringLength(500)]
        public string Title { get; set; }
        [StringLength(500)]
        public string Photo { get; set; }
        public string Sumary { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? CreateModified { get; set; }

        [StringLength(128)]
        public string Page_CategoryPostID { get; set; }
        
        public string Content { get; set; }

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

namespace Erp.BackOffice.Areas.Sale.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Page_Slide
    {
        public string ID { get; set; }

        [StringLength(500)]
        public string Photo { get; set; }

        [StringLength(500)]
        public string Link { get; set; }

        public int? STT { get; set; }

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

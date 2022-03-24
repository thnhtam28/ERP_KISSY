namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class System_PageMenu
    {
        public int Id { get; set; }

        [StringLength(10)]
        public string LanguageId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public int? PageId { get; set; }

        [StringLength(255)]
        public string Url { get; set; }

        public int? OrderNo { get; set; }

        [StringLength(100)]
        public string CssClassIcon { get; set; }

        public bool? IsVisible { get; set; }

        public int? ParentId { get; set; }

        public bool? IsDashboard { get; set; }

        [StringLength(100)]
        public string DashboardView { get; set; }
    }
}

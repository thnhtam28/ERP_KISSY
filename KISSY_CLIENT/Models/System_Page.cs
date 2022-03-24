namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class System_Page
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(50)]
        public string AreaName { get; set; }

        [StringLength(50)]
        public string ActionName { get; set; }

        [StringLength(50)]
        public string ControllerName { get; set; }

        [StringLength(255)]
        public string Url { get; set; }

        public int? OrderNo { get; set; }

        public bool? Status { get; set; }

        [StringLength(100)]
        public string CssClassIcon { get; set; }

        public int? ParentId { get; set; }

        public bool? IsDeleted { get; set; }

        public bool? IsVisible { get; set; }

        public bool? IsView { get; set; }

        public bool? IsAdd { get; set; }

        public bool? IsEdit { get; set; }

        public bool? IsDelete { get; set; }

        public bool? IsImport { get; set; }

        public bool? IsExport { get; set; }

        public bool? IsPrint { get; set; }
    }
}

namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staff_KPIItem
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        public int? KPICatalogId { get; set; }

        [StringLength(150)]
        public string Measure { get; set; }

        public double? TargetScore_From { get; set; }

        public double? TargetScore_To { get; set; }

        public double? KPIWeight { get; set; }

        [StringLength(300)]
        public string Description { get; set; }
    }
}

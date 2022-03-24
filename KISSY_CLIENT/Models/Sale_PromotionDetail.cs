namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sale_PromotionDetail
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public int? PromotionId { get; set; }

        public int? ProductId { get; set; }

        public double? PercentValue { get; set; }

        [StringLength(50)]
        public string CategoryCode { get; set; }

        public bool? IsAll { get; set; }

        public int? QuantityFor { get; set; }

        [StringLength(20)]
        public string Type { get; set; }
    }
}

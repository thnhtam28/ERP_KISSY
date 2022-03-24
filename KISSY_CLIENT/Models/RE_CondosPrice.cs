namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RE_CondosPrice
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        public int? CondosId { get; set; }

        public double? PriceBefore { get; set; }

        public double? PriceAfter { get; set; }

        public double? Difference { get; set; }

        [StringLength(300)]
        public string Note { get; set; }
    }
}

namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sale_ServiceInvoiceDetail
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        public int? ServiceInvoiceId { get; set; }

        public int? ServiceId { get; set; }

        public int? Price { get; set; }

        public int? Quantity { get; set; }

        [StringLength(50)]
        public string Unit { get; set; }

        public int? PromotionId { get; set; }

        public int? PromotionDetailId { get; set; }

        public double? PromotionValue { get; set; }

        public int? DisCount { get; set; }

        public int? DisCountAmount { get; set; }

        public bool? CheckPromotion { get; set; }
    }
}
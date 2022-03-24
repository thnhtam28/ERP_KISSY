namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sale_ProductInvoiceDetail
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        public int? ProductInvoiceId { get; set; }

        public int? ProductId { get; set; }

        public decimal? Price { get; set; }

        public int? Quantity { get; set; }

        [StringLength(50)]
        public string Unit { get; set; }

        public int? PromotionId { get; set; }

        public int? PromotionDetailId { get; set; }

        public decimal? PromotionValue { get; set; }

        public bool? IsMoney { get; set; }

        [StringLength(20)]
        public string ProductType { get; set; }

        public int? IrregularDiscount { get; set; }

        public decimal? IrregularDiscountAmount { get; set; }

        public int? FixedDiscount { get; set; }

        public decimal? FixedDiscountAmount { get; set; }

        public bool? CheckPromotion { get; set; }

        public bool? IsReturn { get; set; }

        public int? QuantitySaleReturn { get; set; }

        public int? StaffId { get; set; }

        [StringLength(350)]
        public string Status { get; set; }

        [StringLength(50)]
        public string LoCode { get; set; }

        public DateTime? ExpiryDate { get; set; }

        [StringLength(150)]
        public string Origin { get; set; }

        public int? TaxFee { get; set; }

        public decimal? TaxFeeAmount { get; set; }
    }
}

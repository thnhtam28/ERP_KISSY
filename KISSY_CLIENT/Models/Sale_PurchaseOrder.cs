namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sale_PurchaseOrder
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        [StringLength(20)]
        public string Code { get; set; }

        public decimal? TotalAmount { get; set; }

        public double? TaxFee { get; set; }

        public double? Discount { get; set; }

        [StringLength(20)]
        public string DiscountCode { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(300)]
        public string Note { get; set; }

        [StringLength(15)]
        public string Phone { get; set; }

        public bool? IsArchive { get; set; }

        public int? BranchId { get; set; }

        [StringLength(50)]
        public string PaymentMethod { get; set; }

        public decimal? PaidAmount { get; set; }

        public decimal? RemainingAmount { get; set; }

        [StringLength(300)]
        public string CancelReason { get; set; }

        [StringLength(50)]
        public string BarCode { get; set; }

        public int? SupplierId { get; set; }

        public int? WarehouseSourceId { get; set; }

        public int? WarehouseDestinationId { get; set; }

        public int? ProductInboundId { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        public DateTime? NextPaymentDate { get; set; }

        [StringLength(50)]
        public string CodeOrderRed { get; set; }

        public DateTime? DateOrder { get; set; }
    }
}

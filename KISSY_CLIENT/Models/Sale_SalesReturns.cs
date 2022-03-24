namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sale_SalesReturns
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

        public int? CustomerId { get; set; }

        public decimal? TotalAmount { get; set; }

        public double? TaxFee { get; set; }

        public double? Discount { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(300)]
        public string Note { get; set; }

        public int? BranchId { get; set; }

        [StringLength(150)]
        public string PaymentMethod { get; set; }

        public decimal? PaidAmount { get; set; }

        public int? SalerId { get; set; }
    }
}

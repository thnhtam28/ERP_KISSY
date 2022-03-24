namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sale_TotalDiscountMoneyNT
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        public int? DrugStoreId { get; set; }

        public int? BranchId { get; set; }

        public int? UserManagerId { get; set; }

        public int? Month { get; set; }

        public int? Year { get; set; }

        public int? QuantityDay { get; set; }

        public decimal? PercentDecrease { get; set; }

        public decimal? DiscountAmount { get; set; }

        [Column("DecreaseAmount ")]
        public decimal? DecreaseAmount_ { get; set; }

        public decimal? RemainingAmount { get; set; }

        [StringLength(50)]
        public string Status { get; set; }
    }
}

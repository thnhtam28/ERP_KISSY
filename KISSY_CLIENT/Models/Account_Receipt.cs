namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Account_Receipt
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Code { get; set; }

        public decimal? Amount { get; set; }

        [StringLength(150)]
        public string PaymentMethod { get; set; }

        [StringLength(20)]
        public string BankAccountNo { get; set; }

        [StringLength(150)]
        public string BankAccountName { get; set; }

        [StringLength(150)]
        public string BankName { get; set; }

        public int? OriginalTransactionId { get; set; }

        public DateTime? VoucherDate { get; set; }

        [StringLength(350)]
        public string Address { get; set; }

        public int? CustomerId { get; set; }

        [StringLength(350)]
        public string Note { get; set; }

        public int? SalerId { get; set; }

        [StringLength(150)]
        public string Payer { get; set; }

        [StringLength(20)]
        public string MaChungTuGoc { get; set; }

        [StringLength(20)]
        public string LoaiChungTuGoc { get; set; }

        [StringLength(350)]
        public string CancelReason { get; set; }

        public int? BranchId { get; set; }
    }
}

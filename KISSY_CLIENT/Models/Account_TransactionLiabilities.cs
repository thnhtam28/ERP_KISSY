namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Account_TransactionLiabilities
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        [StringLength(20)]
        public string TransactionModule { get; set; }

        [StringLength(20)]
        public string TransactionCode { get; set; }

        [StringLength(50)]
        public string TransactionName { get; set; }

        [StringLength(50)]
        public string TargetModule { get; set; }

        [StringLength(20)]
        public string TargetCode { get; set; }

        public decimal? Debit { get; set; }

        public decimal? Credit { get; set; }

        public DateTime? NextPaymentDate { get; set; }

        [StringLength(150)]
        public string PaymentMethod { get; set; }

        [StringLength(20)]
        public string MaChungTuGoc { get; set; }

        [StringLength(20)]
        public string LoaiChungTuGoc { get; set; }

        [StringLength(300)]
        public string Note { get; set; }
    }
}

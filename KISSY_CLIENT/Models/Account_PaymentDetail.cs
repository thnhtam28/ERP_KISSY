namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Account_PaymentDetail
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

        public int? PaymentId { get; set; }

        [StringLength(20)]
        public string MaChungTuGoc { get; set; }

        [StringLength(20)]
        public string LoaiChungTuGoc { get; set; }

        public decimal? Amount { get; set; }

        [StringLength(150)]
        public string GroupName { get; set; }
    }
}

namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Account_Contract
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        [StringLength(20)]
        public string Type { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(150)]
        public string Place { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public int? Quantity { get; set; }

        [StringLength(150)]
        public string TemplateFile { get; set; }

        public int? InfoPartyAId { get; set; }

        [StringLength(20)]
        public string IdTypeContract { get; set; }

        public int? CustomerId { get; set; }

        [StringLength(20)]
        public string TransactionCode { get; set; }

        [StringLength(150)]
        public string Status { get; set; }
    }
}

namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sale_PhysicalInventoryDetail
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        public int? PhysicalInventoryId { get; set; }

        public int? ProductId { get; set; }

        public int? QuantityInInventory { get; set; }

        public int? QuantityDiff { get; set; }

        public int? QuantityRemaining { get; set; }

        [StringLength(300)]
        public string Note { get; set; }

        [StringLength(50)]
        public string ReferenceVoucher { get; set; }

        [StringLength(50)]
        public string LoCode { get; set; }

        public DateTime? ExpiryDate { get; set; }
    }
}

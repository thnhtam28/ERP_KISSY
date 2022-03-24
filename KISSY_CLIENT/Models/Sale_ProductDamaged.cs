namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sale_ProductDamaged
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public int? Quantity { get; set; }

        [StringLength(300)]
        public string Reason { get; set; }

        public int? ProductInboundId { get; set; }

        public int? ProductInboundDetailId { get; set; }
    }
}

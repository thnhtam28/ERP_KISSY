namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sale_WarehouseLocationItem
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string SN { get; set; }

        [StringLength(10)]
        public string Shelf { get; set; }

        [StringLength(10)]
        public string Floor { get; set; }

        [StringLength(10)]
        public string Position { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public bool? IsOut { get; set; }

        public int? WarehouseId { get; set; }

        public int? ProductId { get; set; }

        public int? ProductOutboundId { get; set; }

        public int? ProductOutboundDetailId { get; set; }

        public int? ProductInboundId { get; set; }

        public int? ProductInboundDetailId { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        [StringLength(50)]
        public string LoCode { get; set; }
    }
}

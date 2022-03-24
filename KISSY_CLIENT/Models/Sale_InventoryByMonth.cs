namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sale_InventoryByMonth
    {
        public int Id { get; set; }

        public int? ProductId { get; set; }

        public int? WarehouseId { get; set; }

        public int? Month { get; set; }

        public int? Year { get; set; }

        public int? InventoryQuantityEndPreviousMonth { get; set; }

        public int? InventoryQuantityThisMonth { get; set; }

        public int? InventoryQuantityBeginNextMonth { get; set; }

        public int? InboundQuantityOfMonth { get; set; }

        public int? OutboundQuantityOfMonth { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }
    }
}

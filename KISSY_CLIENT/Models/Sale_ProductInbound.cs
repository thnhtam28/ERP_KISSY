namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sale_ProductInbound
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(300)]
        public string Note { get; set; }

        public int? WarehouseDestinationId { get; set; }

        public int? WarehouseKeeperId { get; set; }

        [StringLength(20)]
        public string Type { get; set; }

        public int? PurchaseOrderId { get; set; }

        public decimal? TotalAmount { get; set; }

        public bool? IsDone { get; set; }

        public int? BranchId { get; set; }

        [StringLength(50)]
        public string ShipperName { get; set; }

        [StringLength(15)]
        public string ShipperPhone { get; set; }

        public int? SupplierId { get; set; }

        public int? SalesReturnsId { get; set; }

        public int? PhysicalInventoryId { get; set; }

        public int? ProductOutboundId { get; set; }

        public bool? IsArchive { get; set; }

        public int? CreatedStaffId { get; set; }

        [StringLength(50)]
        public string CodeOrderRed { get; set; }

        public DateTime? DateOrder { get; set; }
    }
}

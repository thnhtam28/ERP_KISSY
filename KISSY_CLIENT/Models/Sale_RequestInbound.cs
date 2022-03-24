namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sale_RequestInbound
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

        public decimal? TotalAmount { get; set; }

        public int? WarehouseSourceId { get; set; }

        public int? WarehouseDestinationId { get; set; }

        [StringLength(20)]
        public string Status { get; set; }

        [StringLength(50)]
        public string BarCode { get; set; }

        [StringLength(300)]
        public string CancelReason { get; set; }

        [StringLength(300)]
        public string Note { get; set; }

        public int? InboundId { get; set; }

        public int? OutboundId { get; set; }

        [StringLength(150)]
        public string ShipName { get; set; }

        [StringLength(15)]
        public string ShipPhone { get; set; }

        public int? BranchId { get; set; }

        public int? StaffId { get; set; }

        public bool? Error { get; set; }

        public int? ErrorQuantity { get; set; }
    }
}

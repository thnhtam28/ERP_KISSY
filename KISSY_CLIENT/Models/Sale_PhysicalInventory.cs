namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sale_PhysicalInventory
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        public int? WarehouseId { get; set; }

        [StringLength(20)]
        public string Code { get; set; }

        [StringLength(500)]
        public string Note { get; set; }

        [StringLength(20)]
        public string Status { get; set; }

        public int? TotalProductCheck { get; set; }

        public int? TotalLost { get; set; }

        public int? TotalBreak { get; set; }

        public int? BranchId { get; set; }

        public bool? IsExchange { get; set; }
    }
}

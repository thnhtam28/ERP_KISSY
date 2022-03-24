namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staff_HistoryCommissionStaff
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

        public int? StaffId { get; set; }

        [StringLength(350)]
        public string StaffName { get; set; }

        public int? Month { get; set; }

        public int? Year { get; set; }

        [StringLength(200)]
        public string PositionName { get; set; }

        public decimal? CommissionPercent { get; set; }

        public decimal? MinimumRevenue { get; set; }

        public decimal? RevenueDS { get; set; }

        public decimal? AmountCommission { get; set; }

        public int? StaffParentId { get; set; }
    }
}

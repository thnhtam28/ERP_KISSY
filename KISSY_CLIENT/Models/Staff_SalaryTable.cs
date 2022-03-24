namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staff_SalaryTable
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

        public int? TargetMonth { get; set; }

        public int? TargetYear { get; set; }

        public DateTime? PaymentDate { get; set; }

        [StringLength(150)]
        public string Status { get; set; }

        public byte[] ListSalarySettingDetail { get; set; }

        public int? SalarySettingId { get; set; }

        public int? BranchId { get; set; }

        public bool? IsSend { get; set; }

        public bool? Submitted { get; set; }

        [StringLength(150)]
        public string SalaryApprovalType { get; set; }

        public decimal? TotalSalary { get; set; }

        public bool? HiddenForMonth { get; set; }
    }
}

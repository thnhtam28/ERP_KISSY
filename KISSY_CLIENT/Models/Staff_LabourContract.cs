namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staff_LabourContract
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

        public DateTime? SignedDay { get; set; }

        public int? StaffId { get; set; }

        public int? Type { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public string Content { get; set; }

        public int? ApprovedUserId { get; set; }

        public int? WageAgreement { get; set; }

        [StringLength(150)]
        public string FormWork { get; set; }

        public string Job { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(150)]
        public string PositionStaff { get; set; }

        [StringLength(150)]
        public string PositionApproved { get; set; }

        public int? DepartmentStaffId { get; set; }

        public int? DepartmentApprovedId { get; set; }
    }
}

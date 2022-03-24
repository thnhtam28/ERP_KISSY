namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staff_TransferWork
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        [StringLength(150)]
        public string Code { get; set; }

        public int? StaffId { get; set; }

        public int? BranchDepartmentOldId { get; set; }

        public int? BranchDepartmentNewId { get; set; }

        [StringLength(150)]
        public string PositionOld { get; set; }

        [StringLength(150)]
        public string PositionNew { get; set; }

        public DateTime? DayDecision { get; set; }

        public int? UserId { get; set; }

        [StringLength(250)]
        public string Reason { get; set; }

        public DateTime? DayEffective { get; set; }

        [StringLength(150)]
        public string Status { get; set; }

        [StringLength(50)]
        public string CodeStaffOld { get; set; }

        [StringLength(50)]
        public string CodeStaffNew { get; set; }
    }
}

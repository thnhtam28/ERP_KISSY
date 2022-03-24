namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staff_SalarySetting
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

        [StringLength(300)]
        public string Note { get; set; }

        public bool? IsTemplate { get; set; }

        public bool? IsSend { get; set; }

        public int? OrderNo { get; set; }

        [StringLength(150)]
        public string SalaryApprovalType { get; set; }

        public bool? HiddenForMonth { get; set; }
    }
}
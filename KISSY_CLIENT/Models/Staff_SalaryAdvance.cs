namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staff_SalaryAdvance
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        [StringLength(150)]
        public string CodeAdvance { get; set; }

        public int? Pay { get; set; }

        public int? StaffId { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(300)]
        public string Note { get; set; }

        public DateTime? DayAdvance { get; set; }
    }
}

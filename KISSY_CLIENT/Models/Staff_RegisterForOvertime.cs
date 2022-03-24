namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staff_RegisterForOvertime
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        [StringLength(150)]
        public string Code { get; set; }

        public int? StaffId { get; set; }

        public DateTime? DayOvertime { get; set; }

        public DateTime? StartHour { get; set; }

        public DateTime? EndHour { get; set; }

        [StringLength(300)]
        public string Note { get; set; }
    }
}

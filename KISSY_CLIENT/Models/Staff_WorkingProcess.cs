namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staff_WorkingProcess
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(150)]
        public string WorkPlace { get; set; }

        [StringLength(50)]
        public string Position { get; set; }

        public DateTime? DayStart { get; set; }

        public DateTime? DayEnd { get; set; }

        public int? BonusDisciplineId { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        public int? StaffId { get; set; }
    }
}

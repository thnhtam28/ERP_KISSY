namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staff_Shifts
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(50)]
        public string StartTime { get; set; }

        [StringLength(50)]
        public string StartTimeOut { get; set; }

        [StringLength(50)]
        public string EndTimeIn { get; set; }

        [StringLength(50)]
        public string EndTime { get; set; }

        public bool? NightShifts { get; set; }

        [StringLength(100)]
        public string CategoryShifts { get; set; }

        [StringLength(50)]
        public string StartTimeIn { get; set; }

        [StringLength(50)]
        public string EndTimeOut { get; set; }

        public int? MinuteLate { get; set; }

        public int? MinuteEarly { get; set; }
    }
}

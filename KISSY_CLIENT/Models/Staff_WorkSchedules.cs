namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staff_WorkSchedules
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? StaffId { get; set; }

        public DateTime? Day { get; set; }

        public int? ShiftsId { get; set; }

        public DateTime? HoursIn { get; set; }

        public DateTime? HoursOut { get; set; }

        public int? Symbol { get; set; }

        public int? Total_minute_work_late { get; set; }

        public int? Total_minute_work_early { get; set; }

        public int? Total_minute_work_overtime { get; set; }

        public int? Total_minute_work { get; set; }

        public int? DayOffId { get; set; }

        public int? TimekeepingListId { get; set; }

        public int? FPMachineId { get; set; }
    }
}

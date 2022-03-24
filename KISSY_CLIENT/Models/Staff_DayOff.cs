namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staff_DayOff
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public DateTime? DayStart { get; set; }

        public DateTime? DayEnd { get; set; }

        public int? Quantity { get; set; }

        public int? TypeDayOffId { get; set; }

        public int? QuantityNotUsed { get; set; }

        public int? StaffId { get; set; }

        [StringLength(50)]
        public string Code { get; set; }
    }
}

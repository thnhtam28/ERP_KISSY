namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staff_ProcessPay
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? BasicPay { get; set; }

        [StringLength(50)]
        public string LevelPay { get; set; }

        public DateTime? DayDecision { get; set; }

        public DateTime? DayEffective { get; set; }

        public int? StaffId { get; set; }

        [StringLength(50)]
        public string CodePay { get; set; }

        public string Content { get; set; }
    }
}

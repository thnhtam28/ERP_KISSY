namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staff_CheckInOut
    {
        public int Id { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UserId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? TimeDate { get; set; }

        public DateTime? TimeStr { get; set; }

        [StringLength(5)]
        public string TimeType { get; set; }

        [StringLength(2)]
        public string TimeSource { get; set; }

        public int? MachineNo { get; set; }

        [StringLength(30)]
        public string CardNo { get; set; }

        public int? FPMachineId { get; set; }
    }
}

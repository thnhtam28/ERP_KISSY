namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staff_FingerPrint
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        public int? FingerIndex { get; set; }

        public string TmpData { get; set; }

        public int? Privilege { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public bool? Enabled { get; set; }

        public int? Flag { get; set; }

        public int? FPMachineId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}

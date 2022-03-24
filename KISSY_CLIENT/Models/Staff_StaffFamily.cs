namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staff_StaffFamily
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? StaffId { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        public bool? Gender { get; set; }

        [StringLength(10)]
        public string Correlative { get; set; }

        public DateTime? Birthday { get; set; }

        [StringLength(11)]
        public string Phone { get; set; }

        public bool? IsDependencies { get; set; }
    }
}

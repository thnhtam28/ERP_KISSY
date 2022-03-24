namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staff_SymbolTimekeeping
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        public int? Quantity { get; set; }

        public bool? Timekeeping { get; set; }

        public bool? DayOff { get; set; }

        [StringLength(50)]
        public string Color { get; set; }

        [StringLength(50)]
        public string CodeDefault { get; set; }

        public bool? Absent { get; set; }

        [StringLength(50)]
        public string Icon { get; set; }
    }
}

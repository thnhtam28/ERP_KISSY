namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staff_BonusDiscipline
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        [StringLength(150)]
        public string Category { get; set; }

        [StringLength(150)]
        public string Formality { get; set; }

        [StringLength(350)]
        public string Reason { get; set; }

        public DateTime? DayDecision { get; set; }

        public DateTime? DayEffective { get; set; }

        public int? PlaceDecisions { get; set; }

        [StringLength(350)]
        public string Note { get; set; }

        public int? StaffId { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        public int? Money { get; set; }
    }
}

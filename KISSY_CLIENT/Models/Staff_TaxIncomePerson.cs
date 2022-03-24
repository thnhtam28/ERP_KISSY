namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staff_TaxIncomePerson
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        public string Name { get; set; }

        [StringLength(150)]
        public string Code { get; set; }

        [Required]
        [StringLength(350)]
        public string GeneralTaxationId { get; set; }

        [StringLength(350)]
        public string GeneralManageId { get; set; }

        public DateTime? StaffStartDate { get; set; }

        public DateTime? StaffEndDate { get; set; }
    }
}

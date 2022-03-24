namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staff_DocumentField
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        public int? DocumentTypeId { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(300)]
        public string IsSearch { get; set; }

        [StringLength(100)]
        public string Category { get; set; }

        public int? CategoryId { get; set; }
    }
}

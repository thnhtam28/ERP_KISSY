namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staff_DocumentAttribute
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        public int? DocumentFieldId { get; set; }

        [StringLength(250)]
        public string File { get; set; }

        [StringLength(300)]
        public string Note { get; set; }

        public int? OrderNo { get; set; }

        [StringLength(250)]
        public string Size { get; set; }

        [StringLength(50)]
        public string TypeFile { get; set; }
    }
}

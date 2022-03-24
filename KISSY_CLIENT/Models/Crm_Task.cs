namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Crm_Task
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        [StringLength(150)]
        public string Subject { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? DueDate { get; set; }

        [StringLength(50)]
        public string ParentType { get; set; }

        public int? ParentId { get; set; }

        public int? ContactId { get; set; }

        [StringLength(50)]
        public string Priority { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        [StringLength(300)]
        public string Note { get; set; }

        public bool? IsSendNotifications { get; set; }
    }
}

namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staff_InternalNotifications
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        [StringLength(200)]
        public string PlaceOfNotice { get; set; }

        [StringLength(200)]
        public string PlaceOfReceipt { get; set; }

        [StringLength(200)]
        public string Titles { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        public string Content { get; set; }

        public bool? Seen { get; set; }

        public bool? Disable { get; set; }

        [StringLength(50)]
        public string ActionName { get; set; }

        [StringLength(50)]
        public string ModuleName { get; set; }
    }
}

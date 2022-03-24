namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Crm_Process
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(150)]
        public string Category { get; set; }

        public bool? IsActive { get; set; }

        [StringLength(50)]
        public string ActivateAs { get; set; }

        public int? ModuleId { get; set; }

        [StringLength(100)]
        public string DataSource { get; set; }

        [StringLength(250)]
        public string QueryRecivedUser { get; set; }
    }
}

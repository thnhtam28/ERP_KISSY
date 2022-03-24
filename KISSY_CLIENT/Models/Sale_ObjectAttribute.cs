namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sale_ObjectAttribute
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(20)]
        public string DataType { get; set; }

        [StringLength(50)]
        public string ModuleType { get; set; }

        [StringLength(50)]
        public string ModuleCategoryType { get; set; }

        public int? ModuleId { get; set; }

        public int? OrderNo { get; set; }

        public bool? IsSelected { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }
    }
}

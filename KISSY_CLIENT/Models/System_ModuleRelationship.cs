namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class System_ModuleRelationship
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        [StringLength(100)]
        public string First_ModuleName { get; set; }

        [StringLength(100)]
        public string First_MetadataFieldName { get; set; }

        [StringLength(100)]
        public string Second_ModuleName { get; set; }

        [StringLength(100)]
        public string Second_MetadataFieldName { get; set; }

        [StringLength(100)]
        public string Second_ModuleName_Alias { get; set; }
    }
}

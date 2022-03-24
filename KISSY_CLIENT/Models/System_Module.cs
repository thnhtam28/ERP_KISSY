namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class System_Module
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(50)]
        public string TableName { get; set; }

        public int? OrderNo { get; set; }

        public bool? IsVisible { get; set; }

        [StringLength(150)]
        public string DisplayName { get; set; }

        [StringLength(50)]
        public string AreaName { get; set; }
    }
}

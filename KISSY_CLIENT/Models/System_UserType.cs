namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class System_UserType
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        public int? OrderNo { get; set; }

        public string Note { get; set; }

        public bool? Scope { get; set; }

        public bool? Scope2 { get; set; }

        public bool? IsSystem { get; set; }

        [StringLength(50)]
        public string Code { get; set; }
    }
}

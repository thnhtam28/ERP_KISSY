namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class System_Loc_District
    {
        [Key]
        [StringLength(10)]
        public string DistrictId { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Type { get; set; }

        [StringLength(255)]
        public string Location { get; set; }

        [StringLength(10)]
        public string ProvinceId { get; set; }

        public int? PHISHIP { get; set; }
    }
}
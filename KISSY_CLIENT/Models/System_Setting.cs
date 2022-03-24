namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class System_Setting
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }

        public string Note { get; set; }

        public bool? IsLocked { get; set; }

        [StringLength(50)]
        public string Code { get; set; }
    }
}

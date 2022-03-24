namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class System_BOLog
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required]
        [StringLength(200)]
        public string UserName { get; set; }

        [Required]
        [StringLength(200)]
        public string Action { get; set; }

        [Required]
        public string Note { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(200)]
        public string Controller { get; set; }

        [StringLength(200)]
        public string Area { get; set; }

        public string Data { get; set; }

        public int? Type { get; set; }
    }
}

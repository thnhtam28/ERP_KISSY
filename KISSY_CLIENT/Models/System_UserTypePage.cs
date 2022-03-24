namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class System_UserTypePage
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserTypeId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PageId { get; set; }

        public bool? View { get; set; }

        public bool? Edit { get; set; }

        public bool? Add { get; set; }

        public bool? Delete { get; set; }

        public bool? Import { get; set; }

        public bool? Export { get; set; }

        public bool? Print { get; set; }
    }
}

namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DM_TINTUC_tags
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TINTUC_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string TagId { get; set; }
    }
}

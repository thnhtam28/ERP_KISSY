namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class System_News_ViewedUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? NewsId { get; set; }

        public int? ViewedUser { get; set; }

        public int? ViewCount { get; set; }

        public DateTime? ViewedDT { get; set; }
    }
}

namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class System_News
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int CreatedUser { get; set; }

        public int? UpdateUser { get; set; }

        public int? OrderNo { get; set; }

        [StringLength(500)]
        public string ThumbnailPath { get; set; }

        public bool? IsDeleted { get; set; }

        [StringLength(500)]
        public string ImagePath { get; set; }

        public int? CategoryId { get; set; }

        public bool? IsPublished { get; set; }

        [StringLength(250)]
        public string Title { get; set; }

        public string ShortContent { get; set; }

        public string Content { get; set; }

        [StringLength(100)]
        public string Url { get; set; }

        public DateTime? PublishedDate { get; set; }
    }
}

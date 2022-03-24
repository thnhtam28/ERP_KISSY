namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Crm_SMSLog
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? CustomerID { get; set; }

        [StringLength(100)]
        public string TargetModule { get; set; }

        public int? TargetID { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(300)]
        public string Body { get; set; }

        [StringLength(150)]
        public string Status { get; set; }

        public DateTime SentDate { get; set; }
    }
}

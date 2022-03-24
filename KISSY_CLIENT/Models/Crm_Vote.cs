namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Crm_Vote
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        public int? CustomerId { get; set; }

        public int? InvoiceId { get; set; }

        public int? QuestionId { get; set; }

        public int? AnswerId { get; set; }

        public int? UsingServiceLogDetailId { get; set; }

        [StringLength(350)]
        public string Note { get; set; }
    }
}

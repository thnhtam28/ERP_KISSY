namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sale_LogServiceRemminder
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        public int? ProductInvoiceDetailId { get; set; }

        public int? ReminderId { get; set; }

        [StringLength(300)]
        public string ReminderName { get; set; }

        public DateTime? ReminderDate { get; set; }

        public int? ServiceId { get; set; }

        public int? ProductInvoiceId { get; set; }
    }
}

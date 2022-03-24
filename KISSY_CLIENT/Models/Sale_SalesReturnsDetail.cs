namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sale_SalesReturnsDetail
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        public int? SalesReturnsId { get; set; }

        public int? ProductId { get; set; }

        public int? Quantity { get; set; }

        public int? DisCount { get; set; }

        public int? DisCountAmount { get; set; }

        public int? ProductInvoiceDetailId { get; set; }

        public int? ProductInvoiceId { get; set; }
    }
}

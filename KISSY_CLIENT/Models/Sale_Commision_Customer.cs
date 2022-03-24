namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sale_Commision_Customer
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int CommissionCusId { get; set; }

        public int Type { get; set; }

        public int ProductId { get; set; }

        public decimal Minvalue { get; set; }

        public decimal CommissionValue { get; set; }

        public bool IsMoney { get; set; }
    }
}

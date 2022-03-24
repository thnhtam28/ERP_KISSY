namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sale_Commision
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        public int? BranchId { get; set; }

        [StringLength(300)]
        public string Note { get; set; }

        public int? StaffId { get; set; }

        public int? ProductId { get; set; }

        public int? ServiceId { get; set; }

        public decimal? CommissionValue { get; set; }

        public bool? IsMoney { get; set; }
    }
}

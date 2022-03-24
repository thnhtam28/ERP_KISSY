namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staff_DotGQCDBHXHDetail
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        public int? DotGQCDBHXHId { get; set; }

        public int? StaffId { get; set; }

        public int? DayOffId { get; set; }

        [StringLength(100)]
        public string SocietyCode { get; set; }

        [StringLength(250)]
        public string DKTH_TinhTrang { get; set; }

        public DateTime? DKTH_ThoiDiem { get; set; }

        public DateTime? DayStart { get; set; }

        public DateTime? DayEnd { get; set; }

        public int? Quantity { get; set; }

        [StringLength(250)]
        public string PaymentMethod { get; set; }

        [StringLength(250)]
        public string Note { get; set; }

        [StringLength(350)]
        public string Type { get; set; }
    }
}

namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sale_Warehouse
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        [StringLength(20)]
        public string Code { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(300)]
        public string Note { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(50)]
        public string WardId { get; set; }

        [StringLength(50)]
        public string DistrictId { get; set; }

        [StringLength(10)]
        public string CityId { get; set; }

        public int? BranchId { get; set; }

        public string KeeperId { get; set; }

        public bool? IsSale { get; set; }

        [StringLength(150)]
        public string Categories { get; set; }
    }
}

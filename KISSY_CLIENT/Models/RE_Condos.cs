namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RE_Condos
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

        public int? ProjectId { get; set; }

        public int? BlockId { get; set; }

        public int? FloorId { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        public double? Area { get; set; }

        public double? Price { get; set; }

        public int? NumbersOfLivingRoom { get; set; }

        public int? NumbersOfBedRoom { get; set; }

        public int? NumbersOfKitchenRoom { get; set; }

        public int? NumbersOfToilet { get; set; }

        public int? NumbersOfBalcony { get; set; }

        [StringLength(150)]
        public string Orientation { get; set; }

        [StringLength(150)]
        public string Status { get; set; }

        [StringLength(50)]
        public string Currency { get; set; }
    }
}

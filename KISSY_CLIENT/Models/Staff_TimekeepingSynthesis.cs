namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staff_TimekeepingSynthesis
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        public int? StaffId { get; set; }

        public int? Month { get; set; }

        public int? Year { get; set; }

        public int? NgayCongThucTe { get; set; }

        public int? NgayNghiCoPhep { get; set; }

        public int? SoNgayNghiBu { get; set; }

        public int? SoNgayNghiLe { get; set; }

        public double? TrongGioNgayThuong { get; set; }

        public double? TangCaNgayThuong { get; set; }

        public double? TrongGioNgayNghi { get; set; }

        public double? TangCaNgayNghi { get; set; }

        public double? TrongGioNgayLe { get; set; }

        public double? TangCaNgayLe { get; set; }

        public double? GioDiTre { get; set; }

        public double? GioVeSom { get; set; }

        public double? GioCaDem { get; set; }

        public int? TimekeepingListId { get; set; }

        public int? NgayDiTre { get; set; }

        public int? NgayVeSom { get; set; }
    }
}

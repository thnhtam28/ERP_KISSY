namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staff_SalarySettingDetail
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

        public int? SalarySettingId { get; set; }

        public int? OrderNo { get; set; }

        public double? DefaultValue { get; set; }

        public bool? IsDefaultValue { get; set; }

        public string Formula { get; set; }

        public int? ParentId { get; set; }

        [StringLength(50)]
        public string FormulaType { get; set; }

        [StringLength(150)]
        public string GroupName { get; set; }

        public bool? IsGroup { get; set; }

        public bool? IsDisplay { get; set; }

        [StringLength(50)]
        public string DataType { get; set; }

        public bool? IsSum { get; set; }

        public bool? IsChange { get; set; }
    }
}

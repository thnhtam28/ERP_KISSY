namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class System_UserSetting
    {
        public int Id { get; set; }

        public int? SettingId { get; set; }

        public int? UserId { get; set; }

        [StringLength(300)]
        public string Value { get; set; }
    }
}

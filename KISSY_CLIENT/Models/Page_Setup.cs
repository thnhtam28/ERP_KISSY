namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Page_Setup
    {
        public string ID { get; set; }
        [StringLength(150)]
        public string CompanyName { get; set; }
        [StringLength(500)]
        public string Logo { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        [StringLength(15)]
        public string Phone1 { get; set; }

        [StringLength(15)]
        public string Phone2 { get; set; }

        [StringLength(15)]
        public string Phone3 { get; set; }

        [StringLength(15)]
        public string Fax { get; set; }

        [StringLength(100)]
        public string Email1 { get; set; }

        [StringLength(100)]
        public string Email2 { get; set; }

        [StringLength(100)]
        public string Email3 { get; set; }

        [StringLength(100)]
        public string Facebook { get; set; }

        [StringLength(100)]
        public string Google { get; set; }

        [StringLength(100)]
        public string MST { get; set; }
    }
}

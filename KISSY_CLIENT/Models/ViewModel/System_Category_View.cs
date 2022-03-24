using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_API.Models.ViewModel
{
    public class System_Category_View
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(150)]
        public string Value { get; set; }

        public string Description { get; set; }

        public int? OrderNo { get; set; }

        public int? ParentId { get; set; }

        [StringLength(50)]
        public string Code { get; set; }
        [StringLength(300)]
        public string Icon { get; set; }
        public ICollection<Sale_Product_View> Sale_Products { get; set; }
    }
}
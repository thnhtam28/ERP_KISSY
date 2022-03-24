using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using Erp.Domain.Sale.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Sale.Models
{
    public class CommissionCusViewModel
    {
        public int Id { get; set; }

        [Display(Name = "IsDeleted")]
        public bool? IsDeleted { get; set; }

        [Display(Name = "CreatedUser", ResourceType = typeof(Wording))]
        public int? CreatedUserId { get; set; }
        public string CreatedUserName { get; set; }

        [Display(Name = "CreatedDate", ResourceType = typeof(Wording))]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "ModifiedUser", ResourceType = typeof(Wording))]
        public int? ModifiedUserId { get; set; }
        public string ModifiedUserName { get; set; }

        [Display(Name = "ModifiedDate", ResourceType = typeof(Wording))]
        public DateTime? ModifiedDate { get; set; }

        [Display(Name = "AssignedUserId", ResourceType = typeof(Wording))]
        public int? AssignedUserId { get; set; }


        [Display(Name = "GIATRHANGHOA", ResourceType = typeof(Wording))]
        public decimal? TIEN_HANG { get; set; }

        [Display(Name = "GIATRIKHUYENMAI", ResourceType = typeof(Wording))]
        public decimal? TIEN_KM { get; set; }

        [Display(Name = "StatusKM", ResourceType = typeof(Wording))]
        public int? status { get; set; }

        [Display(Name = "StatusKM", ResourceType = typeof(Wording))]
        public string status1 { get; set; }
        public string AssignedUserName { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [StringLength(100, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "StringError", ErrorMessage = null)]
        [Display(Name = "Name", ResourceType = typeof(Wording))]
        public string Name { get; set; }
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "Type", ResourceType = typeof(Wording))]
        public string Type { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "StartDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> StartDate { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "EndDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> EndDate { get; set; }
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "ApplyFor", ResourceType = typeof(Wording))]
        public string ApplyFor { get; set; }
        [Display(Name = "Note", ResourceType = typeof(Wording))]
        public string Note { get; set; }
        public List<CommisionCustomerViewModel> DetailList { get; set; }
        public List<ProductViewModel> ProductList { get; set; }
        public List<CommisionApplyViewModel> ApplyDetail { get; set; }
        public string TEN_CTKM { get; set; }
        public string TEN_CUAHNG { get; set; }
        //
        public string STT { get; set; }
        public string IdCus { get; set; }
        public string BranchName { get; set; }
        public string IrregularDiscount { get; set; }
        public decimal IrregularDiscountAmount { get; set; }
        public decimal SumAmount { get; set; }
        //
        public bool IsMoney { get; set; }
        public string DiscountTabBill { get; set; }
        public string DiscountTabBillAmount { get; set; }
        public string nameCommitionCus { get; set; }
    }
}
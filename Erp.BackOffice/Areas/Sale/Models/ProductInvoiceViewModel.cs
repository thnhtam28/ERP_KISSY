using Erp.BackOffice.Account.Models;
using Erp.BackOffice.App_GlobalResources;
using Erp.BackOffice.Areas.Sale.Models;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Sale.Models
{
    public class ProductInvoiceViewModel
    {
        public ProductInvoiceViewModel()
        {
            FixedDiscount = 0;
            TaxFee = 0;
            TotalAmount = 0;
            IrregularDiscount = 0;
        }

        public int Id { get; set; }
        public decimal TienTra { get; set; }

        [Display(Name = "IsDeleted")]
        public bool? IsDeleted { get; set; }
        public bool recallCreateNT {get;set;}
        [Display(Name = "CreatedUser", ResourceType = typeof(Wording))]
        public int? CreatedUserId { get; set; }
        [Display(Name = "CreatedUserName", ResourceType = typeof(Wording))]
        public string CreatedUserName { get; set; }

        [Display(Name = "UserName", ResourceType = typeof(Wording))]
        public string UserName { get; set; }

        [Display(Name = "CreatedDate", ResourceType = typeof(Wording))]
        public DateTime? CreatedDate { get; set; }
        [Display(Name = "CreatedDateTemp", ResourceType = typeof(Wording))]
        public string CreatedDateTemp { get; set; }

        [Display(Name = "ModifiedUser", ResourceType = typeof(Wording))]
        public int? ModifiedUserId { get; set; }
        public string ModifiedUserName { get; set; }

        [Display(Name = "ModifiedDate", ResourceType = typeof(Wording))]
        public DateTime? ModifiedDate { get; set; }
        public string strModifiedDate
        {
            get
            {
                if (ModifiedDate != null)
                {
                    return ((DateTime)ModifiedDate).ToString("dd/MM/yyyy");
                }
                return "";
            }
        }

        [Display(Name = "Code", ResourceType = typeof(Wording))]
        public string Code { get; set; }

        [Display(Name = "TotalAmount", ResourceType = typeof(Wording))]
        public decimal TotalAmount { get; set; }

        [Display(Name = "TaxFee", ResourceType = typeof(Wording))]
        public double TaxFee { get; set; }

        //[Display(Name = "CustomerDiscount", ResourceType = typeof(Wording))]
        //public double Discount { get; set; }

        [Display(Name = "TongTienSauVAT", ResourceType = typeof(Wording))]
        public decimal TongTienSauVAT { get; set; }

        [Display(Name = "DiscountCode", ResourceType = typeof(Wording))]
        public string DiscountCode { get; set; }
        public decimal Amount2 { get; set; }

        [Display(Name = "CustomerName", ResourceType = typeof(Wording))]
        public int? CustomerId { get; set; }
        [Display(Name = "CustomerName", ResourceType = typeof(Wording))]
        public int? CustomerId2 { get; set; }
        [Display(Name = "CustomerName", ResourceType = typeof(Wording))]
        public int? CustomerId3 { get; set; }
        [Display(Name = "CustomerDiscount", ResourceType = typeof(Wording))]
        public Nullable<int> CustomerDiscountId { get; set; }

        [Display(Name = "Status", ResourceType = typeof(Wording))]
        public string Status { get; set; }

        [Display(Name = "Note", ResourceType = typeof(Wording))]
        public string Note { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Wording))]
        public string Email { get; set; }

        [Display(Name = "PaymentNow", ResourceType = typeof(Wording))]
        public bool IsArchive { get; set; }
        [Display(Name = "DrugStore", ResourceType = typeof(Wording))]
        public int? BranchId { get; set; }

        [Display(Name = "PaymentMethod", ResourceType = typeof(Wording))]
        public string PaymentMethod { get; set; }

        [Display(Name = "PaidAmount", ResourceType = typeof(Wording))]
        public decimal? PaidAmount { get; set; }

        [Display(Name = "RemainingAmount", ResourceType = typeof(Wording))]
        public decimal? RemainingAmount { get; set; }

        [Display(Name = "LyDoHuyChungTu", ResourceType = typeof(Wording))]
        public string CancelReason { get; set; }

        [Display(Name = "BarCode", ResourceType = typeof(Wording))]
        public string BarCode { get; set; }

        public int? SaleOrderId { get; set; }

        [Display(Name = "SaleOrderCode", ResourceType = typeof(Wording))]
        public string SaleOrderCode { get; set; }

        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "Saler", ResourceType = typeof(Wording))]
        public int? SalerId { get; set; }

        [Display(Name = "Saler", ResourceType = typeof(Wording))]
        public string SalerFullName { get; set; }

        [Display(Name = "CustomerCode", ResourceType = typeof(Wording))]
        public string CustomerCode { get; set; }

        [Display(Name = "CustomerName", ResourceType = typeof(Wording))]
        public string CustomerName { get; set; }
        [Display(Name = "CustomerName", ResourceType = typeof(Wording))]
        public string CustomerName2 { get; set; }
        [Display(Name = "CustomerName", ResourceType = typeof(Wording))]
        public string CustomerName3 { get; set; }

        [Display(Name = "DrugStore", ResourceType = typeof(Wording))]
        public string BranchName { get; set; }
   

        [Display(Name = "ProductOutboundCode", ResourceType = typeof(Wording))]
        public string ProductOutboundCode { get; set; }

        public int? ProductOutboundId { get; set; }

        public bool? IsReturn { get; set; }

        public List<ProductInvoiceDetailViewModel> DetailList { get; set; }
        public List<KmHd_ViewModel> DetailListKMHD { get; set; }
        public List<ProductInvoiceDetailViewModel> GroupProduct { get; set; }
        public ReceiptViewModel ReceiptViewModel { get; set; }

        [Display(Name = "NextPaymentDate", ResourceType = typeof(Wording))]
        public DateTime? NextPaymentDate { get; set; }

        [Display(Name = "NextPaymentDate", ResourceType = typeof(Wording))]
        public DateTime? NextPaymentDate_Temp { get; set; }

        [Display(Name = "CodeInvoiceRed", ResourceType = typeof(Wording))]
        public string CodeInvoiceRed { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "ProductItemCount", ResourceType = typeof(Wording))]
        public Nullable<int> ProductItemCount { get; set; }
        [Display(Name = "DisCountAmount", ResourceType = typeof(Wording))]
        public int? DisCountAmount { get; set; }


        //[Display(Name = "StaffCreateName", ResourceType = typeof(Wording))]
        //public string StaffCreateName { get; set; }

        public bool? AllowEdit { get; set; }
        public ProductOutboundViewModel ProductOutboundViewModel { get; set; }
        public List<TransactionLiabilitiesViewModel> ListTransactionLiabilities { get; set; }
        public List<ProcessPaymentViewModel> ListProcessPayment { get; set; }
        public int? QuantityCodeSaleReturns { get; set; }

        public string Image { get; set; }
        [Display(Name = "Phone", ResourceType = typeof(Wording))]
        public string CustomerPhone { get; set; }
        [Display(Name = "TotalFixedDiscount", ResourceType = typeof(Wording))]
        public decimal? FixedDiscount { get; set; }
        [Display(Name = "TotalIrregularDiscount", ResourceType = typeof(Wording))]
        public decimal? IrregularDiscount { get; set; }

        public int? Month { get; set; }
        public int? Year { get; set; }
        public int? Day { get; set; }
        public int? WeekOfYear { get; set; }
        public string UserTypeCode { get; set; }
        public string strCreatedDate { get; set; }

        public string DistrictId { get; set; }
        public string CityId { get; set; }
        public string ProvinceName { get; set; }
        public string DistrictName { get; set; }
        [Display(Name = "CodeTax", ResourceType = typeof(Wording))]
        public string TaxCode { get; set; }
        [Display(Name = "BankAccountNumber", ResourceType = typeof(Wording))]
        public string BankAccount { get; set; }
        [Display(Name = "BankName", ResourceType = typeof(Wording))]
        public string BankName { get; set; }
        [Display(Name = "CompanyName", ResourceType = typeof(Wording))]
        public string CompanyName { get; set; }
        [Display(Name = "Address", ResourceType = typeof(Wording))]
        public string Address { get; set; }

        [Display(Name = "PHISHIP", ResourceType = typeof(Wording))]
        public int? PHISHIP { get; set; }
        public decimal? DiscountTabBillAmount { get; set; }
        public decimal? DiscountTabBill { get; set; }
        [Display(Name = "MoneyPay", ResourceType = typeof(Wording))]
        public decimal? MoneyPay { get; set; }
        [Display(Name = "TransferPay", ResourceType = typeof(Wording))]
        public decimal? TransferPay { get; set; }
        [Display(Name = "ATMPay", ResourceType = typeof(Wording))]
        public decimal? ATMPay { get; set; }
        public bool? isDisCountAllTab { get; set; }
        public decimal? DisCountAllTab { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// su sung cho logvip
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public decimal? tienmuahang { get; set; }
        public decimal? tientralai { get; set; }
        public int? NAM { get; set; }
        public string Phone { get; set; }

        [Display(Name = "PlusPoint", ResourceType = typeof(Wording))]
        public int? PlusPoint { get; set; }
        public decimal? tongmua { get; set; }

        public string TenHang { get; set; }
        public string loai { get; set; }
        public int IdHang { get; set; }
        public int Idxephangcu { get; set; }
        public int TongDonHang { get; set; }
        public DateTime NgayMuaCuoi { get; set; }
        public string Mobile { get; set; }

    }
}
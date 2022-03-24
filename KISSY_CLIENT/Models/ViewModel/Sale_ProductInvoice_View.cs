using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_API.Models.ViewModel
{
    public class Sale_ProductInvoice_View
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

        public decimal? TotalAmount { get; set; }

        public double? TaxFee { get; set; }

        public double? Discount { get; set; }

        public decimal? PaidAmount { get; set; }

        public decimal? RemainingAmount { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(300)]
        public string Note { get; set; }

        public bool? IsArchive { get; set; }

        public int? BranchId { get; set; }

        [StringLength(250)]
        public string PaymentMethod { get; set; }

        [StringLength(300)]
        public string CancelReason { get; set; }

        [StringLength(50)]
        public string CodeInvoiceRed { get; set; }

        public bool? IsReturn { get; set; }

        public DateTime? NextPaymentDate { get; set; }

        [StringLength(50)]
        public string BarCode { get; set; }

        public int? CustomerId { get; set; }

        [StringLength(15)]
        public string CustomerPhone { get; set; }

        [StringLength(300)]
        public string CustomerName { get; set; }

        public decimal? FixedDiscount { get; set; }

        public decimal? IrregularDiscount { get; set; }

        [StringLength(15)]
        public string TaxCode { get; set; }

        [StringLength(50)]
        public string BankAccount { get; set; }

        [StringLength(200)]
        public string BankName { get; set; }

        [StringLength(300)]
        public string CompanyName { get; set; }

        [StringLength(150)]
        public string Address { get; set; }

        [StringLength(128)]
        public string UserID { get; set; }
        public string Province_Id { get; set; }
        public string District_Id { get; set; }
        public List<Sale_ProductInvoiceDetail_View> Sale_ProductInvoiceDetail_Views { get; set; }
    }
}
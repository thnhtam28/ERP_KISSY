using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class vwProductInvoice
    {
        public vwProductInvoice()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public string Code { get; set; }
        public decimal TotalAmount { get; set; }
        public double TaxFee { get; set; }
        public double Discount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RemainingAmount { get; set; }

        public string Status { get; set; }
        public string Note { get; set; }
        public bool IsArchive { get; set; }
        public int BranchId { get; set; }
        public string PaymentMethod { get; set; }
        public string CancelReason { get; set; }

        public string CustomerCode { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string ProductOutboundCode { get; set; }
        public string BranchName { get; set; }
        public string CodeInvoiceRed { get; set; }
        public string SalerFullName { get; set; }
        public int? ProductOutboundId { get; set; }
        public Nullable<bool> IsReturn { get; set; }
        public Nullable<System.DateTime> NextPaymentDate { get; set; }

        public string CustomerPhone { get; set; }
        public string CustomerName { get; set; }
        public string UserName { get; set; }
        public decimal FixedDiscount { get; set; }
        public decimal IrregularDiscount { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public int? Day { get; set; }
        public int? WeekOfYear { get; set; }

        public string DistrictId { get; set; }
        public string CityId { get; set; }
        public string ProvinceName { get; set; }
        public string DistrictName { get; set; }

        public string TaxCode { get; set; }
        public string BankAccount { get; set; }
        public string BankName { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public decimal? MoneyPay { get; set; }
        public decimal? TransferPay { get; set; }
        public decimal? ATMPay { get; set; }
        public int? PHISHIP { get; set; }
        public decimal? DiscountTabBillAmount { get; set; }
        public decimal? DiscountTabBill { get; set; }
        public bool? isDisCountAllTab { get; set; }
        public decimal? DisCountAllTab { get; set; }
        public string Mobile { get; set; }
        public bool? Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public decimal TienTra { get; set; }

    }
}

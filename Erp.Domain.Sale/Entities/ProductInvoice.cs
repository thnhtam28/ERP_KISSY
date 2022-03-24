using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class ProductInvoice
    {
        public ProductInvoice()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public decimal? DiscountTabBillAmount { get; set; }
        public decimal? MoneyPay { get; set; }
        public decimal? TransferPay { get; set; }
        public decimal? ATMPay { get; set; }

        public int? DiscountTabBill { get; set; }
        public string Code { get; set; }
        public decimal TotalAmount { get; set; }
        public double TaxFee { get; set; }
        public double Discount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RemainingAmount { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public bool IsArchive { get; set; }
        public int BranchId { get; set; }
        public string PaymentMethod { get; set; }
        public string CancelReason { get; set; }
        public string CodeInvoiceRed { get; set; }
        public bool IsReturn { get; set; }
        public Nullable<System.DateTime> NextPaymentDate { get; set; }

        public string CustomerPhone { get; set; }
        public string CustomerName { get; set; }
        public decimal FixedDiscount { get; set; }
        public decimal IrregularDiscount { get; set; }

        public string TaxCode { get; set; }
        public string BankAccount { get; set; }
        public string BankName { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public bool? isDisCountAllTab { get; set; }
        public decimal? DisCountAllTab { get; set; }

        public int? PromotionId { get; set; }
        public int? PromotionDetailId { get; set; }
        public int? PromotionValue { get; set; }
        public bool? IsMoney { get; set; }


    }
}

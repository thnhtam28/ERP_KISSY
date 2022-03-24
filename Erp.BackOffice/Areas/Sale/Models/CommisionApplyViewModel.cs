using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class CommisionApplyViewModel
    {
        public CommisionApplyViewModel() { }
        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public int CommissionCusId { get; set; }
        public int Type { get; set; }
        public int BranchId { get; set; }
        //thông tin Đối tượng Type=3
        public string BranchName { get; set; }
        public string BranchAdress { get; set; }
        public string PhoneBranch { get; set; }
        //thông tin Đối tượng Type=2
        public string LogVipName { get; set;}
        public decimal? MinMoney { get; set; }
        public decimal? MaxMoney { get; set; }
        public Nullable<int> PlusPoint { get; set; }
        //thông tin Đối tượng Type=1
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerPhone { get; set; }
    }
}

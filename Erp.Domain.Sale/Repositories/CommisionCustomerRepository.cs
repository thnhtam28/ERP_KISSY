using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class CommisionCustomerRepository : GenericRepository<ErpSaleDbContext, CommisionCustomer>, ICommisionCustomerRepository
    {
        public CommisionCustomerRepository(ErpSaleDbContext context)
            : base(context)
        {

        }

        public IQueryable<vwCommisionCustomer> GetAllCommisionCustomer()
        {
            return Context.vwCommisionCustomer.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }


        public IQueryable<CommisionCustomer> GetAllCommisionCustomerbyidcus(int idcus)
        {
            return Context.CommisionCustomer.Where(item => (item.CommissionCusId== idcus) &&(item.IsDeleted == null || item.IsDeleted == false));
        }

        public List<vwCommisionCustomer> GetListAllCommisionCustomer()
        {
            return Context.vwCommisionCustomer.Where(item => (item.IsDeleted == null || item.IsDeleted == false)).ToList();
        }
        public CommisionCustomer GetCommisionCustomerById(int Id)
        {
            return Context.CommisionCustomer.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public vwCommisionCustomer GetvwCommisionCustomerById(int Id)
        {
            return Context.vwCommisionCustomer.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public void InsertCommisionCustomer(CommisionCustomer CommisionCustomer)
        {
            Context.CommisionCustomer.Add(CommisionCustomer);
            Context.Entry(CommisionCustomer).State = EntityState.Added;
            Context.SaveChanges();
        }

        public void DeleteCommisionCustomer(int Id)
        {
            CommisionCustomer deletedCommisionCustomer = GetCommisionCustomerById(Id);
            Context.CommisionCustomer.Remove(deletedCommisionCustomer);
            Context.Entry(deletedCommisionCustomer).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        public void DeleteCommisionCustomerRs(int Id)
        {
            CommisionCustomer deleteCommisionCustomerRs = GetCommisionCustomerById(Id);
            deleteCommisionCustomerRs.IsDeleted = true;
            UpdateCommisionCustomer(deleteCommisionCustomerRs);
        }

        public void DeleteAllCommissionCustomerByIdCus(int IdCus)
        {
            var AllCus = GetAllCommisionCustomerbyidcus(IdCus);
            Context.CommisionCustomer.RemoveRange(AllCus);
            Context.SaveChanges();
        }

        public void UpdateCommisionCustomer(CommisionCustomer CommisionCustomer)
        {
            Context.Entry(CommisionCustomer).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}

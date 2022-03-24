using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class CommisionApplyRepository : GenericRepository<ErpSaleDbContext, CommisionApply>, ICommisionApplyRepository
    {
        public CommisionApplyRepository(ErpSaleDbContext context)
            : base(context)
        {

        }

        public IQueryable<CommisionApply> GetAllCommisionApply()
        {
            return Context.CommisionApply.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public List<CommisionApply> GetlistAllCommisionApply()
        {
            return Context.CommisionApply.Where(item => (item.IsDeleted == null || item.IsDeleted == false)).ToList();
        }
        public IQueryable<CommisionApply> GetAllCommisionApplybyIDCus(int idcus)
        {
            return Context.CommisionApply.Where(item => (item.CommissionCusId == idcus) && (item.IsDeleted == null || item.IsDeleted == false));
        }


        public CommisionApply GetCommisionApplyById(int Id)
        {
            return Context.CommisionApply.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public List<CommisionApply> GetListCommisionApplyByIdCus(int Id)
        {
            return Context.CommisionApply.Where(item => item.CommissionCusId == Id && (item.IsDeleted == null || item.IsDeleted == false)).ToList();
        }
        public void InsertCommisionApply(CommisionApply CommisionApply)
        {
            Context.CommisionApply.Add(CommisionApply);
            Context.Entry(CommisionApply).State = EntityState.Added;
            Context.SaveChanges();
        }

        public void DeleteCommisionApply(int Id)
        {
            CommisionApply deletedCommisionApply = GetCommisionApplyById(Id);
            Context.CommisionApply.Remove(deletedCommisionApply);
            Context.Entry(deletedCommisionApply).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        public void DeleteAllCommisionApplyByIdCus(int IdCus)
        {
            var AllCus = GetAllCommisionApplybyIDCus(IdCus);
            Context.CommisionApply.RemoveRange(AllCus);
            Context.SaveChanges();
        }

        public void DeleteCommisionApplyRs(int Id)
        {
            CommisionApply deleteCommisionApplyRs = GetCommisionApplyById(Id);
            deleteCommisionApplyRs.IsDeleted = true;
            UpdateCommisionApply(deleteCommisionApplyRs);
        }

        public void UpdateCommisionApply(CommisionApply CommisionApply)
        {
            Context.Entry(CommisionApply).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}

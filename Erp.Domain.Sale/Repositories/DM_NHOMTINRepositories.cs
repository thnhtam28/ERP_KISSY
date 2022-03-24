using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class DM_NHOMTINRepositories : GenericRepository<ErpSaleDbContext, DM_NHOMTIN>, IDM_NHOMTINRepositories
    {
        public DM_NHOMTINRepositories(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all DM_NHOMTIN
        /// </summary>
        /// <returns>DM_NHOMTIN list</returns>
        /// 
        public IQueryable<DM_NHOMTIN> GetAllDM_NHOMTIN()
        {
            return Context.DM_NHOMTIN.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        public DM_NHOMTIN GetDM_NHOMTINByNHOMTIN_ID(int NHOMTIN_ID)
        {
            return Context.DM_NHOMTIN.SingleOrDefault(item => item.NHOMTIN_ID == NHOMTIN_ID && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public DM_NHOMTIN GetDM_NHOMTINByTEN_LOAISANPHAM(string TEN_LOAISANPHAM)
        {
            return Context.DM_NHOMTIN.SingleOrDefault(item => item.TEN_LOAISANPHAM == TEN_LOAISANPHAM && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert DM_NHOMTIN into database
        /// </summary>
        /// <param name="DM_NHOMTIN">Object infomation</param>
        public void InsertDM_NHOMTIN(DM_NHOMTIN DM_NHOMTIN)
        {
            Context.DM_NHOMTIN.Add(DM_NHOMTIN);
            Context.Entry(DM_NHOMTIN).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete DM_NHOMTIN with specific NHOMTIN_ID
        /// </summary>
        /// <param name="NHOMTIN_ID">DM_NHOMTIN NHOMTIN_ID</param>
        public void DeleteDM_NHOMTIN(int NHOMTIN_ID)
        {
            DM_NHOMTIN deletedDM_NHOMTIN = GetDM_NHOMTINByNHOMTIN_ID(NHOMTIN_ID);
            Context.DM_NHOMTIN.Remove(deletedDM_NHOMTIN);
            Context.Entry(deletedDM_NHOMTIN).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete a DM_NHOMTIN with its NHOMTIN_ID and Update IsDeleted IF that DM_NHOMTIN has relationship with others
        /// </summary>
        /// <param name="NHOMTIN_ID">Id of DM_NHOMTIN</param>
        public void DeleteDM_NHOMTINRs(int NHOMTIN_ID)
        {
            DM_NHOMTIN deleteDM_NHOMTINRs = GetDM_NHOMTINByNHOMTIN_ID(NHOMTIN_ID);
            deleteDM_NHOMTINRs.IsDeleted = true;
            UpdateDM_NHOMTIN(deleteDM_NHOMTINRs);
        }

        /// <summary>
        /// Update DM_NHOMTIN into database
        /// </summary>
        /// <param name="DM_NHOMTIN">DM_NHOMTIN object</param>
        public void UpdateDM_NHOMTIN(DM_NHOMTIN DM_NHOMTIN)
        {
            Context.Entry(DM_NHOMTIN).State = EntityState.Modified;
            Context.SaveChanges();
        }

    }
}

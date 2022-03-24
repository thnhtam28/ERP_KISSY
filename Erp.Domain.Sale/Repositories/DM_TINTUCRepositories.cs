using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class DM_TINTUCRepositories : GenericRepository<ErpSaleDbContext, DM_TINTUC>, IDM_TINTUCRepositories
    {
        public DM_TINTUCRepositories(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all DM_TINTUC
        /// </summary>
        /// <returns>DM_TINTUC list</returns>
        /// 
        public IQueryable<DM_TINTUC> GetAllDM_TINTUC()
        {
            return Context.DM_TINTUC.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        public IQueryable<DM_TINTUC> GetAllDM_TINTUCByNHOMTIN_ID(int NHOMTIN_ID)
        {
            return Context.DM_TINTUC.Where(item => item.NHOMTIN_ID == NHOMTIN_ID && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public DM_TINTUC GetDM_TINTUCByTINTUC_ID(int TINTUC_ID)
        {
            return Context.DM_TINTUC.SingleOrDefault(item => item.TINTUC_ID == TINTUC_ID && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert DM_TINTUC into database
        /// </summary>
        /// <param name="DM_TINTUC">Object infomation</param>
        public void InsertDM_TINTUC(DM_TINTUC DM_TINTUC)
        {
            Context.DM_TINTUC.Add(DM_TINTUC);
            Context.Entry(DM_TINTUC).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete DM_TINTUC with specific TINTUC_ID
        /// </summary>
        /// <param name="TINTUC_ID">DM_TINTUC TINTUC_ID</param>
        public void DeleteDM_TINTUC(int TINTUC_ID)
        {
            DM_TINTUC deletedDM_TINTUC = GetDM_TINTUCByTINTUC_ID(TINTUC_ID);
            Context.DM_TINTUC.Remove(deletedDM_TINTUC);
            Context.Entry(deletedDM_TINTUC).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete a DM_TINTUC with its TINTUC_ID and Update IsDeleted IF that DM_TINTUC has relationship with others
        /// </summary>
        /// <param name="TINTUC_ID">Id of DM_TINTUC</param>
        public void DeleteDM_TINTUCRs(int TINTUC_ID)
        {
            DM_TINTUC deleteDM_TINTUCRs = GetDM_TINTUCByTINTUC_ID(TINTUC_ID);
            deleteDM_TINTUCRs.IsDeleted = true;
            UpdateDM_TINTUC(deleteDM_TINTUCRs);
        }

        /// <summary>
        /// Update DM_TINTUC into database
        /// </summary>
        /// <param name="DM_TINTUC">DM_TINTUC object</param>
        public void UpdateDM_TINTUC(DM_TINTUC DM_TINTUC)
        {
            Context.Entry(DM_TINTUC).State = EntityState.Modified;
            Context.SaveChanges();
        }

        

       
    }
}

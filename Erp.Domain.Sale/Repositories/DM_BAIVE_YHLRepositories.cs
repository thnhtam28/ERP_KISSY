using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class DM_BAIVE_YHLRepositories : GenericRepository<ErpSaleDbContext, DM_BAIVE_YHL>, IDM_BAIVE_YHLRepositories
    {
        public DM_BAIVE_YHLRepositories(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all DM_BAIVE_YHL
        /// </summary>
        /// <returns>DM_BAIVE_YHL list</returns>
        /// 
        public IQueryable<DM_BAIVE_YHL> GetAllDM_BAIVE_YHL()
        {
            return Context.DM_BAIVE_YHL.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        public DM_BAIVE_YHL GetDM_BAIVE_YHLByBAIVE_YHL_ID(int BAIVE_YHL_ID)
        {
            return Context.DM_BAIVE_YHL.SingleOrDefault(item => item.BAIVE_YHL_ID == BAIVE_YHL_ID && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert BAIVE_YHL into database
        /// </summary>
        /// <param name="DM_BAIVE_YHL">Object infomation</param>
        public void InsertDM_BAIVE_YHL(DM_BAIVE_YHL DM_BAIVE_YHL)
        {
            Context.DM_BAIVE_YHL.Add(DM_BAIVE_YHL);
            Context.Entry(DM_BAIVE_YHL).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete DM_BAIVE_YHL with specific BAIVE_YHL_ID
        /// </summary>
        /// <param name="BAIVE_YHL_ID">DM_BAIVE_YHL BAIVE_YHL_ID</param>
        public void DeleteDM_BAIVE_YHL(int BAIVE_YHL_ID)
        {
            DM_BAIVE_YHL deletedDM_BAIVE_YHL = GetDM_BAIVE_YHLByBAIVE_YHL_ID(BAIVE_YHL_ID);
            Context.DM_BAIVE_YHL.Remove(deletedDM_BAIVE_YHL);
            Context.Entry(deletedDM_BAIVE_YHL).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete a DM_BAIVE_YHL with its BAIVE_YHL_ID and Update IsDeleted IF that DM_BAIVE_YHL has relationship with others
        /// </summary>
        /// <param name="BAIVE_YHL_ID">Id of DM_BAIVE_YHL</param>
        public void DeleteDM_BAIVE_YHLRs(int BAIVE_YHL_ID)
        {
            DM_BAIVE_YHL deleteDM_BAIVE_YHLRs = GetDM_BAIVE_YHLByBAIVE_YHL_ID(BAIVE_YHL_ID);
            deleteDM_BAIVE_YHLRs.IsDeleted = true;
            UpdateDM_BAIVE_YHL(deleteDM_BAIVE_YHLRs);
        }

        /// <summary>
        /// Update DM_BAIVE_YHL into database
        /// </summary>
        /// <param name="DM_BAIVE_YHL">DM_BAIVE_YHL object</param>
        public void UpdateDM_BAIVE_YHL(DM_BAIVE_YHL DM_BAIVE_YHL)
        {
            Context.Entry(DM_BAIVE_YHL).State = EntityState.Modified;
            Context.SaveChanges();
        }

    }
}

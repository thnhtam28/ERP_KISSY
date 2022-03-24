using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class DM_DEALHOTRepositories : GenericRepository<ErpSaleDbContext, DM_DEALHOT>, IDM_DEALHOTRepositories
    {
        public DM_DEALHOTRepositories(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all DM_DEALHOT
        /// </summary>
        /// <returns>DM_DEALHOT list</returns>
        /// 
        public IQueryable<DM_DEALHOT> GetAllDM_DEALHOT()
        {
            return Context.DM_DEALHOT.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        public DM_DEALHOT GetDM_DEALHOTByDEALHOT_ID(int DEALHOT_ID)
        {
            return Context.DM_DEALHOT.SingleOrDefault(item => item.DEALHOT_ID == DEALHOT_ID && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public DM_DEALHOT GetDM_DEALHOTByCommissionCus_Id(int CommissionCus_Id)
        {
            return Context.DM_DEALHOT.SingleOrDefault(item => item.CommissionCus_Id == CommissionCus_Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert DM_DEALHOT into database
        /// </summary>
        /// <param name="DM_DEALHOT">Object infomation</param>
        public void InsertDM_DEALHOT(DM_DEALHOT DM_DEALHOT)
        {
            Context.DM_DEALHOT.Add(DM_DEALHOT);
            Context.Entry(DM_DEALHOT).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete DM_DEALHOT with specific DEALHOT_ID
        /// </summary>
        /// <param name="DEALHOT_ID">DM_DEALHOT DEALHOT_ID</param>
        public void DeleteDM_DEALHOT(int DEALHOT_ID)
        {
            DM_DEALHOT deletedDM_DEALHOT = GetDM_DEALHOTByDEALHOT_ID(DEALHOT_ID);
            Context.DM_DEALHOT.Remove(deletedDM_DEALHOT);
            Context.Entry(deletedDM_DEALHOT).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete a DM_DEALHOT with its DEALHOT_ID and Update IsDeleted IF that DM_DEALHOT has relationship with others
        /// </summary>
        /// <param name="DEALHOT_ID">Id of DM_DEALHOT</param>
        public void DeleteDM_DEALHOTRs(int DEALHOT_ID)
        {
            DM_DEALHOT deleteDM_DEALHOTRs = GetDM_DEALHOTByDEALHOT_ID(DEALHOT_ID);
            deleteDM_DEALHOTRs.IsDeleted = true;
            UpdateDM_DEALHOT(deleteDM_DEALHOTRs);
        }

        /// <summary>
        /// Update DM_DEALHOT into database
        /// </summary>
        /// <param name="DM_DEALHOT">DM_DEALHOT object</param>
        public void UpdateDM_DEALHOT(DM_DEALHOT DM_DEALHOT)
        {
            Context.Entry(DM_DEALHOT).State = EntityState.Modified;
            Context.SaveChanges();
        }

    }
}

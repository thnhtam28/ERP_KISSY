using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IDM_DEALHOTRepositories
    {
         /// <summary>
        /// Get all DM_DEALHOT
        /// </summary>
        /// <returns>DM_DEALHOT list</returns>
        /// 
        IQueryable<DM_DEALHOT> GetAllDM_DEALHOT();

        DM_DEALHOT GetDM_DEALHOTByDEALHOT_ID(int DEALHOT_ID);
        DM_DEALHOT GetDM_DEALHOTByCommissionCus_Id(int CommissionCus_Id);
        /// <summary>
        /// Insert DM_DEALHOT into database
        /// </summary>
        /// <param name="DM_DEALHOT">Object infomation</param>
        void InsertDM_DEALHOT(DM_DEALHOT DM_DEALHOT);

        /// <summary>
        /// Delete DM_DEALHOT with specific DEALHOT_ID
        /// </summary>
        /// <param name="DEALHOT_ID">DM_DEALHOT DEALHOT_ID</param>
        void DeleteDM_DEALHOT(int DEALHOT_ID);

        /// <summary>
        /// Delete a DM_DEALHOT with its DEALHOT_ID and Update IsDeleted IF that DM_DEALHOT has relationship with others
        /// </summary>
        /// <param name="DEALHOT_ID">Id of DM_DEALHOT</param>
        void DeleteDM_DEALHOTRs(int DEALHOT_ID);

        /// <summary>
        /// Update DM_DEALHOT into database
        /// </summary>
        /// <param name="DM_DEALHOT">DM_DEALHOT object</param>
        void UpdateDM_DEALHOT(DM_DEALHOT DM_DEALHOT);
    }
}

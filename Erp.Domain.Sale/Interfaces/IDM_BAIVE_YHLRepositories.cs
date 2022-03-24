using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IDM_BAIVE_YHLRepositories
    {
        /// <summary>
        /// Get all DM_BAIVE_YHL
        /// </summary>
        /// <returns>DM_BAIVE_YHL list</returns>
        /// 
        IQueryable<DM_BAIVE_YHL> GetAllDM_BAIVE_YHL();
        DM_BAIVE_YHL GetDM_BAIVE_YHLByBAIVE_YHL_ID(int BAIVE_YHL_ID);

        /// <summary>
        /// Insert DM_BAIVE_YHL into database
        /// </summary>
        /// <param name="DM_BAIVE_YHL">Object infomation</param>
        void InsertDM_BAIVE_YHL(DM_BAIVE_YHL DM_BAIVE_YHL);

        /// <summary>
        /// Delete DM_BAIVE_YHL with specific BAIVE_YHL_ID
        /// </summary>
        /// <param name="BAIVE_YHL_ID">DM_BAIVE_YHL BAIVE_YHL_ID</param>
        void DeleteDM_BAIVE_YHL(int BAIVE_YHL_ID);

        /// <summary>
        /// Delete a DM_BAIVE_YHL with its BAIVE_YHL_ID and Update IsDeleted IF that DM_BAIVE_YHL has relationship with others
        /// </summary>
        /// <param name="BAIVE_YHL_ID">Id of DM_BAIVE_YHL</param>
        void DeleteDM_BAIVE_YHLRs(int BAIVE_YHL_ID);

        /// <summary>
        /// Update DM_BAIVE_YHL into database
        /// </summary>
        /// <param name="DM_BAIVE_YHL">DM_BAIVE_YHL object</param>
        void UpdateDM_BAIVE_YHL(DM_BAIVE_YHL DM_BAIVE_YHL);
    }
}

using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IDM_BAOCHIRepositories
    {
        /// <summary>
        /// Get all DM_BAOCHI
        /// </summary>
        /// <returns>DM_BAOCHI list</returns>
        /// 
        IQueryable<DM_BAOCHI> GetAllDM_BAOCHI();

        //IQueryable<DM_BAOCHI> GetAllDM_BAOCHIByBAOCHI_ID(int BAOCHI_ID);
        DM_BAOCHI GetDM_BAOCHIByBAOCHI_ID(int BAOCHI_ID);

        /// <summary>
        /// Insert DM_BAOCHI into database
        /// </summary>
        /// <param name="DM_BAOCHI">Object infomation</param>
        void InsertDM_BAOCHI(DM_BAOCHI DM_BAOCHI);

        /// <summary>
        /// Delete DM_BAOCHI with specific BAOCHI_ID
        /// </summary>
        /// <param name="BAOCHI_ID">DM_BAOCHI BAOCHI_ID</param>
        void DeleteDM_BAOCHI(int BAOCHI_ID);

        /// <summary>
        /// Delete a DM_BAOCHI with its BAOCHI_ID and Update IsDeleted IF that DM_BAOCHI has relationship with others
        /// </summary>
        /// <param name="BAOCHI_ID">Id of DM_BAOCHI</param>
        void DeleteDM_BAOCHIRs(int BAOCHI_ID);

        /// <summary>
        /// Update DM_BAOCHI into database
        /// </summary>
        /// <param name="DM_BAOCHI">DM_BAOCHI object</param>
        void UpdateDM_BAOCHI(DM_BAOCHI DM_BAOCHI);
    }
}

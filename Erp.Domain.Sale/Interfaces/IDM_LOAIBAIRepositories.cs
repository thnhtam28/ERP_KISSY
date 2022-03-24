using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IDM_LOAIBAIRepositories
    {
        /// <summary>
        /// Get all DM_LOAIBAI
        /// </summary>
        /// <returns>DM_LOAIBAI list</returns>
        /// 
        IQueryable<DM_LOAIBAI> GetAllDM_LOAIBAI();
        DM_LOAIBAI GetDM_LOAIBAIByLOAIBAI_ID(int LOAIBAI_ID);
        DM_LOAIBAI GetDM_LOAIBAIByCODE_LOAIBAI(string CODE_LOAIBAI);

        /// <summary>
        /// Insert DM_LOAIBAI into database
        /// </summary>
        /// <param name="DM_LOAIBAI">Object infomation</param>
        void InsertDM_LOAIBAI(DM_LOAIBAI DM_LOAIBAI);

        /// <summary>
        /// Delete DM_LOAIBAI with specific LOAIBAI_ID
        /// </summary>
        /// <param name="LOAIBAI_ID">DM_LOAIBAI LOAIBAI_ID</param>
        void DeleteDM_LOAIBAI(int LOAIBAI_ID);

        /// <summary>
        /// Delete a DM_LOAIBAI with its LOAIBAI_ID and Update IsDeleted IF that LOAIBAI_YHL has relationship with others
        /// </summary>
        /// <param name="LOAIBAI_ID">Id of DM_LOAIBAI</param>
        void DeleteDM_LOAIBAIRs(int LOAIBAI_ID);

        /// <summary>
        /// Update DM_LOAIBAI into database
        /// </summary>
        /// <param name="DM_LOAIBAI">DM_LOAIBAI object</param>
        void UpdateDM_LOAIBAI(DM_LOAIBAI DM_LOAIBAI);
    }
}

using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IDM_BEST_SELLERRepositories
    {
        /// <summary>
        /// Get all DM_BEST_SELLER
        /// </summary>
        /// <returns>DM_BEST_SELLER list</returns>
        /// 
        IQueryable<DM_BEST_SELLER> GetAllDM_BEST_SELLER();
        DM_BEST_SELLER GetDM_BEST_SELLERByBEST_SELLER_ID(int BEST_SELLER_ID);

        /// <summary>
        /// Insert DM_BEST_SELLER into database
        /// </summary>
        /// <param name="DM_BEST_SELLER">Object infomation</param>
        void InsertDM_BEST_SELLER(DM_BEST_SELLER DM_BEST_SELLER);

        /// <summary>
        /// Delete DM_BEST_SELLER with specific BEST_SELLER_ID
        /// </summary>
        /// <param name="BEST_SELLER_ID">DM_BEST_SELLER BEST_SELLER_ID</param>
        void DeleteDM_BEST_SELLER(int BEST_SELLER_ID);

        /// <summary>
        /// Delete a DM_BEST_SELLER with its BEST_SELLER_ID and Update IsDeleted IF that DM_BEST_SELLER has relationship with others
        /// </summary>
        /// <param name="BEST_SELLER_ID">Id of DM_BEST_SELLER</param>
        void DeleteDM_BEST_SELLERRs(int BEST_SELLER_ID);

        /// <summary>
        /// Update DM_BEST_SELLER into database
        /// </summary>
        /// <param name="DM_BEST_SELLER">DM_BEST_SELLER object</param>
        void UpdateDM_BEST_SELLER(DM_BEST_SELLER DM_BEST_SELLER);
    }
}

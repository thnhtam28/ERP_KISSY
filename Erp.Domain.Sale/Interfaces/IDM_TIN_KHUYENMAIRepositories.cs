using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IDM_TIN_KHUYENMAIRepositories
    {
        /// <summary>
        /// Get all DM_TIN_KHUYENMAI
        /// </summary>
        /// <returns>DM_TIN_KHUYENMAI list</returns>
        /// 
        IQueryable<DM_TIN_KHUYENMAI> GetAllDM_TIN_KHUYENMAI();
        DM_TIN_KHUYENMAI GetDM_TIN_KHUYENMAIByTIN_KHUYENMAI_ID(int TIN_KHUYENMAI_ID);

        /// <summary>
        /// Insert DM_TIN_KHUYENMAI into database
        /// </summary>
        /// <param name="DM_TIN_KHUYENMAI">Object infomation</param>
        void InsertDM_TIN_KHUYENMAI(DM_TIN_KHUYENMAI DM_TIN_KHUYENMAI);

        /// <summary>
        /// Delete DM_TIN_KHUYENMAI with specific TIN_KHUYENMAI_ID
        /// </summary>
        /// <param name="TIN_KHUYENMAI_ID">DM_TIN_KHUYENMAI TIN_KHUYENMAI_ID</param>
        void DeleteDM_TIN_KHUYENMAI(int TIN_KHUYENMAI_ID);

        /// <summary>
        /// Delete a DM_TIN_KHUYENMAI with its TIN_KHUYENMAI_ID and Update IsDeleted IF that DM_TIN_KHUYENMAI has relationship with others
        /// </summary>
        /// <param name="TIN_KHUYENMAI_ID">Id of DM_TIN_KHUYENMAI</param>
        void DeleteDM_TIN_KHUYENMAIRs(int TIN_KHUYENMAI_ID);

        /// <summary>
        /// Update DM_TIN_KHUYENMAI into database
        /// </summary>
        /// <param name="DM_TIN_KHUYENMAI">DM_TIN_KHUYENMAI object</param>
        void UpdateDM_TIN_KHUYENMAI(DM_TIN_KHUYENMAI DM_TIN_KHUYENMAI);
    }
}

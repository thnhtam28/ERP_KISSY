using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IDM_CAMNHAN_KHANGRepositories
    {
        /// <summary>
        /// Get all DM_CAMNHAN_KHANG
        /// </summary>
        /// <returns>DM_CAMNHAN_KHANG list</returns>
        /// 
        IQueryable<DM_CAMNHAN_KHANG> GetAllDM_CAMNHAN_KHANG();
        DM_CAMNHAN_KHANG GetDM_CAMNHAN_KHANGByCAMNHAN_KHANG_ID(int CAMNHAN_KHANG_ID);

        /// <summary>
        /// Insert DM_CAMNHAN_KHANG into database
        /// </summary>
        /// <param name="DM_CAMNHAN_KHANG">Object infomation</param>
        void InsertDM_CAMNHAN_KHANG(DM_CAMNHAN_KHANG DM_CAMNHAN_KHANG);

        /// <summary>
        /// Delete DM_CAMNHAN_KHANG with specific CAMNHAN_KHANG_ID
        /// </summary>
        /// <param name="CAMNHAN_KHANG_ID">DM_CAMNHAN_KHANG CAMNHAN_KHANG_ID</param>
        void DeleteDM_CAMNHAN_KHANG(int CAMNHAN_KHANG_ID);

        /// <summary>
        /// Delete a DM_CAMNHAN_KHANG with its CAMNHAN_KHANG_ID and Update IsDeleted IF that CAMNHAN_KHANG_YHL has relationship with others
        /// </summary>
        /// <param name="CAMNHAN_KHANG_ID">Id of CAMNHAN_KHANG_YHL</param>
        void DeleteDM_CAMNHAN_KHANGRs(int CAMNHAN_KHANG_ID);

        /// <summary>
        /// Update DM_CAMNHAN_KHANG into database
        /// </summary>
        /// <param name="DM_CAMNHAN_KHANG">DM_CAMNHAN_KHANG object</param>
        void UpdateDM_CAMNHAN_KHANG(DM_CAMNHAN_KHANG DM_CAMNHAN_KHANG);
    }
}

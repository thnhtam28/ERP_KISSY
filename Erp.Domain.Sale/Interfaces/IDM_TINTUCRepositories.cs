using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IDM_TINTUCRepositories
    {
        /// <summary>
        /// Get all DM_TINTUC
        /// </summary>
        /// <returns>DM_TINTUC list</returns>
        /// 
        IQueryable<DM_TINTUC> GetAllDM_TINTUC();

        IQueryable<DM_TINTUC> GetAllDM_TINTUCByNHOMTIN_ID(int NHOMTIN_ID);

        //IQueryable<DM_TINTUC> GetAllDM_TINTUCByTag(string tag);
        DM_TINTUC GetDM_TINTUCByTINTUC_ID(int TINTUC_ID);

        /// <summary>
        /// Insert DM_TINTUC into database
        /// </summary>
        /// <param name="DM_TINTUC">Object infomation</param>
        void InsertDM_TINTUC(DM_TINTUC DM_TINTUC);

        /// <summary>
        /// Delete DM_TINTUC with specific TINTUC_ID
        /// </summary>
        /// <param name="TINTUC_ID">DM_TINTUC TINTUC_ID</param>
        void DeleteDM_TINTUC(int TINTUC_ID);

        /// <summary>
        /// Delete a DM_TINTUC with its TINTUC_ID and Update IsDeleted IF that DM_TINTUC has relationship with others
        /// </summary>
        /// <param name="TINTUC_ID">Id of DM_TINTUC</param>
        void DeleteDM_TINTUCRs(int TINTUC_ID);

        /// <summary>
        /// Update DM_TINTUC into database
        /// </summary>
        /// <param name="DM_TINTUC">DM_TINTUC object</param>
        void UpdateDM_TINTUC(DM_TINTUC DM_TINTUC);

        
    }
}

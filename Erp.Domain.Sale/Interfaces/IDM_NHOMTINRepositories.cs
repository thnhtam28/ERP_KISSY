using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IDM_NHOMTINRepositories
    {
        /// <summary>
        /// Get all DM_NHOMTIN
        /// </summary>
        /// <returns>DM_NHOMTIN list</returns>
        /// 
        IQueryable<DM_NHOMTIN> GetAllDM_NHOMTIN();

        DM_NHOMTIN GetDM_NHOMTINByNHOMTIN_ID(int NHOMTIN_ID);
        DM_NHOMTIN GetDM_NHOMTINByTEN_LOAISANPHAM(string TEN_LOAISANPHAM);

        /// <summary>
        /// Insert DM_NHOMTIN into database
        /// </summary>
        /// <param name="DM_NHOMTIN">Object infomation</param>
        void InsertDM_NHOMTIN(DM_NHOMTIN DM_NHOMTIN);

        /// <summary>
        /// Delete DM_NHOMTIN with specific NHOMTIN_ID
        /// </summary>
        /// <param name="NHOMTIN_ID">DM_NHOMTIN NHOMTIN_ID</param>
        void DeleteDM_NHOMTIN(int NHOMTIN_ID);

        /// <summary>
        /// Delete a DM_NHOMTIN with its NHOMTIN_ID and Update IsDeleted IF that DM_NHOMTIN has relationship with others
        /// </summary>
        /// <param name="NHOMTIN_ID">Id of DM_NHOMTIN</param>
        void DeleteDM_NHOMTINRs(int NHOMTIN_ID);

        /// <summary>
        /// Update DM_NHOMTIN into database
        /// </summary>
        /// <param name="DM_NHOMTIN">DM_NHOMTIN object</param>
        void UpdateDM_NHOMTIN(DM_NHOMTIN DM_NHOMTIN);
    }
}

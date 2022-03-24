using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IYHL_KIENHANG_TRARepositories
    {
        /// <summary>
        /// Get all YHL_KIENHANG_TRA
        /// </summary>
        /// <returns>YHL_KIENHANG_TRA list</returns>
        /// 
        IQueryable<YHL_KIENHANG_TRA> GetAllYHL_KIENHANG_TRA();
        YHL_KIENHANG_TRA GetKIENHANG_TRAByID(int KIENHANG_TRA_ID);


        /// <summary>
        /// Insert YHL_KIENHANG_TRA into database
        /// </summary>
        /// <param name="YHL_KIENHANG_TRA">Object infomation</param>
        void InsertYHL_KIENHANG_TRA(YHL_KIENHANG_TRA YHL_KIENHANG_TRA);

        /// <summary>
        /// Delete YHL_KIENHANG_TRA with specific YHL_KIENHANG_TRA
        /// </summary>
        /// <param name="YHL_KIENHANG_TRA">YHL_KIENHANG_TRA KIENHANG_TRA_ID</param>
        void DeleteYHL_KIENHANG_TRA(int KIENHANG_TRA_ID);

        /// <summary>
        /// Delete a YHL_KIENHANG_TRA with its KIENHANG_TRA_ID and Update IsDeleted IF that YHL_KIENHANG_TRA has relationship with others
        /// </summary>
        /// <param name="KIENHANG_TRA_ID">Id of YHL_KIENHANG_TRA</param>
        void DeleteYHL_KIENHANG_TRARs(int KIENHANG_TRA_ID);

        /// <summary>
        /// Update YHL_KIENHANG_TRA into database
        /// </summary>
        /// <param name="YHL_KIENHANG_TRA">YHL_KIENHANG_TRA object</param>
        void UpdateYHL_KIENHANG_TRA(YHL_KIENHANG_TRA YHL_KIENHANG_TRA);
    }
}

using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IYHL_KIENHANG_TRA_CTIETRepositories
    {
        /// <summary>
        /// Get all YHL_KIENHANG_TRA_CTIET
        /// </summary>
        /// <returns>YHL_KIENHANG_TRA_CTIET list</returns>
        /// 
        IQueryable<YHL_KIENHANG_TRA_CTIET> GetAllYHL_KIENHANG_TRA_CTIET();
        YHL_KIENHANG_TRA_CTIET GetKIENHANG_TRA_CTIETByID(int KIENHANG_TRA_CTIET_ID);

        YHL_KIENHANG_TRA_CTIET GetKIENHANG_TRA_CTIETByMA_DH(string MA_DON_HANG);

        YHL_KIENHANG_TRA_CTIET GetKIENHANG_TRA_CTIETBySO_HIEU(string SO_HIEU);

        /// <summary>
        /// Insert YHL_KIENHANG_TRA_CTIET into database
        /// </summary>
        /// <param name="YHL_KIENHANG_TRA_CTIET">Object infomation</param>
        void InsertYHL_KIENHANG_TRA_CTIET(YHL_KIENHANG_TRA_CTIET YHL_KIENHANG_TRA_CTIET);

        /// <summary>
        /// Delete YHL_KIENHANG_TRA_CTIET with specific YHL_KIENHANG_TRA_CTIET
        /// </summary>
        /// <param name="YHL_KIENHANG_TRA_CTIET">YHL_KIENHANG_TRA_CTIET KIENHANG_TRA_CTIET_ID</param>
        void DeleteYHL_KIENHANG_TRA_CTIET(int KIENHANG_TRA_CTIET_ID);

        /// <summary>
        /// Delete a YHL_KIENHANG_TRA_CTIET with its KIENHANG_TRA_CTIET_ID and Update IsDeleted IF that YHL_KIENHANG_TRA_CTIET has relationship with others
        /// </summary>
        /// <param name="KIENHANG_TRA_CTIET_ID">Id of YHL_KIENHANG_TRA_CTIET</param>
        void DeleteYHL_KIENHANG_TRA_CTIETRs(int KIENHANG_TRA_CTIET_ID);

        /// <summary>
        /// Update YHL_KIENHANG_TRA_CTIET into database
        /// </summary>
        /// <param name="YHL_KIENHANG_TRA_CTIET">YHL_KIENHANG_TRA_CTIET object</param>
        void UpdateYHL_KIENHANG_TRA_CTIET(YHL_KIENHANG_TRA_CTIET YHL_KIENHANG_TRA_CTIET);
    }
}

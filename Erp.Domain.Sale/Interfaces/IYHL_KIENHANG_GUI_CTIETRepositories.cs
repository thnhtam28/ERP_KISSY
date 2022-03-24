using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IYHL_KIENHANG_GUI_CTIETRepositories
    {
        /// <summary>
        /// Get all YHL_KIENHANG_GUI_CTIET
        /// </summary>
        /// <returns>YHL_KIENHANG_GUI_CTIET list</returns>
        /// 
        IQueryable<YHL_KIENHANG_GUI_CTIET> GetAllYHL_KIENHANG_GUI_CTIET();
        YHL_KIENHANG_GUI_CTIET GetKIENHANG_GUI_CTIETByID(int KIENHANG_GUI_CTIET_ID);

        YHL_KIENHANG_GUI_CTIET GetKIENHANG_GUI_CTIETByMA_DH(string MA_DON_HANG);

        YHL_KIENHANG_GUI_CTIET GetKIENHANG_GUI_CTIETBySO_HIEU(string SO_HIEU);

        /// <summary>
        /// Insert YHL_KIENHANG_GUI_CTIET into database
        /// </summary>
        /// <param name="YHL_KIENHANG_GUI_CTIET">Object infomation</param>
        void InsertYHL_KIENHANG_GUI_CTIET(YHL_KIENHANG_GUI_CTIET YHL_KIENHANG_GUI_CTIET);

        /// <summary>
        /// Delete YHL_KIENHANG_GUI_CTIET with specific YHL_KIENHANG_GUI_CTIET
        /// </summary>
        /// <param name="YHL_KIENHANG_GUI_CTIET">YHL_KIENHANG_GUI_CTIET KIENHANG_GUI_CTIET_ID</param>
        void DeleteYHL_KIENHANG_GUI_CTIET(int KIENHANG_GUI_CTIET_ID);

        /// <summary>
        /// Delete a YHL_KIENHANG_GUI_CTIET with its KIENHANG_GUI_CTIET_ID and Update IsDeleted IF that YHL_KIENHANG_GUI_CTIET has relationship with others
        /// </summary>
        /// <param name="KIENHANG_GUI_CTIET_ID">Id of YHL_KIENHANG_GUI_CTIET</param>
        void DeleteYHL_KIENHANG_GUI_CTIETRs(int KIENHANG_GUI_CTIET_ID);

        /// <summary>
        /// Update YHL_KIENHANG_GUI_CTIET into database
        /// </summary>
        /// <param name="YHL_KIENHANG_GUI_CTIET">YHL_KIENHANG_GUI_CTIET object</param>
        void UpdateYHL_KIENHANG_GUI_CTIET(YHL_KIENHANG_GUI_CTIET YHL_KIENHANG_GUI_CTIET);
    }
}

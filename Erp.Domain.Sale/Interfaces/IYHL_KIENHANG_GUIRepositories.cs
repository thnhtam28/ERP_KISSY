using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IYHL_KIENHANG_GUIRepositories
    {
        /// <summary>
        /// Get all YHL_KIENHANG_GUI
        /// </summary>
        /// <returns>IYHL_KIENHANG_GUI list</returns>
        /// 
        IQueryable<YHL_KIENHANG_GUI> GetAllYHL_KIENHANG_GUI();
        YHL_KIENHANG_GUI GetKIENHANG_GUIByID(int KIENHANG_GUI_ID);

        
        /// <summary>
        /// Insert YHL_KIENHANG_GUI into database
        /// </summary>
        /// <param name="YHL_KIENHANG_GUI">Object infomation</param>
        void InsertYHL_KIENHANG_GUI(YHL_KIENHANG_GUI YHL_KIENHANG_GUI);

        /// <summary>
        /// Delete YHL_KIENHANG_GUI with specific YHL_KIENHANG_GUI
        /// </summary>
        /// <param name="YHL_KIENHANG_GUI">YHL_KIENHANG_GUI KIENHANG_GUI_ID</param>
        void DeleteYHL_KIENHANG_GUI(int KIENHANG_GUI_ID);

        /// <summary>
        /// Delete a YHL_KIENHANG_GUI with its KIENHANG_GUI_ID and Update IsDeleted IF that YHL_KIENHANG_GUI has relationship with others
        /// </summary>
        /// <param name="KIENHANG_GUI_ID">Id of YHL_KIENHANG_GUI</param>
        void DeleteYHL_KIENHANG_GUIRs(int KIENHANG_GUI_ID);

        /// <summary>
        /// Update YHL_KIENHANG_GUI into database
        /// </summary>
        /// <param name="YHL_KIENHANG_GUI">YHL_KIENHANG_GUI object</param>
        void UpdateYHL_KIENHANG_GUI(YHL_KIENHANG_GUI YHL_KIENHANG_GUI);
    }
}



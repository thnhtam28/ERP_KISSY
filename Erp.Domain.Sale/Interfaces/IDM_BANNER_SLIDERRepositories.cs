using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;
namespace Erp.Domain.Sale.Interfaces
{
    public interface IDM_BANNER_SLIDERRepositories
    {
        /// <summary>
        /// Get all DM_BANNER_SLIDER
        /// </summary>
        /// <returns>DM_BANNER_SLIDER list</returns>
        /// 
        IQueryable<DM_BANNER_SLIDER> GetAllDM_BANNER_SLIDER();
        DM_BANNER_SLIDER GetDM_BANNER_SLIDERByBANNER_SLIDER_ID(int BANNER_SLIDER_ID);

        /// <summary>
        /// Insert DM_BANNER_SLIDER into database
        /// </summary>
        /// <param name="DM_BANNER_SLIDER">Object infomation</param>
        void InsertDM_BANNER_SLIDER(DM_BANNER_SLIDER DM_BANNER_SLIDER);

        /// <summary>
        /// Delete DM_BANNER_SLIDER with specific BANNER_SLIDER_ID
        /// </summary>
        /// <param name="BANNER_SLIDER_ID">DM_BANNER_SLIDER BANNER_SLIDER_ID</param>
        void DeleteDM_BANNER_SLIDER(int BANNER_SLIDER_ID);

        /// <summary>
        /// Delete a DM_BANNER_SLIDER with its BANNER_SLIDER_ID and Update IsDeleted IF that DM_BANNER_SLIDER has relationship with others
        /// </summary>
        /// <param name="BANNER_SLIDER_ID">Id of DM_BANNER_SLIDER</param>
        void DeleteDM_BANNER_SLIDERRs(int BANNER_SLIDER_ID);

        /// <summary>
        /// Update DM_BANNER_SLIDER into database
        /// </summary>
        /// <param name="DM_BANNER_SLIDER">DM_BANNER_SLIDER object</param>
        void UpdateDM_BANNER_SLIDER(DM_BANNER_SLIDER DM_BANNER_SLIDER);
    }
   
}

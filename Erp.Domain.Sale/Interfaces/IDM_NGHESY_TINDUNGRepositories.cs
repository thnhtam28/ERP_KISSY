using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IDM_NGHESY_TINDUNGRepositories
    {
          /// <summary>
        /// Get all DM_NGHESY_TINDUNG
        /// </summary>
        /// <returns>DM_NGHESY_TINDUNG list</returns>
        /// 
        IQueryable<DM_NGHESY_TINDUNG> GetAllDM_NGHESY_TINDUNG();
        DM_NGHESY_TINDUNG GetDM_NGHESY_TINDUNGByNGHESY_TINDUNG_ID(int NGHESY_TINDUNG_ID);

        /// <summary>
        /// Insert DM_NGHESY_TINDUNG into database
        /// </summary>
        /// <param name="DM_NGHESY_TINDUNG">Object infomation</param>
        void InsertDM_NGHESY_TINDUNG(DM_NGHESY_TINDUNG DM_NGHESY_TINDUNG);

        /// <summary>
        /// Delete DM_NGHESY_TINDUNG with specific NGHESY_TINDUNG_ID
        /// </summary>
        /// <param name="NGHESY_TINDUNG_ID">DM_NGHESY_TINDUNG NGHESY_TINDUNG_ID</param>
        void DeleteDM_NGHESY_TINDUNG(int NGHESY_TINDUNG_ID);

        /// <summary>
        /// Delete a DM_NGHESY_TINDUNG with its NGHESY_TINDUNG_ID and Update IsDeleted IF that NGHESY_TINDUNG_YHL has relationship with others
        /// </summary>
        /// <param name="NGHESY_TINDUNG_ID">Id of NGHESY_TINDUNG_YHL</param>
        void DeleteDM_NGHESY_TINDUNGRs(int NGHESY_TINDUNG_ID);

        /// <summary>
        /// Update DM_NGHESY_TINDUNG into database
        /// </summary>
        /// <param name="DM_NGHESY_TINDUNG">DM_NGHESY_TINDUNG object</param>
        void UpdateDM_NGHESY_TINDUNG(DM_NGHESY_TINDUNG DM_NGHESY_TINDUNG);
    }
    
}

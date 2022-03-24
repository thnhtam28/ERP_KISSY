using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;


namespace Erp.Domain.Sale.Interfaces
{
    public interface IDM_NHOMSANPHAMRepositories
    {
        /// <summary>
        /// Get all DM_NHOMSANPHAM
        /// </summary>
        /// <returns>DM_NHOMSANPHAM list</returns>
        /// 
        IQueryable<DM_NHOMSANPHAM> GetAllDM_NHOMSANPHAM();
        IQueryable<DM_NHOMSANPHAM> GetAllDM_NHOMSANPHAMByNHOM_CHA(int? NHOM_CHA);
        DM_NHOMSANPHAM GetDM_NHOMSANPHAMByNHOMSANPHAM_ID(int NHOMSANPHAM_ID);
        DM_NHOMSANPHAM GetDM_NHOMSANPHAMByTEN_NHOMSANPHAM(string TEN_NHOMSANPHAM);

        /// <summary>
        /// Insert DM_NHOMSANPHAM into database
        /// </summary>
        /// <param name="DM_NHOMSANPHAM">Object infomation</param>
        void InsertDM_NHOMSANPHAM(DM_NHOMSANPHAM DM_NHOMSANPHAM);

        /// <summary>
        /// Delete DM_NHOMSANPHAM with specific NHOMSANPHAM_ID
        /// </summary>
        /// <param name="NHOMSANPHAM_ID">DM_NHOMSANPHAM NHOMSANPHAM_ID</param>
        void DeleteDM_NHOMSANPHAM(int NHOMSANPHAM_ID);

        /// <summary>
        /// Delete a DM_NHOMSANPHAM with its NHOMSANPHAM_ID and Update IsDeleted IF that DM_NHOMSANPHAM has relationship with others
        /// </summary>
        /// <param name="NHOMSANPHAM_ID">Id of DM_NHOMSANPHAM</param>
        void DeleteDM_NHOMSANPHAMRs(int NHOMSANPHAM_ID);

        /// <summary>
        /// Update DM_NHOMSANPHAM into database
        /// </summary>
        /// <param name="DM_NHOMSANPHAM">DM_NHOMSANPHAM object</param>
        void UpdateDM_NHOMSANPHAM(DM_NHOMSANPHAM DM_NHOMSANPHAM);
    }
}

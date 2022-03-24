using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IDM_LOAISANPHAMRepositories
    {
        /// <summary>
        /// Get all DM_LOAISANPHAM
        /// </summary>
        /// <returns>DM_LOAISANPHAM list</returns>
        /// 
        IQueryable<DM_LOAISANPHAM> GetAllDM_LOAISANPHAM();
        DM_LOAISANPHAM GetDM_LOAISANPHAMByLOAISANPHAM_ID(int LOAISANPHAM_ID);
        DM_LOAISANPHAM GetDM_LOAISANPHAMByTEN_LOAISANPHAM(string TEN_LOAISANPHAM);

        /// <summary>
        /// Insert DM_LOAISANPHAM into database
        /// </summary>
        /// <param name="DM_LOAISANPHAM">Object infomation</param>
        void InsertDM_LOAISANPHAM(DM_LOAISANPHAM DM_LOAISANPHAM);

        /// <summary>
        /// Delete DM_LOAISANPHAM with specific LOAISANPHAM_ID
        /// </summary>
        /// <param name="LOAISANPHAM_ID">DM_LOAISANPHAM LOAISANPHAM_ID</param>
        void DeleteDM_LOAISANPHAM(int LOAISANPHAM_ID);

        /// <summary>
        /// Delete a DM_LOAISANPHAM with its LOAISANPHAM_ID and Update IsDeleted IF that DM_LOAISANPHAM has relationship with others
        /// </summary>
        /// <param name="NHOMSANPHAM_ID">Id of DM_LOAISANPHAM</param>
        void DeleteDM_LOAISANPHAMRs(int LOAISANPHAM_ID);

        /// <summary>
        /// Update DM_LOAISANPHAM into database
        /// </summary>
        /// <param name="DM_NHOMSANPHAM">DM_LOAISANPHAM object</param>
        void UpdateDM_LOAISANPHAM(DM_LOAISANPHAM DM_LOAISANPHAM);
    }
}

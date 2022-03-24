using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class DM_LOAISANPHAMRepositories : GenericRepository<ErpSaleDbContext, DM_LOAISANPHAM>, IDM_LOAISANPHAMRepositories
    {
        public DM_LOAISANPHAMRepositories(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all DM_LOAISANPHAM
        /// </summary>
        /// <returns>DM_LOAISANPHAM list</returns>
        /// 
        public IQueryable<DM_LOAISANPHAM> GetAllDM_LOAISANPHAM()
        {
            return Context.DM_LOAISANPHAM.Where(item => (item.IsDeleted == null || item.IsDeleted == false) );
        }

        public DM_LOAISANPHAM GetDM_LOAISANPHAMByLOAISANPHAM_ID(int LOAISANPHAM_ID)
        {
            return Context.DM_LOAISANPHAM.SingleOrDefault(item => item.LOAISANPHAM_ID == LOAISANPHAM_ID && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public DM_LOAISANPHAM GetDM_LOAISANPHAMByTEN_LOAISANPHAM(string TEN_LOAISANPHAM)
        {
            return Context.DM_LOAISANPHAM.FirstOrDefault(x => x.TEN_LOAISANPHAM.Equals(TEN_LOAISANPHAM) && (x.IsDeleted == null || x.IsDeleted == false));
        }

        /// <summary>
        /// Insert DM_LOAISANPHAM into database
        /// </summary>
        /// <param name="DM_LOAISANPHAM">Object infomation</param>
        public void InsertDM_LOAISANPHAM(DM_LOAISANPHAM DM_LOAISANPHAM)
        {
            Context.DM_LOAISANPHAM.Add(DM_LOAISANPHAM);
            Context.Entry(DM_LOAISANPHAM).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete DM_LOAISANPHAM with specific LOAISANPHAM_ID
        /// </summary>
        /// <param name="LOAISANPHAM_ID">DM_LOAISANPHAM LOAISANPHAM_ID</param>
        public void DeleteDM_LOAISANPHAM(int LOAISANPHAM_ID)
        {
            DM_LOAISANPHAM deletedDM_LOAISANPHAM = GetDM_LOAISANPHAMByLOAISANPHAM_ID(LOAISANPHAM_ID);
            Context.DM_LOAISANPHAM.Remove(deletedDM_LOAISANPHAM);
            Context.Entry(deletedDM_LOAISANPHAM).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete a DM_LOAISANPHAM with its LOAISANPHAM_ID and Update IsDeleted IF that DM_LOAISANPHAM has relationship with others
        /// </summary>
        /// <param name="NHOMSANPHAM_ID">Id of DM_LOAISANPHAM</param>
        public void DeleteDM_LOAISANPHAMRs(int LOAISANPHAM_ID)
        {
            DM_LOAISANPHAM deleteDM_LOAISANPHAMRs = GetDM_LOAISANPHAMByLOAISANPHAM_ID(LOAISANPHAM_ID);
            deleteDM_LOAISANPHAMRs.IsDeleted = true;
            UpdateDM_LOAISANPHAM(deleteDM_LOAISANPHAMRs);
        }

        /// <summary>
        /// Update DM_LOAISANPHAM into database
        /// </summary>
        /// <param name="DM_NHOMSANPHAM">DM_LOAISANPHAM object</param>
        public void UpdateDM_LOAISANPHAM(DM_LOAISANPHAM DM_LOAISANPHAM)
        {
            Context.Entry(DM_LOAISANPHAM).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}

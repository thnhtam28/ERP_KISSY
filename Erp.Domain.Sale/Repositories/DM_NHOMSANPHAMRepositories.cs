using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class DM_NHOMSANPHAMRepositories : GenericRepository<ErpSaleDbContext, DM_NHOMSANPHAM>, IDM_NHOMSANPHAMRepositories
    {
        public DM_NHOMSANPHAMRepositories(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all DM_NHOMSANPHAM
        /// </summary>
        /// <returns>DM_NHOMSANPHAM list</returns>
        /// 
        public IQueryable<DM_NHOMSANPHAM> GetAllDM_NHOMSANPHAM()
        {
            return Context.DM_NHOMSANPHAM.Where(item => (item.IsDeleted == null || item.IsDeleted == false) && item.IS_SHOW==1);
        }

        public IQueryable<DM_NHOMSANPHAM> GetAllDM_NHOMSANPHAMByNHOM_CHA(int? NHOM_CHA)
        {
            return Context.DM_NHOMSANPHAM.Where(item => item.NHOM_CHA == NHOM_CHA && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public DM_NHOMSANPHAM GetDM_NHOMSANPHAMByNHOMSANPHAM_ID(int NHOMSANPHAM_ID)
        {
            return Context.DM_NHOMSANPHAM.SingleOrDefault(item => item.NHOMSANPHAM_ID == NHOMSANPHAM_ID && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public DM_NHOMSANPHAM GetDM_NHOMSANPHAMByTEN_NHOMSANPHAM(string TEN_NHOMSANPHAM)
        {
            return Context.DM_NHOMSANPHAM.SingleOrDefault(x => x.TEN_NHOMSANPHAM.Equals(TEN_NHOMSANPHAM) && (x.IsDeleted == null || x.IsDeleted == false));
        }

        /// <summary>
        /// Insert DM_NHOMSANPHAM into database
        /// </summary>
        /// <param name="DM_NHOMSANPHAM">Object infomation</param>
        public void InsertDM_NHOMSANPHAM(DM_NHOMSANPHAM DM_NHOMSANPHAM)
        {
            Context.DM_NHOMSANPHAM.Add(DM_NHOMSANPHAM);
            Context.Entry(DM_NHOMSANPHAM).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete DM_NHOMSANPHAM with specific NHOMSANPHAM_ID
        /// </summary>
        /// <param name="NHOMSANPHAM_ID">DM_NHOMSANPHAM NHOMSANPHAM_ID</param>
        public void DeleteDM_NHOMSANPHAM(int NHOMSANPHAM_ID)
        {
            DM_NHOMSANPHAM deletedDM_NHOMSANPHAM = GetDM_NHOMSANPHAMByNHOMSANPHAM_ID(NHOMSANPHAM_ID);
            Context.DM_NHOMSANPHAM.Remove(deletedDM_NHOMSANPHAM);
            Context.Entry(deletedDM_NHOMSANPHAM).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete a DM_NHOMSANPHAM with its NHOMSANPHAM_ID and Update IsDeleted IF that DM_NHOMSANPHAM has relationship with others
        /// </summary>
        /// <param name="NHOMSANPHAM_ID">Id of DM_NHOMSANPHAM</param>
        public void DeleteDM_NHOMSANPHAMRs(int NHOMSANPHAM_ID)
        {
            DM_NHOMSANPHAM deleteDM_NHOMSANPHAMRs = GetDM_NHOMSANPHAMByNHOMSANPHAM_ID(NHOMSANPHAM_ID);
            deleteDM_NHOMSANPHAMRs.IsDeleted = true;
            UpdateDM_NHOMSANPHAM(deleteDM_NHOMSANPHAMRs);
        }

        /// <summary>
        /// Update DM_NHOMSANPHAM into database
        /// </summary>
        /// <param name="DM_NHOMSANPHAM">DM_NHOMSANPHAM object</param>
        public void UpdateDM_NHOMSANPHAM(DM_NHOMSANPHAM DM_NHOMSANPHAM)
        {
            Context.Entry(DM_NHOMSANPHAM).State = EntityState.Modified;
            Context.SaveChanges();
        }

    }
}

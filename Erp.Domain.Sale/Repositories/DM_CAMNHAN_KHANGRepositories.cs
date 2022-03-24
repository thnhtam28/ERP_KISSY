using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class DM_CAMNHAN_KHANGRepositories : GenericRepository<ErpSaleDbContext, DM_CAMNHAN_KHANG>, IDM_CAMNHAN_KHANGRepositories
    {
        public DM_CAMNHAN_KHANGRepositories(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all DM_CAMNHAN_KHANG
        /// </summary>
        /// <returns>DM_CAMNHAN_KHANG list</returns>
        /// 
        public IQueryable<DM_CAMNHAN_KHANG> GetAllDM_CAMNHAN_KHANG()
        {
            return Context.DM_CAMNHAN_KHANG.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        public DM_CAMNHAN_KHANG GetDM_CAMNHAN_KHANGByCAMNHAN_KHANG_ID(int CAMNHAN_KHANG_ID)
        {
            return Context.DM_CAMNHAN_KHANG.SingleOrDefault(item => item.CAMNHAN_KHANG_ID == CAMNHAN_KHANG_ID && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert CAMNHAN_KHANG into database
        /// </summary>
        /// <param name="DM_CAMNHAN_KHANG">Object infomation</param>
        public void InsertDM_CAMNHAN_KHANG(DM_CAMNHAN_KHANG DM_CAMNHAN_KHANG)
        {
            Context.DM_CAMNHAN_KHANG.Add(DM_CAMNHAN_KHANG);
            Context.Entry(DM_CAMNHAN_KHANG).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete DM_CAMNHAN_KHANG with specific CAMNHAN_KHANG_ID
        /// </summary>
        /// <param name="CAMNHAN_KHANG_ID">DM_CAMNHAN_KHANG CAMNHAN_KHANG_ID</param>
        public void DeleteDM_CAMNHAN_KHANG(int CAMNHAN_KHANG_ID)
        {
            DM_CAMNHAN_KHANG deletedDM_CAMNHAN_KHANG = GetDM_CAMNHAN_KHANGByCAMNHAN_KHANG_ID(CAMNHAN_KHANG_ID);
            Context.DM_CAMNHAN_KHANG.Remove(deletedDM_CAMNHAN_KHANG);
            Context.Entry(deletedDM_CAMNHAN_KHANG).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete a DM_CAMNHAN_KHANG with its CAMNHAN_KHANG_ID and Update IsDeleted IF that CAMNHAN_KHANG_YHL has relationship with others
        /// </summary>
        /// <param name="CAMNHAN_KHANG_ID">Id of DM_CAMNHAN_KHANG</param>
        public void DeleteDM_CAMNHAN_KHANGRs(int CAMNHAN_KHANG_ID)
        {
            DM_CAMNHAN_KHANG deleteDM_CAMNHAN_KHANGRs = GetDM_CAMNHAN_KHANGByCAMNHAN_KHANG_ID(CAMNHAN_KHANG_ID);
            deleteDM_CAMNHAN_KHANGRs.IsDeleted = true;
            UpdateDM_CAMNHAN_KHANG(deleteDM_CAMNHAN_KHANGRs);
        }

        /// <summary>
        /// Update DM_CAMNHAN_KHANG into database
        /// </summary>
        /// <param name="DM_CAMNHAN_KHANG">DM_CAMNHAN_KHANG  object</param>
        public void UpdateDM_CAMNHAN_KHANG(DM_CAMNHAN_KHANG DM_CAMNHAN_KHANG)
        {
            Context.Entry(DM_CAMNHAN_KHANG).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}

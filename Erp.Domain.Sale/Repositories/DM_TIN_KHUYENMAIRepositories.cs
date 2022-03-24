using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class DM_TIN_KHUYENMAIRepositories : GenericRepository<ErpSaleDbContext, DM_TIN_KHUYENMAI>, IDM_TIN_KHUYENMAIRepositories
    {
        public DM_TIN_KHUYENMAIRepositories(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all DM_TIN_KHUYENMAI
        /// </summary>
        /// <returns>DM_TIN_KHUYENMAI list</returns>
        /// 
        public IQueryable<DM_TIN_KHUYENMAI> GetAllDM_TIN_KHUYENMAI()
        {
            return Context.DM_TIN_KHUYENMAI.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        public DM_TIN_KHUYENMAI GetDM_TIN_KHUYENMAIByTIN_KHUYENMAI_ID(int TIN_KHUYENMAI_ID)
        {
            return Context.DM_TIN_KHUYENMAI.SingleOrDefault(item => item.TIN_KHUYENMAI_ID == TIN_KHUYENMAI_ID && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert TIN_KHUYENMAI into database
        /// </summary>
        /// <param name="DM_TIN_KHUYENMAI">Object infomation</param>
        public void InsertDM_TIN_KHUYENMAI(DM_TIN_KHUYENMAI DM_TIN_KHUYENMAI)
        {
            Context.DM_TIN_KHUYENMAI.Add(DM_TIN_KHUYENMAI);
            Context.Entry(DM_TIN_KHUYENMAI).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete DM_TIN_KHUYENMAI with specific TIN_KHUYENMAI_ID
        /// </summary>
        /// <param name="TIN_KHUYENMAI_ID">DM_TIN_KHUYENMAI TIN_KHUYENMAI_ID</param>
        public void DeleteDM_TIN_KHUYENMAI(int TIN_KHUYENMAI_ID)
        {
            DM_TIN_KHUYENMAI deletedDM_TIN_KHUYENMAI = GetDM_TIN_KHUYENMAIByTIN_KHUYENMAI_ID(TIN_KHUYENMAI_ID);
            Context.DM_TIN_KHUYENMAI.Remove(deletedDM_TIN_KHUYENMAI);
            Context.Entry(deletedDM_TIN_KHUYENMAI).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete a DM_TIN_KHUYENMAI with its TIN_KHUYENMAI_ID and Update IsDeleted IF that DM_TIN_KHUYENMAI has relationship with others
        /// </summary>
        /// <param name="TIN_KHUYENMAI_ID">Id of DM_TIN_KHUYENMAI</param>
        public void DeleteDM_TIN_KHUYENMAIRs(int TIN_KHUYENMAI_ID)
        {
            DM_TIN_KHUYENMAI deleteDM_TIN_KHUYENMAIRs = GetDM_TIN_KHUYENMAIByTIN_KHUYENMAI_ID(TIN_KHUYENMAI_ID);
            deleteDM_TIN_KHUYENMAIRs.IsDeleted = true;
            UpdateDM_TIN_KHUYENMAI(deleteDM_TIN_KHUYENMAIRs);
        }

        /// <summary>
        /// Update DM_TIN_KHUYENMAI into database
        /// </summary>
        /// <param name="DM_TIN_KHUYENMAI">DM_TIN_KHUYENMAI object</param>
        public void UpdateDM_TIN_KHUYENMAI(DM_TIN_KHUYENMAI DM_TIN_KHUYENMAI)
        {
            Context.Entry(DM_TIN_KHUYENMAI).State = EntityState.Modified;
            Context.SaveChanges();
        }

    }
}

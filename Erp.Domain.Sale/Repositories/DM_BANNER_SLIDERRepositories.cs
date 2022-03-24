using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class DM_BANNER_SLIDERRepositories : GenericRepository<ErpSaleDbContext, DM_BANNER_SLIDER>, IDM_BANNER_SLIDERRepositories
    {
        public DM_BANNER_SLIDERRepositories(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all DM_BANNER_SLIDER
        /// </summary>
        /// <returns>DM_BANNER_SLIDER list</returns>
        /// 
        public IQueryable<DM_BANNER_SLIDER> GetAllDM_BANNER_SLIDER()
        {
            return Context.DM_BANNER_SLIDER.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        public DM_BANNER_SLIDER GetDM_BANNER_SLIDERByBANNER_SLIDER_ID(int BANNER_SLIDER_ID)
        {
            return Context.DM_BANNER_SLIDER.SingleOrDefault(item => item.BANNER_SLIDER_ID == BANNER_SLIDER_ID && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert DM_BANNER_SLIDER into database
        /// </summary>
        /// <param name="DM_BANNER_SLIDER">Object infomation</param>
        public void InsertDM_BANNER_SLIDER(DM_BANNER_SLIDER DM_BANNER_SLIDER)
        {
            Context.DM_BANNER_SLIDER.Add(DM_BANNER_SLIDER);
            Context.Entry(DM_BANNER_SLIDER).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete DM_BANNER_SLIDER with specific BANNER_SLIDER_ID
        /// </summary>
        /// <param name="BANNER_SLIDER_ID">DM_BANNER_SLIDER BANNER_SLIDER_ID</param>
        public void DeleteDM_BANNER_SLIDER(int BANNER_SLIDER_ID)
        {
            DM_BANNER_SLIDER deletedDM_BANNER_SLIDER = GetDM_BANNER_SLIDERByBANNER_SLIDER_ID(BANNER_SLIDER_ID);
            Context.DM_BANNER_SLIDER.Remove(deletedDM_BANNER_SLIDER);
            Context.Entry(deletedDM_BANNER_SLIDER).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete a DM_BANNER_SLIDER with its BANNER_SLIDER_ID and Update IsDeleted IF that DM_BANNER_SLIDER has relationship with others
        /// </summary>
        /// <param name="BEST_SELLER_ID">Id of DM_BANNER_SLIDER</param>
        public void DeleteDM_BANNER_SLIDERRs(int BANNER_SLIDER_ID)
        {
            DM_BANNER_SLIDER deleteDM_BANNER_SLIDERRs = GetDM_BANNER_SLIDERByBANNER_SLIDER_ID(BANNER_SLIDER_ID);
            deleteDM_BANNER_SLIDERRs.IsDeleted = true;
            UpdateDM_BANNER_SLIDER(deleteDM_BANNER_SLIDERRs);
        }

        /// <summary>
        /// Update DM_BANNER_SLIDER into database
        /// </summary>
        /// <param name="DM_BEST_SELLER">DM_BANNER_SLIDER object</param>
        public void UpdateDM_BANNER_SLIDER(DM_BANNER_SLIDER DM_BANNER_SLIDER)
        {
            Context.Entry(DM_BANNER_SLIDER).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}

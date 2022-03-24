using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class DM_BEST_SELLERRepositories : GenericRepository<ErpSaleDbContext, DM_BEST_SELLER>, IDM_BEST_SELLERRepositories
    {
        public DM_BEST_SELLERRepositories(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all DM_BEST_SELLER
        /// </summary>
        /// <returns>DM_BEST_SELLER list</returns>
        /// 
        public IQueryable<DM_BEST_SELLER> GetAllDM_BEST_SELLER()
        {
            return Context.DM_BEST_SELLER.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        public DM_BEST_SELLER GetDM_BEST_SELLERByBEST_SELLER_ID(int BEST_SELLER_ID)
        {
            return Context.DM_BEST_SELLER.SingleOrDefault(item => item.BEST_SELLER_ID == BEST_SELLER_ID && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert DM_BEST_SELLER into database
        /// </summary>
        /// <param name="DM_BEST_SELLER">Object infomation</param>
        public void InsertDM_BEST_SELLER(DM_BEST_SELLER DM_BEST_SELLER)
        {
            Context.DM_BEST_SELLER.Add(DM_BEST_SELLER);
            Context.Entry(DM_BEST_SELLER).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete DM_BEST_SELLER with specific BEST_SELLER_ID
        /// </summary>
        /// <param name="BEST_SELLER_ID">DM_BEST_SELLER BEST_SELLER_ID</param>
        public void DeleteDM_BEST_SELLER(int BEST_SELLER_ID)
        {
            DM_BEST_SELLER deletedDM_BEST_SELLER = GetDM_BEST_SELLERByBEST_SELLER_ID(BEST_SELLER_ID);
            Context.DM_BEST_SELLER.Remove(deletedDM_BEST_SELLER);
            Context.Entry(deletedDM_BEST_SELLER).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete a DM_BEST_SELLER with its BEST_SELLER_ID and Update IsDeleted IF that DM_BEST_SELLER has relationship with others
        /// </summary>
        /// <param name="BEST_SELLER_ID">Id of DM_BEST_SELLER</param>
        public void DeleteDM_BEST_SELLERRs(int BEST_SELLER_ID)
        {
            DM_BEST_SELLER deleteDM_BEST_SELLERRs = GetDM_BEST_SELLERByBEST_SELLER_ID(BEST_SELLER_ID);
            deleteDM_BEST_SELLERRs.IsDeleted = true;
            UpdateDM_BEST_SELLER(deleteDM_BEST_SELLERRs);
        }

        /// <summary>
        /// Update DM_BEST_SELLER into database
        /// </summary>
        /// <param name="DM_BEST_SELLER">DM_BEST_SELLER object</param>
        public void UpdateDM_BEST_SELLER(DM_BEST_SELLER DM_BEST_SELLER)
        {
            Context.Entry(DM_BEST_SELLER).State = EntityState.Modified;
            Context.SaveChanges();
        }

    }
}

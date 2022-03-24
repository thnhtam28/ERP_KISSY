using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class YHL_KIENHANG_TRARepositories : GenericRepository<ErpSaleDbContext, YHL_KIENHANG_TRA>, IYHL_KIENHANG_TRARepositories
    {
        public YHL_KIENHANG_TRARepositories(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all YHL_KIENHANG_TRA
        /// </summary>
        /// <returns>YHL_KIENHANG_TRA list</returns>
        /// 
        public IQueryable<YHL_KIENHANG_TRA> GetAllYHL_KIENHANG_TRA()
        {
            return Context.YHL_KIENHANG_TRA.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        public YHL_KIENHANG_TRA GetKIENHANG_TRAByID(int KIENHANG_TRA_ID)
        {
            return Context.YHL_KIENHANG_TRA.SingleOrDefault(item => item.KIENHANG_TRA_ID == KIENHANG_TRA_ID && (item.IsDeleted == null || item.IsDeleted == false));
        }


        /// <summary>
        /// Insert YHL_KIENHANG_TRA into database
        /// </summary>
        /// <param name="YHL_KIENHANG_TRA">Object infomation</param>
        public void InsertYHL_KIENHANG_TRA(YHL_KIENHANG_TRA YHL_KIENHANG_TRA)
        {
            Context.YHL_KIENHANG_TRA.Add(YHL_KIENHANG_TRA);
            Context.Entry(YHL_KIENHANG_TRA).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete YHL_KIENHANG_TRA with specific KIENHANG_TRA_ID
        /// </summary>
        /// <param name="KIENHANG_TRA_ID">YHL_KIENHANG_TRA KIENHANG_TRA_ID</param>
        public void DeleteYHL_KIENHANG_TRA(int KIENHANG_TRA_ID)
        {
            YHL_KIENHANG_TRA deletedYHL_KIENHANG_TRA = GetKIENHANG_TRAByID(KIENHANG_TRA_ID);
            Context.YHL_KIENHANG_TRA.Remove(deletedYHL_KIENHANG_TRA);
            Context.Entry(deletedYHL_KIENHANG_TRA).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete a YHL_KIENHANG_TRA with its KIENHANG_TRA_ID and Update IsDeleted IF that YHL_KIENHANG_TRA has relationship with others
        /// </summary>
        /// <param name="YHL_KIENHANG_TRA">Id of YHL_KIENHANG_TRA</param>
        public void DeleteYHL_KIENHANG_TRARs(int BAIVE_YHL_ID)
        {
            YHL_KIENHANG_TRA deleteYHL_KIENHANG_TRARs = GetKIENHANG_TRAByID(BAIVE_YHL_ID);
            deleteYHL_KIENHANG_TRARs.IsDeleted = true;
            UpdateYHL_KIENHANG_TRA(deleteYHL_KIENHANG_TRARs);
        }

        /// <summary>
        /// Update YHL_KIENHANG_TRA into database
        /// </summary>
        /// <param name="YHL_KIENHANG_TRA">YHL_KIENHANG_TRA object</param>
        public void UpdateYHL_KIENHANG_TRA(YHL_KIENHANG_TRA YHL_KIENHANG_TRA)
        {
            Context.Entry(YHL_KIENHANG_TRA).State = EntityState.Modified;
            Context.SaveChanges();
        }

    }
}

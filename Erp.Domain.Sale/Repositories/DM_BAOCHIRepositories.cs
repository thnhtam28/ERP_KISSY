using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Data.Entity;


namespace Erp.Domain.Sale.Repositories
{
    public class DM_BAOCHIRepositories : GenericRepository<ErpSaleDbContext, DM_BAOCHI>, IDM_BAOCHIRepositories
    {
         public DM_BAOCHIRepositories(ErpSaleDbContext context)
            : base(context)
        {

        }

         /// <summary>
         /// Get all DM_BAOCHI
         /// </summary>
         /// <returns>DM_BAOCHI list</returns>
         /// 
         public IQueryable<DM_BAOCHI> GetAllDM_BAOCHI()
         {
             return Context.DM_BAOCHI.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
         }

         public DM_BAOCHI GetDM_BAOCHIByBAOCHI_ID(int BAOCHI_ID)
         {
             return Context.DM_BAOCHI.SingleOrDefault(item => item.BAOCHI_ID == BAOCHI_ID && (item.IsDeleted == null || item.IsDeleted == false));
         }

        

         /// <summary>
         /// Insert DM_BAOCHI into database
         /// </summary>
         /// <param name="DM_BAOCHI">Object infomation</param>
         public void InsertDM_BAOCHI(DM_BAOCHI DM_BAOCHI)
         {
             Context.DM_BAOCHI.Add(DM_BAOCHI);
             Context.Entry(DM_BAOCHI).State = EntityState.Added;
             Context.SaveChanges();
         }

         /// <summary>
         /// Delete DM_BAOCHI with specific BAOCHI_ID
         /// </summary>
         /// <param name="BAOCHI_ID">DM_BAOCHI BAOCHI_ID</param>
         public void DeleteDM_BAOCHI(int BAOCHI_ID)
         {
             DM_BAOCHI deletedDM_BAOCHI = GetDM_BAOCHIByBAOCHI_ID(BAOCHI_ID);
             Context.DM_BAOCHI.Remove(deletedDM_BAOCHI);
             Context.Entry(deletedDM_BAOCHI).State = EntityState.Deleted;
             Context.SaveChanges();
         }

         /// <summary>
         /// Delete a DM_BAOCHI with its BAOCHI_ID and Update IsDeleted IF that DM_BAOCHI has relationship with others
         /// </summary>
         /// <param name="BAOCHI_ID">Id of DM_BAOCHI</param>
         public void DeleteDM_BAOCHIRs(int BAOCHI_ID)
         {
             DM_BAOCHI deleteDM_BAOCHIRs = GetDM_BAOCHIByBAOCHI_ID(BAOCHI_ID);
             deleteDM_BAOCHIRs.IsDeleted = true;
             UpdateDM_BAOCHI(deleteDM_BAOCHIRs);
         }

         /// <summary>
         /// Update DM_BAOCHI into database
         /// </summary>
         /// <param name="DM_BAOCHI">DM_BAOCHI object</param>
         public void UpdateDM_BAOCHI(DM_BAOCHI DM_BAOCHI)
         {
             Context.Entry(DM_BAOCHI).State = EntityState.Modified;
             Context.SaveChanges();
         }
    }
}

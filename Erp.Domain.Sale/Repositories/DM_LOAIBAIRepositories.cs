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
    public class DM_LOAIBAIRepositories : GenericRepository<ErpSaleDbContext, DM_LOAIBAI>, IDM_LOAIBAIRepositories
    {
            public DM_LOAIBAIRepositories(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all DM_LOAIBAI
        /// </summary>
            /// <returns>DM_LOAIBAI list</returns>
        /// 
            public IQueryable<DM_LOAIBAI> GetAllDM_LOAIBAI()
        {
            return Context.DM_LOAIBAI.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

            public DM_LOAIBAI GetDM_LOAIBAIByLOAIBAI_ID(int LOAIBAI_ID)
        {
            return Context.DM_LOAIBAI.SingleOrDefault(item => item.LOAIBAI_ID == LOAIBAI_ID && (item.IsDeleted == null || item.IsDeleted == false));
        }

            public DM_LOAIBAI GetDM_LOAIBAIByCODE_LOAIBAI(string CODE_LOAIBAI)
        {
            return Context.DM_LOAIBAI.FirstOrDefault(x => x.CODE_LOAIBAI.Equals(CODE_LOAIBAI) && (x.IsDeleted == null || x.IsDeleted == false));
        }

        /// <summary>
            /// Insert DM_LOAIBAI into database
        /// </summary>
            /// <param name="DM_LOAIBAI">Object infomation</param>
            public void InsertDM_LOAIBAI(DM_LOAIBAI DM_LOAIBAI)
        {
            Context.DM_LOAIBAI.Add(DM_LOAIBAI);
            Context.Entry(DM_LOAIBAI).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
            /// Delete DM_LOAIBAI with specific LOAIBAI_ID
        /// </summary>
            /// <param name="LOAIBAI_ID">DM_LOAIBAI LOAIBAI_ID</param>
            public void DeleteDM_LOAIBAI(int LOAIBAI_ID)
        {
            DM_LOAIBAI deletedDM_LOAIBAI = GetDM_LOAIBAIByLOAIBAI_ID(LOAIBAI_ID);
            Context.DM_LOAIBAI.Remove(deletedDM_LOAIBAI);
            Context.Entry(deletedDM_LOAIBAI).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        /// <summary>
            /// Delete a DM_LOAIBAI with its LOAIBAI_ID and Update IsDeleted IF that DM_LOAIBAI has relationship with others
        /// </summary>
            /// <param name="LOAIBAI_ID">Id of DM_LOAIBAI</param>
            public void DeleteDM_LOAIBAIRs(int LOAIBAI_ID)
        {
            DM_LOAIBAI deleteDM_LOAIBAIRs = GetDM_LOAIBAIByLOAIBAI_ID(LOAIBAI_ID);
            deleteDM_LOAIBAIRs.IsDeleted = true;
            UpdateDM_LOAIBAI(deleteDM_LOAIBAIRs);
        }

        /// <summary>
            /// Update DM_LOAIBAI into database
        /// </summary>
            /// <param name="DM_LOAIBAI">DM_LOAIBAI object</param>
            public void UpdateDM_LOAIBAI(DM_LOAIBAI DM_LOAIBAI)
        {
            Context.Entry(DM_LOAIBAI).State = EntityState.Modified;
            Context.SaveChanges();
        }
    
    }
}

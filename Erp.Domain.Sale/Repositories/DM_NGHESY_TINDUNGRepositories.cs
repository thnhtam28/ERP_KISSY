using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    class DM_NGHESY_TINDUNGRepositories : GenericRepository<ErpSaleDbContext, DM_NGHESY_TINDUNG>, IDM_NGHESY_TINDUNGRepositories
   
    {
        public DM_NGHESY_TINDUNGRepositories(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all DM_NGHESY_TINDUNG
        /// </summary>
        /// <returns>DM_NGHESY_TINDUNG list</returns>
        /// 
        public IQueryable<DM_NGHESY_TINDUNG> GetAllDM_NGHESY_TINDUNG()
        {
            return Context.DM_NGHESY_TINDUNG.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        public DM_NGHESY_TINDUNG GetDM_NGHESY_TINDUNGByNGHESY_TINDUNG_ID(int NGHESY_TINDUNG_ID)
        {
            return Context.DM_NGHESY_TINDUNG.SingleOrDefault(item => item.NGHESY_TINDUNG_ID == NGHESY_TINDUNG_ID && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert DM_NGHESY_TINDUNG into database
        /// </summary>
        /// <param name="DM_NGHESY_TINDUNG">Object infomation</param>
        public void InsertDM_NGHESY_TINDUNG(DM_NGHESY_TINDUNG DM_NGHESY_TINDUNG)
        {
            Context.DM_NGHESY_TINDUNG.Add(DM_NGHESY_TINDUNG);
            Context.Entry(DM_NGHESY_TINDUNG).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete DM_NGHESY_TINDUNG with specific NGHESY_TINDUNG_ID
        /// </summary>
        /// <param name="NGHESY_TINDUNG_ID">DM_NGHESY_TINDUNG NGHESY_TINDUNG_ID</param>
        public void DeleteDM_NGHESY_TINDUNG(int NGHESY_TINDUNG_ID)
        {
            DM_NGHESY_TINDUNG deletedDM_NGHESY_TINDUNG = GetDM_NGHESY_TINDUNGByNGHESY_TINDUNG_ID(NGHESY_TINDUNG_ID);
            Context.DM_NGHESY_TINDUNG.Remove(deletedDM_NGHESY_TINDUNG);
            Context.Entry(deletedDM_NGHESY_TINDUNG).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete a DM_NGHESY_TINDUNG with its NGHESY_TINDUNG_ID and Update IsDeleted IF that DM_NGHESY_TINDUNG has relationship with others
        /// </summary>
        /// <param name="NGHESY_TINDUNG_ID">Id of DM_NGHESY_TINDUNG</param>
        public void DeleteDM_NGHESY_TINDUNGRs(int NGHESY_TINDUNG_ID)
        {
            DM_NGHESY_TINDUNG deleteDM_NGHESY_TINDUNGRs = GetDM_NGHESY_TINDUNGByNGHESY_TINDUNG_ID(NGHESY_TINDUNG_ID);
            deleteDM_NGHESY_TINDUNGRs.IsDeleted = true;
            UpdateDM_NGHESY_TINDUNG(deleteDM_NGHESY_TINDUNGRs);
        }

        /// <summary>
        /// Update DM_NGHESY_TINDUNG into database
        /// </summary>
        /// <param name="DM_NGHESY_TINDUNG">DM_NGHESY_TINDUNG object</param>
        public void UpdateDM_NGHESY_TINDUNG(DM_NGHESY_TINDUNG DM_NGHESY_TINDUNG)
        {
            Context.Entry(DM_NGHESY_TINDUNG).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}

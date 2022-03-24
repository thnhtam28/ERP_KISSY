using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class YHL_KIENHANG_TRA_CTIETRepositories : GenericRepository<ErpSaleDbContext, YHL_KIENHANG_TRA_CTIET>, IYHL_KIENHANG_TRA_CTIETRepositories
    {
        public YHL_KIENHANG_TRA_CTIETRepositories(ErpSaleDbContext context)
            : base(context)
        {
            
        }
        DbContext db;
        
        /// <summary>
        /// Get all YHL_KIENHANG_TRA_CTIET
        /// </summary>
        /// <returns>YHL_KIENHANG_TRA_CTIET list</returns>
        /// 
        public IQueryable<YHL_KIENHANG_TRA_CTIET> GetAllYHL_KIENHANG_TRA_CTIET()
        {
            return Context.YHL_KIENHANG_TRA_CTIET.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        public YHL_KIENHANG_TRA_CTIET GetKIENHANG_TRA_CTIETByID(int KIENHANG_TRA_CTIET_ID)
        {
            return Context.YHL_KIENHANG_TRA_CTIET.SingleOrDefault(item => item.KIENHANG_TRA_CTIET_ID == KIENHANG_TRA_CTIET_ID && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public YHL_KIENHANG_TRA_CTIET GetKIENHANG_TRA_CTIETByMA_DH(string MA_DON_HANG)
        {
            return Context.YHL_KIENHANG_TRA_CTIET.SingleOrDefault(item => item.MA_DON_HANG == MA_DON_HANG && (item.IsDeleted == null || item.IsDeleted == false));

        }

        public YHL_KIENHANG_TRA_CTIET GetKIENHANG_TRA_CTIETBySO_HIEU(string SO_HIEU)
        {
            return Context.YHL_KIENHANG_TRA_CTIET.SingleOrDefault(item => item.SO_HIEU == SO_HIEU && (item.IsDeleted == null || item.IsDeleted == false));

        }

        /// <summary>
        /// Insert YHL_KIENHANG_TRA_CTIET into database
        /// </summary>
        /// <param name="YHL_KIENHANG_TRA_CTIET">Object infomation</param>
        public void InsertYHL_KIENHANG_TRA_CTIET(YHL_KIENHANG_TRA_CTIET YHL_KIENHANG_TRA_CTIET)
        {
            Context.YHL_KIENHANG_TRA_CTIET.Add(YHL_KIENHANG_TRA_CTIET);
            Context.Entry(YHL_KIENHANG_TRA_CTIET).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete YHL_KIENHANG_TRA_CTIET with specific KIENHANG_TRA_CTIET_ID
        /// </summary>
        /// <param name="KIENHANG_TRA_CTIET_ID">YHL_KIENHANG_TRA_CTIET KIENHANG_TRA_CTIET_ID</param>
        public void DeleteYHL_KIENHANG_TRA_CTIET(int KIENHANG_TRA_CTIET_ID)
        {
            YHL_KIENHANG_TRA_CTIET deletedYHL_KIENHANG_TRA_CTIET = GetKIENHANG_TRA_CTIETByID(KIENHANG_TRA_CTIET_ID);
            Context.YHL_KIENHANG_TRA_CTIET.Remove(deletedYHL_KIENHANG_TRA_CTIET);
            Context.Entry(deletedYHL_KIENHANG_TRA_CTIET).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete a YHL_KIENHANG_TRA_CTIET with its YHL_KIENHANG_TRA_CTIET_ID and Update IsDeleted IF that YHL_KIENHANG_TRA_CTIET has relationship with others
        /// </summary>
        /// <param name="YHL_KIENHANG_TRA_CTIET">Id of YHL_KIENHANG_TRA_CTIET</param>
        /// 
        public void DeleteYHL_KIENHANG_TRA_CTIETRs(int BAIVE_YHL_ID)
        {
            YHL_KIENHANG_TRA_CTIET deleteYHL_KIENHANG_TRA_CTIETRs = GetKIENHANG_TRA_CTIETByID(BAIVE_YHL_ID);
            deleteYHL_KIENHANG_TRA_CTIETRs.IsDeleted = true;
            UpdateYHL_KIENHANG_TRA_CTIET(deleteYHL_KIENHANG_TRA_CTIETRs);
        }

        /// <summary>
        /// Update YHL_KIENHANG_TRA_CTIET into database
        /// </summary>
        /// <param name="YHL_KIENHANG_TRA_CTIET">YHL_KIENHANG_TRA_CTIET object</param>
        public void UpdateYHL_KIENHANG_TRA_CTIET(YHL_KIENHANG_TRA_CTIET YHL_KIENHANG_TRA_CTIET)
        {
            Context.Entry(YHL_KIENHANG_TRA_CTIET).State = EntityState.Modified;
            Context.SaveChanges();
        }

    }
}

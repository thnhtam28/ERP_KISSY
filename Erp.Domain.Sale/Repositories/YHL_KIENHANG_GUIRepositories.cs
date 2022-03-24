using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class YHL_KIENHANG_GUIRepositories : GenericRepository<ErpSaleDbContext, YHL_KIENHANG_GUI>, IYHL_KIENHANG_GUIRepositories
    {
          public YHL_KIENHANG_GUIRepositories(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
          /// Get all YHL_KIENHANG_GUI
        /// </summary>
          /// <returns>YHL_KIENHANG_GUI list</returns>
        /// 
          public IQueryable<YHL_KIENHANG_GUI> GetAllYHL_KIENHANG_GUI()
        {
            return Context.YHL_KIENHANG_GUI.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        public YHL_KIENHANG_GUI GetKIENHANG_GUIByID(int KIENHANG_GUI_ID)
        {
            return Context.YHL_KIENHANG_GUI.SingleOrDefault(item => item.KIENHANG_GUI_ID == KIENHANG_GUI_ID && (item.IsDeleted == null || item.IsDeleted == false));
        }
        

        /// <summary>
        /// Insert YHL_KIENHANG_GUI into database
        /// </summary>
        /// <param name="YHL_KIENHANG_GUI">Object infomation</param>
        public void InsertYHL_KIENHANG_GUI(YHL_KIENHANG_GUI YHL_KIENHANG_GUI)
        {
            Context.YHL_KIENHANG_GUI.Add(YHL_KIENHANG_GUI);
            Context.Entry(YHL_KIENHANG_GUI).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete YHL_KIENHANG_GUI with specific KIENHANG_GUI_ID
        /// </summary>
        /// <param name="KIENHANG_GUI_ID">YHL_KIENHANG_GUI KIENHANG_GUI_ID</param>
        public void DeleteYHL_KIENHANG_GUI(int KIENHANG_GUI_ID)
        {
            YHL_KIENHANG_GUI deletedYHL_KIENHANG_GUI = GetKIENHANG_GUIByID(KIENHANG_GUI_ID);
            Context.YHL_KIENHANG_GUI.Remove(deletedYHL_KIENHANG_GUI);
            Context.Entry(deletedYHL_KIENHANG_GUI).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete a YHL_KIENHANG_GUI with its KIENHANG_GUI_ID and Update IsDeleted IF that YHL_KIENHANG_GUI has relationship with others
        /// </summary>
        /// <param name="YHL_KIENHANG_GUI">Id of YHL_KIENHANG_GUI</param>
        public void DeleteYHL_KIENHANG_GUIRs(int BAIVE_YHL_ID)
        {
            YHL_KIENHANG_GUI deleteYHL_KIENHANG_GUIRs = GetKIENHANG_GUIByID(BAIVE_YHL_ID);
            deleteYHL_KIENHANG_GUIRs.IsDeleted = true;
            UpdateYHL_KIENHANG_GUI(deleteYHL_KIENHANG_GUIRs);
        }

        /// <summary>
        /// Update YHL_KIENHANG_GUI into database
        /// </summary>
        /// <param name="YHL_KIENHANG_GUI">YHL_KIENHANG_GUI object</param>
        public void UpdateYHL_KIENHANG_GUI(YHL_KIENHANG_GUI YHL_KIENHANG_GUI)
        {
            Context.Entry(YHL_KIENHANG_GUI).State = EntityState.Modified;
            Context.SaveChanges();
        }

    }
}

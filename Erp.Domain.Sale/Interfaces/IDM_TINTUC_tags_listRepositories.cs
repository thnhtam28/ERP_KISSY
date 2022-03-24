using Erp.Domain.Sale.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IDM_TINTUC_tags_listRepositories
    {
        /// <summary>
        /// Get all DM_TINTUC_TagList
        /// </summary>
        /// <returns>DM_TINTUC_TagList list</returns>
        /// 
        IQueryable<DM_TINTUC_tags_list> GetAllDM_TINTUC_tags_list();
        IQueryable<DM_TINTUC_tags_list> GetDM_TINTUC_tags_list_ByTagList_ID(string TagId);

        //IQueryable<IDM_TINTUC_tags_list> GetAllDM_TINTUCByNHOMTIN_ID(int NHOMTIN_ID);
        DM_TINTUC_tags_list GetDM_TINTUC_tags_list_ByTagList_Id(string TagId);

        /// <summary>
        /// Insert DM_TINTUC into database
        /// </summary>
        /// <param name="DM_TINTUC_TagList">Object infomation</param>
        void InsertDM_TINTUC_TagList(string id, string name);

        /// <summary>
        /// Delete DM_TINTUC_TagList with specific TINTUC_ID
        /// </summary>
        /// <param name="DM_TINTUC_TagList">DM_TINTUC TINTUC_ID</param>
        void DeleteDM_TINTUC_TagList(string TagId);

        /// <summary>
        /// Delete a DM_TINTUC_TagList with its TINTUC_ID and Update IsDeleted IF that DM_TINTUC has relationship with others
        /// </summary>
        /// <param name="TagId">Id of DM_TINTUC</param>
        void DeleteDM_TINTUC_TagList_Rs(string TagId);

        /// <summary>
        /// Update DM_TINTUC into database
        /// </summary>
        /// <param name="DM_TINTUC_TagList">DM_TINTUC object</param>
        void UpdateDM_TINTUC_TagList(DM_TINTUC_tags_list DM_TINTUC_tags_list);
        
    }
}

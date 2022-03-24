using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Repositories
{
    public class DM_TINTUC_tags_listRepositories : GenericRepository<ErpSaleDbContext, DM_TINTUC_tags_list>, IDM_TINTUC_tags_listRepositories
    {
        public DM_TINTUC_tags_listRepositories(ErpSaleDbContext context)
           : base(context)
        {

        }
        public void DeleteDM_TINTUC_TagList(string TagId)
        {
            DM_TINTUC_tags_list deletedDM_TINTUC_Tag_List = GetDM_TINTUC_tags_list_ByTagList_Id(TagId);
            Context.DM_TINTUC_tags_list.Remove(deletedDM_TINTUC_Tag_List);
            Context.Entry(deletedDM_TINTUC_Tag_List).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        public void DeleteDM_TINTUC_TagList_Rs(string TagId)
        {
            DM_TINTUC_tags_list DeleteDM_TINTUC_TagList_Rs = GetDM_TINTUC_tags_list_ByTagList_Id(TagId);
            DeleteDM_TINTUC_TagList_Rs.IsDeleted = true;
            UpdateDM_TINTUC_TagList(DeleteDM_TINTUC_TagList_Rs);
        }

        public IQueryable<DM_TINTUC_tags_list> GetAllDM_TINTUC_tags_list()
        {
            return Context.DM_TINTUC_tags_list.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        public DM_TINTUC_tags_list GetDM_TINTUC_tags_list_ByTagList_Id(string TagId)
        {
            return Context.DM_TINTUC_tags_list.SingleOrDefault(item => item.Id == TagId && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public void InsertDM_TINTUC_TagList(string id, string name)
        {
            var Taglist = new DM_TINTUC_tags_list();
            Taglist.Id = id;
            Taglist.Name = name;
            Context.DM_TINTUC_tags_list.Add(Taglist);
            Context.Entry(Taglist).State = EntityState.Added;
            Context.SaveChanges();
        }

        public void UpdateDM_TINTUC_TagList(DM_TINTUC_tags_list DM_TINTUC_tags_list)
        {
            Context.Entry(DM_TINTUC_tags_list).State = EntityState.Modified;
            Context.SaveChanges();
        }

        IQueryable<DM_TINTUC_tags_list> IDM_TINTUC_tags_listRepositories.GetDM_TINTUC_tags_list_ByTagList_ID(string TagId)
        {
            return Context.DM_TINTUC_tags_list.Where(item => item.Id == TagId && (item.IsDeleted == null || item.IsDeleted == false));
        }
    }
}

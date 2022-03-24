using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Repositories
{
    public class DM_TINTUC_tags_Repositories : GenericRepository<ErpSaleDbContext, DM_TINTUC_tags>, IDM_TINTUC_tagsRepositories
    {
        public DM_TINTUC_tags_Repositories(ErpSaleDbContext context)
           : base(context)
        {

        }

        public IQueryable<DM_TINTUC_tags> GetAllDM_TINTUC_Tags()
        {
            return Context.DM_TINTUC_Tags;
        }

        public IQueryable<DM_TINTUC_tags> GetAllDM_TINTUC_TagsByTINTUC_ID(int TINTUC_ID)
        {
            return Context.DM_TINTUC_Tags.Where(x => x.TINTUC_ID == TINTUC_ID);
        }

        public void InsertContentTag(int TINTUC_ID, string TagId)
        {
            var Tag = new DM_TINTUC_tags();
            Tag.TINTUC_ID = TINTUC_ID;
            Tag.TagId = TagId;
            Context.DM_TINTUC_Tags.Add(Tag);
            Context.SaveChanges();

        }

        public void RemoveAllContentTag(int pTINTUC_ID)
        {

            var customers = Context.DM_TINTUC_Tags.Where(x => x.TINTUC_ID == pTINTUC_ID).ToList();
            foreach (var item in customers)
            {
                Context.DM_TINTUC_Tags.Remove(item);
            }
           
            Context.SaveChanges();
        }

       
    }
}

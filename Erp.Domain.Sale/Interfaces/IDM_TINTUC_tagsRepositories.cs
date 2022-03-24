using Erp.Domain.Sale.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IDM_TINTUC_tagsRepositories
    {

        IQueryable<DM_TINTUC_tags> GetAllDM_TINTUC_Tags();
        IQueryable<DM_TINTUC_tags> GetAllDM_TINTUC_TagsByTINTUC_ID(int TINTUC_ID);
        void RemoveAllContentTag(int pTINTUC_ID);
        void InsertContentTag(int TINTUC_ID, string TagId);

    }
}

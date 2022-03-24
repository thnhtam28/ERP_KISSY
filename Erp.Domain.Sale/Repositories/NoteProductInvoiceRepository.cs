using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;

using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class NoteProductInvoiceRepository : GenericRepository<ErpSaleDbContext, NoteProductInvoice>, INoteProductInvoiceRepository
    {
        public NoteProductInvoiceRepository(ErpSaleDbContext context)
            : base(context)
        {

        }

        public IQueryable<NoteProductInvoice> GetAllNoteProductInvoice()
        {
            return Context.NoteProductInvoice.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        public NoteProductInvoice GetNoteProductInvoiceById(int Id)
        {
            return Context.NoteProductInvoice.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public void InsertNoteProductInvoice(NoteProductInvoice NoteProductInvoice)
        {
            Context.NoteProductInvoice.Add(NoteProductInvoice);
            Context.Entry(NoteProductInvoice).State = EntityState.Added;
            Context.SaveChanges();
        }

        public void DeleteNoteProductInvoice(int Id)
        {
            NoteProductInvoice deletedNoteProductInvoice = GetNoteProductInvoiceById(Id);
            Context.NoteProductInvoice.Remove(deletedNoteProductInvoice);
            Context.Entry(deletedNoteProductInvoice).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        public void DeleteNoteProductInvoiceRs(int Id)
        {
            NoteProductInvoice deleteCommissionCusRs = GetNoteProductInvoiceById(Id);
            deleteCommissionCusRs.IsDeleted = true;
            UpdateNoteProductInvoice(deleteCommissionCusRs);
        }


        public void UpdateNoteProductInvoice(NoteProductInvoice NoteProductInvoice)
        {
            Context.Entry(NoteProductInvoice).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }

}

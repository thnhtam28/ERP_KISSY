using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface INoteProductInvoiceRepository
    {
        IQueryable<NoteProductInvoice> GetAllNoteProductInvoice();
        NoteProductInvoice GetNoteProductInvoiceById(int Id);
        void InsertNoteProductInvoice(NoteProductInvoice NoteProductInvoice);
        void DeleteNoteProductInvoice(int Id);
        void DeleteNoteProductInvoiceRs(int Id);
        void UpdateNoteProductInvoice(NoteProductInvoice NoteProductInvoice);
    }
}

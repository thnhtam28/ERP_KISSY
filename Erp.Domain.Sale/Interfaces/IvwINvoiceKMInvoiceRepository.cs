﻿using Erp.Domain.Sale.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IvwINvoiceKMInvoiceRepository
    {
        // IQueryable<vwINvoiceKMDetail> GetAllvwINvoiceKMInvoice();
        List<vwINvoiceKMInvoice> GetAllListvwINvoiceKMInvoice();
    }
}
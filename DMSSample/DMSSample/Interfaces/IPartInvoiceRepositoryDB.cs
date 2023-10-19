using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinewood.DMSSample.Business.Interfaces
{
    public interface IPartInvoiceRepositoryDB
    {
        void Add(PartInvoice invoice);
    }
}

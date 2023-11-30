using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volxyseat.Domain.Core.Data;

namespace Volxyseat.Domain.Models.Transaction
{
    public interface ITransactionRepository : IRepository<TransactionModel, Guid>
    {
        TransactionModel? GetByClientId(Guid id);
    }
}

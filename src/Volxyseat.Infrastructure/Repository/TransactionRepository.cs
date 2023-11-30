using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volxyseat.Domain.Models.SubscriptionModel;
using Volxyseat.Domain.Models.Transaction;
using Volxyseat.Infrastructure.Data;

namespace Volxyseat.Infrastructure.Repository
{
    public class TransactionRepository : RepositoryBase<TransactionModel, Guid>, ITransactionRepository
    {
        protected readonly ApplicationDataContext _applicationDataContex;
        protected new readonly DbSet<TransactionModel> _entity;
        public TransactionRepository(ApplicationDataContext applicationDataContext) : base(applicationDataContext)
        {
            _applicationDataContex = applicationDataContext;
        }

        public TransactionModel? GetByClientId(Guid id)
        {
                var item = _applicationDataContex.Transactions.FirstOrDefault(i => i.Client == id);

                if (item == null)
                {
                    return null; 
                }

                return item;
        }
    }
}

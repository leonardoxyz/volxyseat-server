using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volxyseat.Domain;
using Volxyseat.Domain.Core.Data;
using Volxyseat.Domain.Models.SubscriptionModel;
using Volxyseat.Domain.Models.Transaction;

namespace Volxyseat.Infrastructure.Data
{
    public class ApplicationDataContext: IdentityDbContext<IdentityUser>, IUnitOfWork
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options)
        {

        }

        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<TransactionModel> Transactions { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volxyseat.Domain.Models.SubscriptionModel;

namespace Volxyseat.Domain.Models.Transaction
{
    public class TransactionModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid Subscription { get; set; }
        public Guid Client { get; set; }
        public DateTime IssueDate { get; set; }
        public int TermInDays { get; set; }
    }
}

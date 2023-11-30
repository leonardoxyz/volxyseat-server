using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volxyseat.Domain.Models.SubscriptionModel;

namespace Volxyseat.Domain.ViewModel
{
    public class TransactionViewModel
    {
        public  Guid Subscription { get; set; }
        public Guid Client { get; set; }
        public int TermInDays { get; set; }

    }
}

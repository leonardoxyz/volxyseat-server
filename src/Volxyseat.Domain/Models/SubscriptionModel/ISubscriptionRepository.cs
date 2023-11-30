using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volxyseat.Domain.Core.Data;

namespace Volxyseat.Domain.Models.SubscriptionModel
{
    public interface ISubscriptionRepository : IRepository<Subscription, Guid>
    {
        void SwitchSubscription(Subscription entity);
    }
}

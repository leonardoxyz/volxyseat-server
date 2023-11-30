using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volxyseat.Domain.Models.SubscriptionModel;
using Volxyseat.Infrastructure.Data;

namespace Volxyseat.Infrastructure.Repository
{
    public class SubscriptionRepository : RepositoryBase<Subscription, Guid>, ISubscriptionRepository
    {
        public SubscriptionRepository(ApplicationDataContext applicationDataContext) : base(applicationDataContext)
        {
            
        }

        public void SwitchSubscription(Subscription entity)
        {
            _entity.Update(entity);
        }
    }
}

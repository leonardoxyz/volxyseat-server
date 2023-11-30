using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volxyseat.Domain.Models.SubscriptionModel;
using Volxyseat.Domain.Models.Transaction;
using Volxyseat.Domain.ViewModel;

namespace Volxyseat.Infrastructure.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<SubscriptionViewModel, Subscription>();
            CreateMap<Subscription, SubscriptionViewModel>();

            CreateMap<TransactionViewModel, TransactionModel>();
            CreateMap<TransactionModel, TransactionViewModel>();
        }
    }
}

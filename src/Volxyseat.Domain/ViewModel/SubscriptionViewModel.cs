using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volxyseat.Domain.ViewModel
{
    public class SubscriptionViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Type { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int TermInDays { get; set; }
        public bool IsActive { get; set; }
    }
}

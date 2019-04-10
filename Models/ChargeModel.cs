using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stripe.Models
{
    public class ChargeModel
    {
        public string StripeToken { get; set; }
        public decimal PledgeAmount { get; set; }
        public string Name { get; set; }

    }
}

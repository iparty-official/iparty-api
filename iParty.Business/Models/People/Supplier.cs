using iParty.Business.Models.PaymentPlans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Business.Models.People
{
    public class Supplier
    {
        public string BusinessDescription { get; set; }

        public List<PaymentPlan> PaymentPlans { get; set; }
    }
}

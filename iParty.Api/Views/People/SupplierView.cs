using iParty.Api.Views.PaymentPlans;
using System;
using System.Collections.Generic;

namespace iParty.Api.Views.People
{
    public class SupplierView : PersonView
    {
        public string BusinessDescription { get; set; }

        public List<Guid> PaymentPlanIds { get; set; }
    }
}

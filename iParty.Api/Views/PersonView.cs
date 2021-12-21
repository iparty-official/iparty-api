﻿using iParty.Business.Models.People;
using System.Collections.Generic;

namespace iParty.Api.Views
{
    public class PersonView : View
    {
        public object User { get; set; }

        public string Name { get; set; }

        public string Document { get; set; }

        public object Photo { get; set; }        

        public List<AddressView> Addresses { get; set; }

        public List<PhoneView> Phones { get; set; }
    }
}

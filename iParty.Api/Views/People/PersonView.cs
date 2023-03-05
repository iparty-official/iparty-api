using iParty.Api.Views.Addresses;
using System.Collections.Generic;

namespace iParty.Api.Views.People
{
    public class PersonView : View
    {
        public object User { get; set; }

        public string Name { get; set; }

        public string Document { get; set; }

        public object Photo { get; set; }        
    }
}

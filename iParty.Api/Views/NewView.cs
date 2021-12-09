using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iParty.Api.Views
{
    public class NewView
    {
        public NewView(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}

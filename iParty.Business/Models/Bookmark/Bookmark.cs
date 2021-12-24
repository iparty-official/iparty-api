using iParty.Business.Models.Items;
using iParty.Business.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Business.Models.Bookmark
{
    public class Bookmark: Entity
    {
        public Person Customer { get; set; }
        public Item Item{ get; set; }
        public DateTime DateTime { get; set; }
    }
}

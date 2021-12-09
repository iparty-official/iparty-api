using iParty.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Business.Infra
{
    public class ServiceResult<TEntity> where TEntity : Entity
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public TEntity Entity { get; set; }
    }
}

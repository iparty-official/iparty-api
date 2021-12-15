using iParty.Business.Interfaces;
using System.Collections.Generic;

namespace iParty.Business.Infra
{
    public class ServiceResult<TEntity> where TEntity : IEntity
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public TEntity Entity { get; set; }
    }
}

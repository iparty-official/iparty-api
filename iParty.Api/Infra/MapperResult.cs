using iParty.Business.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace iParty.Api.Infra
{
    public class MapperResult<TEntity> where TEntity : IEntity
    {
        public MapperResult()
        {
            Errors = new List<string>();
        }

        public bool Success => !Errors.Any();
        public List<string> Errors { get; set; }
        public TEntity Entity { get; set; }
    }
}

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

        public MapperResult(TEntity entity, List<string> errors)
        {
            Entity = entity;
            Errors = errors;
        }

        public bool Success => !Errors.Any();
        public List<string> Errors { get; private set; }
        public TEntity Entity { get; private set; }

        public void Clear()
        {
            Errors.Clear();
            Entity = default(TEntity);
        }

        public void DefineEntity(TEntity entity)
        {
            this.Entity = entity;
        }
    }
}

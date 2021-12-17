using iParty.Business.Interfaces;

namespace iParty.Api.Infra
{
    public abstract class BaseMapper<TEntity> where TEntity : IEntity, new()
    {
        private readonly MapperResult<TEntity> _mapperResult;   
        public BaseMapper()
        {
            _mapperResult = new MapperResult<TEntity>();
        }

        protected MapperResult<TEntity> GetResult()
        {
            return _mapperResult;
        }

        protected void SetEntity(TEntity entity)
        {
            _mapperResult.Entity = entity;
        }

        protected void AddError(string error)
        {
            _mapperResult.Errors.Add(error);
        }

        protected bool ResultIsValid()
        {
            return _mapperResult.Success;
        }

    }
}

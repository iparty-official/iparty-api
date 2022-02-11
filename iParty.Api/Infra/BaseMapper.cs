using iParty.Business.Interfaces;

namespace iParty.Api.Infra
{
    public abstract class BaseMapper<TEntity> where TEntity : IEntity
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
            _mapperResult.DefineEntity(entity);
        }

        protected void AddError(string error)
        {
            _mapperResult.Errors.Add(error);
        }

        protected bool SuccessResult()
        {
            return _mapperResult.Success;
        }

    }
}

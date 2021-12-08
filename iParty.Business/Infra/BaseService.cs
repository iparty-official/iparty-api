using FluentValidation;
using FluentValidation.Results;
using iParty.Business.Models;
using iParty.Data.Repositories;

namespace iParty.Business.Infra
{
    public abstract class BaseService<TEntity, TRepository> 
        where TEntity : Entity, new()
        where TRepository : IRepository<TEntity>
    {
        protected BaseService(TRepository rep)
        {
            Rep = rep;
        }

        protected TRepository Rep { get; private set; }

        protected ValidationResult ExecuteValidation<TValidator>(TValidator validator, TEntity entity) where TValidator : AbstractValidator<TEntity>
        {
            return validator.Validate(entity);
        }
    }
}

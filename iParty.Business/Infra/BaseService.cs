using FluentValidation;
using FluentValidation.Results;
using iParty.Business.Models;
using iParty.Data.Repositories;
using System.Collections.Generic;
using System.Linq;

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

        protected ServiceResult<TEntity> GetSuccessResult(TEntity entity)
        {
            return new ServiceResult<TEntity>()
            {
                Success = true,
                Errors = new List<string>(),
                Entity = entity
            };
        }

        protected ServiceResult<TEntity> GetFailureResult(ValidationResult validationResult)
        {
            return new ServiceResult<TEntity>()
            {
                Success = false,
                Errors = validationResult.Errors.Select(p => p.ErrorMessage).ToList(),
                Entity = null
            };
        }
    }
}

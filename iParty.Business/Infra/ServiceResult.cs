using FluentValidation.Results;
using iParty.Business.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace iParty.Business.Infra
{
    public class ServiceResult<TEntity> where TEntity : IEntity
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public TEntity Entity { get; set; }
        public static ServiceResult<TEntity> SuccessResult(TEntity entity)
        {
            return new ServiceResult<TEntity>()
            {
                Success = true,
                Errors = new List<string>(),
                Entity = entity
            };
        }

        public static ServiceResult<TEntity> FailureResult(ValidationResult validationResult)
        {
            return new ServiceResult<TEntity>()
            {
                Success = false,
                Errors = validationResult.Errors.Select(p => p.ErrorMessage).ToList(),
            };
        }

        public static ServiceResult<TEntity> FailureResult(string errorMessage)
        {
            return new ServiceResult<TEntity>()
            {
                Success = false,
                Errors = new List<string>() { errorMessage },
            };
        }
    }
}

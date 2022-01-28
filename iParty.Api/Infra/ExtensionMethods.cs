using iParty.Business.Interfaces;
using System;

namespace iParty.Api.Infra
{
    public static class ExtensionMethods
    {
        public static MapperResult<T> SetIdAndVersion<T>(this MapperResult<T> mapperResult, Guid id, Guid version) where T : IEntity
        {
            if (mapperResult.Entity != null)
            {
                mapperResult.Entity.Id = id;
                mapperResult.Entity.Version = version;
            }

            return mapperResult;
        }

        public static MapperResult<T> SetId<T>(this MapperResult<T> mapperResult, Guid id) where T : IEntity
        {
            if (mapperResult.Entity != null)
            {
                mapperResult.Entity.Id = id;                
            }

            return mapperResult;
        }

        public static T SetId<T>(this T entity, Guid id) where T : IEntity
        {
            if (entity != null)
            {
                entity.Id = id;
            }

            return entity;
        }
    }
}

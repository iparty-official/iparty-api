﻿using iParty.Business.Interfaces;
using iParty.Business.Interfaces.Filters;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace iParty.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        private IMongoCollection<TEntity> _collection { get; set; }

        private FilterDefinition<TEntity> translateToFilterDefinition(List<IFilter<TEntity>> filters)
        {
            var mongoBuilder = Builders<TEntity>.Filter;

            var filterDefinition = mongoBuilder.Empty;

            foreach (var item in filters)
            {
                switch (item.Operator)
                {
                    case FilterOperatorEnum.Equal:
                        filterDefinition &= mongoBuilder.Eq(item.GetFieldName(item.Field), item.Value);
                        break;
                    case FilterOperatorEnum.Unequal:
                        filterDefinition &= mongoBuilder.Not(mongoBuilder.Eq(item.GetFieldName(item.Field), item.Value));
                        break;
                    case FilterOperatorEnum.GreaterThan:
                        filterDefinition &= mongoBuilder.Gt(item.GetFieldName(item.Field), item.Value);
                        break;
                    case FilterOperatorEnum.LessThan:
                        filterDefinition &= mongoBuilder.Lt(item.GetFieldName(item.Field), item.Value);
                        break;
                    default:
                        break;
                }
            }

            return filterDefinition;
        }

        public Repository(DatabaseConfig databaseConfig)
        {            
            var settings = MongoClientSettings.FromConnectionString(databaseConfig.ConnectionString);

            var client = new MongoClient(settings);

            var database = client.GetDatabase(databaseConfig.DatabaseAlias);

            _collection = database.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public void Create(TEntity entity)
        {
            _collection.InsertOne(entity);
        }

        public void Update(Guid id, TEntity entity)
        {
            entity.Id = id;

            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, id);
           
            _collection.ReplaceOne(filter, entity);
        }

        public void Delete(Guid id)
        {
            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, id);

            var update = Builders<TEntity>.Update.Set(x => x.Removed, true);

            _collection.UpdateOne(filter, update);
        }

        public List<TEntity> Recover(IFilterBuilder<TEntity> filterBuilder)
        {
            var filters = filterBuilder.Build();

            var filterDefinition = translateToFilterDefinition(filters);

            filterDefinition &= Builders<TEntity>.Filter.Eq(x => x.Removed, false);

            return _collection.Find(filterDefinition).ToList();
        }

        public List<TEntity> Recover()
        {
            var filter = Builders<TEntity>.Filter.Eq(x => x.Removed, false);

            return _collection.Find(filter).ToList();
        }

        public TEntity RecoverById(Guid id)
        {
            var builder = Builders<TEntity>.Filter;

            var filter = builder.And(builder.Eq(x => x.Id, id), builder.Eq(x => x.Removed, false));

            return _collection.Find(filter).FirstOrDefault<TEntity>();
        }    
    }
}
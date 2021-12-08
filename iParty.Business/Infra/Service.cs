﻿using AutoMapper;
using iParty.Business.Models;
using iParty.Data.Repositories;
using System;
using System.Collections.Generic;

namespace iParty.Business.Infra
{
    public class Service<TEntity, TRepository> : IService<TEntity>
        where TEntity : Entity, new()
        where TRepository : IRepository<TEntity>
    {
        protected TRepository Rep { get; private set; }

        public Service(TRepository rep)
        {
            Rep = rep;
        }
        public virtual void Delete(Guid id)
        {
            Rep.Delete(id);
        }

        public TEntity Get(Guid id)
        {
            return Rep.RecoverById(id);
        }

        public List<TEntity> Get()
        {
            return Rep.Recover();
        }
    }
}

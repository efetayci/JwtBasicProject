﻿using EFT.JwtBasic.Entites.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EFT.JwtBasic.DataAccess.Interfaces
{
    public interface IGenericDal<TEntity> where TEntity : class,ITable,new()
    {
        Task<List<TEntity>> GetAll();
        Task<List<TEntity>> GetAllByFilter(Expression<Func<TEntity,bool>> filter);
        Task<TEntity> GetById(int id);
        Task<TEntity> GetByFiter(Expression<Func<TEntity, bool>> filter);
        Task Update(TEntity entity);
        Task Remove(TEntity entity);
        Task Add(TEntity entity);

    }
}

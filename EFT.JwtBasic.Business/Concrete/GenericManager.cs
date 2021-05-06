using EFT.JwtBasic.Business.Interfaces;
using EFT.JwtBasic.DataAccess.Interfaces;
using EFT.JwtBasic.Entites.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFT.JwtBasic.Business.Concrete
{
    public class GenericManager<TEntity> : IGenericService<TEntity> where TEntity : class, ITable, new()
    {
        private readonly IGenericDal<TEntity> genericDal;
        public GenericManager(IGenericDal<TEntity> genericDal)
        {
            this.genericDal = genericDal;
        }
        public async Task Add(TEntity entity)
        {
            await genericDal.Add(entity);
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await this.genericDal.GetAll();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await this.genericDal.GetById(id);
        }

        public async Task Remove(TEntity entity)
        {
            await this.genericDal.Remove(entity);
        }

        public async Task Update(TEntity entity)
        {
            await this.genericDal.Update(entity);
        }
    }
}

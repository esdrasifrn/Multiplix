using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Multiplix.Domain.Interfaces.Repository;
using Multiplix.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace Multiplix.Infrastructure.RepositoryEF
{
    public class EFRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        IConfiguration _configuration;       
        protected readonly MultiplixContext _dbContext;

        public EFRepository(MultiplixContext multiplixContext, IConfiguration configuration)
        {
            _dbContext = multiplixContext;
            _configuration = configuration;
        }

        public string GetConnection()
        {
            var connection = _configuration.GetSection("ConnectionStrings").GetSection("MultiplixConnectionString").Value;
            return connection;
        }

        public TEntity Adicionar(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public virtual void Atualizar(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicado)
        {
            return _dbContext.Set<TEntity>().Where(predicado).AsEnumerable();
        }

        public TEntity BuscarEntidade(Expression<Func<TEntity, bool>> predicado)
        {
            return _dbContext.Set<TEntity>().Where(predicado).FirstOrDefault();
        }

        public virtual TEntity ObterPorId(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> ObterTodos()
        {
            return _dbContext.Set<TEntity>().AsEnumerable();
        }

        public IEnumerable<TEntity> ObterTodosPaginado(int skip, int take)
        {
            return _dbContext.Set<TEntity>().Skip(skip).Take(take).AsEnumerable();
        }

        public void Remover(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            _dbContext.SaveChanges();
        } 
    }
}
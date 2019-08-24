using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Multiplix.Domain.Interfaces.Repository
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        TEntity Adicionar(TEntity entity);
        void Atualizar(TEntity entity);
        IEnumerable<TEntity> ObterTodos();
        IEnumerable<TEntity> ObterTodosPaginado(int skip, int take);
        TEntity ObterPorId(int id);
        IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicado);
        TEntity BuscarEntidade(Expression<Func<TEntity, bool>> predicado);
        void Remover(TEntity entity);
    }
}

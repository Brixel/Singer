using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Singer.Helpers;
using Singer.Models;

namespace Singer.Services.Interfaces
{
   public interface IDatabaseService<TEntity, TDTO>
      where TEntity : class, IIdentifiable
   {
      Task<TDTO> CreateAsync(TDTO dto,
         Expression<Func<TDTO, TEntity>> dtoToEntityProjector = null,
         Expression<Func<TEntity, TDTO>> entityToDTOProjector = null);

      Task<TDTO> GetOneAsync(Guid id, Expression<Func<TEntity, TDTO>> projector = null);

      Task<IReadOnlyList<TDTO>> GetAllAsync(Expression<Func<TEntity, TDTO>> projector = null);

      Task<SearchResults<TDTO>> GetAsync(string sortColumn,
         ListSortDirection sortDirection,
         string filter,
         int page = 0,
         int userPerPage = 15,
         Expression<Func<TEntity, TDTO>> projector = null);

      Task<TDTO> UpdateAsync(
         TDTO newValue,
         Guid id,
         Expression<Func<TDTO, TEntity>> dtoToEntityProjector = null,
         Expression<Func<TEntity, TDTO>> entityToDTOProjector = null);

      Task DeleteAsync(Guid id);
   }
}

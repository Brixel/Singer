using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Singer.Helpers;

namespace Singer.Services.Interfaces
{
   public interface IDatabaseService<TEntity, TDTO>
      where TEntity : IIdentifiable
   {
      Task<TDTO> CreateAsync(TDTO dto);

      Task<TDTO> GetOneAsync(Guid id);
      Task<IReadOnlyList<TDTO>> GetAllAsync();
      Task<TDTO> GetAsync(string sortColumn,
         string sortDirection,
         string filter,
         int page = 0,
         int userPerPage = 15);

      Task<TDTO> UpdateAsync(TDTO newValue, Guid id);

      Task DeleteAsync(Guid id);
   }
}

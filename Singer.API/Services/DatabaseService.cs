using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Singer.Data;
using Singer.Helpers;
using Singer.Helpers.Exceptions;
using Singer.Helpers.Extensions;
using Singer.Models;
using Singer.Services.Interfaces;

namespace Singer.Services
{
   public abstract class DatabaseService<TEntity, TDTO> : IDatabaseService<TEntity, TDTO>
      where TEntity : class, IIdentifiable
   {
      #region FIELDS

      #endregion FIELDS

      #region CONSTRUCTOR

      protected DatabaseService(ApplicationDbContext appDbContext, IMapper mapper)
      {
         AppDbContext = appDbContext;
         Mapper = mapper;
      }

      #endregion CONSTRUCTOR


      #region PROPERTIES

      protected abstract DbSet<TEntity> DbContext { get; }

      protected ApplicationDbContext AppDbContext { get; }

      protected IMapper Mapper { get; }

      protected virtual Expression<Func<TEntity, TDTO>> EntityToDTOProjector
         => x => Mapper.Map<TDTO>(x);

      protected virtual Expression<Func<TDTO, TEntity>> DTOToEntityProjector
         => x => Mapper.Map<TEntity>(x);

      #endregion PROPERTIES


      #region METHODS

      protected abstract Expression<Func<TEntity, bool>> Filter(string filter);

      public virtual async Task<TDTO> CreateAsync(
         TDTO dto,
         Expression<Func<TDTO, TEntity>> dtoToEntityProjector = null,
         Expression<Func<TEntity, TDTO>> entityToDTOProjector = null)
      {
         if (entityToDTOProjector == null)
            entityToDTOProjector = EntityToDTOProjector;
         if (dtoToEntityProjector == null)
            dtoToEntityProjector = DTOToEntityProjector;

         var entity = dtoToEntityProjector.Compile()(dto);

         var changeTracker = DbContext.Add(entity);
         await AppDbContext.SaveChangesAsync();

         return entityToDTOProjector.Compile()(changeTracker.Entity);
      }

      public virtual async Task<TDTO> GetOneAsync(Guid id, Expression<Func<TEntity, TDTO>> projector = null)
      {
         if (projector == null)
            projector = EntityToDTOProjector;

         var item = await DbContext.FindAsync(id);
         if (item == null)
            throw new NotFoundException();

         return projector.Compile()(item);
      }

      public virtual async Task<IReadOnlyList<TDTO>> GetAllAsync(Expression<Func<TEntity, TDTO>> projector = null)
      {
         if (projector == null)
            projector = EntityToDTOProjector;

         var items = await DbContext
            .Select(projector)
            .ToListAsync();

         return new ReadOnlyCollection<TDTO>(items);
      }

      public virtual async Task<SearchResults<TDTO>> GetAsync(
         string sortColumn,
         ListSortDirection sortDirection,
         string filter,
         int page = 0,
         int userPerPage = 15,
         Expression<Func<TEntity, TDTO>> projector = null)
      {
         if (projector == null)
            projector = EntityToDTOProjector;

         var orderer = PropertyHelpers.GetPropertySelector<TDTO>(sortColumn);

         return await DbContext
            .AsQueryable()
            .ToPagedListAsync(
               filterExpression: Filter(filter),
               projectionExpression: projector,
               orderByLambda: orderer,
               sortDirection: sortDirection,
               pageIndex: page,
               pageSize: userPerPage);
      }

      public virtual async Task<TDTO> UpdateAsync(
         TDTO newValue,
         Guid id,
         Expression<Func<TDTO, TEntity>> dtoToEntityProjector = null,
         Expression<Func<TEntity, TDTO>> entityToDTOProjector = null)
      {
         if (entityToDTOProjector == null)
            entityToDTOProjector = EntityToDTOProjector;
         if (dtoToEntityProjector == null)
            dtoToEntityProjector = DTOToEntityProjector;

         var itemToUpdate = await DbContext.FindAsync(id);
         if (itemToUpdate == null)
            throw new NotFoundException();

         itemToUpdate = dtoToEntityProjector.Compile()(newValue);

         var tracker = DbContext.Update(itemToUpdate);
         await AppDbContext.SaveChangesAsync();
         return entityToDTOProjector.Compile()(tracker.Entity);
      }

      public virtual async Task DeleteAsync(Guid id)
      {
         var itemToDelete = await DbContext.FindAsync(id);
         if (itemToDelete == null)
            throw new NotFoundException();

         DbContext.Remove(itemToDelete);
         await AppDbContext.SaveChangesAsync();
      }

      #endregion METHODS
   }
}

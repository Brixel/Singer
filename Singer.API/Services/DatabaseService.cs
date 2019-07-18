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
   /// <summary>
   /// Abstract class that implements the <see cref="IDatabaseService{TEntity, TDTO}"/>. It implements the
   /// basic database manipulation methods and properties.
   /// </summary>
   /// <typeparam name="TEntity">The type of the entity to manipulate in the database.</typeparam>
   /// <typeparam name="TDTO">The type that will be exposed to the outside world.</typeparam>
   /// <typeparam name="TCreateDTO">The type that is used to create new entities in the database.</typeparam>
   public abstract class DatabaseService<TEntity, TDTO, TCreateDTO> : IDatabaseService<TEntity, TDTO, TCreateDTO>
      where TEntity : class, IIdentifiable
      where TCreateDTO : class
   {
      #region CONSTRUCTOR

      /// <summary>
      /// Constructs a new instance of the <see cref="DatabaseService{TEntity, TDTO}"/> class.
      /// </summary>
      /// <param name="context">The context in which the database is approachable.</param>
      /// <param name="mapper">The mapper to map the <see cref="TEntity"/>s to <see cref="TDTO"/>s and vice versa.</param>
      protected DatabaseService(ApplicationDbContext context, IMapper mapper)
      {
         Context = context;
         Mapper = mapper;
      }

      #endregion CONSTRUCTOR


      #region PROPERTIES

      /// <summary>
      /// Set that contains the entities in the database.
      /// </summary>
      protected abstract DbSet<TEntity> DbSet { get; }

      /// <summary>
      /// The context in which the database is approachable.
      /// </summary>
      protected ApplicationDbContext Context { get; }

      /// <summary>
      /// The mapper to map the <see cref="TEntity"/>s to <see cref="TDTO"/>s and vice versa.
      /// </summary>
      protected IMapper Mapper { get; }

      /// <summary>
      /// Expression that is used to convert an <see cref="TEntity"/> to a <see cref="TDTO"/> when returning values from the database.
      /// By default this uses the <see cref="Mapper"/> property (<see cref="Mapper.Map{TDestination}(object)"/>).
      /// </summary>
      public virtual Expression<Func<TEntity, TDTO>> EntityToDTOProjector
         => x => Mapper.Map<TDTO>(x);

      /// <summary>
      /// Expression that is used to convert a <see cref="TDTO"/> to an <see cref="TEntity"/> when manipulating values in the database.
      /// By default this uses the <see cref="Mapper"/> property (<see cref="Mapper.Map{TDestination}(object)"/>).
      /// </summary>
      public virtual Expression<Func<TDTO, TEntity>> DTOToEntityProjector
         => x => Mapper.Map<TEntity>(x);

      /// <summary>
      /// Expression that is used to convert a <see cref="TCreateDTO"/> to an <see cref="TEntity"/> when creating entities in the database.
      /// </summary>
      public virtual Expression<Func<TCreateDTO, TEntity>> CreateDTOToEntityProjector
         => x => Mapper.Map<TEntity>(x);

      #endregion PROPERTIES


      #region METHODS

      /// <summary>
      /// Creates an expression to filter the database by using the string input.
      /// </summary>
      /// <param name="filter">String value to filter the database by.</param>
      /// <returns>An expression to filter the database by.</returns>
      protected abstract Expression<Func<TEntity, bool>> Filter(string filter);

      /// <summary>
      /// Creates a <see cref="TDTO"/> in the database by converting it to a <see cref="TEntity"/>.
      /// It will automatically generate an id.
      /// </summary>
      /// <param name="dto">The new value for the database. This will be converted to an entity.</param>
      /// <param name="dtoToEntityProjector">
      /// Expression to convert the given <see cref="TDTO"/> to an <see cref="TEntity"/>.
      /// If this value is null, the <see cref="DTOToEntityProjector"/> property is used.
      /// </param>
      /// <param name="entityToDTOProjector">
      /// Expression to convert the <see cref="TEntity"/> to return to a <see cref="TDTO"/>.
      /// If this value is null, the <see cref="EntityToDTOProjector"/> property is used.
      /// </param>
      /// <returns>The new created <see cref="TEntity"/> converted to a <see cref="TDTO"/>.</returns>
      public virtual async Task<TDTO> CreateAsync(
         TCreateDTO dto,
         Expression<Func<TCreateDTO, TEntity>> dtoToEntityProjector = null,
         Expression<Func<TEntity, TDTO>> entityToDTOProjector = null)
      {
         // set the projectors to the default values if they are null.
         if (entityToDTOProjector == null)
            entityToDTOProjector = EntityToDTOProjector;
         if (dtoToEntityProjector == null)
            dtoToEntityProjector = CreateDTOToEntityProjector;

         // project the DTO to an entity
         var entity = dtoToEntityProjector.Compile()(dto);

         // add the entity to the database
         var changeTracker = DbSet.Add(entity);
         await Context.SaveChangesAsync();

         // return the new created entity
         return entityToDTOProjector.Compile()(changeTracker.Entity);
      }

      /// <summary>
      /// Returns one <see cref="TEntity"/> from the database, converted to a <see cref="TDTO"/>.
      /// </summary>
      /// <param name="id">The id of the <see cref="TEntity"/> to get.</param>
      /// <param name="projector">
      /// Expression to convert the <see cref="TEntity"/> to return to a <see cref="TDTO"/>.
      /// If this value is null, the <see cref="EntityToDTOProjector"/> property is used.
      /// </param>
      /// <returns>The <see cref="TEntity"/> in the database with id <paramref name="id"/> converted to a <see cref="TDTO"/>.</returns>
      /// <exception cref="NotFoundException">There is no element found with the id <paramref name="id"/>.</exception>
      public virtual async Task<TDTO> GetOneAsync(Guid id, Expression<Func<TEntity, TDTO>> projector = null)
      {
         // set the projector if it is null
         if (projector == null)
            projector = EntityToDTOProjector;

         // search for the entity with the given id in the database
         var item = await DbSet.FindAsync(id);
         if (item == null)
            throw new NotFoundException();

         // return the found entity projected to it's DTO
         return projector.Compile()(item);
      }

      /// <summary>
      /// Returns all the <see cref="TEntity"/>s stored in the database, converted to <see cref="TDTO"/>s.
      /// </summary>
      /// <param name="projector">
      /// Expression to convert the <see cref="TEntity"/> to return to a <see cref="TDTO"/>.
      /// If this value is null, the <see cref="EntityToDTOProjector"/> property is used.
      /// </param>
      /// <returns>All the <see cref="TEntity"/>s in the database, converted to <see cref="TDTO"/>s.</returns>
      public virtual async Task<IReadOnlyList<TDTO>> GetAllAsync(Expression<Func<TEntity, TDTO>> projector = null)
      {
         // set the projector if it is null
         if (projector == null)
            projector = EntityToDTOProjector;

         // fetch all the entities from the database
         var items = await DbSet
            .Select(projector)
            .ToListAsync();

         // return all the items
         return new ReadOnlyCollection<TDTO>(items);
      }

      /// <summary>
      /// Returns a selection of <see cref="TEntity"/>s, converted <see cref="TDTO"/>s. The selection is made by filtering the
      /// in the database, ordering the collection and finally selecting a page.
      /// <list type="number">
      ///   <item>
      ///      <term>Filtering</term>
      ///      <description>The collection will be filtered by comparing all relevant fields to the <paramref name="filter"/>.</description>
      ///   </item>
      ///   <item>
      ///      <term>Projecting</term>
      ///      <description>The collection will be projected on the <see cref="TDTO"/>.</description>
      ///   </item>
      ///   <item>
      ///      <term>Sorting</term>
      ///      <description>The collection will be sorted by the <paramref name="orderer"/> in the direction specified with the <paramref name="sortDirection"/>.</description>
      ///   </item>
      ///   <item>
      ///      <term>Paging</term>
      ///      <description>Finally a page will be selected from the collection so that not all elements should be returned.</description>
      ///   </item>
      /// </list>
      /// </summary>
      /// <param name="filter">The string value to compare to the properties of the <see cref="TEntity"/>s.</param>
      /// <param name="projector">The <see cref="Expression"/> to project the <see cref="TEntity"/>s to the <see cref="TDTO"/>s.</param>
      /// <param name="orderer">The column to sort the returned list on.</param>
      /// <param name="sortDirection">The direction to sort the column on.</param>
      /// <param name="page">The page-number to return.</param>
      /// <param name="entitiesPerPage">The number of entities on a page.</param>
      /// <returns></returns>
      public virtual async Task<SearchResults<TDTO>> GetAsync(
         string filter = null,
         Expression<Func<TEntity, TDTO>> projector = null,
         Expression<Func<TDTO, object>> orderer = null,
         ListSortDirection sortDirection = ListSortDirection.Ascending,
         int page = 0,
         int entitiesPerPage = 15)
      {
         // set the projector if it is null
         if (projector == null)
            projector = EntityToDTOProjector;

         // return the paged results
         return await DbSet
            .AsQueryable()
            .ToPagedListAsync(
               filterExpression: Filter(filter),
               projectionExpression: projector,
               orderByLambda: orderer,
               sortDirection: sortDirection,
               pageIndex: page,
               pageSize: entitiesPerPage);
      }

      /// <summary>
      /// Updates a single <see cref="TEntity"/> in the database. This <see cref="TEntity"/> is defined by the <paramref name="id"/>.
      /// </summary>
      /// <param name="id">The identifier of the <see cref="TEntity"/>.</param>
      /// <param name="newValue">The new value for the <see cref="TEntity"/> with id <paramref name="id"/>.</param>
      /// <param name="dtoToEntityProjector">
      /// Expression to convert the given <see cref="TDTO"/> to an <see cref="TEntity"/>.
      /// If this value is null, the <see cref="DTOToEntityProjector"/> property is used.
      /// </param>
      /// <param name="entityToDTOProjector">
      /// Expression to convert the <see cref="TEntity"/> to return to a <see cref="TDTO"/>.
      /// If this value is null, the <see cref="EntityToDTOProjector"/> property is used.
      /// </param>
      /// <returns>The updated <see cref="TEntity"/> converted to a <see cref="TDTO"/>.</returns>
      /// <exception cref="NotFoundException">There is no element found with the id <paramref name="id"/>.</exception>
      public virtual async Task<TDTO> UpdateAsync(
         Guid id,
         TDTO newValue,
         Expression<Func<TDTO, TEntity>> dtoToEntityProjector = null,
         Expression<Func<TEntity, TDTO>> entityToDTOProjector = null)
      {
         // set the projectors if they are null
         if (entityToDTOProjector == null)
            entityToDTOProjector = EntityToDTOProjector;
         if (dtoToEntityProjector == null)
            dtoToEntityProjector = DTOToEntityProjector;

         // search for the item to update
         var itemToUpdate = await DbSet.FindAsync(id);
         if (itemToUpdate == null)
            throw new NotFoundException();

         // update the item
         itemToUpdate = dtoToEntityProjector.Compile()(newValue);
         itemToUpdate.Id = id;
         var tracker = DbSet.Update(itemToUpdate);
         await Context.SaveChangesAsync();

         //return the updated entity
         return entityToDTOProjector.Compile()(tracker.Entity);
      }

      /// <summary>
      /// Deletes one <see cref="TEntity"/> from the database. This <see cref="TEntity"/> is defined by the <paramref name="id"/>.
      /// </summary>
      /// <param name="id">The id of the <see cref="TEntity"/> to delete.</param>
      /// <returns></returns>
      /// <exception cref="NotFoundException">There is no element found with the id <paramref name="id"/>.</exception>
      public virtual async Task DeleteAsync(Guid id)
      {
         // search for the entity to delete
         var itemToDelete = await DbSet.FindAsync(id);
         if (itemToDelete == null)
            throw new NotFoundException();

         // delete the entity
         DbSet.Remove(itemToDelete);
         await Context.SaveChangesAsync();
      }

      #endregion METHODS
   }
}

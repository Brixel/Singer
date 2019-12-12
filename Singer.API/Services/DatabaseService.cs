using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Singer.Data;
using Singer.Helpers;
using Singer.Helpers.Exceptions;
using Singer.Helpers.Extensions;
using Singer.Models;
using Singer.Models.Users;
using Singer.Resources;
using Singer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Singer.Services
{
   /// <summary>
   ///     Abstract class that implements the <see cref="IDatabaseService{TEntity, TDTO}"/>. It
   ///     implements the basic database manipulation methods and properties.
   /// </summary>
   /// <typeparam name="TEntity">The type of the entity to manipulate in the database.</typeparam>
   /// <typeparam name="TDTO">The type that will be exposed to the outside world.</typeparam>
   /// <typeparam name="TCreateDTO">
   ///     The type that is used to create new entities in the database.
   /// </typeparam>
   public abstract class DatabaseService<TEntity, TDTO, TCreateDTO, TUpdateDTO> : IDatabaseService<TEntity, TDTO, TCreateDTO, TUpdateDTO>
      where TEntity : class, IIdentifiable
      where TDTO : class, IIdentifiable
      where TCreateDTO : class
      where TUpdateDTO : class
   {
      #region CONSTRUCTOR

      /// <summary>
      ///     Constructs a new instance of the <see cref="DatabaseService{TEntity, TDTO}"/> class.
      /// </summary>
      /// <param name="context">The context in which the database is approachable.</param>
      /// <param name="mapper">
      ///     The mapper to map the <see cref="TEntity"/> s to <see cref="TDTO"/> s and vice versa.
      /// </param>
      protected DatabaseService(ApplicationDbContext context, IMapper mapper)
      {
         Context = context;
         Mapper = mapper;
      }

      #endregion CONSTRUCTOR

      #region PROPERTIES

      /// <summary>
      ///     Set that contains the entities in the database. Use this to operate on the database.
      /// </summary>
      protected abstract DbSet<TEntity> DbSet { get; }

      /// <summary>
      ///     Queryable object to allow direct querying on the database. Typically has the required Includes
      /// </summary>
      protected abstract IQueryable<TEntity> Queryable { get; }

      /// <summary>The context in which the database is approachable.</summary>
      protected ApplicationDbContext Context { get; }

      /// <summary>
      ///     The mapper to map the <see cref="TEntity"/> s to <see cref="TDTO"/> s and vice versa.
      /// </summary>
      protected IMapper Mapper { get; }

      protected RoleManager<User> RoleManager { get; }

      #endregion PROPERTIES

      #region METHODS

      /// <summary>Creates an expression to filter the database by using the string input.</summary>
      /// <param name="filter">String value to filter the database by.</param>
      /// <returns>An expression to filter the database by.</returns>
      protected abstract Expression<Func<TEntity, bool>> Filter(string filter);

      /// <summary>
      ///     Creates a <see cref="TDTO"/> in the database by converting it to a
      ///     <see cref="TEntity"/>. It will automatically generate an id.
      /// </summary>
      /// <param name="dto">The new value for the database. This will be converted to an entity.</param>
      /// <returns>The new created <see cref="TEntity"/> converted to a <see cref="TDTO"/>.</returns>
      public virtual async Task<TDTO> CreateAsync(TCreateDTO dto)
      {
         // project the DTO to an entity
         var entity = Mapper.Map<TEntity>(dto);
         var changeTracker = await Context.AddAsync(entity);
         await Context.SaveChangesAsync();
         return Mapper.Map<TDTO>(changeTracker.Entity);
      }

      /// <summary>Returns one <see cref="TEntity"/> from the database, converted to a <see cref="TDTO"/>.</summary>
      /// <param name="id">The id of the <see cref="TEntity"/> to get.</param>
      /// <returns>
      ///     The <see cref="TEntity"/> in the database with id <paramref name="id"/> converted to a <see cref="TDTO"/>.
      /// </returns>
      /// <exception cref="NotFoundException">There is no element found with the id <paramref name="id"/>.</exception>
      public virtual async Task<TDTO> GetOneAsync(Guid id)
      {
         // search for the entity with the given id in the database
         var item = await Queryable.SingleOrDefaultAsync(x => x.Id == id);
         if (item == null)
            throw new NotFoundException("Entity was not found", ErrorMessages.NotFoundError);

         // return the found entity projected to it's DTO
         return Mapper.Map<TDTO>(item);
      }

      /// <summary>
      ///     Returns all the <see cref="TEntity"/> s stored in the database, converted to
      ///     <see cref="TDTO"/> s.
      /// </summary>
      /// <returns>
      ///     All the <see cref="TEntity"/> s in the database, converted to <see cref="TDTO"/> s.
      /// </returns>
      public virtual async Task<IReadOnlyList<TDTO>> GetAllAsync()
      {
         // fetch all the entities from the database
         var items = await Queryable
            .ProjectTo<TDTO>(Mapper.ConfigurationProvider)
            .ToListAsync();

         // return all the items
         return new ReadOnlyCollection<TDTO>(items);
      }

      /// <summary>
      ///     Returns a selection of <see cref="TEntity"/> s, converted <see cref="TDTO"/> s. The
      ///     selection is made by filtering the in the database, ordering the collection and
      ///     finally selecting a pageIndex.
      ///     <list type="number">
      ///         <item>
      ///             <term>Filtering</term>
      ///             <description>
      ///                 The collection will be filtered by comparing all relevant fields to the <paramref name="filter"/>.
      ///             </description>
      ///         </item>
      ///         <item>
      ///             <term>Projecting</term>
      ///             <description>The collection will be projected on the <see cref="TDTO"/>.</description>
      ///         </item>
      ///         <item>
      ///             <term>Sorting</term>
      ///             <description>
      ///                 The collection will be sorted by the <paramref name="orderer"/> in the
      ///                 direction specified with the <paramref name="sortDirection"/>.
      ///             </description>
      ///         </item>
      ///         <item>
      ///             <term>Paging</term>
      ///             <description>
      ///                 Finally a pageIndex will be selected from the collection so that not all
      ///                 elements should be returned.
      ///             </description>
      ///         </item>
      ///     </list>
      /// </summary>
      /// <param name="filter">
      ///     The string value to compare to the properties of the <see cref="TEntity"/> s.
      /// </param>
      /// ///
      /// <param name="orderer">The column to sort the returned list on.</param>
      /// <param name="sortDirection">The direction to sort the column on.</param>
      /// <param name="pageIndex">The pageIndex-number to return.</param>
      /// <param name="entitiesPerPage">The number of entities on a pageIndex.</param>
      /// <returns></returns>
      public virtual async Task<SearchResults<TDTO>> GetAsync(
         string filter = null,
         Expression<Func<TDTO, object>> orderer = null,
         ListSortDirection sortDirection = ListSortDirection.Ascending,
         int pageIndex = 0,
         int entitiesPerPage = 15)
      {
         // return the paged results
         return await Queryable
            .ToPagedListAsync(
               Mapper,
               filterExpression: Filter(filter),
               orderByLambda: orderer,
               sortDirection: sortDirection,
               pageIndex: pageIndex,
               pageSize: entitiesPerPage);
      }

      /// <summary>
      ///     Updates a single <see cref="TEntity"/> in the database. This <see cref="TEntity"/> is
      ///     defined by the <paramref name="id"/>.
      /// </summary>
      /// <param name="id">The identifier of the <see cref="TEntity"/>.</param>
      /// <param name="newValue">The new value for the <see cref="TEntity"/> with id <paramref name="id"/>.</param>
      /// <returns>The updated <see cref="TEntity"/> converted to a <see cref="TDTO"/>.</returns>
      /// <exception cref="NotFoundException">There is no element found with the id <paramref name="id"/>.</exception>
      public virtual async Task<TDTO> UpdateAsync(Guid id, TUpdateDTO newValue)
      {
         // search for the item to update
         var itemToUpdate = await Queryable.SingleOrDefaultAsync(x => x.Id == id);
         if (itemToUpdate == null)
            throw new NotFoundException("Entity was not found", ErrorMessages.NotFoundError);

         // update the item
         itemToUpdate = Mapper.Map(newValue, itemToUpdate);
         var tracker = DbSet.Update(itemToUpdate);
         await Context.SaveChangesAsync();

         //return the updated entity
         return Mapper.Map<TDTO>(tracker.Entity);
      }

      /// <summary>
      ///     Deletes one <see cref="TEntity"/> from the database. This <see cref="TEntity"/> is
      ///     defined by the <paramref name="id"/>.
      /// </summary>
      /// <param name="id">The id of the <see cref="TEntity"/> to delete.</param>
      /// <returns></returns>
      /// <exception cref="NotFoundException">There is no element found with the id <paramref name="id"/>.</exception>
      public virtual async Task DeleteAsync(Guid id)
      {
         // search for the entity to delete
         var itemToDelete = await DbSet.FindAsync(id);
         if (itemToDelete == null)
            throw new NotFoundException("Entity was not found", ErrorMessages.NotFoundError);

         // delete the entity
         DbSet.Remove(itemToDelete);
         await Context.SaveChangesAsync();
      }

      /// <summary>
      ///     Archives the <see cref="TEntity"/> with the given id. If it is not an
      ///     <see cref="IArchivable"/>, an exception is thrown.
      /// </summary>
      /// <param name="id">The id fo the <see cref="TEntity"/> to archive.</param>
      /// <returns></returns>
      /// <exception cref="NotFoundException">There is no element found with the id <paramref name="id"/>.</exception>
      /// <exception cref="InvalidOperationException">
      ///     The given entity does not support archiving (doesn't implement the
      ///     <see cref="IArchivable"/> interface.
      /// </exception>
      public virtual async Task ArchiveAsync(Guid id)
      {
         var itemToArchive = await Queryable.SingleOrDefaultAsync(x => x.Id == id);
         if (itemToArchive == null)
            throw new NotFoundException("Entity was not found", ErrorMessages.NotFoundError);

         if (!(itemToArchive is IArchivable archivable))
            throw new InvalidOperationException("Cannot archive an item that does not implement the IArchivable.");

         archivable.IsArchived = true;
         DbSet.Update(archivable as TEntity);
         await Context.SaveChangesAsync();
      }

      #endregion METHODS
   }
}

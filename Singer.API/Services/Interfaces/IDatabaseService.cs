using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Singer.Helpers;
using Singer.Helpers.Exceptions;
using Singer.Models;

namespace Singer.Services.Interfaces
{
   /// <summary>
   /// Interface that describes the methods to read and write to a database.
   /// </summary>
   /// <typeparam name="TEntity">The type of the entity to manipulate in the database.</typeparam>
   /// <typeparam name="TDTO">The type that will be exposed to the outside world.</typeparam>
   /// <typeparam name="TCreateDTO">The type that is used to create new entities in the database.</typeparam>
   /// <typeparam name="TUpdateDTO">The type that is used to update entities in the database.</typeparam>
   public interface IDatabaseService<TEntity, TDTO, TCreateDTO, TUpdateDTO>
      where TEntity : class, IIdentifiable
      where TDTO : class, IIdentifiable
      where TCreateDTO : class
      where TUpdateDTO : class
   {
      /// <summary>
      /// Expression that is used to convert an <see cref="TEntity"/> to a <see cref="TDTO"/> when returning values from the database.
      /// </summary>
      Expression<Func<TEntity, TDTO>> EntityToDTOProjector { get; }

      /// <summary>
      /// Expression that is used to convert a <see cref="TDTO"/> to an <see cref="TEntity"/> when manipulating values in the database.
      /// </summary>
      Expression<Func<TDTO, TEntity>> DTOToEntityProjector { get; }

      /// <summary>
      /// Expression that is used to convert a <see cref="TCreateDTO"/> to an <see cref="TEntity"/> when creating entities in the database.
      /// </summary>
      Expression<Func<TCreateDTO, TEntity>> CreateDTOToEntityProjector { get; }


      /// <summary>
      /// Creates a <see cref="TDTO"/> in the database by converting it to a <see cref="TEntity"/>.
      /// It will automatically generate an id.
      /// </summary>
      /// <param name="dto">The new value for the databse. This will be converted to an entity.</param>
      /// <param name="dtoToEntityProjector">
      /// Expression to convert the given <see cref="TDTO"/> to an <see cref="TEntity"/>.
      /// If this value is null, the <see cref="DTOToEntityProjector"/> property is used.
      /// </param>
      /// <param name="entityToDTOProjector">
      /// Expression to convert the <see cref="TEntity"/> to return to a <see cref="TDTO"/>.
      /// If this value is null, the <see cref="EntityToDTOProjector"/> property is used.
      /// </param>
      /// <returns>The new created <see cref="TEntity"/> converted to a <see cref="TDTO"/>.</returns>
      Task<TDTO> CreateAsync(TCreateDTO dto,
         Expression<Func<TCreateDTO, TEntity>> dtoToEntityProjector = null,
         Expression<Func<TEntity, TDTO>> entityToDTOProjector = null);

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
      Task<TDTO> GetOneAsync(Guid id, Expression<Func<TEntity, TDTO>> projector = null);

      /// <summary>
      /// Returns all the <see cref="TEntity"/>s stored in the database, converted to <see cref="TDTO"/>s.
      /// </summary>
      /// <param name="projector">
      /// Expression to convert the <see cref="TEntity"/> to return to a <see cref="TDTO"/>.
      /// If this value is null, the <see cref="EntityToDTOProjector"/> property is used.
      /// </param>
      /// <returns>All the <see cref="TEntity"/>s in the database, converted to <see cref="TDTO"/>s.</returns>
      Task<IReadOnlyList<TDTO>> GetAllAsync(Expression<Func<TEntity, TDTO>> projector = null);

      /// <summary>
      /// Returns a selection of <see cref="TEntity"/>s, converted <see cref="TDTO"/>s. The selection is made by filtering the
      /// in the database, ordering the collection and finaly selecting a pageIndex.
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
      ///      <description>Finaly a pageIndex will be selected from the collection so that not all elements should be returned.</description>
      ///   </item>
      /// </list>
      /// </summary>
      /// <param name="filter">The string value to compare to the properties of the <see cref="TEntity"/>s.</param>
      /// <param name="projector">The <see cref="Expression"/> to project the <see cref="TEntity"/>s to the <see cref="TDTO"/>s.</param>
      /// <param name="orderer">The column to sort the returned list on.</param>
      /// <param name="sortDirection">The direction to sort the column on.</param>
      /// <param name="pageIndex">The pagenumber to return.</param>
      /// <param name="itemsPerPage">The number of items on a pageIndex.</param>
      /// <returns></returns>
      Task<SearchResults<TDTO>> GetAsync(
         string filter = null,
         Expression<Func<TEntity, TDTO>> projector = null,
         Expression<Func<TDTO, object>> orderer = null,
         ListSortDirection sortDirection = ListSortDirection.Ascending,
         int pageIndex = 0,
         int itemsPerPage = 15);

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
      Task<TDTO> UpdateAsync(
         Guid id,
         TUpdateDTO newValue,
         Expression<Func<TUpdateDTO, TEntity>> dtoToEntityProjector = null,
         Expression<Func<TEntity, TDTO>> entityToDTOProjector = null);

      /// <summary>
      /// Deletes one <see cref="TEntity"/> from the database. This <see cref="TEntity"/> is defined by the <paramref name="id"/>.
      /// </summary>
      /// <param name="id">The id of the <see cref="TEntity"/> to delete.</param>
      /// <returns></returns>
      /// <exception cref="NotFoundException">There is no element found with the id <paramref name="id"/>.</exception>
      Task DeleteAsync(Guid id);
   }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Singer.Helpers;
using Singer.Helpers.Exceptions;
using Singer.Models;

namespace Singer.Services.Interfaces;

/// <summary>Interface that describes the methods to read and write to a database.</summary>
/// <typeparam name="TEntity">The type of the entity to manipulate in the database.</typeparam>
/// <typeparam name="TDTO">The type that will be exposed to the outside world.</typeparam>
/// <typeparam name="TCreateDTO">
///     The type that is used to create new entities in the database.
/// </typeparam>
/// <typeparam name="TUpdateDTO">The type that is used to update entities in the database.</typeparam>
public interface IDatabaseService<TEntity, TDTO, TCreateDTO, TUpdateDTO>
  where TEntity : class, IIdentifiable
  where TDTO : class, IIdentifiable
  where TCreateDTO : class
  where TUpdateDTO : class
{
    /// <summary>
    ///     Creates a <see cref="TDTO"/> in the database by converting it to a
    ///     <see cref="TEntity"/>. It will automatically generate an id.
    /// </summary>
    /// <param name="dto">The new value for the databse. This will be converted to an entity.</param>
    /// <returns>The new created <see cref="TEntity"/> converted to a <see cref="TDTO"/>.</returns>
    Task<TDTO> CreateAsync(TCreateDTO dto);

    /// <summary>Returns one <see cref="TEntity"/> from the database, converted to a <see cref="TDTO"/>.</summary>
    /// <param name="id">The id of the <see cref="TEntity"/> to get.</param>
    /// <returns>
    ///     The <see cref="TEntity"/> in the database with id <paramref name="id"/> converted to a <see cref="TDTO"/>.
    /// </returns>
    /// <exception cref="NotFoundException">There is no element found with the id <paramref name="id"/>.</exception>
    Task<TDTO> GetOneAsync(Guid id);

    /// <summary>
    ///     Returns all the <see cref="TEntity"/> s stored in the database, converted to
    ///     <see cref="TDTO"/> s.
    /// </summary>
    /// <param name="showArchived">Indicates whether archived entities should also be returned.</param>
    /// <returns>
    ///     All the <see cref="TEntity"/> s in the database, converted to <see cref="TDTO"/> s.
    /// </returns>
    Task<IReadOnlyList<TDTO>> GetAllAsync(bool showArchived = false);

    /// <summary>
    ///     Returns a selection of <see cref="TEntity"/> s, converted <see cref="TDTO"/> s. The
    ///     selection is made by filtering the in the database, ordering the collection and finaly
    ///     selecting a pageIndex.
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
    ///                 Finaly a pageIndex will be selected from the collection so that not all
    ///                 elements should be returned.
    ///             </description>
    ///         </item>
    ///     </list>
    /// </summary>
    /// <param name="filter">
    ///     The string value to compare to the properties of the <see cref="TEntity"/> s.
    /// </param>
    /// <param name="orderer">The column to sort the returned list on.</param>
    /// <param name="sortDirection">The direction to sort the column on.</param>
    /// <param name="pageIndex">The pagenumber to return.</param>
    /// <param name="itemsPerPage">The number of items on a pageIndex.</param>
    /// <param name="showArchived">Indicates whether archived entities should also be returned.</param>
    /// <returns></returns>
    Task<SearchResults<TDTO>> GetAsync(
       string filter = null,
       Expression<Func<TDTO, object>> orderer = null,
       ListSortDirection sortDirection = ListSortDirection.Ascending,
       int pageIndex = 0,
       int itemsPerPage = 15,
       bool showArchived = false);

    /// <summary>
    ///     Updates a single <see cref="TEntity"/> in the database. This <see cref="TEntity"/> is
    ///     defined by the <paramref name="id"/>.
    /// </summary>
    /// <param name="id">The identifier of the <see cref="TEntity"/>.</param>
    /// <param name="newValue">The new value for the <see cref="TEntity"/> with id <paramref name="id"/>.</param>
    /// <returns>The updated <see cref="TEntity"/> converted to a <see cref="TDTO"/>.</returns>
    /// <exception cref="NotFoundException">There is no element found with the id <paramref name="id"/>.</exception>
    Task<TDTO> UpdateAsync(
       Guid id,
       TUpdateDTO newValue);

    /// <summary>
    ///     Deletes one <see cref="TEntity"/> from the database. This <see cref="TEntity"/> is
    ///     defined by the <paramref name="id"/>.
    /// </summary>
    /// <param name="id">The id of the <see cref="TEntity"/> to delete.</param>
    /// <returns></returns>
    /// <exception cref="NotFoundException">There is no element found with the id <paramref name="id"/>.</exception>
    Task DeleteAsync(Guid id);

    /// <summary>
    ///     Archives the <see cref="TEntity"/> with the given id. If it is not an
    ///     <see cref="IArchivable"/>, an exception is thrown.
    /// </summary>
    /// <param name="id">The id of the <see cref="TEntity"/> to archive.</param>
    /// <returns></returns>
    /// <exception cref="NotFoundException">There is no element found with the id <paramref name="id"/>.</exception>
    /// <exception cref="InvalidOperationException">
    ///     The given entity does not support archiving (doesn't implement the
    ///     <see cref="IArchivable"/> interface.
    /// </exception>
    Task ArchiveAsync(Guid id);
}

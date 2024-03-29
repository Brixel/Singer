using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Singer.Helpers.Exceptions;
using Singer.Models;
using Singer.Resources;
using Singer.Services;

namespace Singer.Helpers.Extensions;

// ReSharper disable once InconsistentNaming
public static class IQueryableExtensions
{
    public static IQueryable<T> Filter<T>(this IQueryable<T> source, StringFilter<T> filter)
    {
        return filter == null || filter.PropertyList.Count == 0
           ? source
           : source.Where(filter.GetFilterExpression());
    }

    public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
    {
        return source.OrderBy(ToLambda<T>(propertyName));
    }

    public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string propertyName)
    {
        return source.ThenBy(ToLambda<T>(propertyName));
    }

    public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
    {
        return source.OrderByDescending(ToLambda<T>(propertyName));
    }

    private static Expression<Func<T, object>> ToLambda<T>(string propertyName)
    {
        var parameter = Expression.Parameter(typeof(T));
        var property = Expression.Property(parameter, propertyName);
        var propAsObject = Expression.Convert(property, typeof(object));

        return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
    }

    public static IQueryable<T> TakePage<T>(this IOrderedQueryable<T> orderedQueryable, int pageIndex, int pageSize)
    {
        return orderedQueryable
           .Skip(pageIndex * pageSize)
           .Take(pageSize);
    }

    public static SearchResults<TProjection> ToPagedList<TEntity, TProjection>(
          this IQueryable<TEntity> queryable,
          Expression<Func<TEntity, bool>> filterExpression,
          Expression<Func<TEntity, TProjection>> projectionExpression,
          Expression<Func<TProjection, object>> orderByLambda,
          string sortDirectionString,
          int pageIndex = 1,
          int pageSize = 20)
    {
        if (pageSize < 1)
            throw new BadInputException("PageSize should be positive", ErrorMessages.PageSizeLessThanOne);

        var filteredQueryable = queryable.Where(filterExpression);
        var totalItemsCount = filteredQueryable.Count();

        var sortDirection = "desc".Equals(sortDirectionString, StringComparison.InvariantCultureIgnoreCase)
                            || "descending".Equals(sortDirectionString, StringComparison.InvariantCultureIgnoreCase)
            ? ListSortDirection.Descending
            : ListSortDirection.Ascending;

        IReadOnlyList<TProjection> items = new List<TProjection>();
        if (totalItemsCount > 0)
        {
            var projection = filteredQueryable.Select(projectionExpression);
            var orderedQueryable = sortDirection == ListSortDirection.Ascending ? projection.OrderBy(orderByLambda) : projection.OrderByDescending(orderByLambda);

            items = orderedQueryable.TakePage(pageIndex, pageSize).ToList();
        }

        return new SearchResults<TProjection>(items, totalItemsCount, pageIndex);
    }

    public static Task<SearchResults<TProjection>> ToPagedListAsync<TEntity, TProjection>(
       this IQueryable<TEntity> queryable,
       IMapper mapper,
       Expression<Func<TEntity, bool>> filterExpression,
       Expression<Func<TProjection, object>> orderByLambda,
       ListSortDirection sortDirection,
       int pageIndex = 0,
       int pageSize = 15)
    {
        if (mapper == null)
            throw new ArgumentNullException(nameof(mapper));
        if (pageSize < 1)
            throw new BadInputException("Invalid pageSize (-1) provided", ErrorMessages.PageSizeLessThanOne);

        var filteredQueryable = queryable.Where(filterExpression);
        var totalItemsCount = filteredQueryable.Count();

        var items = totalItemsCount <= 0
           ? new List<TProjection>()
           : filteredQueryable
              .ProjectTo<TProjection>(mapper.ConfigurationProvider)
              .OrderBy(orderByLambda, sortDirection)
              .TakePage(pageIndex, pageSize)
              .ToList();
        //.ConfigureAwait(false);

        var result = new SearchResults<TProjection>(items, totalItemsCount, pageIndex);
        return Task.FromResult(result);
    }

    public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, Expression<Func<T, object>> orderByLambda, ListSortDirection sortDirection)
       => sortDirection == ListSortDirection.Ascending
            ? query.OrderBy(orderByLambda)
            : query.OrderByDescending(orderByLambda);
}

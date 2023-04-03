using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using AutoMapper;

using LinqKit;

using Microsoft.EntityFrameworkCore;

using Singer.Data;
using Singer.DTOs;
using Singer.Helpers;
using Singer.Helpers.Extensions;
using Singer.Models;
using Singer.Services.Interfaces;

namespace Singer.Services;

public class SingerLocationService : DatabaseService<SingerLocation, SingerLocationDTO, CreateSingerLocationDTO, UpdateSingerLocationDTO>,
  ISingerLocationService
{
    public SingerLocationService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    protected override DbSet<SingerLocation> DbSet => Context.SingerLocations;

    protected override IQueryable<SingerLocation> Queryable => Context.SingerLocations;

    protected override Expression<Func<SingerLocation, bool>> Filter(string filter)
    {
        if (string.IsNullOrWhiteSpace(filter))
        {
            return o => true;
        }
        Expression<Func<SingerLocation, bool>> filterExpression =
           f =>
              f.Address.Contains(filter) ||
              f.City.Contains(filter) ||
              f.Country.Contains(filter) ||
              f.Name.Contains(filter) ||
              f.PostalCode.Contains(filter);
        return filterExpression;
    }

    public async Task<SearchResults<SingerLocationDTO>> AdvancedSearch(SingerLocationSearchDTO dto)
    {
        var sortColumn = string.IsNullOrEmpty(dto.SortColumn) ? "Id" : dto.SortColumn;
        var orderByLambda = PropertyHelpers.GetPropertySelector<SingerLocationDTO>(sortColumn);
        return await Queryable
        .ToPagedListAsync<SingerLocation, SingerLocationDTO>(
           Mapper,
           filterExpression: Filter(dto),
           orderByLambda: orderByLambda,
           sortDirection: dto.SortDirection,
           pageIndex: dto.PageIndex,
           pageSize: dto.PageSize);
    }

    public Expression<Func<SingerLocation, bool>> Filter(SingerLocationSearchDTO dto)
    {
        var filterPredicate = PredicateBuilder.New<SingerLocation>(true);

        if (!string.IsNullOrWhiteSpace(dto.Text))
        {
            filterPredicate.And(SingerLocationFilter.FilterByText(dto.Text));
        }

        return filterPredicate;
    }
}

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Singer.Data;
using Singer.DTOs;
using Singer.Helpers;
using Singer.Helpers.Extensions;
using Singer.Models;
using Singer.Services.Interfaces;

namespace Singer.Services
{
   public class RegistrationService : DatabaseService<Registration, RegistrationDTO, CreateRegistrationDTO, UpdateRegistrationDTO>
   , IRegistrationService
   {
      public RegistrationService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
      {
      }

      protected override DbSet<Registration> DbSet => Context.Registrations;

      protected override IQueryable<Registration> Queryable => Context.Registrations.AsQueryable();

      protected override Expression<Func<Registration, bool>> Filter(string filter)
      {
         if (string.IsNullOrWhiteSpace(filter))
            return o => true;

         Expression<Func<Registration, bool>> filterExpression =
            f => f.CareUser.User.FirstName.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
               f.CareUser.User.LastName.Contains(filter, StringComparison.OrdinalIgnoreCase);

         return filterExpression;
      }

      public async Task<SearchResults<RegistrationOverviewDTO>> AdvancedSearch(RegistrationSearchDTO dto)
      {
         var sortColumn = string.IsNullOrEmpty(dto.SortColumn) ? "Id" : dto.SortColumn;
         var orderByLambda = PropertyHelpers.GetPropertySelector<RegistrationOverviewDTO>(sortColumn);
         return await Queryable
         .ToPagedListAsync<Registration, RegistrationOverviewDTO>(
            Mapper,
            filterExpression: Filter(dto),
            orderByLambda: orderByLambda,
            sortDirection: dto.SortDirection,
            pageIndex: dto.PageIndex,
            pageSize: dto.PageSize);
      }

      public Expression<Func<Registration, bool>> Filter(RegistrationSearchDTO dto)
      {
         Expression<Func<Registration, bool>> filterExpression = v => true;
         if (!string.IsNullOrWhiteSpace(dto.Text))
         {
            var prefix = filterExpression.Compile();
            filterExpression = v => prefix(v) &&
               v.CareUser.User.FirstName.Contains(dto.Text, StringComparison.OrdinalIgnoreCase) ||
               v.CareUser.User.LastName.Contains(dto.Text, StringComparison.OrdinalIgnoreCase);
         }

         return filterExpression;
      }
   }
}

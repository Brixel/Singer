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

public class RegistrationService : DatabaseService<Registration, RegistrationDTO, CreateRegistrationDTO, UpdateRegistrationDTO>
, IRegistrationService
{
    private readonly IActionNotificationService _actionNotificationService;
    public RegistrationService(ApplicationDbContext context, IMapper mapper, IActionNotificationService actionNotificationService) : base(context, mapper)
    {
        _actionNotificationService = actionNotificationService;
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
        var filterPredicate = PredicateBuilder.New<Registration>(true);

        if (!string.IsNullOrWhiteSpace(dto.Text))
        {
            filterPredicate.And(RegistrationFilter.FilterByText(dto.Text));
        }

        if (dto.CareUserIds != null && dto.CareUserIds.Count > 0)
        {
            filterPredicate.And(RegistrationFilter.FilterByUserIds(dto.CareUserIds));
        }

        if (dto.RegistrationStatus.HasValue)
        {
            filterPredicate.And(RegistrationFilter.FilterByRegistrationStatus(dto.RegistrationStatus.Value));
        }

        if (dto.RegistrationType.HasValue)
        {
            filterPredicate.And(RegistrationFilter.FilterByRegistrationType(dto.RegistrationType.Value));
        }

        if (dto.DateFrom.HasValue)
        {
            filterPredicate.And(RegistrationFilter.FilterByFromDate(dto.DateFrom.Value));
        }

        if (dto.DateTo.HasValue)
        {
            filterPredicate.And(RegistrationFilter.FilterByToDate(dto.DateTo.Value));
        }

        return filterPredicate;
    }


    public async Task<RegistrationStatus> AcceptRegistration(Guid registrationId, Guid executedByUserId)
    {
        var registration = await Context.Registrations.SingleAsync(x => x.Id == registrationId);
        var originalStatus = registration.Status;
        registration.Status = RegistrationStatus.Accepted;

        await _actionNotificationService.RegisterEventRegistrationStatusChange(
           registrationId, executedByUserId, originalStatus, registration.Status);

        await Context.SaveChangesAsync();
        return registration.Status;
    }

    public async Task<RegistrationStatus> RejectRegistration(Guid registrationId, Guid executedByUserId)
    {
        var registration = await Context.Registrations.SingleAsync(x => x.Id == registrationId);
        var previousRegistrationStatus = registration.Status;
        registration.Status = RegistrationStatus.Rejected;

        await _actionNotificationService.RegisterEventRegistrationStatusChange(registrationId, executedByUserId,
           previousRegistrationStatus, registration.Status);

        await Context.SaveChangesAsync();

        return registration.Status;
    }
}

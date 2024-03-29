﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using CsvHelper;

using Duende.IdentityServer.Extensions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Singer.Configuration;
using Singer.DTOs;
using Singer.DTOs.Csv;
using Singer.Helpers;
using Singer.Helpers.Exceptions;
using Singer.Helpers.Extensions;
using Singer.Models;
using Singer.Resources;
using Singer.Services.Interfaces;

namespace Singer.Controllers;

[Authorize]
public class EventController : DataControllerBase<Event, EventDTO, CreateEventDTO, UpdateEventDTO>
{
    #region FIELDS

    private readonly IEventRegistrationService _eventRegistrationService;
    private readonly IDateValidator _dateValidator;
    private readonly IActionNotificationService _actionNotificationService;
    private readonly IEventService _eventService;
    private readonly ICareUserService _careUserService;

    #endregion FIELDS

    #region CONSTRUCTOR

    public EventController(IEventService eventService, IEventRegistrationService eventRegistrationService,
       ICareUserService careUserService, IDateValidator dateValidator, IActionNotificationService actionNotificationService)
       : base(eventService)
    {
        _eventRegistrationService = eventRegistrationService;
        _dateValidator = dateValidator;
        _actionNotificationService = actionNotificationService;
        _eventService = eventService;
        _careUserService = careUserService;
    }

    #endregion CONSTRUCTOR

    #region METHODS

    #region post

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = Roles.ROLE_ADMINISTRATOR)]
    public override async Task<IActionResult> Create([FromBody] CreateEventDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        _dateValidator.Validate(dto);

        return await base.Create(dto);
    }

    [HttpPost("{eventId}/registrations")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<RegistrationDTO>>> Create(Guid eventId, [FromBody] CreateRegistrationDTO dto)
    {
        if (eventId != dto.EventId)
            throw new BadInputException("The event id in the url and the body do not match", ErrorMessages.EventIdMismatchUrlBody);

        var model = ModelState;
        if (!model.IsValid)
            return BadRequest(model);

        dto.Status = !User.IsInRole(Roles.ROLE_ADMINISTRATOR)
            ? RegistrationStatus.Pending
            : RegistrationStatus.Accepted;
        var eventRegistration = await _eventRegistrationService.CreateAsync(dto);
        return Created(nameof(Get), eventRegistration);
    }


    [HttpPost("{eventId}/eventslot/{eventSlotId}/registrations")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RegistrationDTO>> Create(Guid eventId, [FromBody] CreateEventSlotRegistrationDTO dto)
    {
        dto.Status = !User.IsInRole(Roles.ROLE_ADMINISTRATOR)
            ? RegistrationStatus.Pending
            : RegistrationStatus.Accepted;
        var checkExisting = await _eventRegistrationService.GetOneBySlotAsync(dto.EventSlotId, dto.CareUserId);
        if (checkExisting != null)
            throw new BadInputException("Care user already registered on this event slot.", ErrorMessages.UserAlreadyRegisteredOnEventSlot);

        var eventSlotRegistration = await _eventRegistrationService.CreateOneBySlotAsync(dto);

        // Since admins automatically approve a registration, it's needed to register this.
        if (User.IsInRole(Roles.ROLE_ADMINISTRATOR))
        {
            var executedByUserId = Guid.Parse(User.GetSubjectId());
            await _actionNotificationService.RegisterEventRegistrationStatusChange(eventSlotRegistration.Id, executedByUserId,
               RegistrationStatus.Pending, eventSlotRegistration.Status);
        }


        return Created(nameof(Get), eventSlotRegistration);
    }

    [HttpPost("{eventId}/registrations/{eventRegistrationId}/accept")]
    [Authorize(Roles = Roles.ROLE_ADMINISTRATOR)]
    public async Task<ActionResult> AcceptRegistration(Guid eventRegistrationId)
    {
        var userId = User.GetUserId();
        var status = await _eventRegistrationService.AcceptRegistration(eventRegistrationId, userId);
        return Ok(status);
    }

    [HttpPost("{eventId}/registrations/{eventRegistrationId}/reject")]
    [Authorize(Roles = Roles.ROLE_ADMINISTRATOR)]
    public async Task<ActionResult> RejectRegistration(Guid eventRegistrationId)
    {
        var userId = User.GetUserId();
        var status = await _eventRegistrationService.RejectRegistration(eventRegistrationId, userId);
        return Ok(status);
    }

    #endregion post

    #region get

    [HttpGet("{eventId}/registrations")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = Roles.ROLE_ADMINISTRATOR)]
    public async Task<ActionResult<PaginationDTO<EventSlotRegistrationsDTO>>> Get(
       Guid eventId,
       string filter,
       string sortDirection = "0",
       string sortColumn = "Id",
       int pageIndex = 0,
       int pageSize = 15)
    {
        if (sortDirection == "asc")
            sortDirection = "0";
        if (sortDirection == "desc")
            sortDirection = "1";
        if (!Enum.TryParse<ListSortDirection>(sortDirection, true, out var direction))
            throw new BadInputException("The given sort-direction is unknown.", ErrorMessages.UnknownSortDirection);

        var orderByLambda = PropertyHelpers.GetPropertySelector<EventSlotRegistrationsDTO>(sortColumn);

        // get the search results of the database query
        var result = await _eventRegistrationService
           .GetAsync(
              eventId,
              filter,
              orderer: orderByLambda,
              sortDirection: direction,
              pageIndex: pageIndex,
              itemsPerPage: pageSize)
           .ConfigureAwait(false);

        var requestPath = HttpContext.Request.Path;
        var nextPage = (pageIndex * pageSize) + result.Size >= result.TotalCount
           ? null
           : $"{requestPath}?PageIndex={pageIndex++}&Size={pageSize}";

        // create object that holds the paginated elements
        var page = new PaginationDTO<EventSlotRegistrationsDTO>
        {
            Items = result.Items,
            Size = result.Items.Count,
            PageIndex = pageIndex,
            CurrentPageUrl = $"{requestPath}?{HttpContext.Request.QueryString}",
            NextPageUrl = nextPage,
            PreviousPageUrl = pageIndex == 0
              ? null
              : $"{requestPath}?PageIndex={pageIndex--}&Size={pageSize}",
            TotalSize = result.TotalCount
        };

        return Ok(page);
    }

    [HttpGet("{eventId}/registrations/{registrationId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RegistrationDTO>> GetOne(Guid eventId, Guid registrationId)
    {
        var registration = await _eventRegistrationService
           .GetOneAsync(eventId, registrationId)
           .ConfigureAwait(false);

        return Ok(registration);
    }

    [HttpGet("{eventId}/registrations/{eventSlotId}/deelnemerslijst.csv")]
    [Authorize(Roles = Roles.ROLE_ADMINISTRATOR)]
    public async Task<ActionResult> GetRegistrationsAsCsv(Guid eventId, Guid eventSlotId)
    {
        var list = await _eventRegistrationService.GetParticipantsForSlotAsync(eventId, eventSlotId);

        using var writer = new StringWriter();
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

        csv.Context.RegisterClassMap<CsvRegistrationDTO.Mapper>();
        csv.WriteRecords(list);

        return Ok(writer.ToString());
    }

    #endregion get

    #region put

    /// <summary>Updates a single entity in the database.</summary>
    /// <param name="id">The id of the entity to update.</param>
    /// <param name="dto">The new value of the entity.</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = Roles.ROLE_ADMINISTRATOR)]
    public override async Task<IActionResult> Update(Guid id, [FromBody] UpdateEventDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        _dateValidator.Validate(dto);

        return await base.Update(id, dto);
    }

    [HttpPut("{eventId}/registrations/{registrationId}/status")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = Roles.ROLE_ADMINISTRATOR)]
    public async Task<ActionResult<RegistrationDTO>> Update(Guid eventId, Guid registrationId, [FromBody] RegistrationStatus status)
    {
        var model = ModelState;
        if (!model.IsValid)
            return BadRequest(model);

        var result = await _eventRegistrationService
           .UpdateStatusAsync(eventId, registrationId, status)
           .ConfigureAwait(false);

        return Ok(result);
    }

    [HttpPut("{eventId}/registrations/{registrationId}/location")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = Roles.ROLE_ADMINISTRATOR)]
    public async Task<ActionResult<DaycareLocationDTO>> Update(Guid registrationId, [FromBody] Guid locationId)
    {
        var userId = User.GetUserId();
        var result = await _eventRegistrationService.UpdateDaycareLocationForRegistration(
           registrationId, locationId, userId);
        return Ok(result);
    }

    #endregion put

    #region delete

    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = Roles.ROLE_ADMINISTRATOR)]
    public override async Task<IActionResult> Delete(Guid id)
    {
        await DatabaseService.ArchiveAsync(id);
        return NoContent();
    }

    [HttpDelete("{eventId}/registrations/{registrationId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = Roles.ROLE_ADMINISTRATOR)]
    public async Task<ActionResult> Delete(Guid eventId, Guid registrationId)
    {
        await _eventRegistrationService
           .DeleteAsync(eventId, registrationId)
           .ConfigureAwait(false);

        return NoContent();
    }

    #endregion delete

    [HttpPost("search")]
    public async Task<IActionResult> GetPublicEvents([FromBody] EventFilterParametersDTO searchEventParams)
    {
        var model = ModelState;
        if (!model.IsValid)
            return BadRequest(model);

        var events = await _eventService
           .GetPublicEventsAsync(searchEventParams)
           .ConfigureAwait(false);

        return Ok(events);
    }

    [HttpGet("{eventId}/isuserregistered/{careUserId}")]
    public async Task<ActionResult<UserRegisteredDTO>> IsUserRegisteredForEvent(Guid eventId, Guid careUserId)
    {
        var userRegistrationStatus = await _eventRegistrationService.GetUserRegistrationStatus(eventId, careUserId);
        return Ok(userRegistrationStatus);
    }

    [HttpGet("{eventId}/geteventregisterdetails")]
    public async Task<ActionResult<EventRegisterDetailsDTO>> GetEventRegisterDetails(Guid eventId)
    {
        var singerEvent = await _eventService.GetOneAsync(eventId);
        var details = new EventRegisterDetailsDTO
        {
            AgeGroups = singerEvent.AllowedAgeGroups,
            Description = singerEvent.Description,
            EndDate = singerEvent.EndDateTime,
            EventSlots = null,
            Id = singerEvent.Id,
            RegistrationOnDailyBasis = singerEvent.RegistrationOnDailyBasis,
            StartDate = singerEvent.StartDateTime,
            Title = singerEvent.Title
        };
        var careUsers = new List<EventRelevantCareUserDTO>();
        if (User.IsInRole(Roles.ROLE_LEGALGUARDIANUSER))
        {
            var legalGuardianUserId = User.GetUserId();
            careUsers = await _careUserService.GetCareUsersForLegalGuardianAsync(legalGuardianUserId);

            foreach (var user in careUsers)
            {
                user.AppropriateAgeGroup = singerEvent.AllowedAgeGroups.Contains(user.AgeGroup);
            }
        }
        else if (User.IsInRole(Roles.ROLE_ADMINISTRATOR))
        {
            careUsers = await _careUserService.GetCareUsersInAgeGroups(singerEvent.AllowedAgeGroups);
        }

        details.RelevantCareUsers = careUsers;

        var registrations = await _eventRegistrationService.GetAllSlotsForEventAsync(eventId);
        details.EventSlots = singerEvent.EventSlots.Select(eventSlot => new EventSlotRegistrationsDTO
        {
            EndDateTime = eventSlot.EndDateTime,
            Id = eventSlot.Id,
            StartDateTime = eventSlot.StartDateTime,
            Registrations = registrations
              .Where(registration => registration.EventSlot.Id == eventSlot.Id)
              .Select(registration => new EventCareUserRegistrationDTO
              {
                  RegistrationId = registration.Id,
                  CareUserId = registration.CareUser.Id,
                  FirstName = registration.CareUser.FirstName,
                  LastName = registration.CareUser.LastName,
                  Status = registration.Status
              }).ToList()
        }).ToList();

        return Ok(details);
    }

    [HttpGet("registrations/status/pending")]
    [Authorize(Roles = Roles.ROLE_ADMINISTRATOR)]
    public async Task<ActionResult<PaginationDTO<RegistrationDTO>>> GetPendingRegistrations(
       ListSortDirection sortDirection = ListSortDirection.Ascending,
       string sortColumn = "Id",
       int pageIndex = 0,
       int pageSize = 15)
    {
        var orderByLambda = PropertyHelpers.GetPropertySelector<RegistrationDTO>(sortColumn);
        var result = await _eventRegistrationService.GetPendingRegistrations(orderByLambda, sortDirection, pageSize, pageIndex);
        var requestPath = HttpContext.Request.Path;
        var nextPage = (pageIndex * pageSize) + result.Size >= result.TotalCount
           ? null
           : $"{requestPath}?PageIndex={pageIndex++}&Size={pageSize}";
        var page = new PaginationDTO<RegistrationDTO>
        {
            Items = result.Items,
            Size = result.Items.Count,
            PageIndex = pageIndex,
            CurrentPageUrl = $"{requestPath}?{HttpContext.Request.QueryString}",
            NextPageUrl = nextPage,
            PreviousPageUrl = pageIndex == 0
              ? null
              : $"{requestPath}?PageIndex={pageIndex--}&Size={pageSize}",
            TotalSize = result.TotalCount
        };

        return Ok(page);
    }

    #endregion METHODS
}

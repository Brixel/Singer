using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Singer.DTOs;
using Singer.Helpers;
using Singer.Helpers.Exceptions;
using Singer.Models;
using Singer.Services.Interfaces;
using System;
using System.ComponentModel;
using Singer.Configuration;
using System.Collections.Generic;
using System.Linq;
using Singer.Resources;

namespace Singer.Controllers
{
   [Authorize]
   public class EventController : DataControllerBase<Event, EventDTO, CreateEventDTO, UpdateEventDTO>
   {
      #region FIELDS

      private readonly IEventRegistrationService _eventRegistrationService;
      private readonly IDateValidator _dateValidator;
      private readonly IEventService _eventService;
      private readonly ICareUserService _careUserService;

      #endregion FIELDS


      #region CONSTRUCTOR

      public EventController(IEventService eventService, IEventRegistrationService eventRegistrationService,
         ICareUserService careUserService, IDateValidator dateValidator)
         : base(eventService)
      {
         _eventRegistrationService = eventRegistrationService;
         _dateValidator = dateValidator;
         _eventService = eventService;
         _careUserService = careUserService;
      }

      #endregion CONSTRUCTOR


      #region METHODS

      #region post

      [HttpPost]
      [ProducesResponseType(StatusCodes.Status201Created)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public override async Task<IActionResult> Create([FromBody]CreateEventDTO dto)
      {
         if (dto is null)
            throw new BadInputException("DTO is null", ErrorMessages.NoDataPassed);
         _dateValidator.Validate(dto);

         return await base.Create(dto);
      }

      [HttpPost("{eventId}/registrations")]
      [ProducesResponseType(StatusCodes.Status201Created)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public async Task<ActionResult<List<EventRegistrationDTO>>> Create(Guid eventId, [FromBody]CreateEventRegistrationDTO dto)
      {
         if (eventId != dto.EventId)
            throw new BadInputException("The event id in the url and the body do not match", ErrorMessages.EventIdMismatchUrlBody);

         var model = ModelState;
         if (!model.IsValid)
            return BadRequest(model);

         if (!User.IsInRole(Roles.ROLE_ADMINISTRATOR))
         {
            //throw new BadInputException("As a non-admin user, you are not allowed to pass a status for the registration!");
            dto.Status = RegistrationStatus.Pending;
         }
         else
         {
            dto.Status = RegistrationStatus.Accepted;
         }
         var eventRegistration = await _eventRegistrationService.CreateAsync(dto);
         return Created(nameof(Get), eventRegistration);
      }

      [HttpPost("{eventId}/eventslot/{eventSlotId}/registrations")]
      [ProducesResponseType(StatusCodes.Status201Created)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public async Task<ActionResult<EventRegistrationDTO>> Create(Guid eventId, [FromBody]CreateEventSlotRegistrationDTO dto)
      {
         if (!User.IsInRole(Roles.ROLE_ADMINISTRATOR))
         {
            //throw new BadInputException("As a non-admin user, you are not allowed to pass a status for the registration!");
            dto.Status = RegistrationStatus.Pending;
         }
         else
         {
            dto.Status = RegistrationStatus.Accepted;
         }
         var checkExisting = await _eventRegistrationService.GetOneBySlotAsync(dto.EventSlotId, dto.CareUserId);
         if (checkExisting != null)
            throw new BadInputException("Care user already registered on this event slot.", ErrorMessages.UserAlreadyRegisteredOnEventSlot);

         var eventSlotRegistration = await _eventRegistrationService.CreateOneBySlotAsync(dto);
         return Created(nameof(Get), eventSlotRegistration);
      }

      #endregion post

      #region get

      [HttpGet("{eventId}/registrations")]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public async Task<ActionResult<PaginationDTO<EventRegistrationDTO>>> Get(
         Guid eventId,
         string filter,
         string sortDirection = "0",
         string sortColumn = "Id",
         int pageIndex = 0,
         int pageSize = 15)
      {
         if (sortDirection == "asc") sortDirection = "0";
         if (sortDirection == "desc") sortDirection = "1";
         if (!Enum.TryParse<ListSortDirection>(sortDirection, true, out var direction))
            throw new BadInputException("The given sort-direction is unknown.", ErrorMessages.UnknownSortDirection);

         var orderByLambda = PropertyHelpers.GetPropertySelector<EventRegistrationDTO>(sortColumn);

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
         var page = new PaginationDTO<EventRegistrationDTO>
         {
            Items = result.Items,
            Size = result.Items.Count,
            PageIndex = pageIndex,
            CurrentPageUrl = $"{requestPath}?{HttpContext.Request.QueryString.ToString()}",
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
      public async Task<ActionResult<EventRegistrationDTO>> GetOne(Guid eventId, Guid registrationId)
      {
         var registration = await _eventRegistrationService
            .GetOneAsync(eventId, registrationId)
            .ConfigureAwait(false);

         return Ok(registration);
      }

      #endregion get

      #region put

      /// <summary>
      /// Updates a single entity in the database.
      /// </summary>
      /// <param name="id">The id of the entity to update.</param>
      /// <param name="dto">The new value of the entity.</param>
      /// <returns></returns>
      [HttpPut("{id}")]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public override async Task<IActionResult> Update(Guid id, [FromBody]UpdateEventDTO dto)
      {
         if (dto is null)
            throw new BadInputException("DTO is null", "Er is geen data meegegeven.");
         _dateValidator.Validate(dto);

         return await base.Update(id, dto);
      }

      [HttpPut("{eventId}/registrations/{registrationId}/status")]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public async Task<ActionResult<EventRegistrationDTO>> Update(Guid eventId, Guid registrationId, [FromBody]RegistrationStatus status)
      {
         var model = ModelState;
         if (!model.IsValid)
            return BadRequest(model);

         var result = await _eventRegistrationService
            .UpdateStatusAsync(eventId, registrationId, status)
            .ConfigureAwait(false);

         return Ok(result);
      }

      #endregion put

      #region delete

      [HttpDelete("{eventId}/registrations/{registrationId}")]
      [ProducesResponseType(StatusCodes.Status204NoContent)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public async Task<ActionResult> Delete(Guid eventId, Guid registrationId)
      {
         await _eventRegistrationService
            .DeleteAsync(eventId, registrationId)
            .ConfigureAwait(false);

         return NoContent();
      }

      #endregion delete

      [HttpPost("search")]
      public async Task<IActionResult> GetPublicEvents([FromBody] SearchEventParamsDTO searchEventParams)
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
         List<EventRelevantCareUserDTO> careUsers;
         if (!User.IsInRole(Roles.ROLE_ADMINISTRATOR))
         {
            var legalGuardianUserId = Guid.Parse(User.GetSubjectId());
            // TODO Validate if the user is a legalguardian
            careUsers = await _careUserService.GetCareUsersForLegalGuardian(legalGuardianUserId);

            foreach (var user in careUsers)
            {
               user.AppropriateAgeGroup = singerEvent.AllowedAgeGroups.Contains(user.AgeGroup);
            }
         }
         else
         {
            careUsers = await _careUserService.GetCareUsersInAgeGroups(singerEvent.AllowedAgeGroups);
         }

         details.RelevantCareUsers = careUsers;

         var registrations = await _eventRegistrationService.GetAllSlotsForEventAsync(eventId);
         details.EventSlots = singerEvent.EventSlots.Select(x => new EventSlotRegistrationsDTO
         {
            EndDateTime = x.EndDateTime,
            Id = x.Id,
            StartDateTime = x.StartDateTime,
            Registrations = registrations.Where(y => y.EventSlot.Id == x.Id).Select(z => new EventCareUserRegistrationDTO
            {
               CareUserId = z.CareUser.Id,
               Status = z.Status
            }).ToList()
         }).ToList();


         return Ok(details);
      }

      #endregion METHODS
   }
}

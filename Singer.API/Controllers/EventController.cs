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

namespace Singer.Controllers
{
   [Authorize]
   public class EventController : DataControllerBase<Event, EventDTO, CreateEventDTO, UpdateEventDTO>
   {
      #region FIELDS

      private readonly IEventRegistrationService _eventRegistrationService;
      private readonly IEventService _eventService;
      private readonly ICareUserService _careUserService;

      #endregion FIELDS


      #region CONSTRUCTOR

      public EventController(IEventService eventService, IEventRegistrationService eventRegistrationService, ICareUserService careUserService)
         : base(eventService)
      {
         _eventRegistrationService = eventRegistrationService;
         _eventService = eventService;
         _careUserService = careUserService;
      }

      #endregion CONSTRUCTOR


      #region METHODS

      #region post

      [HttpPost("{eventId}/registrations")]
      [ProducesResponseType(StatusCodes.Status201Created)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public async Task<ActionResult<List<EventRegistrationDTO>>> Create(Guid eventId, [FromBody]CreateEventRegistrationDTO dto)
      {
         if (eventId != dto.EventId)
            throw new BadInputException("The event id in the url and the body doe not match");

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
         {
            throw new BadInputException("Deze gebruiker is reeds geregistreerd op dit tijdslot!");
         }
         var eventSlotRegistration = await _eventRegistrationService.CreateOneBySlotAsync(dto);
         return Created(nameof(Get), eventSlotRegistration);
      }

      #endregion post

      #region get

      [HttpGet("{eventId}/registrations")]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public async Task<ActionResult<PaginationDTO<EventSlotRegistrationsDTO>>> Get(
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
            throw new BadInputException("The given sort-direction is unknown.");

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

      [HttpPost("{eventId}/registrations/{eventRegistrationId}/accept")]
      public async Task<ActionResult> AcceptRegistration(Guid eventId, Guid eventRegistrationId)
      {
         var status = await _eventRegistrationService.AcceptRegistration(eventRegistrationId);
         return Ok(status);
      }

      [HttpPost("{eventId}/registrations/{eventRegistrationId}/reject")]
      public async Task<ActionResult> RejectRegistration(Guid eventId, Guid eventRegistrationId)
      {
         var status = await _eventRegistrationService.RejectRegistration(eventRegistrationId);
         return Ok(status);
      }

      #endregion get

      #region put

      [HttpPut("{eventId}/registrations/{registrationId}/status")]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public async Task<ActionResult<EventRegistrationDTO>> Update(Guid eventId, Guid registrationId, [FromBody]RegistrationStatus status)
      {
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
         if (model.IsValid)
         {
            var events = await _eventService
               .GetPublicEventsAsync(searchEventParams)
               .ConfigureAwait(false);
            return Ok(events);
         }

         return BadRequest(model);
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

      #endregion METHODS
   }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Singer.DTOs;
using Singer.Helpers;
using Singer.Helpers.Exceptions;
using Singer.Models;
using Singer.Services.Interfaces;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Singer.Controllers
{
   public class EventController : DataControllerBase<Event, EventDTO, CreateEventDTO, UpdateEventDTO>
   {
      #region FIELDS

      private readonly IEventRegistrationService _eventRegistrationService;

      #endregion FIELDS


      #region CONSTRUCTOR

      public EventController(IEventService databaseService, IEventRegistrationService eventRegistrationService)
         : base(databaseService)
      {
         _eventRegistrationService = eventRegistrationService;
      }

      #endregion CONSTRUCTOR


      #region METHODS

      #region post

      [HttpPost("{eventId}/registrations")]
      [ProducesResponseType(StatusCodes.Status201Created)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public async Task<ActionResult<EventRegistrationDTO>> Create(Guid eventId, [FromBody]CreateEventRegistrationDTO dto)
      {
         if (eventId != dto.EventId)
            throw new BadInputException("The event id in the url and the body doe not match");

         var eventRegistration = await _eventRegistrationService.CreateAsync(dto);
         return Created(nameof(Get), eventRegistration);
      }

      #endregion post

      #region get

      [HttpGet("{eventId}/registrations")]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public async Task<ActionResult<PaginationDTO<EventRegistrationDTO>>> Get(
         Guid eventId,
         string sortDirection = "0",
         string sortColumn = "Id",
         int pageIndex = 0,
         int pageSize = 15,
         string filter = "")
      {
         if (sortDirection == "asc") sortDirection = "0";
         if (sortDirection == "desc") sortDirection = "1";
         if (!Enum.TryParse<ListSortDirection>(sortDirection, true, out var direction))
            throw new BadInputException("The given sort-direction is unknown.");

         var orderByLambda = PropertyHelpers.GetPropertySelector<EventRegistrationDTO>(sortColumn);

         // get the search results of the database query
         var result = await _eventRegistrationService.GetAsync(
           filter: FilterEventRegistration(eventId, filter),
           orderer: orderByLambda,
           sortDirection: direction,
           pageIndex: pageIndex,
           itemsPerPage: pageSize);


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
         var registration = await _eventRegistrationService.GetOneAsync(registrationId);
         if (registration.EventId != eventId)
            throw new NotFoundException($"The event with id {eventId} does not contain any registrations with id {registrationId}");

         return Ok(registration);
      }

      #endregion get

      #region put

      [HttpPut("{eventId}/registrations/{registrationId}")]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public async Task<ActionResult<EventRegistrationDTO>> Update(Guid eventId, Guid registrationId, [FromBody]UpdateEventRegistrationDTO dto)
      {
         if (dto.EventId != eventId)
            throw new BadInputException("The event id in the url and the body doe not match");

         var result = await _eventRegistrationService.UpdateAsync(registrationId, dto);
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
         var registration = await _eventRegistrationService.GetOneAsync(registrationId);
         if (registration.EventId != eventId)
            throw new NotFoundException($"The event with id {eventId} does not contain any registrations with id {registrationId}");

         await _eventRegistrationService.DeleteAsync(registrationId);

         return NoContent();
      }

      #endregion delete

      protected Expression<Func<EventRegistration, bool>> FilterEventRegistration(Guid eventId, string filter)
      {
         return o => o.EventSlot.EventId == eventId;
      }

      #endregion METHODS
   }
}

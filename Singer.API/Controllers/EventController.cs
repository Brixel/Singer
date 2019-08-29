using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Singer.DTOs;
using Singer.Models;
using Singer.Services;
using Singer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Singer.Controllers
{
   public class EventController : DataControllerBase<Event, EventDTO, CreateEventDTO>
   {
      #region CONSTRUCTOR

      public EventController(EventService databaseService)
         : base(databaseService)
      {
      }

      #endregion CONSTRUCTOR


      #region METHODS

      #region post

      [HttpPost("{eventId}")]
      [ProducesResponseType(StatusCodes.Status201Created)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public async Task<ActionResult<RegistrationDTO>> Create(Guid eventId, [FromBody]CreateRegistrationDTO dto)
      {
         // TODO
         return Created(nameof(Get), null);
      }

      #endregion post

      #region get

      [HttpPost("{eventId}")]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public async Task<ActionResult<IList<RegistrationDTO>>> Get(Guid eventId)
      {
         // TODO
         return Ok();
      }

      [HttpPost("{eventId}/{userId}")]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public async Task<ActionResult<RegistrationDTO>> GetOne(Guid eventId, Guid userId)
      {
         // TODO
         return Ok();
      }

      #endregion get

      #region put

      [HttpPost("{eventId}/{userId}")]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public async Task<ActionResult<RegistrationDTO>> Update(Guid eventId, Guid userId, [FromBody]RegistrationDTO dto)
      {
         // TODO
         return Ok();
      }

      #endregion put

      #region delete

      [HttpDelete("{eventId}/{userId}")]
      [ProducesResponseType(StatusCodes.Status204NoContent)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public async Task<ActionResult> Delete(Guid eventId, Guid userId)
      {
         // TODO
         return NoContent();
      }

      #endregion delete

      #endregion METHODS
   }
}

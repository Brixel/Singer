using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Singer.DTOs;
using Singer.Helpers;
using Singer.Helpers.Exceptions;
using Singer.Services.Interfaces;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Singer.Controllers
{
   [Route("api/[controller]")]
   public abstract class DataControllerBase<TEntity, TDTO> : Controller
      where TEntity : class, IIdentifiable
      where TDTO : class
   {
      #region CONSTRUCTORS

      protected DataControllerBase(IDatabaseService<TEntity, TDTO> databaseService, IHostingEnvironment env)
      {
         Env = env;
         DatabaseService = databaseService;
      }

      #endregion CONSTRUCTORS


      #region PROPERTEIS

      protected IHostingEnvironment Env { get; }
      protected IDatabaseService<TEntity, TDTO> DatabaseService { get; }

      #endregion PROPERTEIS


      #region METHODS

      #region post

      [HttpPost]
      [ProducesResponseType(StatusCodes.Status201Created)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public async Task<ActionResult<TDTO>> CreateUser(TDTO dto)
      {
         try
         {
            var returnUser = await DatabaseService.CreateAsync(dto);
            return Created(nameof(Get), returnUser);
         }
         catch (Exception e)
         {
            return Env.IsDevelopment()
               ? StatusCode(StatusCodes.Status500InternalServerError, e.Message)
               : StatusCode(StatusCodes.Status500InternalServerError, "An error happened");
         }
      }

      #endregion post

      #region get

      [HttpGet]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public virtual async Task<ActionResult<TDTO>> Get(string sortDirection, string sortColumn, int pageIndex, int pageSize, string filter)
      {
         pageIndex++;

         if (!Enum.TryParse<ListSortDirection>(sortDirection, true, out var direction))
            throw new BadInputException("The given sortdirection is unknown.");

         var orderByLambda = PropertyHelpers.GetPropertySelector<TDTO>(sortColumn);

         try
         {
            var result = await DatabaseService.GetAsync(
              filter: filter,
              orderer: orderByLambda,
              sortDirection: direction,
              page: pageIndex,
              itemsPerPage: pageSize);

            var requestPath = HttpContext.Request.Path;
            var nextPage = (pageIndex * pageSize) + result.Size >= result.TotalCount
               ? null
               : $"{requestPath}?PageIndex={pageIndex + pageSize}&Size={pageSize}";

            var page = new PaginationDTO<TDTO>
            {
               Items = result.Items,
               Size = result.Items.Count,
               PageIndex = pageIndex,
               CurrentPageUrl = $"{requestPath}?{HttpContext.Request.QueryString.ToString()}",
               NextPageUrl = nextPage,
               PreviousPageUrl = pageIndex == 0
                  ? null
                  : $"{requestPath}?PageIndex={pageIndex - pageSize}&Size={pageSize}",
               TotalSize = result.TotalCount
            };

            return Ok(page);
         }
         catch (Exception e)
         {
            return Env.IsDevelopment()
               ? StatusCode(StatusCodes.Status500InternalServerError, e.Message)
               : StatusCode(StatusCodes.Status500InternalServerError, "An error happened");
         }
      }

      [HttpGet("{id}")]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public virtual async Task<ActionResult<CareUserDTO>> GetOne(Guid id)
      {
         try
         {
            var dto = await DatabaseService.GetOneAsync(id);
            return Ok(dto);
         }
         catch (NotFoundException)
         {
            return NotFound("No entity found with the given id");
         }
         catch (Exception e)
         {
            return Env.IsDevelopment()
               ? StatusCode(StatusCodes.Status500InternalServerError, e.Message)
               : StatusCode(StatusCodes.Status500InternalServerError, "An error happened");
         }
      }

      #endregion get

      #region put

      [HttpPut("{id}")]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public async Task<ActionResult> UpdateUser(Guid id, TDTO dto)
      {
         try
         {
            var result = await DatabaseService.UpdateAsync(dto, id);
            return Ok(result);
         }
         catch (NotFoundException)
         {
            return NotFound("No entity found with the given id");
         }
         catch (Exception e)
         {
            return Env.IsDevelopment()
               ? StatusCode(StatusCodes.Status500InternalServerError, e.Message)
               : StatusCode(StatusCodes.Status500InternalServerError, "An error happened");
         }
      }

      #endregion put

      #region delete

      [HttpDelete("{id}")]
      [ProducesResponseType(StatusCodes.Status204NoContent)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public async Task<ActionResult> DeleteUser(Guid id)
      {
         try
         {
            await DatabaseService.DeleteAsync(id);
            return NoContent();
         }
         catch (NotFoundException)
         {
            return NotFound("No entity found with the given id");
         }
         catch (Exception e)
         {
            return Env.IsDevelopment()
               ? StatusCode(StatusCodes.Status500InternalServerError, e.Message)
               : StatusCode(StatusCodes.Status500InternalServerError, "An error happened");
         }
      }

      #endregion delete

      #endregion METHODS
   }
}

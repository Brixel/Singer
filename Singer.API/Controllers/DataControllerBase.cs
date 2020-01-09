using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Singer.DTOs;
using Singer.Helpers;
using Singer.Helpers.Exceptions;
using Singer.Services.Interfaces;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Singer.Resources;

namespace Singer.Controllers
{
   /// <summary>
   /// Abstract class that implements the basic CRUD operations for a controller.
   /// </summary>
   /// <typeparam name="TEntity">The type of the entity to manipulate in the database.</typeparam>
   /// <typeparam name="TDTO">The type that will be exposed to the outside world.</typeparam>
   [Route("api/[controller]")]
   [Authorize]
   public abstract class DataControllerBase<TEntity, TDTO, TCreateDTO, TUpdateDTO> : Controller
      where TEntity : class, IIdentifiable
      where TDTO : class, IIdentifiable
      where TCreateDTO : class
      where TUpdateDTO : class

   {
      #region CONSTRUCTORS

      /// <summary>
      /// Constructs a new instance of the <see cref="DataControllerBase{TEntity, TDTO}"/> class.
      /// </summary>
      /// <param name="databaseService">Service to perform operations on the database.</param>
      protected DataControllerBase(IDatabaseService<TEntity, TDTO, TCreateDTO, TUpdateDTO> databaseService)
      {
         DatabaseService = databaseService;
      }

      #endregion CONSTRUCTORS


      #region PROPERTIES

      /// <summary>
      /// Service to perform operations on the database.
      /// </summary>
      protected IDatabaseService<TEntity, TDTO, TCreateDTO, TUpdateDTO> DatabaseService { get; }

      #endregion PROPERTIES


      #region METHODS

      #region post

      /// <summary>
      /// The POST method for the controller to create a new entity in the database.
      /// </summary>
      /// <param name="dto">The new element to create in the database.</param>
      /// <returns>The new created entity.</returns>
      [HttpPost]
      [ProducesResponseType(StatusCodes.Status201Created)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public virtual async Task<IActionResult> Create([FromBody]TCreateDTO dto)
      {
         var model = ModelState;
         if (!model.IsValid)
            throw new BadInputException("Invalid dto", $"De data is niet geldig: {model}");

         var returnItem = await DatabaseService.CreateAsync(dto);
         return Created(nameof(Get), returnItem);
      }

      #endregion post

      #region get

      /// <summary>
      /// The GET method to get a selection of entities from the database.
      /// </summary>
      /// <param name="sortDirection">
      /// The direction in which the returned collection should be sorted (string version of the <see cref="ListSortDirection"/>)
      /// </param>
      /// <param name="sortColumn">Column on which the returned collection should be sorted.</param>
      /// <param name="pageIndex">Index of the pageIndex of elements to be returned.</param>
      /// <param name="pageSize">Number of elements on a pageIndex.</param>
      /// <param name="filter">The filter that should be applied on the collection.</param>
      /// <returns>A selection of entities from the database.</returns>
      [HttpGet]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public virtual async Task<IActionResult> Get(string sortDirection = "0", string sortColumn = "Id", int pageIndex = 0, int pageSize = 15, string filter = "", bool showArchived = false)
      {
         sortDirection = sortDirection switch
         {
            "desc" => "1",
            _ => "0",
         };

         if (!Enum.TryParse<ListSortDirection>(sortDirection, true, out var direction))
            throw new BadInputException("The given sort-direction is unknown.", ErrorMessages.UnknownSortDirection);

         var orderByLambda = PropertyHelpers.GetPropertySelector<TDTO>(sortColumn);

         // get the search results of the database query
         var result = await DatabaseService.GetAsync(
           filter: filter,
           orderer: orderByLambda,
           sortDirection: direction,
           pageIndex: pageIndex,
           itemsPerPage: pageSize,
           showArchived: showArchived);


         var requestPath = HttpContext.Request.Path;
         var nextPage = (pageIndex * pageSize) + result.Size >= result.TotalCount
            ? null
            : $"{requestPath}?PageIndex={pageIndex++}&Size={pageSize}";

         // create object that holds the paginated elements
         var page = new PaginationDTO<TDTO>
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

      /// <summary>
      /// The GET method to get a single entity from the database.
      /// </summary>
      /// <param name="id">Id of the element to return.</param>
      /// <returns>The entity in the database that has the given id.</returns>
      [HttpGet("{id}")]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public virtual async Task<IActionResult> GetOne(Guid id)
      {
         var dto = await DatabaseService.GetOneAsync(id);
         return Ok(dto);
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
      public virtual async Task<IActionResult> Update(Guid id, [FromBody]TUpdateDTO dto)
      {
         var model = ModelState;
         if (!model.IsValid)
            throw new BadInputException("Invalid dto", $"De data is niet geldig: {model}");

         var result = await DatabaseService.UpdateAsync(id, dto);
         return Ok(result);
      }

      #endregion put

      #region delete

      /// <summary>
      /// Deletes a single entity from the database.
      /// </summary>
      /// <param name="id">The id of the entity to remove.</param>
      /// <returns></returns>
      [HttpDelete("{id}")]
      [ProducesResponseType(StatusCodes.Status204NoContent)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public virtual async Task<IActionResult> Delete(Guid id)
      {
         await DatabaseService.DeleteAsync(id);
         return NoContent();
      }
      #endregion delete

      #endregion METHODS
   }
}

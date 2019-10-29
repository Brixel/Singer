using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Singer.DTOs
{
   /// <summary>
   ///     Model that represents a page of items. It is used to devide a big list of data in multiple
   ///     pages and to provide the functionality to navigate to previous/next pages with ease.
   /// </summary>
   /// <typeparam name="T">Type of the items that are passed with the model.</typeparam>
   public class PaginationDTO<T>
   {
      /// <summary>Url to the previous page. If this is the first page, the value is <see cref="null"/>.</summary>
      [Required]
      [Url]
      [DisplayName("Url naar vorige pagina")]
      public string PreviousPageUrl { get; set; }

      /// <summary>Url to the current page.</summary>
      [Required]
      [Url]
      [DisplayName("Url van huidige pagina")]
      public string CurrentPageUrl { get; set; }

      /// <summary>
      ///     Url to the next page of items. If this is the last page, the value is <see cref="null"/>.
      /// </summary>
      [Required]
      [Url]
      [DisplayName("Url naar volgende pagina")]
      public string NextPageUrl { get; set; }

      /// <summary>The number of items given with this response.</summary>
      [Required]
      [Range(0, int.MaxValue)]
      [DisplayName("Aantal elementen")]
      public int Size { get; set; }

      /// <summary>The index at which this section of values is located.</summary>
      [Required]
      [Range(0, int.MaxValue)]
      [DisplayName("Pagina index")]
      public int PageIndex { get; set; }

      /// <summary>Total number of items in the data base.</summary>
      [Required]
      [Range(0, int.MaxValue)]
      [DisplayName("Totaal aantal elementen in de database")]
      public int TotalSize { get; set; }

      /// <summary>The returned items.</summary>
      [Required]
      [DisplayName("Items")]
      public IReadOnlyList<T> Items { get; set; }
   }
}

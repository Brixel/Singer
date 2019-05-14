using System.Collections.Generic;

namespace Singer.Models
{
   /// <summary>
   /// Model that represents a page of values.
   /// It is used to devide a big list of data in multiple pages and to provide the functionality to navigate to previous/next pages with ease.
   /// </summary>
   /// <typeparam name="T">Type of the values that are passed with the model.</typeparam>
   public class PaginationModel<T>
   {
      /// <summary>
      /// Url to the previous page. If this is the first page, the value is <see cref="null"/>.
      /// </summary>
      public string PreviousPageUrl { get; set; }

      /// <summary>
      /// Url to the current page.
      /// </summary>
      public string CurrentPageUrl { get; set; }

      /// <summary>
      /// Url to the next page of items. If this is the last page, the value is <see cref="null"/>.
      /// </summary>
      public string NextPageUrl { get; set; }

      /// <summary>
      /// The number of values given with this response.
      /// </summary>
      public int NumberOfValues { get; set; }

      /// <summary>
      /// The index at which this section of values is located.
      /// </summary>
      public int StartAt { get; set; }

      /// <summary>
      /// Total number of values in the data base.
      /// </summary>
      public int TotalNumberOfValues { get; set; }

      /// <summary>
      /// The returned values.
      /// </summary>
      public IList<T> Values { get; set; }
   }
}

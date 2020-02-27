using System.ComponentModel;

namespace Singer.DTOs
{
   public class SearchDTOBase
   {
      public ListSortDirection SortDirection { get; set; }
      public string SortColumn { get; set; }
      public int PageIndex { get; set; }
      public int PageSize { get; set; }
      public string Text { get; set; }
   }
}

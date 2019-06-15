using Singer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Singer.Models
{
   public class SearchResults<T>
   {

      public SearchResults(IReadOnlyList<T> items, int totalItemsCount, int pageIndex)
      {
         Items = items;
         TotalCount = totalItemsCount;
         Start = pageIndex;
         Size = items.Count;
      }

      public int TotalCount { get;  }
      public int Start { get;  }
      public int Size { get; }
      public IReadOnlyList<T> Items { get; }
   }
}

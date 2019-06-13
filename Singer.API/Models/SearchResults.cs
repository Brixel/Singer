using Singer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Singer.Models
{
   public class SearchResults<T>
   {
      public int TotalCount { get; set; }
      public int Start { get; set; }
      public int Size { get; set; }
      public List<T> Items { get; set; }
   }
}

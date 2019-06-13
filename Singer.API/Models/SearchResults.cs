using Singer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Singer.Models
{
   public class SearchResults<T>
   {
      public int NumResults { get; set; }
      public int Start { get; set; }
      public int Size { get; set; }
      public List<T> Results { get; set; }
   }
}

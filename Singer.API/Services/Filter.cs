using System.Collections.Generic;

namespace Singer.Services
{
   public class Filter<T>
   {
      public T FilterModel { get; set; }
      public IList<string> PropertiesToFilterOn { get; set; }
   }
}

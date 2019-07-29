using System;

namespace Singer.Models
{
   [Flags]
   public enum AgeGroup
   {
      Toddler = 1,
      Kindergartener = 2,
      Child = 4,
      Youngster = 8,
      Adult = 16
   }
}

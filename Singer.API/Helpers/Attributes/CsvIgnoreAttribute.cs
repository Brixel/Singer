using System;

namespace Singer.Helpers.Attributes
{
   [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
   public sealed class CsvIgnoreAttribute : Attribute
   {
      public CsvIgnoreAttribute()
      {
      }
   }
}

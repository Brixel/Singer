using System;

namespace Singer.Helpers.Attributes
{
   [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
   public sealed class CsvPropertyAttribute : Attribute
   {
      private string _propertyName;

      public CsvPropertyAttribute(string propertyName = null)
      {
         _propertyName = propertyName;
      }

      public string PropertyName => _propertyName ?? PropertyNameResourceType?.GetProperty(PropertyNmeResourceName)?.GetValue(null, null) as string;
      public string PropertyNmeResourceName { get; set; }
      public Type PropertyNameResourceType { get; set; }
   }
}
